using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be minimum of 3 characters")]
        [MaxLength(3, ErrorMessage = "Code has to be maximum of 3 characters")]
        public required string Code { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Name has to be minimum of 5 characters")]
        [MaxLength(100, ErrorMessage = "Name has to be maximum of 100 characters")]
        public required string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
