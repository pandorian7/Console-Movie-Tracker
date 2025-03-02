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
        Console.WriteLine("ex: ? the matrix");
        Console.WriteLine("    info 1");
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
            case "?":
                CLI.CheckNthArg(res, 1, "No title provided");
                Tracker.Res = Tracker.Search(CLI.MergeFrom(res, 1));
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
            case "info":
                CLI.CheckNthArg(res, 1, "No serach result id provided");
                int searchId = Convert.ToInt32(res[1]);
                if (searchId < 1 || searchId > Tracker.Res.Count) {
                    throw new Exception("Invalid search result id");
                }
                Tracker.ShowMoveInfo(searchId);
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
                        Tracker.Res.Reverse();
                        Tracker.ShowResults();
                        break;
                    case "rating":
                        Tracker.Sorting.Sort(Tracker.Res, x => x.Rating ?? 0);
                        Tracker.Res.Reverse();
                        Tracker.ShowResults();
                        break;
                    default:
                        throw new Exception("Invalid search parameter");
                }
                break;

            case "filter":
                CLI.CheckNthArg(res, 1, "No filter parameter provided");
                switch(res[1]) {
                    case "year":
                        CLI.CheckNthArg(res, 2, "No year provided");
                        int year = Convert.ToInt32(res[2]);
                        Tracker.Res = Tracker.Res.Filter(m => m.ReleaseYear == year);
                        Tracker.ShowResults();
                        break;
                    case "genre":
                        CLI.CheckNthArg(res, 2, "No genre provided");
                        string query = CLI.MergeFrom(res, 2);
                        var g = Tracker.Store!.GetMatchingGenres(query);
                        Tracker.Res = Tracker.Res.Filter(m => m.Genres.ContainsAny(g));
                        Tracker.ShowResults();
                        break;
                    case "rating":
                        CLI.CheckNthArg(res, 2, "No rating provided");
                        float rating = Convert.ToSingle(res[2]);
                        Tracker.Res = Tracker.Res.Filter(m => m.Rating >= rating);
                        Tracker.ShowResults();
                        break;
                    default:
                        throw new Exception("Invalid filter parameter");
                }
                break;
            case "list":
                CLI.CheckNthArg(res, 1, "No list parameter provided");
                switch(res[1]) {
                    case "new":
                        CLI.CheckNthArg(res, 2, "No list name provided");
                        string listName = CLI.MergeFrom(res, 2);
                        Tracker.UserData.UserLists.AddLast(new UserList(listName));
                        break;
                    case "all":
                        for (int i=0; i<Tracker.UserData.UserLists.Count; i++) {
                            Console.WriteLine(Tracker.UserData.UserLists.At(i));
                        }
                        break;
                    case "add":
                        CLI.CheckNthArg(res, 2, "No rearch result id provided");
                        int resultId = Convert.ToInt32(res[2]);
                        if (resultId < 1 || resultId > Tracker.Res.Count) {
                            throw new Exception("Invalid search result id");
                        }
                        Console.WriteLine("Enter List Id:");
                        Console.WriteLine();
                        for (int i=0; i<Tracker.UserData.UserLists.Count; i++) {
                            Console.WriteLine($"{i+1}) {Tracker.UserData.UserLists.At(i).Representation()}");
                        }
                        Console.WriteLine("+) Add to a New List");
                        Console.Write("Enter list id: ");

                        string listIdSrt = Console.ReadLine()!;
                        
                        int list_id;

                        if (listIdSrt == "+") {
                            Console.Write("Enter New List Name: ");
                            string list_name = Console.ReadLine()!;
                            Tracker.UserData.UserLists.AddLast(new UserList(list_name));
                            list_id = Tracker.UserData.UserLists.Count-1;
                        } else {
                            list_id = Convert.ToInt32(listIdSrt)-1;
                            
                            if (list_id < 0 || list_id >= Tracker.UserData.UserLists.Count) {
                                throw new Exception("Invalid list id");
                        }
                        }
                        Tracker.UserData.UserLists.At(list_id).Movies.AddLast(Tracker.Res.At(resultId-1));

                        break;
                    default:
                        throw new Exception("Invalid list parameter");
                }

                break;
            case "exit":
                Environment.Exit(0);
                break;

            default:
                throw new Exception($"Invalid command: {command}");
        }
    }

    public void Run() {
        string? command;
        while (true) {
            command = ReadUserCommand();
            Console.WriteLine();
            if (command == null) {
                continue;
            }
            try {
                ExecuteCommand(command);
                Console.WriteLine();
            } catch (Exception e) {
                Console.WriteLine($"Error: {e.Message}");
            }

        }
    }


}