Feature: Group
![Calculator](https://specflow.org/wp-content/uploads/2020/09/calculator.png)
Simple calculator for adding **two** numbers

Link to a feature: [Calculator](SmartCharging.Specs/Features/Calculator.feature)
***Further read***: **[Learn more about how to generate Living Documentation](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Generating-Documentation.html)**

@CreateGroup
Scenario: Create group
	Given the name of group is 'group1'
	And the capacity in amps is 30
	When the group is created
	Then the group is created with name 'group1'
	Then the group is created with capacity in amps 30
	Then the group has some id

@CreateGroupWithInvalidCapacity
Scenario: Create group with invalid capacity
	Given the name of group is 'group2'
	And the capacity in amps is -10
	When the group is created
	Then the group creation failed with exception 'InvalidCapacityInAmpsException'