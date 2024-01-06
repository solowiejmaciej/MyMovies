namespace MyMovies.Models.RequestsBody;

public class UpdateMovieRequestBody
{
    public string Title { get; set; }
    public string? Director { get; set; }
    public int Year { get; set; }
    public double? Rate { get; set; }
}