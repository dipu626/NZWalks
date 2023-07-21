using Web_API_Versioning.API.Models.Domain;

namespace Web_API_Versioning.API
{
    public static class CountriesData
    {
        public static List<Country> Get()
        {
            List<Country> countries = new()
            {
                new Country{ Id = 1, Name = "Bangladesh" },
                new Country{ Id = 2, Name = "India" },
                new Country{ Id = 3, Name = "Nepal" },
                new Country{ Id = 4, Name = "Pakistan" },
                new Country{ Id = 5, Name = "Bhutan" },
                new Country{ Id = 6, Name = "Thailand" },
            };
            return countries;
        }
    }
}
