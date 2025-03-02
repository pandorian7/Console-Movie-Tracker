namespace MovieTracker;

class CLI: CLIActions {
    public override int ExecuteCommand(string command) {
        var res = command.Trim().Split(" ");
        if (res.Length == 1 && res[0] == "") {
            return 0;
        }
        string query, listName;
        int year, resultId, listId;
        float rating;

        var AllMovies = Tracker.Store!.Movies;
        var Res = Tracker.Res;

        switch (res[0]) {
            case "help":
                PrintHelp();
                return 0;
            case "res":
                ShowResults();
                return 0;
            case "?":
                CheckNthArg(res, 1, "No title provided");
                query = MergeFrom(res, 1);
                FilterByTitle(AllMovies, query);
                ShowResults();
                return 0;
            case "search":
                CheckNthArg(res, 1, "No search parameter provided");
                switch(res[1]){
                    case "title":
                        CheckNthArg(res, 2, "No title provided");
                        query = MergeFrom(res, 2);
                        FilterByTitle(AllMovies, query);
                        ShowResults();
                        return 0;
                    case "year":
                        CheckNthArg(res, 2, "No year provided");
                        year = Convert.ToInt32(res[2]);
                        FilterByYear(AllMovies, year);
                        ShowResults();
                        return 0;
                    case "genre":
                        CheckNthArg(res, 2, "No genre provided");
                        query = MergeFrom(res, 2);
                        FilterByGenre(AllMovies, query);
                        ShowResults();
                        return 0;
                    case "imdb":
                        CheckNthArg(res, 2, "No imdb id provided");
                        query = res[2];
                        FilterByIMDbId(AllMovies, query);
                        ShowResults();
                        return 0;
                    
                    default:
                        throw new Exception("Invalid search parameter");
                }
            case "info":
                CheckNthArg(res, 1, "No serach result id provided");
                resultId = Convert.ToInt32(res[1]);
                VerityValidResIndex(resultId);
                Tracker.ShowMoveInfo(resultId);
                return 0;
            case "sort":
                CheckNthArg(res, 1, "No sort parameter provided");
                switch(res[1]) {
                    case "title":
                        SortByTitle();
                        ShowResults();
                        return 0;
                    case "year":
                        SortByYear();
                        ShowResults();
                        return 0;
                    case "rating":
                        SortByRating();
                        Tracker.ShowResults();
                        return 0;
                    default:
                        throw new Exception("Invalid search parameter");
                }

            case "filter":
                CheckNthArg(res, 1, "No filter parameter provided");
                switch(res[1]) {
                    case "year":
                        CheckNthArg(res, 2, "No year provided");
                        year = Convert.ToInt32(res[2]);
                        FilterByYear(Res, year);
                        ShowResults();
                        return 0;
                    case "genre":
                        CheckNthArg(res, 2, "No genre provided");
                        query = MergeFrom(res, 2);
                        FilterByGenre(Res, query);
                        ShowResults();
                        return 0;
                    case "rating":
                        CheckNthArg(res, 2, "No rating provided");
                        rating = Convert.ToSingle(res[2]);
                        FilterByRating(Res, rating);
                        ShowResults();
                        return 0;
                    default:
                        throw new Exception("Invalid filter parameter");
                }
            case "list":
                CheckNthArg(res, 1, "No list parameter provided");
                switch(res[1]) {
                    case "new":
                        CheckNthArg(res, 2, "No list name provided");
                        listName = MergeFrom(res, 2);
                        NewList(listName);
                        return 0;
                    case "all":
                       ShowAllLists();
                        return 0;
                    case "add":
                        CheckNthArg(res, 2, "No search result id provided");
                        resultId = Convert.ToInt32(res[2]);
                        VerityValidResIndex(resultId);
                        listId = GetOrCreateListIdFromUser(true);
                        AddMovieToListFromResults(listId, resultId);
                        return 0;
                    case "show":
                        listId = GetOrCreateListIdFromUser(false);
                        Tracker.Res = new(Tracker.UserData.UserLists.At(listId).Movies);
                        ShowResults();
                        return 0;
                    default:
                        throw new Exception("Invalid list parameter");
                }
            case "exit":
                return -1;

            default:
                throw new Exception($"Invalid command: {command}");
        }
    }

}