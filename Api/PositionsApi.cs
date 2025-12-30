using System.Text.Json;
using Mudrex.TradingSDK.Models;

namespace Mudrex.TradingSDK;

/// <summary>
/// Positions API endpoints
/// </summary>
public class PositionsApi
{
    private readonly MudrexClient _client;

    internal PositionsApi(MudrexClient client)
    {
        _client = client;
    }

    /// <summary>
    /// List all open positions
    /// </summary>
    public async Task<List<Position>> ListOpenAsync()
    {
        var resp = await _client.GetAsync("/positions");
        var apiResp = JsonSerializer.Deserialize<ApiResponse<List<Position>>>(resp);
        return apiResp?.Data ?? throw new InvalidOperationException("Invalid response");
    }

    /// <summary>
    /// Get a specific position
    /// </summary>
    public async Task<Position> GetAsync(string positionId)
    {
        var path = $"/positions/{positionId}";
        var resp = await _client.GetAsync(path);
        var apiResp = JsonSerializer.Deserialize<ApiResponse<Position>>(resp);
        return apiResp?.Data ?? throw new InvalidOperationException("Invalid response");
    }

    /// <summary>
    /// Close a position completely
    /// </summary>
    public async Task<bool> CloseAsync(string positionId)
    {
        var path = $"/positions/{positionId}/close";
        await _client.PostAsync(path, null);
        return true;
    }

    /// <summary>
    /// Close a position partially
    /// </summary>
    public async Task<bool> ClosePartialAsync(string positionId, string quantity)
    {
        var path = $"/positions/{positionId}/close";
        var body = JsonSerializer.Serialize(new { quantity });
        await _client.PostAsync(path, body);
        return true;
    }

    /// <summary>
    /// Reverse a position (LONG to SHORT or vice versa)
    /// </summary>
    public async Task<bool> ReverseAsync(string positionId)
    {
        var path = $"/positions/{positionId}/reverse";
        await _client.PostAsync(path, null);
        return true;
    }

    /// <summary>
    /// Set a risk order (stop loss or take profit)
    /// </summary>
    public async Task<RiskOrder> SetRiskOrderAsync(string positionId, string triggerType, string triggerPrice)
    {
        var path = $"/positions/{positionId}/risk-order";
        var body = JsonSerializer.Serialize(new { trigger_type = triggerType, trigger_price = triggerPrice });
        var resp = await _client.PostAsync(path, body);
        var apiResp = JsonSerializer.Deserialize<ApiResponse<RiskOrder>>(resp);
        return apiResp?.Data ?? throw new InvalidOperationException("Invalid response");
    }

    /// <summary>
    /// Set a stop loss
    /// </summary>
    public async Task<RiskOrder> SetStopLossAsync(string positionId, string triggerPrice)
    {
        return await SetRiskOrderAsync(positionId, "STOP_LOSS", triggerPrice);
    }

    /// <summary>
    /// Set a take profit
    /// </summary>
    public async Task<RiskOrder> SetTakeProfitAsync(string positionId, string triggerPrice)
    {
        return await SetRiskOrderAsync(positionId, "TAKE_PROFIT", triggerPrice);
    }

    /// <summary>
    /// Edit an existing risk order
    /// </summary>
    public async Task<RiskOrder> EditRiskOrderAsync(string positionId, string riskOrderId, string triggerPrice)
    {
        var path = $"/positions/{positionId}/risk-order/{riskOrderId}";
        var body = JsonSerializer.Serialize(new { trigger_price = triggerPrice });
        var resp = await _client.PatchAsync(path, body);
        var apiResp = JsonSerializer.Deserialize<ApiResponse<RiskOrder>>(resp);
        return apiResp?.Data ?? throw new InvalidOperationException("Invalid response");
    }

    /// <summary>
    /// Get position history with pagination
    /// </summary>
    public async Task<List<Position>> GetHistoryAsync(int page = 0, int perPage = 0)
    {
        var path = "/positions/history";
        var queryParams = new List<string>();

        if (page > 0) queryParams.Add($"page={page}");
        if (perPage > 0) queryParams.Add($"per_page={perPage}");

        if (queryParams.Count > 0)
            path += "?" + string.Join("&", queryParams);

        var resp = await _client.GetAsync(path);
        var apiResp = JsonSerializer.Deserialize<ApiResponse<List<Position>>>(resp);
        return apiResp?.Data ?? throw new InvalidOperationException("Invalid response");
    }
}
