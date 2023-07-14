using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateWalkRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Name must have atleast 5 characters")]
        [MaxLength(100, ErrorMessage = "Name must have atmost 100 characters")]
        public required string Name { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "Name must have atmost 250 characters")]
        public required string Description { get; set; }

        [Required]
        [Range(0, 50, ErrorMessage = "LengthInKm must be in between 0 to 50 km")]
        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }

        [Required]
        public Guid DifficultyId { get; set; }

        [Required]
        public Guid RegionId { get; set; }
    }
}
