using MovieTracker;

string fp = "movie_metadata.txt";

var l = new Loader(fp);

var store = l.Read();

var m = store.Movies.Find("Avatar", m=>m.Title);

Console.WriteLine(m);
