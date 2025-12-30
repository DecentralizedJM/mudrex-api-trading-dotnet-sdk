using System.Text.Json.Serialization;

namespace Mudrex.TradingSDK.Models;

/// <summary>
/// Wallet-related models
/// </summary>
public class WalletBalance
{
    [JsonPropertyName("total")]
    public string Total { get; set; } = string.Empty;

    [JsonPropertyName("available")]
    public string Available { get; set; } = string.Empty;

    [JsonPropertyName("rewards")]
    public string Rewards { get; set; } = string.Empty;

    [JsonPropertyName("withdrawable")]
    public string Withdrawable { get; set; } = string.Empty;

    [JsonPropertyName("currency")]
    public string Currency { get; set; } = string.Empty;

    public override string ToString() =>
        $"WalletBalance {{ Total={Total}, Available={Available}, Currency={Currency} }}";
}

public class FuturesBalance
{
    [JsonPropertyName("balance")]
    public string Balance { get; set; } = string.Empty;

    [JsonPropertyName("available_transfer")]
    public string AvailableTransfer { get; set; } = string.Empty;

    [JsonPropertyName("unrealized_pnl")]
    public string UnrealizedPnL { get; set; } = string.Empty;

    [JsonPropertyName("margin_used")]
    public string MarginUsed { get; set; } = string.Empty;

    [JsonPropertyName("currency")]
    public string Currency { get; set; } = string.Empty;

    public override string ToString() =>
        $"FuturesBalance {{ Balance={Balance}, UnrealizedPnL={UnrealizedPnL}, Currency={Currency} }}";
}

public class TransferResult
{
    [JsonPropertyName("transaction_id")]
    public string TransactionId { get; set; } = string.Empty;

    [JsonPropertyName("success")]
    public bool Success { get; set; }

    public override string ToString() =>
        $"TransferResult {{ TransactionId={TransactionId}, Success={Success} }}";
}
