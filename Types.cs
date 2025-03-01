namespace MovieTracker;

class Movie
{
    public DSA.DynamicArray<Genre> Genres;
    public int Id { get; private set; }
    public string? IMDb { get; private set; }
    public string? Overview { get; private set; }
    public string Title { get; private set; }
    public string CleanTitle {get; private set;}
    public double? Runtime { get; private set; }
    public double? Rating { get; private set; }
    public int ReleaseYear { get; private set; }

    public override string ToString()
    {
        var ret = "";
        ret += $"Id={Id}, ";
        ret += $"Title={Title}, ";
        ret += "Overview=" + (Overview ?? "null") + ", ";
        ret += "Genres=" + (Genres?.ToString() ?? "null") + ", ";
        ret += $"ReleaseYear={ReleaseYear}, ";
        ret += "Runtime=" + (Runtime?.ToString() ?? "null") + ", ";
        ret += "Rating=" + (Rating?.ToString() ?? "null") + ", ";
        ret += "IMBb=" + (IMDb ?? "null");

        return $"Movie({ret})";
    }

    public Movie(int id, string title, DSA.DynamicArray<Genre> genres, string overview, int release, double? runtime, double? rating, string? imdb)
    {
        Id = id;
        Title = title;
        Genres = genres;
        Overview = overview;
        Runtime = runtime;
        Rating = rating;
        IMDb = imdb;
        ReleaseYear = release;

        CleanTitle = Utils.Clean(title);
    }
}

class Genre 
{
    public int Id { get; private set; }
    public string Name { get; private set; }

    public string CleanName { get; private set; }

    public Genre(int id, string name)
    {
        Id = id;
        Name = name;
        CleanName = Utils.Clean(name);

    }

    public override string ToString()
    {
        return $"Genre(id={Id}, name={Name})";
    }
}