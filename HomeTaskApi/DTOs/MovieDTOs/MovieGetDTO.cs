using HomeTaskApi.Entities;

namespace HomeTaskApi.DTOs.MovieDTOs;

public class MovieGetDTO
{
    public int Id { get; set; }
    public int GenreId { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string Desc { get; set; }
}
