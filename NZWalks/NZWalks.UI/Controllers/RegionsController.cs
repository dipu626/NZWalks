using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models.DTO.Region;

namespace NZWalks.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            List<RegionDto> response = new();
            try
            {
                // Get all Regions from Web API
                HttpClient client = httpClientFactory.CreateClient();
                HttpResponseMessage httpResponseMessage = await client.GetAsync("https://localhost:7140/api/regions");
                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());
            }
            catch (Exception ex)
            {
                // Log the exception
            }

            return View(response);
        }
    }
}
