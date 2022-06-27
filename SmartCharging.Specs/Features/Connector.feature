Feature: Connector

A short summary of the feature

@CreateConnector
Scenario: Create connector
	Given a group is created with name is 'group1' and capacity in amps is 30
	Given a charge station is created with name 'charge station 1' and assigned to group just created
	Given a charge station is present and assinged to the connector
	Given max current in amps of connector is 10
	Given id of connector is 1
	When the connector is created
	Then the connector is created with max current in amps is 10
	Then the connector id is 1

@CreateConnectorWithInvalidId
Scenario: Create connector with invalid id
	Given a group is created with name is 'group1' and capacity in amps is 30
	Given a charge station is created with name 'charge station 1' and assigned to group just created
	Given a charge station is present and assinged to the connector
	Given max current in amps of connector is 10
	Given id of connector is -1
	When the connector is created
	Then the connector creation failed with exception 'InvalidConnectorIdException'


