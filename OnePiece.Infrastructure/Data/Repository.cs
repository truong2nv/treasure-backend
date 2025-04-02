using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnePiece.Core.Interfaces;

namespace OnePiece.Infrastructure.Data;
public class Repository<T> : IRepository<T> where T : class
{
    private readonly OpDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(OpDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<T>();
    }
    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        return entity;
    }
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public IQueryable<T> AsQueryable()
    {
        return _dbSet.AsQueryable();
    }
}