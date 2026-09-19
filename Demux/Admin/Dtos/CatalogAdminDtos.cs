using System.Text.Json.Serialization;
using Platform.Common.Web;
using Meeko.Contracts.Demux.Common;
using MessagePack;

namespace Meeko.Contracts.Demux.Admin;

[MessagePackObject]
public sealed class VendorDto
{
    // console docs §3：业务实体（非用户）主键统一为 `id`，long → string 序列化避免 JS 精度。
    [Key(0)]
    [JsonConverter(typeof(LongToStringConverter))]
    public long Id { get; set; }

    [Key(1)] public string QueueGroup { get; set; } = string.Empty;
    [Key(2)] public string? VendorSlug { get; set; }
    [Key(3)] public AiVendorStatus Status { get; set; }
    /// <summary>渠道倍率，默认 1。</summary>
    [Key(4)] public decimal PriceMultiplier { get; set; } = 1m;
    [Key(5)] public DateTime CreatedAtUtc { get; set; }
    [Key(6)] public DateTime UpdatedAtUtc { get; set; }
}

[MessagePackObject]
public sealed class UpsertVendorCommand
{
    // null → 创建；有值 → 按 id 更新。
    [Key(0)]
    [JsonConverter(typeof(NullableLongToStringConverter))]
    public long? Id { get; set; }

    [Key(1)] public string Name { get; set; } = string.Empty;
    [Key(2)] public string? VendorSlug { get; set; }
    [Key(3)] public AiVendorStatus Status { get; set; } = AiVendorStatus.Active;
}

[MessagePackObject]
public sealed class ModelMetaAdminDto
{
    [Key(0)]
    [JsonConverter(typeof(LongToStringConverter))]
    public long Id { get; set; }

    [Key(1)] public string ModelName { get; set; } = string.Empty;
    [Key(2)] public string VendorName { get; set; } = string.Empty;
    [Key(3)] public string DisplayName { get; set; } = string.Empty;
    [Key(4)] public AiModelStatus Status { get; set; }
    [Key(5)] public AiModelEndpointType[] EndpointTypes { get; set; } = [];
    [Key(6)] public string[] Tags { get; set; } = [];
    [Key(7)] public string? Description { get; set; }
    [Key(8)] public DateTime CreatedAtUtc { get; set; }
    [Key(9)] public DateTime UpdatedAtUtc { get; set; }
}

[MessagePackObject]
public sealed class UpsertModelMetaCommand
{
    [Key(0)]
    [JsonConverter(typeof(NullableLongToStringConverter))]
    public long? Id { get; set; }

    [Key(1)] public string ModelName { get; set; } = string.Empty;
    [Key(2)] public string VendorName { get; set; } = string.Empty;
    [Key(3)] public string DisplayName { get; set; } = string.Empty;
    [Key(4)] public AiModelStatus Status { get; set; } = AiModelStatus.Active;
    [Key(5)] public AiModelEndpointType[] EndpointTypes { get; set; } = [];
    [Key(6)] public string[] Tags { get; set; } = [];
    [Key(7)] public string? Description { get; set; }
}
