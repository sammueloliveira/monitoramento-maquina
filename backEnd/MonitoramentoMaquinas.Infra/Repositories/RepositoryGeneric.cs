using Microsoft.EntityFrameworkCore;
using MonitoramentoMaquinas.Domain.Interfaces;
using MonitoramentoMaquinas.Infra.Data;

public class RepositoryGeneric<T> : IGeneric<T> , IDisposable where T : class
{
    private readonly Context _context;

    public RepositoryGeneric(Context context)
    {
        _context = context;
    }

    public async Task Add(T objeto)
    {
        await _context.Set<T>().AddAsync(objeto);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(T objeto)
    {
        _context.Set<T>().Remove(objeto);
        await _context.SaveChangesAsync();
    }

    public async Task Update(T objeto)
    {
        _context.Set<T>().Update(objeto);
        await _context.SaveChangesAsync();
    }

    public async Task<T> GetEntityById(Guid id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<List<T>> List()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    #region IDisposable Support

    private bool disposedValue = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {

                _context.Dispose();
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #endregion
}