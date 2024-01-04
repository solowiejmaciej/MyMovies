using System.ComponentModel.DataAnnotations;

namespace MyMovies.Entities;

public class Movie
{
    [Key]
    public int Id { get; set; }
    public int? ThirdPartyId { get; set; }
    public string Title { get; set; }
    public string Director { get; set; }
    public int Year { get; set; }
    public double Rate { get; set; }
}