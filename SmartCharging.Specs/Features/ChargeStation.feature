Feature: ChargeStation

A short summary of the feature

@CreateChargeStation
Scenario: Create charge station
	Given a group is created with name is 'group1' and capacity in amps is 30
	And the name of charge station is 'charge station1'
	And a group is present and charge station is assigned to the group
	When the charge station is created
	Then the charge station is created with name 'charge station1'
	Then the charge station has some id

@CreateChargeStationWithoutAGroup
Scenario: Create charge station without a group	
	Given the name of charge station is 'charge station1'	
	When the charge station is created
	Then the charge station creation failed with exception 'GroupDoesNotExistException' 
