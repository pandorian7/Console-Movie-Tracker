using System.Runtime.InteropServices;

namespace MovieTracker;

class CLI: CLIActions {
    public override void ExecuteCommand(string command) {
        var res = command.Trim().Split(" ");
        if (res.Length == 1 && res[0] == "") {
            return;
        }
        string query, listName;
        int year, resultId, listId;
        float rating;

        var AllMovies = Tracker.Store!.Movies;
        var Res = Tracker.Res;

        switch (res[0]) {
            case "help":
                PrintHelp();
                break;
            case "res":
                ShowResults();
                break;
            case "?":
                CheckNthArg(res, 1, "No title provided");
                query = MergeFrom(res, 1);
                FilterByTitle(AllMovies, query);
                ShowResults();
                break;
            case "search":
                CheckNthArg(res, 1, "No search parameter provided");
                switch(res[1]){
                    case "title":
                        CheckNthArg(res, 2, "No title provided");
                        query = MergeFrom(res, 2);
                        FilterByTitle(AllMovies, query);
                        ShowResults();
                        break;
                    case "year":
                        CheckNthArg(res, 2, "No year provided");
                        year = Convert.ToInt32(res[2]);
                        FilterByYear(AllMovies, year);
                        ShowResults();
                        break;
                    case "genre":
                        CheckNthArg(res, 2, "No genre provided");
                        query = MergeFrom(res, 2);
                        FilterByGenre(AllMovies, query);
                        ShowResults();
                        break;
                    case "imdb":
                        CheckNthArg(res, 2, "No imdb id provided");
                        query = res[2];
                        FilterByIMDbId(AllMovies, query);
                        ShowResults();
                        break;
                    
                    default:
                        throw new Exception("Invalid search parameter");
                }
                break;
            case "info":
                CheckNthArg(res, 1, "No serach result id provided");
                resultId = Convert.ToInt32(res[1]);
                VerityValidResIndex(resultId);
                Tracker.ShowMoveInfo(resultId);
                break;
            case "sort":
                CheckNthArg(res, 1, "No sort parameter provided");
                switch(res[1]) {
                    case "title":
                        SortByTitle();
                        ShowResults();
                        break;
                    case "year":
                        SortByYear();
                        ShowResults();
                        break;
                    case "rating":
                        SortByRating();
                        Tracker.ShowResults();
                        break;
                    default:
                        throw new Exception("Invalid search parameter");
                }
                break;

            case "filter":
                CheckNthArg(res, 1, "No filter parameter provided");
                switch(res[1]) {
                    case "year":
                        CheckNthArg(res, 2, "No year provided");
                        year = Convert.ToInt32(res[2]);
                        FilterByYear(Res, year);
                        ShowResults();
                        break;
                    case "genre":
                        CheckNthArg(res, 2, "No genre provided");
                        query = MergeFrom(res, 2);
                        FilterByGenre(Res, query);
                        ShowResults();
                        break;
                    case "rating":
                        CheckNthArg(res, 2, "No rating provided");
                        rating = Convert.ToSingle(res[2]);
                        FilterByRating(Res, rating);
                        ShowResults();
                        break;
                    default:
                        throw new Exception("Invalid filter parameter");
                }
                break;
            case "list":
                CheckNthArg(res, 1, "No list parameter provided");
                switch(res[1]) {
                    case "new":
                        CheckNthArg(res, 2, "No list name provided");
                        listName = MergeFrom(res, 2);
                        NewList(listName);
                        break;
                    case "all":
                       ShowAllLists();
                        break;
                    case "add":
                        CheckNthArg(res, 2, "No search result id provided");
                        resultId = Convert.ToInt32(res[2]);
                        VerityValidResIndex(resultId);
                        listId = GetOrCreateListIdFromUser(true);
                        AddMovieToListFromResults(listId, resultId);
                        break;
                    case "show":
                        listId = GetOrCreateListIdFromUser(false);
                        Tracker.Res = new(Tracker.UserData.UserLists.At(listId).Movies);
                        ShowResults();
                        break;
                    default:
                        throw new Exception("Invalid list parameter");
                }
                break;
            case "exit":
                Environment.Exit(0);
                break;

            default:
                throw new Exception($"Invalid command: {command}");
        }
    }

}