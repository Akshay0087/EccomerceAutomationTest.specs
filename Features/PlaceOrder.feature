Feature: PlaceOrder
As a user I want to able to place one or several orders

@Placing_Orders
	@Chrome
	Scenario: As a user i want to order a valid product
	Given User is logs in with Email_Address "demouser@microsoft.com" and Password "Pass@word1"
	And User is on Homepage
	When User searches in Brand "All" and Type "All" 
	And User selects items with quantities
	| Item Name              | Quantity |
	| .NET BLACK & WHITE MUG | 4        |
	And the order is placed
	Then a confirmation message is displayed 
	And the items appear in my orders section 

	@Edge
	Scenario:  As a user i want to order an invalid quantity of product
	Given User is logs in with Email_Address "demouser@microsoft.com" and Password "Pass@word1"
	And User is on Homepage
	When User searches in Brand "All" and Type "All" 
	And User selects items with quantities
	| Item Name              | Quantity |
	| .NET BLACK & WHITE MUG | -4        |
	And the order is placed
	Then a confirmation message is displayed 
	And the items are ordered with a quatity of one
	And the items appear in my orders section

	@Edge
	Scenario: As a user i want to order several valid products
	Given User is logs in with Email_Address "demouser@microsoft.com" and Password "Pass@word1"
	And User is on Homepage
	When User searches in Brand "All" and Type "All" 
	And User selects items with quantities
	| Item Name              | Quantity |
	| .NET BLACK & WHITE MUG | 4        |
	| .NET BLUE SWEATSHIRT   | 5        |
	| PRISM WHITE TSHIRT     | 2        |
	| CUP<T> SHEET           | 3        |
	And the order is placed
	Then a confirmation message is displayed 
	And the items appear in my orders section 

