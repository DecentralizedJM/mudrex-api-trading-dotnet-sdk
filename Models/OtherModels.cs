using System.Text.Json.Serialization;

namespace Mudrex.TradingSDK.Models;

/// <summary>
/// Leverage model
/// </summary>
public class Leverage
{
    [JsonPropertyName("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [JsonPropertyName("leverage")]
    public string LeverageAmount { get; set; } = string.Empty;

    [JsonPropertyName("margin_type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Enums.MarginType MarginType { get; set; }

    public override string ToString() =>
        $"Leverage {{ AssetId={AssetId}, LeverageAmount={LeverageAmount}, MarginType={MarginType} }}";
}

/// <summary>
/// Fee record model
/// </summary>
public class FeeRecord
{
    [JsonPropertyName("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;

    [JsonPropertyName("fee_amount")]
    public string FeeAmount { get; set; } = string.Empty;

    [JsonPropertyName("fee_rate")]
    public string FeeRate { get; set; } = string.Empty;

    [JsonPropertyName("trade_type")]
    public string TradeType { get; set; } = string.Empty;

    [JsonPropertyName("order_id")]
    public string OrderId { get; set; } = string.Empty;

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    public override string ToString() =>
        $"FeeRecord {{ Symbol={Symbol}, FeeAmount={FeeAmount}, TradeType={TradeType} }}";
}

/// <summary>
/// API Response wrapper
/// </summary>
public class ApiResponse<T>
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("data")]
    public T? Data { get; set; }

    [JsonPropertyName("error")]
    public ApiError? Error { get; set; }
}

/// <summary>
/// API Error model
/// </summary>
public class ApiError
{
    [JsonPropertyName("code")]
    public int Code { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}
