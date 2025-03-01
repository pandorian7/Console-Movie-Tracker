using System.Runtime.InteropServices;

namespace MovieTracker;

class CLI {

    public string HelpText { get; private set; }
    public string WelclomeText { get; private set; }

    public MovieTracker Tracker { get; private set; }
    public CLI() {
        StreamReader sr = new("help-menu.txt");
        HelpText = sr.ReadToEnd();
        sr.Close();
        sr = new("welcome-banner.txt");
        WelclomeText = sr.ReadToEnd();
        sr.Close();

        Tracker = new MovieTracker();
    }


    public void PrintHelp() {
        Console.WriteLine(HelpText);
    }

    public void PrintWelcome() {
        Console.WriteLine(WelclomeText);
    }

    public void Init() {
        PrintWelcome();
        Tracker.LoadStore();
        PrintHelp();
    }

    private static string MergeFrom(string[] args, int start) {
        return string.Join(" ", args.Skip(start).ToList());
    }

    private static void CheckNthArg(string[] args, int n, string msg) {
        if (args.Length-1 < n) {
            throw new Exception(msg);
        }
    }

    public static string? ReadUserCommand() {
        Console.Write(" >> ");
        return Console.ReadLine();
    }
    public void ExecuteCommand(string command) {
        var res = command.Trim().Split(" ");
        if (res.Length == 1 && res[0] == "") {
            return;
        }
        switch (res[0]) {
            case "help":
                PrintHelp();
                break;
            case "res":
                Tracker.ShowResults();
                break;
            case "search":
                CLI.CheckNthArg(res, 1, "No search parameter provided");
                switch(res[1]){
                    case "title":
                        CLI.CheckNthArg(res, 2, "No title provided");
                        Tracker.Res = Tracker.Search(CLI.MergeFrom(res, 2));
                        Tracker.ShowResults();
                        // Console.WriteLine($"searching by title {Utils.Clean(CLI.MergeFrom(res, 2))}");
                        break;
                    case "year":
                        CLI.CheckNthArg(res, 2, "No year provided");
                        Console.WriteLine($"searching by year {res[2]}");
                        break;
                    case "genre":
                        CLI.CheckNthArg(res, 2, "No genre provided");
                        Console.WriteLine($"searching by genre {res[2]}");
                        break;
                    case "imdb":
                        CLI.CheckNthArg(res, 2, "No rating provided");
                        Console.WriteLine($"searching by rating {res[2]}");
                        break;
                    
                    default:
                        throw new Exception("Invalid search parameter");
                }
                break;
            default:
                throw new Exception($"Invalid command: {command}");
        }
    }

    public void Run() {
        string? command;
        while (true) {
            command = ReadUserCommand();
            if (command == null) {
                continue;
            }
            try {
                ExecuteCommand(command);
            } catch (Exception e) {
                Console.WriteLine($"Error: {e.Message}");
            }

        }
    }


}