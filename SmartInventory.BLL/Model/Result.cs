using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.BLL.Model; 

public class Result<T>
{
    public bool Success { get; set; }

    public string Error { get; set; } = string.Empty;

    public T ? Data { get; set; }

    public Result(bool success,  string error, T? data)
    {
        Success = success;
        Error = error;
        Data = data;
    }

    public static Result<T> SuccessResult(T? data)
    {
        return new Result<T>(true, string.Empty, data);
    }

    public static Result<T> FailedResult(string error)
    {
        return new Result<T>(false, error, default);
    }
}
