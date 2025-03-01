namespace MovieTracker;

class Movie
{
    public DSA.DynamicArray<Genre> Genres;
    public int Id { get; private set; }
    public string? IMBb { get; private set; }
    public string? Overview { get; private set; }
    public string Title { get; private set; }
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
        ret += "IMBb=" + (IMBb ?? "null");

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
        IMBb = imdb;
        ReleaseYear = release;
    }
}

class Genre 
{
    public int Id { get; private set; }
    public string Name { get; private set; }

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