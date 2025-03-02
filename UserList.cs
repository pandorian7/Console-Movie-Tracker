using System.Reflection.Metadata.Ecma335;

namespace MovieTracker;

class UserList {
    public string Name { get; set; }
    public DSA.LinkedList<Movie> Movies { get; private set; }

    public UserList(string name) {
        Name = name;
        Movies = new();
    }

    public override string ToString()
    {
        return $"UserList(Name={Name}, NumMovies={Movies.Count})";
    }

    public string Representation() {
        return $"{Name} ({Movies.Count} movies)";
    }
}