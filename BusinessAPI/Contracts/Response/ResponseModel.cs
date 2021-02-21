using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Contracts.Response
{
    public class ResponseModel<T>
    {
        public ResponseModel() { }
        public ResponseModel(T data, bool success)
        {
            Data = data;
            Success = success;
        }
        public ResponseModel(bool success, string error)
        {
            Success = success;
            Errors.Add(error);
        }

        public bool Success { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public T Data { get; set; }
    }
}
