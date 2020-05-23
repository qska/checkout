# API Documentation
Project supports Swagger, the URL is configured as:
https://localhost:44355/swagger/index.html

# Projects within the solution:
* PaymentGateway - the "proper" PaymentGateway service.
* PaymentGateway.Banking - the integration library to connect with an external banking platform
* PaymentGateway.BankingMock - a simple implementation pretending to be a banking platform, returning random Transaction Ids.
* PaymentGateway.Data - the data layer for the main application. Empty scaffolding, to be replaced with proper persistance code.
* PaymentGateway.Services - the service layer for the main application.
* PaymentGateway.Tests - the catch-all unit test project, to be replaced with separate test project for each project in the solution.

## Next steps:
* Add request validation (FluentValidation is good)
* Implement Luhn algorithm validation for the card number
* Add logging (log4net is simple)

## API Usage:

The minimum payload to get any meaningful output from the system is:

## Payment Processor:
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
		CardNumber: "0123456789123456",
		MerchantId: "1",
		ExpiryMonth: "01",
		ExpiryYear: "01",
		Amount: 10,
		Currency: "GBP",
		Cvv: "000"
	}

This will return the success/failure flag and the Transaction Id that could later on be used to fetch the transaction details using the endpoint below.
For now this part is missing the actual persistence and hydration stages - returns stub data.

## Data retrieval:
GET https://localhost:44355/api/payments/df8a4caf-d261-4a7a-b7b3-708dd9a103ec

Notes: The Async/Await pattern could be rolled out fully in the future.
