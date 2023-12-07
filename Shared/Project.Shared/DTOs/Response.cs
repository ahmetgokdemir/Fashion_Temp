using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Project.Shared.DTOs
{
    public class Response<T>
    {
        public T Data_throughput { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }

        [JsonIgnore] // serilaze edilmeyecek
        public bool IsSuccessful { get; set; }

        public List<string> Errors { get; set; }

        // Static Factory Method
        public static Response<T> Success(T data, int statusCode)
        {
            return new Response<T> { Data_throughput = data, StatusCode = statusCode, IsSuccessful = true };
        }

        // update, delete operations (T --> NoContent )
        public static Response<T> Success(int statusCode)
        {
            return new Response<T> { Data_throughput = default(T), StatusCode = statusCode, IsSuccessful = true };
        }

        public static Response<T> Fail(List<string>errors,int statusCode)
        {
            return new Response<T> 
            { 
                Errors = errors, 
                StatusCode = statusCode, 
                IsSuccessful = false 
            };
        }
    
        public static Response<T> Fail(string error, int statusCode)
        {
            return new Response<T>
            {
                // Errors.Add(error),
                Errors = new List<string> { error },
                StatusCode = statusCode,
                IsSuccessful = false
            };
        }
    
    }
}
