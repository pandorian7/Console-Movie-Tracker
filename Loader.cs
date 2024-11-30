namespace MovieTracker;

class Movie
{
    List<int>? Genres;
    string? Id;
    string? IMBb;
    string? Overview;
    string? Title;
    double? Runtime;
    double? Rating;

}

class Loader(string fp)
{
    public string FilePath = fp;
    public List<int> GenreIds = [];

    public List<string> GenreNames = [];
    public List<Movie> Movies = [];

    public void Read()
    {
        string? tmp;

        StreamReader sr = new(FilePath);

        tmp = sr.ReadLine();
        if (tmp is not null)
        {
            Console.Write(tmp);
        }

        sr.Close();
    }
}