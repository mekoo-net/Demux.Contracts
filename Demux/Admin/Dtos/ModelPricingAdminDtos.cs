using MessagePack;

namespace Meeko.Contracts.Demux.Admin;

/// <summary>控制台模型定价（wire 形状对齐 console <c>pricing.types.ts</c>）。</summary>
[MessagePackObject]
public sealed class PricingAdminDto
{
    [Key(0)] public long Id { get; set; }
    [Key(1)] public string ModelId { get; set; } = string.Empty;
    [Key(2)] public string BillingType { get; set; } = "per_token";
    [Key(3)] public string PricingJson { get; set; } = "{}";
    [Key(4)] public string Currency { get; set; } = "CNY";
    [Key(5)] public string TierMultipliersJson { get; set; } = "{}";
    [Key(6)] public DateTime EffectiveFromUtc { get; set; }
    [Key(7)] public DateTime UpdatedAtUtc { get; set; }
}

[MessagePackObject]
public sealed class UpsertModelPricingPayload
{
    [Key(0)] public string? ModelId { get; set; }
    [Key(1)] public string? BillingType { get; set; }
    /// <summary>嵌套 pricing 对象 JSON（按 billingType 变体）。</summary>
    [Key(2)] public string? PricingJson { get; set; }
    [Key(3)] public string? Currency { get; set; }
    [Key(4)] public string? TierMultipliersJson { get; set; }
    [Key(5)] public DateTime? EffectiveFromUtc { get; set; }

    [Key(6)] public string? Reason { get; set; }
}

[MessagePackObject]
public sealed class VendorPricingStatsEntryDto
{
    [Key(0)] public int Configured { get; set; }
    [Key(1)] public int Unconfigured { get; set; }
}

[MessagePackObject]
public sealed class UnconfiguredAliasDto
{
    [Key(0)] public string Alias { get; set; } = string.Empty;
    [Key(1)] public string VendorKey { get; set; } = string.Empty;
    [Key(2)] public string VendorModel { get; set; } = string.Empty;
}
