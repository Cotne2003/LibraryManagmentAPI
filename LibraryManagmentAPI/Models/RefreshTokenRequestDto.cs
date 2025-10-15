namespace LibraryManagmentAPI.Models
{
    public class RefreshTokenRequestDto
    {
        public Guid AuthorId { get; set; }
        public required string RefreshToken { get; set; }
    }
}
