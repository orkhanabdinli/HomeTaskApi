using HomeTaskApi.Data;
using HomeTaskApi.DTOs.GenreDTOs;
using HomeTaskApi.DTOs.MovieDTOs;
using HomeTaskApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeTaskApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenreController : ControllerBase
{
    private readonly HomeTaskApiDbContext _homeTaskApiDbContext;

    public GenreController(HomeTaskApiDbContext homeTaskApiDbContext)
    {
        _homeTaskApiDbContext = homeTaskApiDbContext;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        var genres = await _homeTaskApiDbContext.Genres.ToListAsync();
        List<GenreGetDTO> genreGetDTOs = new List<GenreGetDTO>();
        foreach (var genre in genres)
        {
            GenreGetDTO genreGetDTO = new GenreGetDTO()
            {
                Id = genre.Id,
                Name = genre.Name
            };
            genreGetDTOs.Add(genreGetDTO);
        }
        return Ok(genreGetDTOs);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        Genre? genre = await _homeTaskApiDbContext.Genres.FindAsync(id);
        if (genre is null) return NotFound();
        GenreGetDTO genreGetDTO = new GenreGetDTO()
        {
            Id = genre.Id,
            Name = genre.Name
        };
        return Ok(genreGetDTO);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create(GenrePostDTO genrePostDTO)
    {
        Genre genre = new Genre()
        {
            Name = genrePostDTO.Name,
            IsDeleted = genrePostDTO.IsDeleted,
            CreatedDate = DateTime.UtcNow.AddHours(4),
            UpdatedDate = DateTime.UtcNow.AddHours(4)
        };
        await _homeTaskApiDbContext.AddAsync(genre);
        await _homeTaskApiDbContext.SaveChangesAsync();
        return Ok();
    }
}
