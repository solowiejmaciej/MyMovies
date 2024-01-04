namespace MyMovies.Models.ThirdParty;

public class ExternalMovie
{
    public string Title { get; set; }
    public string Director { get; set; }
    public int Year { get; set; }
    public double Rate { get; set; }
    public int ExternalId { get; set; }
}