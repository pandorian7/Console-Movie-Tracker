namespace MovieTracker;

class WatchedList {
    public string Name = "WatchedList";
    public DSA.LinkedList<Movie> Movies { get; private set; }

    public WatchedList() {
        Movies = new();
    }

    public override string ToString()
    {
        return $"WatchedList(Name={Name}, NumMovies={Movies.Count})";
    }

    public string Representation() {
        return $"{Name} ({Movies.Count} movies)";
    }
}