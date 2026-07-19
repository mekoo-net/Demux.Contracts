using Meeko.Contracts.Demux.Common;
using MessagePack;

namespace Meeko.Contracts.Demux.LlmBackend;

[MessagePackObject]
public sealed class ModelMetaDto
{
    [Key(0)] public string ModelName { get; set; } = string.Empty;
    [Key(1)] public string VendorName { get; set; } = string.Empty;
    [Key(2)] public string DisplayName { get; set; } = string.Empty;
    [Key(3)] public string? Description { get; set; }
    [Key(4)] public AiModelEndpointType[] EndpointTypes { get; set; } = [];
    [Key(5)] public AiModelStatus Status { get; set; }
}
