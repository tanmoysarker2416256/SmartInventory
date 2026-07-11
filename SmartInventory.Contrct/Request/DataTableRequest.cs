using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Contrct.Request; 

public class DataTableRequest
{
    public int Draw {  get; set; }

    public int Start { get; set; }
    public int Length { get; set; }

    public DataTableSearch Search { get; set; } = new();

    public List<DataTableOrder>Order { get; set; } = new();

    public List<DataTableColumn> Column=new();

}

public class DataTableSearch
{
    public string Value { get; set; }=string.Empty;
}
 public class DataTableOrder
{
   public int Column {  get; set; }
    public string Dir { get; set; } = "asc";
}  

public class DataTableColumn
{
    public string Data { get; set; } = string.Empty;
    public string Name { get; set; }= string.Empty;
    public bool Searchable { get; set; }
    public bool Orderable { get; set; }

    public DataTableSearch Search{ get; set; } = new();
}

