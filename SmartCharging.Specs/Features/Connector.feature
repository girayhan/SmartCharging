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

@CreateConnectorsThatExceedsCapacityOfGroup
Scenario: Create connectors that exceed capacity of group
	Given a group is created with name is 'group1' and capacity in amps is 30
	Given a charge station is created with name 'charge station 1' and assigned to group just created
	Given a charge station is present and assinged to the connector
	Given max current in amps of connector is 10
	Given id of connector is 1
	When the connector is created
	Given max current in amps of connector is 11
	Given id of connector is 2
	When the connector is created	
	Given max current in amps of connector is 10
	Given id of connector is 3
	When the connector is created
	Then the connector creation failed with exception 'CapacityInAmpsExceededException'

@CreateConnectorsWithSameId
Scenario: Create connectors with same id
	Given a group is created with name is 'group1' and capacity in amps is 30
	Given a charge station is created with name 'charge station 1' and assigned to group just created
	Given a charge station is present and assinged to the connector
	Given max current in amps of connector is 10
	Given id of connector is 1
	When the connector is created
	Given max current in amps of connector is 11
	Given id of connector is 1	
	When the connector is created
	Then the connector creation failed with exception 'ConnectorWithGivenIdAlreadyAlreadyExistsException'

@CreateConnectorsWithoutChargeStation
Scenario: Create connectors without charge station
	Given a group is created with name is 'group1' and capacity in amps is 30	
	Given max current in amps of connector is 10
	Given id of connector is 1
	When the connector is created	
	Then the connector creation failed with exception 'ChargeStationDoesNotExistException'

@RemoveConnector
Scenario: Remove connector
	Given a group is created with name is 'group1' and capacity in amps is 30
	Given a charge station is created with name 'charge station 1' and assigned to group just created
	Given a charge station is present and assinged to the connector
	Given max current in amps of connector is 10
	Given id of connector is 1
	When the connector is created
	When remove the connector that has id is 1 and the created charge station is assigned
	Then the connector does not exist with id 1 and the charge station is assigned

@UpdateConnector
Scenario: UpdateConnector
	Given a group is created with name is 'group1' and capacity in amps is 30
	Given a charge station is created with name 'charge station 1' and assigned to group just created
	Given a charge station is present and assinged to the connector
	Given max current in amps of connector is 10
	Given id of connector is 1
	When the connector is created
	When update the max current in amps of connector with id of 1 and with the created charge station to 5
	Then the connector is updated with id of 1 has max current in amps is 5

