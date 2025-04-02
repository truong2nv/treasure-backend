using System.Collections.Generic;
using System.Threading.Tasks;
using OnePiece.Core.Entities;

namespace OnePiece.Application.Interfaces;

public interface ITreasureHuntService
{
    TreasureHunt? GetLatest();
    Task Create(TreasureHunt treasureHunt);
    Task<(double, List<(int, int)>)> Hunt(TreasureHunt treasureHunt, List<List<int>> matrix);
}