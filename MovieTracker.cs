using System.Security.Cryptography.X509Certificates;
using DSA;

namespace MovieTracker;

class MovieTracker{
    public MovieStore? Store { get; private set; }
    public DSA.DynamicArray<Movie> Res { get; set; }

    public SortingEngine Sorting;

    public MovieTracker() {
        Res = new();
        Sorting = new();
    }

    public void LoadStore() {
        var loader = new Loader("movie_metadata.txt");
        Console.Write("Loading Movies...");
        Store = loader.Read();
        Console.Write($"\r{Store.Movies.Count} movies loaded.");
        Thread.Sleep(2000);
        Console.Write("\r\n");
    }

    //show results

    public void ShowMoveInfo(int id) {
        Console.WriteLine($"information of move with id {id}");
    }

    public void ShowResults() {
    if (Res == null || Res.Count == 0) {
        Console.WriteLine("No results found.");
        return;
    }
    
    // Get terminal width dynamically at runtime
    int terminalWidth = Console.WindowWidth;
    
    // Define fixed widths for columns
    int idWidth = 3;
    int yearWidth = 7;
    
    // Calculate remaining space
    int remainingSpace = terminalWidth - idWidth - yearWidth - 13; // 13 accounts for all separators and spacing
    
    // Divide remaining space between title and genres
    int titleWidth = (int)(remainingSpace * 0.4); // 40% for title
    int genresWidth = remainingSpace - titleWidth; // 60% for genres
    
    // Ensure minimum widths
    titleWidth = Math.Max(20, titleWidth);
    genresWidth = Math.Max(20, genresWidth);
    
    // Consistent spacing
    string idCol = "ID";
    string titleCol = "Title";
    string yearCol = "Year";
    string genresCol = "Genres";
    
    // Simpler box drawing with consistent spacing
    string topLine = "┌" + new string('─', idWidth + 2) + 
                    "┬" + new string('─', titleWidth + 2) + 
                    "┬" + new string('─', yearWidth + 2) + 
                    "┬" + new string('─', genresWidth + 2) + "┐";
                    
    string midLine = "├" + new string('─', idWidth + 2) + 
                   "┼" + new string('─', titleWidth + 2) + 
                   "┼" + new string('─', yearWidth + 2) + 
                   "┼" + new string('─', genresWidth + 2) + "┤";
                   
    string bottomLine = "└" + new string('─', idWidth + 2) + 
                      "┴" + new string('─', titleWidth + 2) + 
                      "┴" + new string('─', yearWidth + 2) + 
                      "┴" + new string('─', genresWidth + 2) + "┘";
    
    Console.WriteLine("\nSearch Results:");
    Console.WriteLine(topLine);
    
    // Create header with consistent spacing
    Console.WriteLine(
        "│ " + idCol.PadRight(idWidth) + 
        " │ " + titleCol.PadRight(titleWidth) + 
        " │ " + yearCol.PadRight(yearWidth) + 
        " │ " + genresCol.PadRight(genresWidth) + " │"
    );
    
    Console.WriteLine(midLine);

    int id = 1;
    for (int i = 0; i < Res.Count; i++) {
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
            "│ " + id.ToString().PadRight(idWidth) + 
            " │ " + displayTitle.PadRight(titleWidth) + 
            " │ " + (movie.ReleaseYear.ToString() ?? "N/A").PadRight(yearWidth) + 
            " │ " + displayGenres.PadRight(genresWidth) + " │"
        );
        id++;
    }

    Console.WriteLine(bottomLine);
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