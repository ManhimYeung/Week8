Feature: SinglePostcodeRequest

In order to get the details of a postcode
As a user
I want to be able to make a request for a signle postcode on postcodes.io

Background: 
	Given I had initialised the Single PostcodeService

@Happy
Scenario: Request for a valid postcode returns valid status code in response header and JSON response body
	When I make a request for the postcode "EC2y 5AS"
	Then the status in the JSON response body should be 200
	And the status header should be 200


@Happy
Scenario: Request for a valid postcode returns a JSON response body with the correct schema
	When I make a request for the postcode "EC2Y 5AS"
	Then the JSON response body should match the JSON schema in "SuccessfulSinglePostcodeResponse.json"