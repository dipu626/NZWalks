using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models.DTO.Region;
using NZWalks.UI.Models.ViewModels.Region;
using System.Text;
using System.Text.Json;

namespace NZWalks.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
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

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel addRegionViewModel)
        {
            HttpClient client = httpClientFactory.CreateClient();
            HttpRequestMessage httpRequestMessage = new()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7140/api/regions"),
                Content = new StringContent(JsonSerializer.Serialize(addRegionViewModel), Encoding.UTF8, "application/json"),
            };
            HttpResponseMessage httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            RegionDto? response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();
            
            if (response is not null)
            {
                return RedirectToAction("Index", "Regions");
            }

            return View();
        }
    }
}
