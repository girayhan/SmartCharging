using RestSharp;
using SmartCharging.Domain.Command.Commands.ChargeStation;
using SmartCharging.Domain.Command.Commands.Group;
using SmartCharging.Domain.Query.DTOs;
using SmartCharging.Specs.API;

namespace SmartCharging.Specs.StepDefinitions
{
    [Binding]
    public class ChargeStationDefinitions
    {
        private readonly ChargeStationCommandApi chargeStationCommandApi;
        private readonly ChargeStationQueryApi chargeStationQueryApi;
        private ScenarioContext scenarioContext;

        private CreateChargeStationCommand createChargeStationCommand;

        public ChargeStationDefinitions(ScenarioContext scenarioContext, ChargeStationCommandApi chargeStationCommandApi, ChargeStationQueryApi chargeStationQueryApi)
        {
            this.createChargeStationCommand = new CreateChargeStationCommand();
            this.scenarioContext = scenarioContext;
            this.chargeStationCommandApi = chargeStationCommandApi;
            this.chargeStationQueryApi = chargeStationQueryApi;
        }

        [Given(@"a charge station is created with name '([^']*)' and assigned to group just created")]
        public async Task GivenAChargeStationIsCreatedWithNameAndAssignedToGroupJustCreated(string name)
        {
            var groupId = this.scenarioContext.Get<Guid>("groupId");
            var createChargeStationCommand = new CreateChargeStationCommand { Name = name, GroupId = groupId };

            var response = await chargeStationCommandApi.CreateChargeStation(createChargeStationCommand);
            this.scenarioContext.Add("chargeStationId", response.Data);
        }

        [Given(@"the name of charge station is '([^']*)'")]
        public void GivenTheNameOfChargeStationIs(string name)
        {
            createChargeStationCommand.Name = name;
        }

        [Given(@"a group is present and charge station is assigned to the group")]
        public void GivenAGroupIsPresentAndChargeStationIsAssignedToTheFirstGroup()
        {
            createChargeStationCommand.GroupId = this.scenarioContext.Get<Guid>("groupId");
        }

        [When(@"the charge station is created")]
        public async Task WhenTheChargeStationIsCreated()
        {
            var response = await chargeStationCommandApi.CreateChargeStation(createChargeStationCommand);

            if (!response.IsSuccessful)
            {
                this.scenarioContext.Add("ChargeStationCreateFailed", response);
                return;
            }

            var chargeStation = await chargeStationQueryApi.GetChargeStation(response.Data);
            this.scenarioContext.Add("ChargeStation", chargeStation);
        }

        [Then(@"the charge station is created with name '([^']*)'")]
        public void ThenTheChargeStationIsCreatedWithName(string name)
        {
            this.scenarioContext.Get<ChargeStationDto>("ChargeStation").Name.Should().Be(name);
        }

        [Then(@"the charge station has some id")]
        public void ThenTheChargeStationHasSomeId()
        {
            this.scenarioContext.Get<ChargeStationDto>("ChargeStation").Id.Should().NotBeEmpty();
        }

        [Then(@"the charge station creation failed with exception '([^']*)'")]
        public void ThenTheChargeStationCreationFailedWithException(string groupDoesNotExistException)
        {
            var response = this.scenarioContext.Get<RestResponse>("ChargeStationCreateFailed");

            response.Content.Should().Contain(groupDoesNotExistException);
        }

    }
}
