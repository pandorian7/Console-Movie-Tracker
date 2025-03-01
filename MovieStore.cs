namespace MovieTracker;

class MovieStore {
    public DSA.DynamicArray<Movie> Movies { get; private set; }
    public DSA.DynamicArray<Genre> Genres { get; private set; }

    public MovieStore(DSA.DynamicArray<Movie> movies, DSA.DynamicArray<Genre> genres)
    {
        Movies = movies;
        Genres = genres;
    }

    public DSA.DynamicArray<Genre> GetMatchingGenres(string query)
    {
        var res = new DSA.DynamicArray<Genre>();

        string searchTerm = Utils.Clean(query);

        for (int i=0; i<Genres.Count; i++) {
            if (Genres.At(i).CleanName.Contains(searchTerm)) {
                res.AddLast(Genres.At(i));
            }
        }

        return res;
    }
}