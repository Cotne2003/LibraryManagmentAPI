using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentAPI.Helpers
{
    public class QueryObject
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        [Range(1000, 2100)]
        public int? Year { get; set; } = null;
        public bool? Availability { get; set; } = null;
    }
}
