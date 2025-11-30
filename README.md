# BizCover

GitHub repository => https://github.com/pdjimritsch/BizCover.git

BizCover Web API

Framework:
BizCover.Cars.Api => Web API service
BizCover.Cars.Common => Utility service
BizCover.Cars.Configuration => Web API service configuration
BizCover.Cars.Data.Factory => Web API data management service for BizCover.Cars.Repository

BizCover.Cars.Logging => Web API service logging (Development, Staging and Production environments)

BizCover.Cars.Models => Web API service model references
BizCover.Cars.Network => Web API network service
BizCover.Cars.Repository => Web API data repository

BizCover.Cars.RulesEngine => Web API business rule provider that deserializes rules from the respective BizCover business rules that are maintained within an JSON file 

BizCover.Cars.Security => provides the required security for the Web API.
BizCover.Cars.Service => renders GetCars, Add and Update car service. Referenced within the Web API Car APIController.

BizCover.Cars.Api.http => Back-end Web API unit testing