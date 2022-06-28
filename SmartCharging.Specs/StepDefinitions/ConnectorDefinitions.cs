using RestSharp;
using SmartCharging.Domain.Command.Commands.Connector;
using SmartCharging.Domain.Query.DTOs;
using SmartCharging.Specs.API;

namespace SmartCharging.Specs.StepDefinitions
{
    [Binding]
    public class ConnectorDefinitions
    {
        private readonly ConnectorCommandApi connectorCommandApi;
        private readonly ConnectorQueryApi connectorQueryApi;
        private ScenarioContext scenarioContext;

        private CreateConnectorCommand createConnectorCommand;

        public ConnectorDefinitions(ScenarioContext scenarioContext, ConnectorCommandApi connectorCommandApi, ConnectorQueryApi connectorQueryApi)
        {
            this.createConnectorCommand = new CreateConnectorCommand();
            this.scenarioContext = scenarioContext;
            this.connectorCommandApi = connectorCommandApi;
            this.connectorQueryApi = connectorQueryApi;
        }

        [Given(@"a charge station is present and assinged to the connector")]
        public void GivenAChargeStationIsPresentAndAssingedToTheConnector()
        {
            createConnectorCommand.ChargeStationId = this.scenarioContext.Get<Guid>("chargeStationId");
        }

        [Given(@"max current in amps of connector is (.*)")]
        public void GivenMaxCurrentInAmpsOfConnectorIs(int maxCurrentInAmps)
        {
            createConnectorCommand.MaxCurrentInAmps = maxCurrentInAmps;
        }

        [Given(@"id of connector is (.*)")]
        public void GivenIdOfConnectorIs(int id)
        {
            createConnectorCommand.Id = id;
        }

        [When(@"the connector is created")]
        public async Task WhenTheConnectorIsCreated()
        {
            var response = await connectorCommandApi.CreateConnector(createConnectorCommand);

            if (!response.IsSuccessful)
            {
                this.scenarioContext["ConnectorCreationFailed"] = response;
                return;
            }

            var connector = await connectorQueryApi.GetConnector(createConnectorCommand.Id, createConnectorCommand.ChargeStationId);
            this.scenarioContext["Connector"] = connector;
        }

        [Then(@"the connector is created with max current in amps is (.*)")]
        public void ThenTheConnectorIsCreatedWithMaxCurrentInAmpsIs(int maxCurrentInAmps)
        {
            this.scenarioContext.Get<ConnectorDto>("Connector").MaxCurrentInAmps.Should().Be(maxCurrentInAmps);
        }

        [Then(@"the connector id is (.*)")]
        public void ThenTheConnectorIdIs(int id)
        {
            this.scenarioContext.Get<ConnectorDto>("Connector").Id.Should().Be(id);
        }

        [Then(@"the connector creation failed with exception '([^']*)'")]
        public void ThenTheConnectorCreationFailedWithException(string invalidConnectorIdException)
        {
            var response = this.scenarioContext.Get<RestResponse>("ConnectorCreationFailed");

            response.Content.Should().Contain(invalidConnectorIdException);
        }

        [When(@"remove the connector that has id is (.*) and the created charge station is assigned")]
        public async Task WhenRemoveTheConnectorThatHasIdIsAndTheCreatedChargeStationIsAssigned(int id)
        {
            var chargeStationId = this.scenarioContext.Get<Guid>("chargeStationId");
            var removeConnectorCommand = new RemoveConnectorCommand { Id = id, ChargeStationId = chargeStationId };

            await connectorCommandApi.RemoveConnector(removeConnectorCommand);
        }      

        [Then(@"the connector does not exist with id (.*) and the charge station is assigned")]
        public async Task ThenTheConnectorDoesNotExistWithIdAndTheChargeStationIsAssigned(int id)
        {
            var chargeStationId = this.scenarioContext.Get<Guid>("chargeStationId");
            var connector = await connectorQueryApi.GetConnector(id, chargeStationId);
            connector.Should().BeNull();
        }

        [When(@"update the max current in amps of connector with id of (.*) and with the created charge station to (.*)")]
        public async Task WhenUpdateTheMaxCurrentInAmpsOfConnectorWithIdOfAndWithTheCreatedChargeStationTo(int id, int updatedMaxCurrentInAmps)
        {
            var chargeStationId = this.scenarioContext.Get<Guid>("chargeStationId");
            var updateConnectorCommand = new UpdateConnectorCommand { Id = id, ChargeStationId = chargeStationId, MaxCurrentInAmps = updatedMaxCurrentInAmps };

            await connectorCommandApi.UpdateConnector(updateConnectorCommand);
        }

        [Then(@"the connector is updated with id of (.*) has max current in amps is (.*)")]
        public async Task ThenTheConnectorIsUpdatedWithIdOfHasMaxCurrentInAmpsIs(int id, int updatedMaxCurrentInAmps)
        {
            var chargeStationId = this.scenarioContext.Get<Guid>("chargeStationId");
            var connector = await connectorQueryApi.GetConnector(id, chargeStationId);
            connector.MaxCurrentInAmps.Should().Be(updatedMaxCurrentInAmps);
        }
    }
}
