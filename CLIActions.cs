using System.Runtime.InteropServices;

namespace MovieTracker;

abstract class CLIActions {

    public string HelpText { get; private set; }
    public string WelclomeText { get; private set; }

    public MovieTracker Tracker { get; private set; }
    public CLIActions() {
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

    public static string MergeFrom(string[] args, int start) {
        return string.Join(" ", args.Skip(start).ToList());
    }

    public static void CheckNthArg(string[] args, int n, string msg) {
        if (args.Length-1 < n) {
            throw new Exception(msg);
        }
    }

    public static string? ReadUserCommand() {
        Console.Write(" >> ");
        return Console.ReadLine();
    }
    public abstract int ExecuteCommand(string command);
    
    public void Run() {
        string? command;
        int ret;
        while (true) {
            command = ReadUserCommand();
            Console.WriteLine();
            if (command == null) {
                continue;
            }
            try {
                ret = ExecuteCommand(command);
                Console.WriteLine();
                if (ret == -1) {
                    break;
                }
            } catch (Exception e) {
                Console.WriteLine($"Error: {e.Message}");
            }

        }
    }

    public void FilterByTitle(DSA.DynamicArray<Movie> list, string query) {
        Tracker.Res = new();
        query = Utils.Clean(query);
        Tracker.Res = Tracker.Search(query);
    }

    public void FilterByYear(DSA.DynamicArray<Movie> list, int year) {
        Tracker.Res = list.Filter(x => x.ReleaseYear == year);
    }

    public void FilterByGenre(DSA.DynamicArray<Movie> list, string genre) {
        var matching_genres = Tracker.Store!.GetMatchingGenres(genre);
        Tracker.Res = list.Filter(m => m.Genres.ContainsAny(matching_genres));
    }

    public void FilterByIMDbId(DSA.DynamicArray<Movie> list, string imdbId) {
        Tracker.Res = new();
        var m = list.Find(imdbId, x => x.IMDb);
        if (m != null) {
            Tracker.Res.AddLast(m);
        }
    }

    public void FilterByRating(DSA.DynamicArray<Movie> list, float rating) {
        Tracker.Res = list.Filter(m => m.Rating >= rating);
    }

    public void NewList(string listName) {
        Tracker.UserData.UserLists.AddLast(new UserList(listName));
        Tracker.UserData.Save();
    }

    public void ShowAllLists() {
        foreach(var list in Tracker.UserData.UserLists) {
            Console.WriteLine(list);
        }
    }

    public int GetOrCreateListIdFromUser(bool allowCreate=false) {
        Console.WriteLine();
        for (int i=0; i<Tracker.UserData.UserLists.Count; i++) {
            Console.WriteLine($"{i+1}) {Tracker.UserData.UserLists.At(i).Representation()}");
        }
        if (allowCreate) {
            Console.WriteLine("+) Add to a New List");
        }
        Console.WriteLine();
        Console.Write("Enter list id: ");

        string listIdSrt = Console.ReadLine()!;
        int listId;

        if (allowCreate && listIdSrt == "+") {
            Console.Write("Enter New List Name: ");
            string list_name = Console.ReadLine()!;
            Tracker.UserData.UserLists.AddLast(new UserList(list_name));
            Tracker.UserData.Save();
            listId = Tracker.UserData.UserLists.Count-1;
        } else {
            listId = Convert.ToInt32(listIdSrt)-1;
            VerityValidIndex(Tracker.UserData.UserLists, listId+1);
        }
        return listId;
    } 

    public void AddMovieToListFromResults(int listId, int resultId) {
        Tracker.UserData.UserLists.At(listId).Movies.AddLast(Tracker.Res.At(resultId-1));
        Tracker.UserData.Save();
        
    }

    public void VerityValidIndex<T>(DSA.DynamicArray<T> list, int index) {
        if (index < 1 || index > list.Count) {
            throw new Exception("Invalid index");
        }
    }

    public void VerityValidResIndex(int index) {
        VerityValidIndex(Tracker.Res, index);
    }

    public void SortByTitle() {
        Tracker.Sorting.Sort(Tracker.Res, x => x.Title);
    }

    public void SortByYear() {
        Tracker.Sorting.Sort(Tracker.Res, x => x.ReleaseYear);
        Tracker.Res.Reverse();
    }

    public void SortByRating() {
        Tracker.Sorting.Sort(Tracker.Res, x => x.Rating ?? 0);
        Tracker.Res.Reverse();
    }



    public void ShowResults() {
        Tracker.ShowResults();
    }


}