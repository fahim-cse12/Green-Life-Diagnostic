namespace Shared.DataTransferObject
{
    public record UserDto
    {
        public Guid Id {  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LoginId { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
