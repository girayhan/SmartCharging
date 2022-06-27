using Microsoft.AspNetCore.Mvc.Testing;
using RestSharp;
using SmartCharging.Domain.Command.Commands.Connector;

namespace SmartCharging.Specs.API
{
    public class ConnectorCommandApi
    {
        private readonly HttpClient client;

        public ConnectorCommandApi(WebApplicationFactory<Program> factory)
        {
            this.client = factory.CreateDefaultClient(new Uri("http://localhost/"));
        }

        public async Task<RestResponse> CreateConnector(CreateConnectorCommand createConnectorCommand)
        {
            var restClient = new RestClient(this.client);

            var request = new RestRequest("ConnectorCommand/CreateConnector/", Method.Post);
            request.AddBody(createConnectorCommand);

            var response = await restClient.ExecuteAsync(request);

            return response;
        }

        public async Task<RestResponse> UpdateConnector(UpdateConnectorCommand updateConnectorCommand)
        {
            var restClient = new RestClient(this.client);

            var request = new RestRequest("ConnectorCommand/UpdateConnector/", Method.Put);
            request.AddBody(updateConnectorCommand);

            var response = await restClient.ExecuteAsync(request);

            return response;
        }

        public async Task<RestResponse> RemoveConnector(RemoveConnectorCommand removeConnectorCommand)
        {
            var restClient = new RestClient(this.client);

            var request = new RestRequest("ConnectorCommand/RemoveConnector/", Method.Delete);
            request.AddBody(removeConnectorCommand);

            var response = await restClient.ExecuteAsync(request);

            return response;
        }
    }
}
