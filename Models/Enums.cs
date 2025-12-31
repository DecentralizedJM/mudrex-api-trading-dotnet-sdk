using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Mudrex.TradingSDK.Models;

/// <summary>
/// Enums for Mudrex Trading API
/// </summary>
public static class Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OrderType
    {
        [EnumMember(Value = "LONG")]
        Long,
        [EnumMember(Value = "SHORT")]
        Short
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TriggerType
    {
        [EnumMember(Value = "MARKET")]
        Market,
        [EnumMember(Value = "LIMIT")]
        Limit
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MarginType
    {
        [EnumMember(Value = "ISOLATED")]
        Isolated
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OrderStatus
    {
        [EnumMember(Value = "OPEN")]
        Open,
        [EnumMember(Value = "FILLED")]
        Filled,
        [EnumMember(Value = "PARTIALLY_FILLED")]
        PartiallyFilled,
        [EnumMember(Value = "CANCELLED")]
        Cancelled,
        [EnumMember(Value = "EXPIRED")]
        Expired
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PositionStatus
    {
        [EnumMember(Value = "OPEN")]
        Open,
        [EnumMember(Value = "CLOSED")]
        Closed,
        [EnumMember(Value = "LIQUIDATED")]
        Liquidated
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum WalletType
    {
        [EnumMember(Value = "SPOT")]
        Spot,
        [EnumMember(Value = "FUTURES")]
        Futures
    }
}
