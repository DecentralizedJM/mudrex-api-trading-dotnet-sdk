using System.Text.Json.Serialization;

namespace Mudrex.TradingSDK.Models;

/// <summary>
/// Asset-related models
/// </summary>
public class Asset
{
    [JsonPropertyName("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;

    [JsonPropertyName("base_currency")]
    public string BaseCurrency { get; set; } = string.Empty;

    [JsonPropertyName("quote_currency")]
    public string QuoteCurrency { get; set; } = string.Empty;

    [JsonPropertyName("min_quantity")]
    public string MinQuantity { get; set; } = string.Empty;

    [JsonPropertyName("max_quantity")]
    public string MaxQuantity { get; set; } = string.Empty;

    [JsonPropertyName("quantity_step")]
    public string QuantityStep { get; set; } = string.Empty;

    [JsonPropertyName("min_leverage")]
    public string MinLeverage { get; set; } = string.Empty;

    [JsonPropertyName("max_leverage")]
    public string MaxLeverage { get; set; } = string.Empty;

    [JsonPropertyName("maker_fee")]
    public string MakerFee { get; set; } = string.Empty;

    [JsonPropertyName("taker_fee")]
    public string TakerFee { get; set; } = string.Empty;

    [JsonPropertyName("is_active")]
    public bool IsActive { get; set; }

    public override string ToString() =>
        $"Asset {{ Symbol={Symbol}, MaxLeverage={MaxLeverage}x }}";
}

public class AssetListResponse
{
    [JsonPropertyName("assets")]
    public List<Asset> Assets { get; set; } = new();

    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("per_page")]
    public int PerPage { get; set; }

    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("total_pages")]
    public int TotalPages { get; set; }
}
