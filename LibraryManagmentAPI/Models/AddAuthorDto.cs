using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentAPI.Models
{
    public class AddAuthorDto
    {
        public required string Name { get; set; }
    }
}
