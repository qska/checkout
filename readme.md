# API Documentation
Project supports Swagger, the URL is configured as:
https://localhost:44355/swagger/index.html

# Postman collection
In the application root folder, there is a Postman collection that is helpful when calling the service.
PaymentGateway local.postman_collection.json

# Projects within the solution:
* PaymentGateway - PaymentGateway service API project.
* PaymentGateway.Banking - the integration library to connect with an external banking platform
* PaymentGateway.BankingMock - a simple implementation pretending to be a banking platform, returning random Transaction Ids.
* PaymentGateway.Data - the data layer for the main application. Empty scaffolding, to be replaced with proper persistance code.
* PaymentGateway.Services - the service layer for the main application.
* PaymentGateway.Tests - the catch-all unit test project, to be replaced with separate test project for each project in the solution.

## Next steps:
* Add logging (e.g. log4net)
* Finish off the authentication implementation

## API Usage:

## Login:

Verifies the username and password against a known list of users.
Username: Marcin
Password: abc123
Will return a JWT token for a user with MerchantId 'be9e09d5-2bf9-45b5-b35b-de3d68f249d4'

Username: Intruder
Password: <any>
Will return a JWT token for a user with MerchantId '35122e54-f5f1-4872-9ad1-3d2e5216bc90'

## Payment Processor:
Note - I couldn't quite get the locally issued tokens to work correctly, hence the Bearer token is not currently required.

Executes the payment via the banking provider.
Initial validation occurs, based on a Luhn validation of the card number, and presence of all required fields.
Required fields:
* CardNumber
* MerchantId
* ExpiryMonth
* ExpiryYear
* Amount
* Currency
* Cvv

Sample request:
	POST https://localhost:44355/api/payments 
	{
		CardNumber: "2720992378118650",
		MerchantId: "be9e09d5-2bf9-45b5-b35b-de3d68f249d4",
		ExpiryMonth: "01",
		ExpiryYear: "2020",
		Amount: 10,
		Currency: "GBP",
		Cvv: "000"
	}

This will return the success/failure flag and the Transaction Id that could later on be used to fetch the transaction details using the endpoint below.
For now this part is missing the actual persistence and hydration stages - returns stub data.

## Data retrieval:

We call the endpoint below with a TransactionID parameter that corresponds to the Id returned from the POST endpoint above.
GET https://localhost:44355/api/payments/df8a4caf-d261-4a7a-b7b3-708dd9a103ec

