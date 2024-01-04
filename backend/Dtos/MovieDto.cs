namespace MyMovies.Dtos;

public class MovieDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Director { get; set; }
    public int Year { get; set; }
    public double Rate { get; set; }
    public int ThirdPartyId { get; set; }
}