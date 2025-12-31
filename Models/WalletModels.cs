using System.Text.Json.Serialization;

namespace Mudrex.TradingSDK.Models;

/// <summary>
/// Wallet-related models
/// </summary>
public class WalletBalance
{
    [JsonPropertyName("total")]
    public string Total { get; set; } = string.Empty;

    [JsonPropertyName("withdrawable")]
    public string Withdrawable { get; set; } = string.Empty;

    [JsonPropertyName("invested")]
    public string Invested { get; set; } = string.Empty;

    [JsonPropertyName("rewards")]
    public string Rewards { get; set; } = string.Empty;

    [JsonPropertyName("coin_investable")]
    public string CoinInvestable { get; set; } = string.Empty;

    [JsonPropertyName("coinset_investable")]
    public string CoinsetInvestable { get; set; } = string.Empty;

    [JsonPropertyName("vault_investable")]
    public string VaultInvestable { get; set; } = string.Empty;

    public override string ToString() =>
        $"WalletBalance {{ Total={Total}, Withdrawable={Withdrawable}, Invested={Invested} }}";
}

public class FuturesBalance
{
    [JsonPropertyName("balance")]
    public string Balance { get; set; } = string.Empty;

    [JsonPropertyName("locked_amount")]
    public string LockedAmount { get; set; } = string.Empty;

    [JsonPropertyName("first_time_user")]
    public bool FirstTimeUser { get; set; }

    public override string ToString() =>
        $"FuturesBalance {{ Balance={Balance}, LockedAmount={LockedAmount}, FirstTimeUser={FirstTimeUser} }}";
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
