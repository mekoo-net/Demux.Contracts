using MessagePack;

namespace Meeko.Contracts.Demux.LlmBackend;

[MessagePackObject]
public sealed class RatioSnapshot
{
    [Key(0)] public int Version { get; set; }
    [Key(1)] public DateTime UpdatedAtUtc { get; set; }
    [Key(2)] public string[] Items { get; set; } = [];
    [Key(3)] public string[] DeletedKeys { get; set; } = [];
    /// <summary>与 <see cref="Items"/> 同源的别名→渠道绑定（active pricing + active alias）。</summary>
    [Key(4)] public RatioRouteDto[] Routes { get; set; } = [];
}
