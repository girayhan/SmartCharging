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
                this.scenarioContext.Add("ConnectorCreationFailed", response);
                return;
            }

            var connector = await connectorQueryApi.GetConnector(createConnectorCommand.Id, createConnectorCommand.ChargeStationId);
            this.scenarioContext.Add("Connector", connector);
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

    }
}
