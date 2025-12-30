using System.Text.Json;
using Mudrex.TradingSDK.Models;

namespace Mudrex.TradingSDK;

/// <summary>
/// Leverage API endpoints
/// </summary>
public class LeverageApi
{
    private readonly MudrexClient _client;

    internal LeverageApi(MudrexClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Get current leverage for an asset
    /// </summary>
    public async Task<Leverage> GetAsync(string assetId)
    {
        var path = $"/futures/{assetId}/leverage";
        var resp = await _client.GetAsync(path);
        var apiResp = JsonSerializer.Deserialize<ApiResponse<Leverage>>(resp);
        return apiResp?.Data ?? throw new InvalidOperationException("Invalid response");
    }

    /// <summary>
    /// Set leverage and margin type for an asset
    /// </summary>
    public async Task<Leverage> SetAsync(string assetId, string leverage, Enums.MarginType marginType)
    {
        var path = $"/futures/{assetId}/leverage";
        var body = JsonSerializer.Serialize(new
        {
            leverage,
            margin_type = marginType.ToString()
        });

        var resp = await _client.PatchAsync(path, body);
        var apiResp = JsonSerializer.Deserialize<ApiResponse<Leverage>>(resp);
        return apiResp?.Data ?? throw new InvalidOperationException("Invalid response");
    }
}
