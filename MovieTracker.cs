using System.Security.Cryptography.X509Certificates;

namespace MovieTracker;

class MovieTracker{
    public MovieStore? Store { get; private set; }

    public MovieTracker() {

    }

    public void LoadStore() {
        var loader = new Loader("movie_metadata.txt");
        Console.Write("Loading Movies...");
        Store = loader.Read();
        Console.Write($"\r{Store.Movies.Count} movies loaded.");
        Thread.Sleep(2000);
        Console.Write("\r");
    }
}