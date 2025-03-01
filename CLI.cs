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
        Console.WriteLine();
        Console.WriteLine("ex: search title the matrix");
        Console.WriteLine("\"help\" for more detail help");
        Console.WriteLine();

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
                        break;
                    case "year":
                        CLI.CheckNthArg(res, 2, "No year provided");
                        Tracker.Res = Tracker.Store!.Movies.Filter(x => x.ReleaseYear == Convert.ToInt32(res[2]));
                        Tracker.ShowResults();
                        break;
                    case "genre":
                        CLI.CheckNthArg(res, 2, "No genre provided");
                        Tracker.Res = Tracker.Store!.Movies.Filter(m => {
                            var matching_genres = Tracker.Store!.GetMatchingGenres(CLI.MergeFrom(res, 2));
                            for (int i=0; i<m.Genres.Count; i++) {
                                if (matching_genres.Contains(m.Genres.At(i))) {
                                    return true;
                                }
                            }
                            return false;
                        });
                        Tracker.ShowResults();
                        break;
                    case "imdb":
                        CLI.CheckNthArg(res, 2, "No imdb id provided");
                        Tracker.Res = new();
                        var m = Tracker.Store!.Movies.Find(res[2], x => x.IMDb);
                        if (m != null) {
                            Tracker.Res.AddLast(m);
                        }
                        Tracker.ShowResults();
                        break;
                    
                    default:
                        throw new Exception("Invalid search parameter");
                }
                break;

            case "sort":
                CLI.CheckNthArg(res, 1, "No sort parameter provided");
                switch(res[1]) {
                    case "title":
                        Tracker.Sorting.Sort(Tracker.Res, x => x.Title);
                        Tracker.ShowResults();
                        break;
                    case "year":
                        Tracker.Sorting.Sort(Tracker.Res, x => x.ReleaseYear);
                        Tracker.ShowResults();
                        break;
                    case "rating":
                        Tracker.Sorting.Sort(Tracker.Res, x => x.Rating ?? 0);
                        Tracker.ShowResults();
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