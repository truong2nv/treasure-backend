using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnePiece.Application.Interfaces;
using OnePiece.Core.Entities;
using OnePiece.Core.Interfaces;

namespace OnePiece.Application.Services;

public class TreasureHuntService : ITreasureHuntService
{
    private readonly IRepository<TreasureHunt> _repository;

    public TreasureHuntService(IRepository<TreasureHunt> repository)
    {
        _repository = repository;
    }

    public TreasureHunt? GetLatest()
    {
        return _repository.AsQueryable().OrderByDescending(e => e.CreatedAt).FirstOrDefault();
    }

    public async Task Create(TreasureHunt treasureHunt)
    {
        await _repository.AddAsync(treasureHunt);
        await _repository.SaveChangesAsync();
    }

    public async Task<(double, List<(int, int)>)> Hunt(TreasureHunt treasureHunt, List<List<int>> matrix)
    {
        var (fuel, lst) = FindMinimumFuel(treasureHunt.N, treasureHunt.M, treasureHunt.P, ConvertListToArray(matrix));
        treasureHunt.Status = fuel > -1 ? "Solved" : "Failed";
        treasureHunt.Result = fuel;
        await Create(treasureHunt);
        return (fuel, lst);
    }

    private static (double, List<(int,int)>) FindMinimumFuel(int n, int m, int p, int[,] matrix)
    {
        // Lưu danh sách vị trí của từng rương (có thể trùng nhau)
        Dictionary<int, List<(int, int)>> chestPositions = new();
        for (var i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
            {
                int chest = matrix[i, j];
                if (!chestPositions.ContainsKey(chest))
                    chestPositions[chest] = new List<(int, int)>();
                chestPositions[chest].Add((i, j));
            }

        // Kiểm tra xem có đủ rương từ 1 đến p không
        for (var i = 1; i <= p; i++)
            if (!chestPositions.ContainsKey(i))
                return (-1, new List<(int, int)>());

        // Khởi tạo Priority Queue: (fuel, x, y, key)
        var pq = new SortedSet<(double fuel, int x, int y, int key)>(
            Comparer<(double, int, int, int)>.Create((a, b) =>
            a.Item1 == b.Item1 ? (a.Item4 == b.Item4 ? (a.Item2 == b.Item2 ? a.Item3 - b.Item3 : a.Item2 - b.Item2) : a.Item4 - b.Item4) : a.Item1.CompareTo(b.Item1))
        );

        // Khởi tạo khoảng cách min
        Dictionary<(int, int, int), double> dist = new();
        pq.Add((0, 0, 0, 0)); // Bắt đầu từ (0,0) với chìa khóa 0
        dist[(0, 0, 0)] = 0;
        
        Dictionary<(int, int, int), (int, int, int)> previous = new();

        // Dijkstra
        while (pq.Count > 0)
        {
            var (fuel, x, y, key) = pq.Min;
            pq.Remove(pq.Min);

            // Nếu đến rương cuối, trả về kết quả
            if (key == p)
            {
                List<(int, int)> path = new();
                var current = (x, y, key);
                while (previous.ContainsKey(current))
                {
                    path.Add((current.Item1, current.Item2));
                    current = previous[current];
                }
                path.Add((0, 0));
                path.Reverse();
                return (fuel, path);
            }

            // Kiểm tra các vị trí chứa rương tiếp theo
            foreach (var (nx, ny) in chestPositions[key + 1])
            {
                var newFuel = fuel + Math.Sqrt((nx - x) * (nx - x) + (ny - y) * (ny - y));

                if (!dist.ContainsKey((nx, ny, key + 1)) || newFuel < dist[(nx, ny, key + 1)])
                {
                    dist[(nx, ny, key + 1)] = newFuel;
                    pq.Add((newFuel, nx, ny, key + 1));
                    previous[(nx, ny, key + 1)] = (x, y, key);
                }
            }
        }
        return (-1, new List<(int, int)>());
    }

    private static int[,] ConvertListToArray(List<List<int>> list)
    {
        int rows = list.Count;
        int cols = list[0].Count;

        int[,] array = new int[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                array[i, j] = list[i][j];
            }
        }

        return array;
    }
}