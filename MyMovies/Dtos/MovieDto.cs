namespace MyMovies.Dtos;

public class MovieDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Director { get; set; }
    public required int Year { get; set; }
    public required double Rate { get; set; }
}