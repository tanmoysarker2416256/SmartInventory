using SmartInventory.Contrct.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.BLL.Helpers; 

public class DataTableHelper
{
    public static (int PageIndex , int PageSize) CalculatePagination(DataTableRequest request)
    {
        var pageIndex = (request.Start / request.Length) + 1;
        
        var pageSize = request.Length >0 ? request.Length : 10;
        return (pageIndex, pageSize);
    }

    public static Expression<Func<TEntity , bool>> ? BuilderSearchPredicate<TEntity>(
        DataTableRequest request,
        Func<string , Expression<Func<TEntity , bool>>>? searchBuilder = null)
    {
        if(string.IsNullOrWhiteSpace(request.Search.Value)||searchBuilder==null)
            return null;

        return searchBuilder(request.Search.Value);
    }

    public static Func<IQueryable<TEntity> ,IOrderedQueryable<TEntity>>?BuildOrderby<TEntity>(
        DataTableRequest request ,
        Dictionary<string , (Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>>Asc ,
        Func<IQueryable<TEntity> , IOrderedQueryable<TEntity>> Desc)>columnMappings,
        Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>>? defaultOrderBy=null)
    {
        if(request.Order==null || !request.Order.Any()|| request.Column == null)
        {
            return defaultOrderBy;
        } 

        var order = request.Order.First();
        var columnIndex = order.Column;
        if(columnIndex<0 || columnIndex>=request.Column.Count)
        {
            return defaultOrderBy;
        }

        var column = request.Column[columnIndex];
        var columnKey = column.Data.ToLower();
        var isAscending = order.Dir.ToLower() == "asc";

        if(columnMappings.TryGetValue(columnKey, out var orderFunctions))
        {
            return isAscending ? orderFunctions.Asc : orderFunctions.Desc;
        }

        return defaultOrderBy;




    }

}
