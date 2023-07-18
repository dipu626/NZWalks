using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO.Auth
{
    public class LoginRequestDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public required string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
