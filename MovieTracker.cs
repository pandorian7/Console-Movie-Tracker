using System.Security.Cryptography.X509Certificates;
using DSA;

namespace MovieTracker;

class MovieTracker{
    public MovieStore? Store { get; private set; }

    public UserData UserData { get; private set; }
    public DSA.DynamicArray<Movie> Res { get; set; }

    public int ItemsPerPage { get; set; } = 10;

    public int CurrentPage { get; set; } = 0;

    public SortingEngine Sorting;

    public MovieTracker() {
        Res = new();
        Sorting = new();
        UserData = new("user-data.txt");
    }

    public void LoadStore() {
        var loader = new Loader("movie_metadata.txt");
        Console.Write("Loading Movies...");
        Store = loader.Read();
        UserData.Movies = Store.Movies;
        UserData.LoadIfPossible();
        Console.Write($"\r{Store.Movies.Count} movies loaded.");
        Thread.Sleep(2000);
        Console.Write("\r\n");
    }

    //show results

    public void ShowMoveInfo(int id) {
    var movie = Res.At(id - 1);

    // Get the full width of the console
    int consoleWidth = Console.WindowWidth;
    string border = new string('=', consoleWidth);
    string divider = new string('-', consoleWidth);

    // Extract genres
    DynamicArray<string> genreNames = new DynamicArray<string>();
    for (int j = 0; j < movie.Genres.Count; j++) {
        genreNames.AddLast(movie.Genres.At(j).Name);
    }

    // Join genres with comma and space - no brackets
    string genres = string.Join(", ", genreNames);
    genres = genres.Trim('[', ']');

    // Fancy display
    Console.WriteLine();
    Console.WriteLine(border); // Top border
    Console.WriteLine($"🎬  {movie.Title} ({movie.ReleaseYear})");
    Console.WriteLine(divider); // Divider
    Console.WriteLine();
    Console.WriteLine($"📜 Overview: {movie.Overview}");
    Console.WriteLine();
    Console.WriteLine($"🎭 Genres: {genres}");
    Console.WriteLine($"⏳ Runtime: {movie.Runtime} mins");
    Console.WriteLine($"⭐ Rating: {movie.Rating}/10");
    Console.WriteLine($"🔗 IMDb: https://www.imdb.com/title/{movie.IMDb}");
    Console.WriteLine();
    // Console.WriteLine(border); // Bottom border
}



 public void ShowResults() {
    if (Res == null || Res.Count == 0) {
        Console.WriteLine("No results found.");
        return;
    }
    
    // Get terminal width dynamically at runtime
    int terminalWidth = Console.WindowWidth;
    
    // Define fixed widths for columns
    int idWidth = 5;
    int yearWidth = 7;
    int ratingWidth = 8; // Width for Ratings column
    
    // Calculate remaining space
    int remainingSpace = terminalWidth - idWidth - yearWidth - ratingWidth - 17; // 17 accounts for separators & spacing
    
    // Divide remaining space between title and genres
    int titleWidth = (int)(remainingSpace * 0.4); // 40% for title
    int genresWidth = remainingSpace - titleWidth; // 60% for genres
    
    // Ensure minimum widths
    titleWidth = Math.Max(20, titleWidth);
    genresWidth = Math.Max(20, genresWidth);
    
    // Column headers
    string idCol = "ID";
    string titleCol = "Title";
    string yearCol = "Year";
    string ratingCol = "Rating";
    string genresCol = "Genres";
    
    // Table borders
    string topLine = "┌" + new string('─', idWidth + 2) + 
                    "┬" + new string('─', titleWidth + 2) + 
                    "┬" + new string('─', yearWidth + 2) + 
                    "┬" + new string('─', ratingWidth + 2) + 
                    "┬" + new string('─', genresWidth + 2) + "┐";
                    
    string midLine = "├" + new string('─', idWidth + 2) + 
                   "┼" + new string('─', titleWidth + 2) + 
                   "┼" + new string('─', yearWidth + 2) + 
                   "┼" + new string('─', ratingWidth + 2) + 
                   "┼" + new string('─', genresWidth + 2) + "┤";
                   
    string bottomLine = "└" + new string('─', idWidth + 2) + 
                      "┴" + new string('─', titleWidth + 2) + 
                      "┴" + new string('─', yearWidth + 2) + 
                      "┴" + new string('─', ratingWidth + 2) + 
                      "┴" + new string('─', genresWidth + 2) + "┘";
    
    Console.WriteLine("\nSearch Results:");
    Console.WriteLine(topLine);
    
    // Table Header
    Console.WriteLine(
        "│ " + idCol.PadRight(idWidth) + 
        " │ " + titleCol.PadRight(titleWidth) + 
        " │ " + yearCol.PadRight(yearWidth) + 
        " │ " + ratingCol.PadRight(ratingWidth) + 
        " │ " + genresCol.PadRight(genresWidth) + " │"
    );
    
    Console.WriteLine(midLine);

    for (int i = CurrentPage*ItemsPerPage; i < Math.Min(Res.Count, (CurrentPage+1)*ItemsPerPage); i++) {
        Movie movie = Res.At(i);
        
        // Collect all genre names
        DynamicArray<string> genreNames = new DynamicArray<string>();
        for (int j = 0; j < movie.Genres.Count; j++) {
            genreNames.AddLast(movie.Genres.At(j).Name);
        }
        
        // Join genres with comma and space - no brackets
        string genres = string.Join(", ", genreNames);

        genres = genres.Trim('[', ']');
        
        // Truncate title and genres if they're too long
        string displayTitle = movie.Title;
        if (displayTitle.Length > titleWidth) {
            displayTitle = displayTitle.Substring(0, titleWidth - 3) + "...";
        }
        
        string displayGenres = genres;
        if (displayGenres.Length > genresWidth) {
            displayGenres = displayGenres.Substring(0, genresWidth - 3) + "...";
        }

        
        
        // Create row with consistent spacing
        Console.WriteLine(
            "│ " + (i+1).ToString().PadRight(idWidth) + 
            " │ " + displayTitle.PadRight(titleWidth) + 
            " │ " + (movie.ReleaseYear.ToString() ?? "N/A").PadRight(yearWidth) + 
            " │ " + movie.Rating?.ToString().PadRight(ratingWidth) + 
            " │ " + displayGenres.PadRight(genresWidth) + " │"
        );
    }

    Console.WriteLine(bottomLine);
    Console.WriteLine();
    string pagination = $"<<p Page {CurrentPage+1} of {Math.Ceiling((double)Res.Count/ItemsPerPage)} n>>";
    int consoleWidth = Console.WindowWidth;
    int padding = (consoleWidth - pagination.Length) / 2;
    if (padding > 0)
            Console.WriteLine(new string(' ', padding) + pagination);
    else
        Console.WriteLine(pagination);
    }


    // public void ShowResults() {
    //     Console.WriteLine();
    //     if (Res.Count == 0) {
    //         Console.WriteLine("No Results");
    //         return;
    //     } else {
    //         for (int i=0; i<Res.Count; i++) {
    //             Console.WriteLine(Res.At(i));
    //             Console.WriteLine();
    //         }
    //     }
    //     Console.WriteLine();
    // }

    public DSA.DynamicArray<Movie> Search(string query) {
    
        Res = new();

        string searchTerm = Utils.Clean(query);

        for (int i=0; i<Store!.Movies.Count; i++) {
            if (Store.Movies.At(i).CleanTitle.Contains(searchTerm)) {
                Res.AddLast(Store.Movies.At(i));
            }
        }

        return Res;
    }
}