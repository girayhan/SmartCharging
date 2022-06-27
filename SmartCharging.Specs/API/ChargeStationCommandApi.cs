using Microsoft.AspNetCore.Mvc.Testing;
using RestSharp;
using SmartCharging.Domain.Command.Commands.ChargeStation;

namespace SmartCharging.Specs.API
{
    public class ChargeStationCommandApi
    {
        private readonly HttpClient client;

        public ChargeStationCommandApi(WebApplicationFactory<Program> factory)
        {
            this.client = factory.CreateDefaultClient(new Uri("http://localhost/"));
        }

        public async Task<RestResponse<Guid>> CreateChargeStation(CreateChargeStationCommand createChargeStationCommand)
        {
            var restClient = new RestClient(this.client);

            var request = new RestRequest("ChargeStationCommand/CreateChargeStation/", Method.Post);
            request.AddBody(createChargeStationCommand);

            var response = await restClient.ExecuteAsync<Guid>(request);

            return response;
        }

        public async Task<RestResponse> UpdateChargeStation(UpdateChargeStationCommand updateChargeStationCommand)
        {
            var restClient = new RestClient(this.client);

            var request = new RestRequest("ChargeStationCommand/UpdateChargeStation/", Method.Put);
            request.AddBody(updateChargeStationCommand);

            var response = await restClient.ExecuteAsync(request);

            return response;
        }

        public async Task<RestResponse> RemoveChargeStation(RemoveChargeStationCommand removeChargeStationCommand)
        {
            var restClient = new RestClient(this.client);

            var request = new RestRequest("ChargeStationCommand/RemoveChargeStation/", Method.Delete);
            request.AddBody(removeChargeStationCommand);

            var response = await restClient.ExecuteAsync(request);

            return response;
        }
    }
}
