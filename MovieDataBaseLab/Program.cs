using MovieDataBaseLab.Models;
//populateTable();
Console.WriteLine("Welcome to the movies database!");
while (true)
{
    if (Validator.Validator.GetContinue("Would you like to search for a movie by title or by genre?", "t", "g"))
    {
        List<Movie> titles = SearchByTitle();
        DisplayList(titles);
    }
    else
    {
        List<Movie> genres = SearchByGenre();
        DisplayList(genres);
    }
    if(!Validator.Validator.GetContinue())
    {
        break;
    }
    Console.Clear();
}



//methods

static void populateTable()
{
    List<string> movieInfo = new List<string>()
        {
            "A Bug's Life", "Animated", "95",
            "Toy Story", "Animated", "81",
            "Shrek", "Animated", "90",
            "Titanic", "Drama", "194",
            "The Godfather", "Drama", "175",
            "Friday The 13th", "Horror", "95",
            "The Evil Dead", "Horror", "85",
            "I, Robot", "Scifi", "115",
            "Jurrasic Park", "Scifi", "127",
            "Alien vs. Predator", "Scifi", "115"
        };
    using (MovieDBContext context = new MovieDBContext())
    {
        for (int i = 0; i < movieInfo.Count; i += 3)
        {
            Movie newMovie = new Movie()
            {
                Title = movieInfo[i],
                Genre = movieInfo[i + 1],
                Runtime = float.Parse(movieInfo[i + 2]),
            };
            context.Movies.Add(newMovie);
            context.SaveChanges();
        }
    }
}

static List<Movie> SearchByGenre()
{
    using (MovieDBContext context = new MovieDBContext())
    {
        List<Movie> all = context.Movies.ToList();
        List<Movie> genres = all.DistinctBy(m => m.Genre).ToList();
        while (true)
        {
            genres.ForEach(g => Console.WriteLine("{0,-5}{1,-30}", $"{genres.IndexOf(g) + 1}.", $"{g.Genre}"));
            int intChoice = Validator.Validator.GetUserNumberInt();
            if (!Validator.Validator.InRange(intChoice, 1, genres.Count))
            {
                Console.Clear();
                Console.WriteLine("Out of range. Please try again.");
                continue;
            }
            Movie choice = genres[intChoice - 1];
            List<Movie> result = context.Movies.Where(m => m.Genre.ToLower() == choice.Genre.ToLower()).ToList();
            return result;
        }
    }
    
}

static List<Movie> SearchByTitle()
{
    using (MovieDBContext context = new MovieDBContext())
    {
        List<Movie> all = context.Movies.ToList();
        List<Movie> titles = all.DistinctBy(m => m.Title).ToList();
        while (true)
        {
            titles.ForEach(t => Console.WriteLine("{0,-5}{1,-30}", $"{titles.IndexOf(t) + 1}.", $"{t.Title}"));
            int intChoice = Validator.Validator.GetUserNumberInt();
            if (!Validator.Validator.InRange(intChoice, 1, titles.Count))
            {
                Console.Clear();
                Console.WriteLine("Out of range. Please try again.");
                continue;
            }
            Movie choice = titles[intChoice - 1];
            List<Movie> result = context.Movies.Where(m => m.Title.ToLower() == choice.Title.ToLower()).ToList();
            return result;
        }
    }

}

static void DisplayList(List<Movie> list)
{
    list.ForEach(m => Console.WriteLine(m.ToString()));
}