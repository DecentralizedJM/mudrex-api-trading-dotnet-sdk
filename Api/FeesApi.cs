using System.Text.Json;
using Mudrex.TradingSDK.Models;

namespace Mudrex.TradingSDK;

/// <summary>
/// Fees API endpoints
/// </summary>
public class FeesApi
{
    private readonly MudrexClient _client;

    internal FeesApi(MudrexClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Get fee history with pagination
    /// </summary>
    public async Task<List<FeeRecord>> GetHistoryAsync(int page = 0, int perPage = 0)
    {
        var path = "/fees/history";
        var queryParams = new List<string>();

        if (page > 0) queryParams.Add($"page={page}");
        if (perPage > 0) queryParams.Add($"per_page={perPage}");

        if (queryParams.Count > 0)
            path += "?" + string.Join("&", queryParams);

        var resp = await _client.GetAsync(path);
        var apiResp = JsonSerializer.Deserialize<ApiResponse<List<FeeRecord>>>(resp);
        return apiResp?.Data ?? throw new InvalidOperationException("Invalid response");
    }
}
