using MessagePack;

namespace Meeko.Contracts.Demux.Admin;

[MessagePackObject]
public sealed class ModelRouteDto
{
    [Key(0)] public long Uid { get; set; }
    [Key(1)] public string Alias { get; set; } = string.Empty;
    [Key(2)] public string VendorKey { get; set; } = string.Empty;
    [Key(3)] public string VendorModel { get; set; } = string.Empty;
    [Key(4)] public bool IsPublished { get; set; }
    [Key(5)] public string? Notes { get; set; }
    [Key(6)] public DateTime CreatedAtUtc { get; set; }
    [Key(7)] public DateTime UpdatedAtUtc { get; set; }
}

[MessagePackObject]
public sealed class ModelRouteWritePayload
{
    [Key(0)] public string Alias { get; set; } = string.Empty;
    [Key(1)] public string VendorKey { get; set; } = string.Empty;
    [Key(2)] public string VendorModel { get; set; } = string.Empty;
    [Key(3)] public bool? IsPublished { get; set; }
    [Key(4)] public string? Notes { get; set; }
}

[MessagePackObject]
public sealed class ModelRoutePublishedPayload
{
    [Key(0)] public bool IsPublished { get; set; }
}

/// <summary>按渠道 + 上游模型聚合的别名计数（不含明细行）。</summary>
[MessagePackObject]
public sealed class ModelRouteStatsDto
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
    [Key(3)] public int MappingWeight { get; set; } = 100;
    [Key(4)] public bool Enabled { get; set; }
}
