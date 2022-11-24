Feature: Login
As a user of the website,
I want that valid and invalid credentials are supported.

	@login
	@Chrome
	Scenario: Successful Login with Valid Credentials
	Given User is at the Home Page
	And User Navigates to Login Page
	When User enters Email_Address "demouser@microsoft.com" and Password "Pass@word1"
	And User clicks on the LogIn button
	Then User is Redirected to Home Page
	And Users' Email_Address "demouser@microsoft.com" is displayed in navigation bar

	
	@Edge
	Scenario: Unsuccessful Login with Invalid Password
	Given User is at the Home Page
	And User Navigates to Login Page
	When User enters Email_Address "demouser@microsoft.com" and Password "InvalidPassword"
	And User clicks on the LogIn button
	Then Invalid credentials error message is displayed

	@Chrome
	Scenario: Unsuccessful Login with Invalid Email
	Given User is at the Home Page
	And User Navigates to Login Page
	When User enters Email_Address "InvalidEmail@new.com" and Password "Pass@word1"
	And User clicks on the LogIn button
	Then Invalid credentials error message is displayed