namespace MovieTracker;

class Movie
{
    public List<int> Genres;
    int Id;
    string? IMBb;
    string? Overview;
    public string Title { get; private set; }
    double? Runtime;
    double? Rating;
    int ReleaseYear;
    
    public Movie(int id, string title, List<int> genres, string overview, int release, double? runtime, double? rating, string? imdb)
    {
        Id = id;
        Title = title;
        Genres = genres;
        Overview = overview;
        Runtime = runtime;
        Rating = rating;
        IMBb = imdb;
        ReleaseYear = release;
    }
}

class Genre 
{
    int Id;
    string Name;

    public Genre(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public override string ToString()
    {
        return $"Genre(id={Id}, name={Name})";
    }
}

class MovieStore {
    public DSA.DynamicArray<Movie> Movies { get; private set; }
    public DSA.DynamicArray<Genre> Genres { get; private set; }

    public MovieStore(DSA.DynamicArray<Movie> movies, DSA.DynamicArray<Genre> genres)
    {
        Movies = movies;
        Genres = genres;
    }
}