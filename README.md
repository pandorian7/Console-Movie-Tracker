# 🎬 Movie Tracker

Movie Tracker is a command-line application that helps users search, sort, filter, and manage their favorite movies efficiently. With built-in IMDb search functionality, users can create personalized watchlists and explore movie details effortlessly.

## 📌 Features

- 🔍 **Search Movies** by title, release year, genre, or IMDb ID.
- 📊 **Sort** search results by title, year, or rating.
- 🎭 **Filter** search results by year, rating, or genre.
- 🌟 **Create and manage watchlists** with custom movie selections.
- 📝 **View last search results** for quick access.
- 📜 **User-friendly command structure** for seamless navigation.

## 🚀 Getting Started

1. Clone the repository:
   ```sh
   git clone https://github.com/yourusername/movie-tracker.git
   ```
2. Navigate to the project folder:
   ```sh
   cd movie-tracker
   ```
3. Run the program:
   ```sh
   python main.py
   ```

## 🛠 Commands

### 🔍 Search Movies
```sh
search {title, year, genre, imdb} [text]
```
✅ Example:
```sh
search title Inception
```
Shortcut for title search:
```sh
? Inception
```

### 📌 View Movie Details
```sh
info [search results id]
```
✅ Example:
```sh
info 1
```

### 📊 Sort Search Results
```sh
sort {title, year, rating}
```
✅ Example:
```sh
sort year
```

### 🎭 Filter Search Results
```sh
filter {year, rating, genre} [text]
```
✅ Example:
```sh
filter year 2012
```

### 🌟 Manage Watchlists
```sh
list new [name]      # Create a new list
list add [id]        # Add a movie to the list
list show            # View watchlist
list all             # View all lists
```
✅ Example:
```sh
list new Favorites
list add 2
list show
```

### 📝 View Last Search Results
```sh
res
```

### 🔎 View Help Menu
```sh
help
```

### 🚪 Exit the Program
```sh
exit
```
