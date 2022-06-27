using Microsoft.AspNetCore.Mvc.Testing;
using RestSharp;
using SmartCharging.Domain.Query.DTOs;

namespace SmartCharging.Specs.API
{
    public class ChargeStationQueryApi
    {
        private readonly HttpClient client;

        public ChargeStationQueryApi(WebApplicationFactory<Program> factory)
        {
            this.client = factory.CreateDefaultClient(new Uri("http://localhost/"));
        }

        public async Task<IEnumerable<ChargeStationDto>> GetAllChargeStation()
        {
            var restClient = new RestClient(this.client);

            var request = new RestRequest("ChargeStationQuery/GetAllChargeStation/", Method.Get);

            var response = await restClient.ExecuteAsync<List<ChargeStationDto>>(request);

            return response.Data;
        }

        public async Task<ChargeStationDto> GetChargeStation(Guid id)
        {
            var restClient = new RestClient(this.client);

            var request = new RestRequest("ChargeStationQuery/GetChargeStation/", Method.Get);
            request.AddParameter("id", id);

            var response = await restClient.ExecuteAsync<ChargeStationDto>(request);

            return response.Data;
        }
    }
}
