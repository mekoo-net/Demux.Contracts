using MessagePack;

namespace Meeko.Contracts.Demux.Admin;

[MessagePackObject]
public sealed class VendorRouteDto
{
    [Key(0)] public long Uid { get; set; }
    [Key(1)] public string RouteKey { get; set; } = string.Empty;
    [Key(2)] public string VendorKey { get; set; } = string.Empty;
    [Key(3)] public string VendorModel { get; set; } = string.Empty;
    /// <summary>有效价来路：<c>compiled</c>（跟价格表）/ <c>manual</c>（手工定价）。</summary>
    [Key(4)] public string PricingMode { get; set; } = "compiled";
    /// <summary>路由倍率覆盖；null = 只用渠道倍率。</summary>
    [Key(5)] public decimal? PriceMultiplier { get; set; }
    [Key(6)] public bool IsPublished { get; set; }
    [Key(7)] public string? Notes { get; set; }
    [Key(8)] public DateTime CreatedAtUtc { get; set; }
    [Key(9)] public DateTime UpdatedAtUtc { get; set; }
}

[MessagePackObject]
public sealed class VendorRouteWritePayload
{
    [Key(0)] public string RouteKey { get; set; } = string.Empty;
    [Key(1)] public string VendorKey { get; set; } = string.Empty;
    [Key(2)] public string VendorModel { get; set; } = string.Empty;
    [Key(3)] public bool? IsPublished { get; set; }
    [Key(4)] public string? Notes { get; set; }
}

[MessagePackObject]
public sealed class VendorRoutePublishedPayload
{
    [Key(0)] public bool IsPublished { get; set; }
}

/// <summary>按渠道 + 上游模型聚合的别名计数（不含明细行）。</summary>
[MessagePackObject]
public sealed class VendorRouteStatsDto
{
    [Key(0)] public string VendorKey { get; set; } = string.Empty;
    [Key(1)] public int Total { get; set; }
    [Key(2)] public Dictionary<string, int> ByVendorModel { get; set; } = new(StringComparer.Ordinal);
}

[MessagePackObject]
public sealed class ModelCarrierEntryDto
{
    [Key(0)] public string ProviderUid { get; set; } = string.Empty;
    [Key(1)] public string ProviderName { get; set; } = string.Empty;
    [Key(2)] public string ModelName { get; set; } = string.Empty;
    [Key(3)] public bool Enabled { get; set; }
    [Key(4)] public int MappingWeight { get; set; } = 100;
}
