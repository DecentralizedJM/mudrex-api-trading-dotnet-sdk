using System.Text.Json;
using Mudrex.TradingSDK.Models;

namespace Mudrex.TradingSDK;

/// <summary>
/// Wallet API endpoints
/// </summary>
public class WalletApi
{
    private readonly MudrexClient _client;

    internal WalletApi(MudrexClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Get spot wallet balance
    /// </summary>
    public async Task<WalletBalance> GetSpotBalanceAsync()
    {
        var resp = await _client.GetAsync("/wallet/funds");
        var apiResp = JsonSerializer.Deserialize<ApiResponse<WalletBalance>>(resp);
        return apiResp?.Data ?? throw new InvalidOperationException("Invalid response");
    }

    /// <summary>
    /// Get futures wallet balance
    /// </summary>
    public async Task<FuturesBalance> GetFuturesBalanceAsync()
    {
        var resp = await _client.GetAsync("/wallet/balance");
        var apiResp = JsonSerializer.Deserialize<ApiResponse<FuturesBalance>>(resp);
        return apiResp?.Data ?? throw new InvalidOperationException("Invalid response");
    }

    /// <summary>
    /// Transfer funds between wallets
    /// </summary>
    public async Task<TransferResult> TransferAsync(Enums.WalletType fromWallet, Enums.WalletType toWallet, string amount)
    {
        var body = JsonSerializer.Serialize(new
        {
            from_wallet_type = fromWallet.ToString(),
            to_wallet_type = toWallet.ToString(),
            amount
        });

        var resp = await _client.PostAsync("/wallet/transfer", body);
        var apiResp = JsonSerializer.Deserialize<ApiResponse<TransferResult>>(resp);
        return apiResp?.Data ?? throw new InvalidOperationException("Invalid response");
    }

    /// <summary>
    /// Transfer from spot to futures
    /// </summary>
    public async Task<TransferResult> TransferToFuturesAsync(string amount)
    {
        return await TransferAsync(Enums.WalletType.Spot, Enums.WalletType.Futures, amount);
    }

    /// <summary>
    /// Transfer from futures to spot
    /// </summary>
    public async Task<TransferResult> TransferToSpotAsync(string amount)
    {
        return await TransferAsync(Enums.WalletType.Futures, Enums.WalletType.Spot, amount);
    }
}
