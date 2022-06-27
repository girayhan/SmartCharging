using Microsoft.AspNetCore.Mvc.Testing;
using RestSharp;
using SmartCharging;
using SmartCharging.Domain.Command.Commands.Group;
using SmartCharging.Domain.Query.DTOs;
using SmartCharging.Specs.API;

namespace SpecFlowCalculator.Specs.Steps
{
    [Binding]
    public sealed class GroupDefinitions
    {
        private readonly GroupCommandApi groupCommandApi;
        private readonly GroupQueryApi groupQueryApi;
        private ScenarioContext scenarioContext;

        private CreateGroupCommand createGroupCommand;

        public GroupDefinitions(GroupCommandApi groupCommandApi, GroupQueryApi groupQueryApi, ScenarioContext scenarioContext)
        {
            this.groupCommandApi = groupCommandApi;
            this.groupQueryApi = groupQueryApi;
            this.scenarioContext = scenarioContext;

            createGroupCommand = new CreateGroupCommand();            
        }

        [Given(@"a group is created with name is '([^']*)' and capacity in amps is (.*)")]
        public async Task GivenAGroupIsCreatedWithNameIsAndCapacityInAmpsIs(string name, int capacityInAmps)
        {
            var createGroupCommand = new CreateGroupCommand { Name = name, CapacityInAmps = capacityInAmps };

            var response = await groupCommandApi.CreateGroup(createGroupCommand);
            this.scenarioContext.Add("groupId", response.Data);
        }

        [Given(@"the name of group is '([^']*)'")]
        public void GivenTheNameOfGroupIs(string name)
        {
            createGroupCommand.Name = name;
        }

        [Given("the capacity in amps is (.*)")]
        public void GivenTheCapacityInAmpsIs(double capacityInAmps)
        {
            createGroupCommand.CapacityInAmps = capacityInAmps;
        }

        [When("the group is created")]
        public async Task WhenTheGroupIsCreated()
        {
            var response = await groupCommandApi.CreateGroup(createGroupCommand);
            
            if(!response.IsSuccessful)
            {
                this.scenarioContext.Add("GroupCreateFailed", response);
                return;
            }

            var group = await groupQueryApi.GetGroup(response.Data);
            this.scenarioContext.Add("Group", group);
        }
      
        [Then(@"the group is created with name '([^']*)'")]
        public void ThenTheGroupIsCreatedWithName(string name)
        {
            this.scenarioContext.Get<GroupDto>("Group").Name.Should().Be(name);
        }

        [Then("the group is created with capacity in amps (.*)")]
        public void TheGroupIsCreatedWithCapacityInAmps(double capacityInAmps)
        {
            this.scenarioContext.Get<GroupDto>("Group").CapacityInAmps.Should().Be(capacityInAmps);
        }

        [Then(@"the group has some id")]
        public void ThenTheGroupHasSomeId()
        {
            this.scenarioContext.Get<GroupDto>("Group").Id.Should().NotBeEmpty();
        }

        [Then(@"the group creation failed with exception '([^']*)'")]
        public void ThenTheGroupCreationFailedWithException(string invalidCapacityInAmpsException)
        {
            var response = this.scenarioContext.Get<RestResponse>("GroupCreateFailed");

            response.Content.Should().Contain(invalidCapacityInAmpsException);
        }

    }
}