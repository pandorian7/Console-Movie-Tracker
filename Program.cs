using DSA;
using MovieTracker;

string fp = "movie_metadata.txt";

var l = new Loader(fp);

var store = l.Read();

DynamicArray<Movie> movies = new DynamicArray<Movie>();
for (int i = 0; i < 15; i++)
{
    movies.AddLast(store.Movies.At(i)); 
}


MergeSort.Sort(movies, m => m.Title);
for (var i =0 ; i < 15; i++)
{
    Console.WriteLine(movies.At(i).Title);
}





// var m = store.Movies.Find("The Dark Knight", m=>m.Title);

// Console.WriteLine(m);
