using System.Text.Json;
using System.Web;
using Mudrex.TradingSDK.Models;

namespace Mudrex.TradingSDK;

/// <summary>
/// Assets API endpoints
/// </summary>
public class AssetsApi
{
    private readonly MudrexClient _client;

    internal AssetsApi(MudrexClient client)
    {
        _client = client;
    }

    /// <summary>
    /// List all tradable assets with pagination
    /// </summary>
    public async Task<List<Asset>> ListAllAsync(int page = 0, int perPage = 0, string sortBy = "", string sortOrder = "")
    {
        var path = "/assets";
        var queryParams = new List<string>();

        if (page > 0) queryParams.Add($"page={page}");
        if (perPage > 0) queryParams.Add($"per_page={perPage}");
        if (!string.IsNullOrEmpty(sortBy)) queryParams.Add($"sort_by={HttpUtility.UrlEncode(sortBy)}");
        if (!string.IsNullOrEmpty(sortOrder)) queryParams.Add($"sort_order={HttpUtility.UrlEncode(sortOrder)}");

        if (queryParams.Count > 0)
            path += "?" + string.Join("&", queryParams);

        var resp = await _client.GetAsync(path);
        var apiResp = JsonSerializer.Deserialize<ApiResponse<AssetListResponse>>(resp);
        return apiResp?.Data?.Assets ?? throw new InvalidOperationException("Invalid response");
    }

    /// <summary>
    /// Get a specific asset by ID
    /// </summary>
    public async Task<Asset> GetAssetAsync(string assetId)
    {
        var path = $"/assets/{assetId}";
        var resp = await _client.GetAsync(path);
        var apiResp = JsonSerializer.Deserialize<ApiResponse<Asset>>(resp);
        return apiResp?.Data ?? throw new InvalidOperationException("Invalid response");
    }
}
