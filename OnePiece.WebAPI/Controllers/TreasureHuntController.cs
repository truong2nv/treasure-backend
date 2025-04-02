using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnePiece.Application.Interfaces;
using OnePiece.Core.Entities;
using OnePiece.WebAPI.Models;

namespace OnePiece.WebAPI.Controllers;

[ApiController]
public class TreasureHuntController(ITreasureHuntService treasureHuntService, IMapper mapper) : ControllerBase
{
    [HttpGet("api/treasure-hunt")]
    public IActionResult Index()
    {
       var result =  treasureHuntService.GetLatest();
        return Ok(result);
    }

    [HttpPost("api/treasure-hunt")]
    public async Task<IActionResult> Hunt([FromBody] HuntRequest request)
    {
        var entity = mapper.Map<TreasureHunt>(request);
        var (fuel, lst) = await treasureHuntService.Hunt(entity, request.Matrix);
        return Ok(new { fuel });
    }

    [HttpPost("api/treasure-hunt/save")]
    public async Task<IActionResult> Save([FromBody] HuntRequest request)
    {
        var entity = mapper.Map<TreasureHunt>(request);
        await treasureHuntService.Create(entity);
        return Ok();
    }


}