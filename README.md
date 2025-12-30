# Mudrex .NET Trading SDK

Unofficial .NET SDK for the [Mudrex Futures API](https://mudrex.com/). This SDK provides a type-safe, async-first interface for trading on Mudrex futures markets.

**Maintainer:** [DecentralizedJM](https://github.com/DecentralizedJM)

## Features

- 🔐 **Type-Safe**: Full type definitions for all API requests and responses
- ⚡ **Async/Await**: Modern async-first design using Tasks
- 🛡️ **Error Handling**: Comprehensive exception hierarchy with specific error types
- 📊 **Rate Limiting**: Built-in rate limiter (2 requests/second)
- 🔗 **Zero Dependencies**: Uses only .NET built-in libraries (System.Net.Http, System.Text.Json)
- 📦 **NuGet Package**: Easy installation via NuGet

## Installation

### Via NuGet Package Manager

```bash
dotnet add package Mudrex.TradingSDK
```

Or via Package Manager Console:

```powershell
Install-Package Mudrex.TradingSDK
```

Or edit your `.csproj` file:

```xml
<ItemGroup>
    <PackageReference Include="Mudrex.TradingSDK" Version="1.0.0" />
</ItemGroup>
```

## Quick Start

### Initialize the Client

```csharp
using Mudrex.TradingSDK;

var client = new MudrexClient("your-api-key");
```

### Get Wallet Balance

```csharp
// Spot balance
var spotBalance = await client.Wallet.GetSpotBalanceAsync();
Console.WriteLine($"Spot Balance: {spotBalance.Total}");

// Futures balance
var futuresBalance = await client.Wallet.GetFuturesBalanceAsync();
Console.WriteLine($"Futures Balance: {futuresBalance.Total}");
```

### List Assets

```csharp
var assets = await client.Assets.ListAllAsync();
foreach (var asset in assets)
{
    Console.WriteLine($"Asset: {asset.Id} - Min Order: {asset.MinOrder}");
}
```

### Create an Order

```csharp
var order = await client.Orders.CreateLimitOrderAsync(
    assetId: "BTCUSD",
    side: "BUY",
    quantity: "0.1",
    price: "50000"
);
Console.WriteLine($"Order Created: {order.Id}");
```

### Get Open Positions

```csharp
var positions = await client.Positions.ListOpenAsync();
foreach (var position in positions)
{
    Console.WriteLine($"Position: {position.AssetId} - Size: {position.Size} - Leverage: {position.Leverage}");
}
```

### Close a Position

```csharp
var success = await client.Positions.CloseAsync("position-id");
if (success)
    Console.WriteLine("Position closed");
```

### Set Stop Loss

```csharp
var riskOrder = await client.Positions.SetStopLossAsync("position-id", "48000");
Console.WriteLine($"Stop Loss Set at: {riskOrder.TriggerPrice}");
```

## API Reference

### Wallet API

```csharp
// Get spot wallet balance
var balance = await client.Wallet.GetSpotBalanceAsync();

// Get futures wallet balance
var balance = await client.Wallet.GetFuturesBalanceAsync();

// Transfer between spot and futures
var result = await client.Wallet.TransferAsync("BTC", "1.0", "TO_FUTURES");

// Convenience methods
await client.Wallet.TransferToFuturesAsync("BTC", "1.0");
await client.Wallet.TransferToSpotAsync("BTC", "1.0");
```

### Assets API

```csharp
// List all available assets
var assets = await client.Assets.ListAllAsync();

// Get specific asset details
var asset = await client.Assets.GetAssetAsync("BTCUSD");
```

### Leverage API

```csharp
// Get current leverage
var leverage = await client.Leverage.GetAsync("BTCUSD");

// Set leverage
await client.Leverage.SetAsync("BTCUSD", 10);
```

### Orders API

```csharp
// Create order
var order = await client.Orders.CreateAsync("BTCUSD", new OrderRequest
{
    Side = "BUY",
    Type = "LIMIT",
    Quantity = "0.1",
    Price = "50000"
});

// Convenience methods
await client.Orders.CreateMarketOrderAsync("BTCUSD", "BUY", "0.1");
await client.Orders.CreateLimitOrderAsync("BTCUSD", "BUY", "0.1", "50000");

// List open orders
var orders = await client.Orders.ListOpenAsync();

// Get specific order
var order = await client.Orders.GetAsync("order-id");

// Get order history with pagination
var history = await client.Orders.GetHistoryAsync(page: 1, perPage: 20);

// Cancel order
await client.Orders.CancelAsync("order-id");

// Amend order
await client.Orders.AmendAsync("order-id", new { quantity = "0.2" });
```

### Positions API

```csharp
// List open positions
var positions = await client.Positions.ListOpenAsync();

// Get specific position
var position = await client.Positions.GetAsync("position-id");

// Close position completely
await client.Positions.CloseAsync("position-id");

// Close position partially
await client.Positions.ClosePartialAsync("position-id", "0.5");

// Reverse position
await client.Positions.ReverseAsync("position-id");

// Risk orders (stop loss / take profit)
await client.Positions.SetStopLossAsync("position-id", "48000");
await client.Positions.SetTakeProfitAsync("position-id", "52000");

// Generic risk order setting
var riskOrder = await client.Positions.SetRiskOrderAsync(
    "position-id",
    "STOP_LOSS",
    "48000"
);

// Edit existing risk order
await client.Positions.EditRiskOrderAsync("position-id", "risk-order-id", "47000");

// Get position history with pagination
var history = await client.Positions.GetHistoryAsync(page: 1, perPage: 20);
```

### Fees API

```csharp
// Get fee history with pagination
var fees = await client.Fees.GetHistoryAsync(page: 1, perPage: 20);
foreach (var fee in fees)
{
    Console.WriteLine($"Fee: {fee.Amount} - Asset: {fee.Asset}");
}
```

## Error Handling

The SDK provides specific exception types for different error scenarios:

```csharp
try
{
    var balance = await client.Wallet.GetSpotBalanceAsync();
}
catch (MudrexAuthenticationException ex)
{
    Console.WriteLine($"Authentication failed: {ex.Message}");
}
catch (MudrexRateLimitException ex)
{
    Console.WriteLine($"Rate limited: {ex.Message}");
    // Implement backoff logic
}
catch (MudrexValidationException ex)
{
    Console.WriteLine($"Validation error: {ex.Message}");
}
catch (MudrexNotFoundException ex)
{
    Console.WriteLine($"Resource not found: {ex.Message}");
}
catch (MudrexInsufficientBalanceException ex)
{
    Console.WriteLine($"Insufficient balance: {ex.Message}");
}
catch (MudrexServerException ex)
{
    Console.WriteLine($"Server error: {ex.Message}");
}
catch (MudrexException ex)
{
    Console.WriteLine($"API error: {ex.Message}");
}
```

## Configuration

### Custom Base URL

```csharp
var client = new MudrexClient("your-api-key", "https://custom-api-endpoint.com");
```

### Custom Rate Limit

The SDK uses a default rate limit of 2 requests per second. This is handled automatically by the client.

## Models

All models are in the `Mudrex.TradingSDK.Models` namespace and use C# conventions:
- PascalCase property names
- Nullable reference types enabled
- JSON serialization via `System.Text.Json`

Example models:

```csharp
public class Order
{
    public string Id { get; set; }
    public string AssetId { get; set; }
    public string Side { get; set; }
    public string Type { get; set; }
    public string Quantity { get; set; }
    public string Price { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class Position
{
    public string Id { get; set; }
    public string AssetId { get; set; }
    public string Side { get; set; }
    public string Size { get; set; }
    public string EntryPrice { get; set; }
    public string MarkPrice { get; set; }
    public string Leverage { get; set; }
    public string UnrealizedPnl { get; set; }
    public string UnrealizedPnlPct { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

## Rate Limiting

The SDK includes built-in rate limiting (2 requests/second by default). If you exceed rate limits, a `MudrexRateLimitException` is thrown.

## Contributing

This is an unofficial SDK maintained by the community. Contributions are welcome!

## License

MIT License - see LICENSE file for details

## API Documentation

For complete API documentation, visit: https://mudrex.com/api/docs

## Support

For issues, questions, or suggestions:
- Open an issue on [GitHub](https://github.com/DecentralizedJM/mudrex-dotnet-sdk)
- Check the [API docs](https://mudrex.com/api/docs)
