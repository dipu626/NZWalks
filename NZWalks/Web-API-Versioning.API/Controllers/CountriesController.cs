using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web_API_Versioning.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class CountriesController : ControllerBase
    {
        [HttpGet]
        [MapToApiVersion("1.0")]
        public IActionResult GetV1()
        {
            List<Models.Domain.Country> countriesDomainModel = CountriesData.Get();

            List<Models.DTO.CountryDtoV1> response = new();
            foreach (Models.Domain.Country country in countriesDomainModel)
            {
                response.Add(new Models.DTO.CountryDtoV1
                {
                    Id = country.Id,
                    Name = country.Name,
                });
            }
            
            return Ok(response);
        }

        [HttpGet]
        [MapToApiVersion("2.0")]
        public IActionResult GetV2()
        {
            List<Models.Domain.Country> countriesDomainModel = CountriesData.Get();

            List<Models.DTO.CountryDtoV2> response = new();
            foreach (Models.Domain.Country country in countriesDomainModel)
            {
                response.Add(new Models.DTO.CountryDtoV2
                {
                    Id = country.Id,
                    CountryName = country.Name,
                });
            }

            return Ok(response);
        }
    }
}
