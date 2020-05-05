Feature: Registration
	In order to get access to the site
	As a user
	I want to register

@mytag
Scenario: Registration process
	Given I am on page for registration
	And I have entered a Firstname as Hanna and a Secondname as Ostapenko
	And I have entered Address as Lesi Ukrainki,8 and Zipcode as 04115
	When I press Sign up
	Then I see Thank you! on the page
