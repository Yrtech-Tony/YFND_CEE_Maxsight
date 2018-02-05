using System.Collections.Generic;

namespace NetMidApi.Models
{
    public class ApiResult<T>
    {
        public bool Success { get; set; }
        public T Result { get; set; }
        public IDictionary<string, object> Error { get; set; }
    }
}