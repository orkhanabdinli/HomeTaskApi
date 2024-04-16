using HomeTaskApi.Data;
using HomeTaskApi.DTOs.MovieDTOs;
using HomeTaskApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeTaskApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovieController : ControllerBase
{
    private readonly HomeTaskApiDbContext _homeTaskApiDbContext;

    public MovieController(HomeTaskApiDbContext homeTaskApiDbContext)
    {
        _homeTaskApiDbContext = homeTaskApiDbContext;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        var movies = await _homeTaskApiDbContext.Movies.ToListAsync();
        List <MovieGetDTO> movieGetDTOs = new List <MovieGetDTO> ();
        foreach (var movie in movies)
        {
            MovieGetDTO movieGetDTO = new MovieGetDTO()
            {
                Id = movie.Id,
                Name = movie.Name,
                Desc = movie.Desc,
                Price = movie.Price,
                GenreId = movie.GenreId
            };
            movieGetDTOs.Add(movieGetDTO);
        }
        return Ok(movieGetDTOs);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        Movie? movie = await _homeTaskApiDbContext.Movies.FindAsync(id);
        if (movie is null) return NotFound();
        MovieGetDTO movieGetDTO = new MovieGetDTO()
        {
            Id = movie.Id,
            Name = movie.Name,
            Desc = movie.Desc,
            Price = movie.Price,
            GenreId = movie.GenreId
        };
        return Ok(movieGetDTO);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create(MoviePostDTO moviePostDTO)
    {
        Movie movie = new Movie()
        {
            Name = moviePostDTO.Name,
            Desc = moviePostDTO.Desc,
            Price = moviePostDTO.Price,
            CostPrice = moviePostDTO.CostPrice,
            GenreId = moviePostDTO.GenreId,
            IsDeleted = moviePostDTO.IsDeleted,
            CreatedDate = DateTime.UtcNow.AddHours(4),
            UpdatedDate = DateTime.UtcNow.AddHours(4)
        };
        await _homeTaskApiDbContext.AddAsync(movie);
        await _homeTaskApiDbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, MoviePutDTO moviePutDTO)
    {
        var movie = await _homeTaskApiDbContext.Movies.FindAsync(id);
        if (movie == null)
        {
            return NotFound();
        }

        movie.Name = moviePutDTO.Name;
        movie.Desc = moviePutDTO.Desc;
        movie.Price = moviePutDTO.Price;
        movie.CostPrice = moviePutDTO.CostPrice;
        movie.GenreId = moviePutDTO.GenreId;
        movie.IsDeleted = moviePutDTO.IsDeleted;
        movie.UpdatedDate = DateTime.UtcNow.AddHours(4);

        await _homeTaskApiDbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var movie = await _homeTaskApiDbContext.Movies.FindAsync(id);
        if (movie == null)
        {
            return NotFound();
        }

        _homeTaskApiDbContext.Movies.Remove(movie);
        await _homeTaskApiDbContext.SaveChangesAsync();

        return Ok();
    }
}
