namespace HomeTaskApi.DTOs.MovieDTOs;

public class MoviePutDTO
{
    public bool IsDeleted { get; set; }
    public int GenreId { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public double CostPrice { get; set; }
    public string Desc { get; set; }
}
