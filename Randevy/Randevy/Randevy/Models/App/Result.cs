using System.Collections.Generic;

namespace Randevy.Models.App
{
    /// <summary>
    /// Base result answer from rest server 
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    public class Result<TDto>
    {
        public int Total { get; set; } = -1;
        public List<TDto> Data { get; set; }
        public bool Pagination { get; set; }
    }
}
