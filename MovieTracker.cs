using System.Security.Cryptography.X509Certificates;

namespace MovieTracker;

class MovieTracker{
    public MovieStore? Store { get; private set; }
    public DSA.DynamicArray<Movie> Res { get; set; }

    public MovieTracker() {
        Res = new();
    }

    public void LoadStore() {
        var loader = new Loader("movie_metadata.txt");
        Console.Write("Loading Movies...");
        Store = loader.Read();
        Console.Write($"\r{Store.Movies.Count} movies loaded.");
        Thread.Sleep(2000);
        Console.Write("\r\n");
    }

    public void ShowResults() {
        Console.WriteLine();
        if (Res.Count == 0) {
            Console.WriteLine("No Results");
            return;
        } else {
            for (int i=0; i<Res.Count; i++) {
                Console.WriteLine(Res.At(i));
                Console.WriteLine();
            }
        }
        Console.WriteLine();
    }

    public DSA.DynamicArray<Movie> Search(string query) {
    
        Res = new();

        string searchTerm = Utils.Clean(query);

        for (int i=0; i<Store!.Movies.Count; i++) {
            if (Store.Movies.At(i).CleanTitle.Contains(searchTerm)) {
                Res.AddLast(Store.Movies.At(i));
            }
        }

        return Res;
    }
}