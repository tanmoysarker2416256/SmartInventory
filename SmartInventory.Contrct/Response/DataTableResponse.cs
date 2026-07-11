using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Contrct.Response;


public class DataTableResponse <T>
{
    public int Draw {  get; set; }
    public int RecordsTotal { get; set; }

    public int RecordsFiltered { get; set; }

    public List<T> Data { get; set; } = new();

    public string? Error { get; set; }
}
