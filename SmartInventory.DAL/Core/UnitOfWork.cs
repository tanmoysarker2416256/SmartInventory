using Microsoft.EntityFrameworkCore;
using SmartInventory.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.DAL.Core;

public class UnitOfWork : IUnitOfWork
{
    private bool _disposed;

    protected readonly SmartInventoryDbContext _context;

    public UnitOfWork(SmartInventoryDbContext context)
    {
        _context = context;
    }


    public bool SaveChanegs()
    {
        return _context?.SaveChanges() > 0;
    }

    public async Task<bool> SaveChanegsAsync()
    {
        return (await _context.SaveChangesAsync()) > 0;
    }



    public void RollBack()
    {
        _context.ChangeTracker.Entries().ToList().ForEach(x=>x.Reload());
    }


    #region Dispose
    ~UnitOfWork() 
    { 
       Dispose(false);
    }

    public void Dispose()
    { 
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _context?.Dispose();
        }       
       _disposed = true;
    }
    #endregion


}
