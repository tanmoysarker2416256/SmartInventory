using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.DAL.Core; 

public interface IUnitOfWork : IDisposable
{
    bool SaveChanegs();

    void RollBack();

    Task<bool> SaveChanegsAsync();
}
