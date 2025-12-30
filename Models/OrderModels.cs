using System.Text.Json.Serialization;

namespace Mudrex.TradingSDK.Models;

/// <summary>
/// Order-related models
/// </summary>
public class OrderRequest
{
    [JsonPropertyName("leverage")]
    public string Leverage { get; set; } = string.Empty;

    [JsonPropertyName("quantity")]
    public string Quantity { get; set; } = string.Empty;

    [JsonPropertyName("order_type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Enums.OrderType OrderType { get; set; }

    [JsonPropertyName("trigger_type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Enums.TriggerType TriggerType { get; set; }

    [JsonPropertyName("price")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Price { get; set; }

    [JsonPropertyName("stoploss_price")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? StoplossPrice { get; set; }

    [JsonPropertyName("takeprofit_price")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? TakeprofitPrice { get; set; }

    [JsonPropertyName("reduce_only")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool ReduceOnly { get; set; }
}

public class Order
{
    [JsonPropertyName("order_id")]
    public string OrderId { get; set; } = string.Empty;

    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;

    [JsonPropertyName("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [JsonPropertyName("order_type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Enums.OrderType OrderType { get; set; }

    [JsonPropertyName("trigger_type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Enums.TriggerType TriggerType { get; set; }

    [JsonPropertyName("price")]
    public string Price { get; set; } = string.Empty;

    [JsonPropertyName("quantity")]
    public string Quantity { get; set; } = string.Empty;

    [JsonPropertyName("filled_quantity")]
    public string FilledQuantity { get; set; } = string.Empty;

    [JsonPropertyName("avg_filled_price")]
    public string AvgFilledPrice { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Enums.OrderStatus Status { get; set; }

    [JsonPropertyName("leverage")]
    public string Leverage { get; set; } = string.Empty;

    [JsonPropertyName("stoploss_price")]
    public string? StoplossPrice { get; set; }

    [JsonPropertyName("takeprofit_price")]
    public string? TakeprofitPrice { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [JsonPropertyName("reduce_only")]
    public bool ReduceOnly { get; set; }

    public override string ToString() =>
        $"Order {{ OrderId={OrderId}, Symbol={Symbol}, Status={Status} }}";
}
