using MessagePack;

namespace Meeko.Contracts.Demux.Admin;

[MessagePackObject]
public sealed class ProviderGroupDto
{
    /// <summary>供应商组（vendor）主键，删除/编辑均以此为准。</summary>
    [Key(0)] public long Id { get; set; }
    [Key(1)] public string QueueGroup { get; set; } = string.Empty;
    [Key(2)] public string? VendorSlug { get; set; }
    [Key(3)] public string Status { get; set; } = "active";
    [Key(4)] public int UpstreamModelCount { get; set; }
    [Key(5)] public string? Notes { get; set; }
    [Key(6)] public DateTime ImportedAtUtc { get; set; }
    [Key(7)] public DateTime UpdatedAtUtc { get; set; }
}

[MessagePackObject]
public sealed class ProviderUpstreamModelDto
{
    /// <summary>入库条目（model_meta）主键，删除/编辑均以此为准，避免在路径里传含斜杠的模型名。</summary>
    [Key(0)] public long Id { get; set; }
    [Key(1)] public string QueueGroup { get; set; } = string.Empty;
    [Key(2)] public string VendorModel { get; set; } = string.Empty;
    [Key(3)] public string? Label { get; set; }
}

[MessagePackObject]
public sealed class DiscoveredUpstreamModelDto
{
    [Key(0)] public string VendorModel { get; set; } = string.Empty;
    [Key(1)] public bool AlreadyImported { get; set; }
}

[MessagePackObject]
public sealed class DiscoveredProviderGroupDto
{
    [Key(0)] public string QueueGroup { get; set; } = string.Empty;
    [Key(1)] public string DisplayName { get; set; } = string.Empty;
    [Key(2)] public bool AlreadyImported { get; set; }
    [Key(3)] public DiscoveredUpstreamModelDto[] Models { get; set; } = [];
}

[MessagePackObject]
public sealed class DiscoverCatalogResultDto
{
    [Key(0)] public DiscoveredProviderGroupDto[] Groups { get; set; } = [];
    [Key(1)] public DateTime DiscoveredAtUtc { get; set; }
}

[MessagePackObject]
public sealed class ImportUpstreamModelPayload
{
    [Key(0)] public string VendorModel { get; set; } = string.Empty;
}

[MessagePackObject]
public sealed class ImportProviderGroupPayload
{
    [Key(0)] public string QueueGroup { get; set; } = string.Empty;
    [Key(1)] public string? VendorSlug { get; set; }
    [Key(2)] public string? Notes { get; set; }
    [Key(3)] public ImportUpstreamModelPayload[] Models { get; set; } = [];
}

[MessagePackObject]
public sealed class ImportProviderGroupResultDto
{
    [Key(0)] public string QueueGroup { get; set; } = string.Empty;
    [Key(1)] public int ImportedModelCount { get; set; }
    [Key(2)] public DateTime ImportedAtUtc { get; set; }
}
