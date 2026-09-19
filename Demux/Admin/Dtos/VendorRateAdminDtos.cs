using MessagePack;

namespace Meeko.Contracts.Demux.Admin;

/// <summary>
/// 控制台模型定价（wire 形状对齐 console <c>rate.ts</c>）。
/// </summary>
[MessagePackObject]
public sealed class RateAdminDto
{
    [Key(0)] public long Id { get; set; }
    [Key(1)] public string ModelId { get; set; } = string.Empty;
    [Key(2)] public string BillingType { get; set; } = "per_token";
    [Key(3)] public string Currency { get; set; } = "CNY";
    [Key(4)] public string RateJson { get; set; } = "{}";
    /// <summary>编译来源基准价行 id；自定义价为 null。</summary>
    [Key(5)] public long? SourcePriceId { get; set; }
    /// <summary>编译时施加的总倍率；自定义价为 null。</summary>
    [Key(6)] public decimal? MultiplierApplied { get; set; }
    [Key(7)] public DateTime EffectiveFromUtc { get; set; }
    [Key(8)] public DateTime UpdatedAtUtc { get; set; }
}

[MessagePackObject]
public sealed class UpsertVendorRatePayload
{
    [Key(0)] public string? ModelId { get; set; }
    [Key(1)] public string? BillingType { get; set; }
    [Key(2)] public string? Currency { get; set; }
    /// <summary>嵌套 rate 对象 JSON（按 billingType 变体）。</summary>
    [Key(3)] public string? RateJson { get; set; }
    [Key(4)] public string? Reason { get; set; }
    [Key(5)] public DateTime? EffectiveFromUtc { get; set; }
}

[MessagePackObject]
public sealed class VendorRateStatsEntryDto
{
    [Key(0)] public int Configured { get; set; }
    [Key(1)] public int Unconfigured { get; set; }
}

[MessagePackObject]
public sealed class UnconfiguredRouteDto
{
    [Key(0)] public long RouteId { get; set; }
    [Key(1)] public string RouteKey { get; set; } = string.Empty;
    [Key(2)] public string VendorKey { get; set; } = string.Empty;
    [Key(3)] public string VendorModel { get; set; } = string.Empty;
}
