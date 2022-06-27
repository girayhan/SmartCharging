using Microsoft.AspNetCore.Mvc.Testing;
using RestSharp;
using SmartCharging.Domain.Query.DTOs;
using System.Net;

namespace SmartCharging.Specs.API
{
    public class GroupQueryApi
    {
        private readonly HttpClient client;

        public GroupQueryApi(WebApplicationFactory<Program> factory)
        {
            this.client = factory.CreateDefaultClient(new Uri("http://localhost/"));
        }

        public async Task<IEnumerable<GroupDto>> GetAllGroups()
        {
            var restClient = new RestClient(this.client);
 
            var request = new RestRequest("GroupQuery/GetAllGroups/", Method.Get);

            var response = await restClient.ExecuteAsync<List<GroupDto>>(request);

            return response.Data;
        }

        public async Task<GroupDto> GetGroup(Guid id)
        {
            var restClient = new RestClient(this.client);

            var request = new RestRequest("GroupQuery/GetGroup/", Method.Get);
            request.AddParameter("id", id);

            var response = await restClient.ExecuteAsync<GroupDto>(request);

            return response.Data;
        }
    }
}
