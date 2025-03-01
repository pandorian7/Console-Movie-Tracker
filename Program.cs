using MovieTracker;

string fp = "movie_metadata.txt";

var l = new Loader(fp);

l.Read();


DSA.InsertionSort.Sort<Movie, string>(l.Movies, m => m.Title);
Console.WriteLine(l.Movies.At(0).Title);
Console.WriteLine(l.Movies.At(1).Title);
