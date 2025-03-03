namespace MovieTracker;

class UserData 
{
    public DSA.DynamicArray<UserList> UserLists { get; private set; }
    public string UserDataPath { get;  private set; }
    public DSA.DynamicArray<Movie>? Movies { get; set; }

    public WatchedList WatchedList { get; private set; }
    

    private StreamWriter FileWrite() {
        return new StreamWriter(UserDataPath);
    }

    private StreamReader FileRead() {
        return new StreamReader(UserDataPath);
    }

    public bool DoesFileExists() {
        return File.Exists(UserDataPath);
    }
    public UserData(string userDataPath)
    {
        UserLists = new();
        WatchedList = new();
        UserDataPath = userDataPath;
    }

    public void Save() {
        using (var sw = FileWrite()) {
            sw.WriteLine(UserLists.Count);
           foreach (var list in UserLists) {
                sw.WriteLine(list.Name);
                sw.WriteLine(list.Movies.Count);
               foreach (var movie in list.Movies) {
                   sw.WriteLine(movie.Id);
               }
           }
        }
    }

    public void LoadIfPossible() {
        if (DoesFileExists()) {
            Load();
        }
    }
    private void Load() {
        using (var sr = FileRead()) {
        var ReadInt = () => Convert.ToInt32(sr.ReadLine());

        int NUserLists = ReadInt();
        for(int i=0; i<NUserLists; i++) {
            string listName = sr.ReadLine()!;
            var list = new UserList(listName);
            int NMovies = ReadInt();
            for(int j=0; j<NMovies; j++) {
                int movieId = ReadInt();
                var m = Movies!.Find(movieId, m=>m.Id);
                list.Movies.AddLast(Movies!.Find(movieId, m=>m.Id)!);
            }
            UserLists.AddLast(list);
        }

        }
    }
}