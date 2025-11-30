/* .NET Core 9 Web API 2  */
using System.Text.Json;
using BizCover.Cars.Api;
using BizCover.Cars.Common;

(JsonDocument? node, Exception? error)? document = JDocument.ParseDocument(AppContext.BaseDirectory, "environment.json");

var response  = JDocument.Get<string?>("environment", document?.node);

var environment = response.Item1 ?? Environments.Development;

ApiGenerator.Start(environment, args);

