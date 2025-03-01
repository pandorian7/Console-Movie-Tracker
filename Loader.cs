namespace MovieTracker;

class Loader(string fp)
{
    public string FilePath = fp;
 
    public DSA.DynamicArray<Movie> Movies = new();
    public DSA.DynamicArray<Genre> Genres = new();

    public MovieStore Read()
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
        }

        tmp = sr.ReadLine();

        if (tmp != "movies start") {
            throw new Exception("Invalid file format");
        }

        int movie_id;
        string movie_title;
        List<int> movie_genres;
        DSA.DynamicArray<Genre> movie_genres_parsed;
        string movie_overview;
        double? movie_runtime;
        double? movie_rating;
        string? movie_imdb;
        int movie_release;

        while ((tmp = sr.ReadLine()) != "movies end")
        {
            if (tmp != "") {
                movie_genres = tmp!.Split(' ').Select(x => Convert.ToInt32(x)).ToList();
            } else {
                movie_genres = [];
            }
            movie_id = Convert.ToInt32(sr.ReadLine());
            movie_imdb = sr.ReadLine();
            movie_overview = sr.ReadLine()!;
            movie_title = sr.ReadLine()!;
            movie_runtime = Convert.ToDouble(sr.ReadLine());
            movie_rating = Convert.ToDouble(sr.ReadLine());
            tmp = sr.ReadLine();
            movie_release = int.TryParse(tmp, out int result_release) ? result_release : 1990;
            movie_genres_parsed = new();
            for (int i=0; i<movie_genres.Count; i++)
            {
                movie_genres_parsed.AddLast(Genres.Find(movie_genres[i], g=>g.Id)!);
            }
            Movies.AddLast(new Movie(movie_id, movie_title, movie_genres_parsed, movie_overview, movie_release, movie_runtime, movie_rating, movie_imdb));
            
        }
        sr.Close();

        MovieStore store = new(Movies, Genres);
        return store;
    }
}