using Microsoft.AspNetCore.Mvc.Testing;
using RestSharp;
using SmartCharging.Domain.Command.Commands.Group;
using System.Net;

namespace SmartCharging.Specs.API
{    
    public class GroupCommandApi
    {
        private readonly HttpClient client;

        public GroupCommandApi(WebApplicationFactory<Program> factory)
        {
            this.client = factory.CreateDefaultClient(new Uri("http://localhost/"));
        }

        public async Task<RestResponse<Guid>> CreateGroup(CreateGroupCommand createGroupCommand)
        {
            var restClient = new RestClient(this.client);
           
            var request = new RestRequest("GroupCommand/CreateGroup/", Method.Post);
            request.AddBody(createGroupCommand);

            var response = await restClient.ExecuteAsync<Guid>(request);
            
            return response;
        }

        public async Task<RestResponse> UpdateGroup(UpdateGroupCommand updateGroupCommand)
        {
            var restClient = new RestClient(this.client);

            var request = new RestRequest("GroupCommand/UpdateGroup/", Method.Put);
            request.AddBody(updateGroupCommand);

            var response = await restClient.ExecuteAsync(request);

            return response;
        }

        public async Task<RestResponse> RemoveGroup(RemoveGroupCommand removeGroupCommand)
        {
            var restClient = new RestClient(this.client);

            var request = new RestRequest("GroupCommand/RemoveGroup/", Method.Delete);
            request.AddBody(removeGroupCommand);

            var response = await restClient.ExecuteAsync(request);

            return response;
        }
    }
}
