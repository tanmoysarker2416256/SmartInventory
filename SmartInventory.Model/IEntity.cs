using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Model; 

internal interface IEntity<T1>
{
    T1 Id { get; set; }
    T1 CreatedBy { get; set; }
    T1 UpdatedBy { get; set; }
    DateTime CreateTime { get; set; }   
    DateTime UpdateTime { get; set; }
}

public class Entity : IEntity<int>
{
    public int Id { get; set; }
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime UpdateTime { get; set; }
}