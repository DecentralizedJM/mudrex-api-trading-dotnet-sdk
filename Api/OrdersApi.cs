using System.Text.Json;
using Mudrex.TradingSDK.Models;

namespace Mudrex.TradingSDK;

/// <summary>
/// Orders API endpoints
/// </summary>
public class OrdersApi
{
    private readonly MudrexClient _client;

    internal OrdersApi(MudrexClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Create a new order
    /// </summary>
    public async Task<Order> CreateAsync(string assetId, OrderRequest request)
    {
        var path = $"/futures/{assetId}/order";
        var body = JsonSerializer.Serialize(request);
        var resp = await _client.PostAsync(path, body);
        var apiResp = JsonSerializer.Deserialize<ApiResponse<Order>>(resp);
        return apiResp?.Data ?? throw new InvalidOperationException("Invalid response");
    }

    /// <summary>
    /// Create a market order
    /// </summary>
    public async Task<Order> CreateMarketOrderAsync(string assetId, Enums.OrderType side, string quantity, string leverage)
    {
        var request = new OrderRequest
        {
            Leverage = leverage,
            Quantity = quantity,
            OrderType = side,
            TriggerType = Enums.TriggerType.Market
        };
        return await CreateAsync(assetId, request);
    }

    /// <summary>
    /// Create a limit order
    /// </summary>
    public async Task<Order> CreateLimitOrderAsync(string assetId, Enums.OrderType side, string quantity, string price, string leverage)
    {
        var request = new OrderRequest
        {
            Leverage = leverage,
            Quantity = quantity,
            Price = price,
            OrderType = side,
            TriggerType = Enums.TriggerType.Limit
        };
        return await CreateAsync(assetId, request);
    }

    /// <summary>
    /// List all open orders for an asset
    /// </summary>
    public async Task<List<Order>> ListOpenAsync(string assetId)
    {
        var path = $"/futures/{assetId}/orders";
        var resp = await _client.GetAsync(path);
        var apiResp = JsonSerializer.Deserialize<ApiResponse<List<Order>>>(resp);
        return apiResp?.Data ?? throw new InvalidOperationException("Invalid response");
    }

    /// <summary>
    /// Get a specific order
    /// </summary>
    public async Task<Order> GetAsync(string assetId, string orderId)
    {
        var path = $"/futures/{assetId}/order/{orderId}";
        var resp = await _client.GetAsync(path);
        var apiResp = JsonSerializer.Deserialize<ApiResponse<Order>>(resp);
        return apiResp?.Data ?? throw new InvalidOperationException("Invalid response");
    }

    /// <summary>
    /// Get order history with pagination
    /// </summary>
    public async Task<List<Order>> GetHistoryAsync(string assetId, int page = 0, int perPage = 0)
    {
        var path = $"/futures/{assetId}/orders/history";
        var queryParams = new List<string>();

        if (page > 0) queryParams.Add($"page={page}");
        if (perPage > 0) queryParams.Add($"per_page={perPage}");

        if (queryParams.Count > 0)
            path += "?" + string.Join("&", queryParams);

        var resp = await _client.GetAsync(path);
        var apiResp = JsonSerializer.Deserialize<ApiResponse<List<Order>>>(resp);
        return apiResp?.Data ?? throw new InvalidOperationException("Invalid response");
    }

    /// <summary>
    /// Cancel an order
    /// </summary>
    public async Task<bool> CancelAsync(string assetId, string orderId)
    {
        var path = $"/futures/{assetId}/order/{orderId}";
        await _client.DeleteAsync(path, null);
        return true;
    }

    /// <summary>
    /// Amend an existing order
    /// </summary>
    public async Task<Order> AmendAsync(string assetId, string orderId, string price, string quantity)
    {
        var path = $"/futures/{assetId}/order/{orderId}";
        var body = JsonSerializer.Serialize(new { price, quantity });
        var resp = await _client.PatchAsync(path, body);
        var apiResp = JsonSerializer.Deserialize<ApiResponse<Order>>(resp);
        return apiResp?.Data ?? throw new InvalidOperationException("Invalid response");
    }
}
