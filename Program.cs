using MovieTracker;
using DynamicArray;

string fp = "movie_metadata.txt";

var l = new Loader(fp);

l.Read();

DynamicArray<int> d = new();

