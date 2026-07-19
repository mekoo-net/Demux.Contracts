using MessagePack;

namespace Meeko.Contracts.Demux.LlmBackend;

[MessagePackObject]
public sealed class ModelMetaSnapshot
{
    [Key(0)] public int Version { get; set; }
    [Key(1)] public DateTime UpdatedAtUtc { get; set; }
    [Key(2)] public ModelMetaDto[] Items { get; set; } = [];
    [Key(3)] public string[] DeletedModelNames { get; set; } = [];
}
