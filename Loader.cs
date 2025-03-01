namespace MovieTracker;

class Loader(string fp)
{
    public string FilePath = fp;
    public List<int> GenreIds = [];

    public List<string> GenreNames = [];

    
    public DSA.DynamicArray<Movie> Movies = new();
    public DSA.DynamicArray<Genre> Genres = new();

    public void Read()
    {
        string? tmp;

        StreamReader sr = new(FilePath);

        tmp = sr.ReadLine();

        if (tmp != "genres start") {
            throw new Exception("Invalid file format");
        }
        int genre_id;
        string genre_name;
        while ((tmp = sr.ReadLine()) != "genres end")
        {
            genre_id = Convert.ToInt32(tmp);
            genre_name = sr.ReadLine()!;
            Genres.AddLast(new Genre(genre_id, genre_name));
            Console.WriteLine(new Genre(genre_id, genre_name));
        }

        sr.Close();
    }
}