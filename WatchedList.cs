namespace MovieTracker;

class WatchedList 
{
    private string Name { get; set; } // Ensure Name is always initialized
    public DSA.BST<Movie> Movies { get; private set; } // No need for nullable (?)

    public WatchedList() 
    {
        Name = "Watched List";
        Movies = new DSA.BST<Movie>(x => x.Title); // Ensure proper initialization
    }

    public void AddMovie(Movie movie)
    {
        Movies.Insert(movie);
    }

    public void PrintMovies()
    {
        Movies.Print();
    }

    public string Representation() {
        return $"{Name} ({Movies.Count} movies)";
    }

}
