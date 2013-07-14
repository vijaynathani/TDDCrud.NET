Feature: Check CRUD Operations
	Ensure Create, Retrive, Update and Delete 
	work for customers.

Scenario: Create a new Customer
	Given A non-existant Customer Number 500
	When I try to create a new Customer with number 500, name 'Vijay', address 'Mumbai' and Mobile 999888
	Then the Customer should be added successfully
	And I should be able to View the same customer

@DeveloperTestedSeparately
Scenario: Check for Invalid Values
	When I try to add or change Customer values
	Then Empty name, address and mobile should be treated as invalid
	And  Mobile number having non-digit characters should also be treated as invalid.
	