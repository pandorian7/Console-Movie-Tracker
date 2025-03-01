namespace MovieTracker;

class Movie
{
    public List<int> Genres;
    string Id;
    string? IMBb;
    string? Overview;
    public string Title { get; private set; }
    double? Runtime;
    double? Rating;
    int ReleaseYear;
    
    public Movie(string id, string title, List<int> genres, string overview, int release, double? runtime, double? rating, string? imdb)
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