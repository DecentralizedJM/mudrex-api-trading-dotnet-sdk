using System.Text.Json.Serialization;

namespace Mudrex.TradingSDK.Models;

/// <summary>
/// Position-related models
/// </summary>
public class Position
{
    [JsonPropertyName("position_id")]
    public string PositionId { get; set; } = string.Empty;

    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;

    [JsonPropertyName("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [JsonPropertyName("entry_price")]
    public string EntryPrice { get; set; } = string.Empty;

    [JsonPropertyName("quantity")]
    public string Quantity { get; set; } = string.Empty;

    [JsonPropertyName("side")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Enums.OrderType Side { get; set; }

    [JsonPropertyName("status")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Enums.PositionStatus Status { get; set; }

    [JsonPropertyName("leverage")]
    public string Leverage { get; set; } = string.Empty;

    [JsonPropertyName("unrealized_pnl")]
    public string UnrealizedPnL { get; set; } = string.Empty;

    [JsonPropertyName("realized_pnl")]
    public string RealizedPnL { get; set; } = string.Empty;

    [JsonPropertyName("margin")]
    public string Margin { get; set; } = string.Empty;

    [JsonPropertyName("margin_ratio")]
    public string MarginRatio { get; set; } = string.Empty;

    [JsonPropertyName("mark_price")]
    public string MarkPrice { get; set; } = string.Empty;

    [JsonPropertyName("stop_loss")]
    public string? StopLoss { get; set; }

    [JsonPropertyName("take_profit")]
    public string? TakeProfit { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

    public double GetPnLPercentage()
    {
        // Placeholder - actual implementation would calculate from prices
        return 0.0;
    }

    public override string ToString() =>
        $"Position {{ Symbol={Symbol}, UnrealizedPnL={UnrealizedPnL}, Side={Side} }}";
}

public class RiskOrder
{
    [JsonPropertyName("order_id")]
    public string OrderId { get; set; } = string.Empty;

    [JsonPropertyName("position_id")]
    public string PositionId { get; set; } = string.Empty;

    [JsonPropertyName("order_type")]
    public string OrderType { get; set; } = string.Empty;

    [JsonPropertyName("trigger_price")]
    public string TriggerPrice { get; set; } = string.Empty;

    [JsonPropertyName("execution_price")]
    public string? ExecutionPrice { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }
}
