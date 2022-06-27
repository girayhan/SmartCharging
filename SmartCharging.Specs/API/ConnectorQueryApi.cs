using Microsoft.AspNetCore.Mvc.Testing;
using RestSharp;
using SmartCharging.Domain.Query.DTOs;

namespace SmartCharging.Specs.API
{
    public class ConnectorQueryApi
    {
        private readonly HttpClient client;

        public ConnectorQueryApi(WebApplicationFactory<Program> factory)
        {
            this.client = factory.CreateDefaultClient(new Uri("http://localhost/"));
        }

        public async Task<IEnumerable<ConnectorDto>> GetAllConnectors()
        {
            var restClient = new RestClient(this.client);

            var request = new RestRequest("ConnectorQuery/GetAllConnectors/", Method.Get);

            var response = await restClient.ExecuteAsync<List<ConnectorDto>>(request);

            return response.Data;
        }

        public async Task<ConnectorDto> GetConnector(int id, Guid chargeStationId)
        {
            var restClient = new RestClient(this.client);

            var request = new RestRequest("ConnectorQuery/GetConnector/", Method.Get);
            request.AddParameter("id", id);
            request.AddParameter("chargeStationId", chargeStationId);

            var response = await restClient.ExecuteAsync<ConnectorDto>(request);

            return response.Data;
        }
    }
}
