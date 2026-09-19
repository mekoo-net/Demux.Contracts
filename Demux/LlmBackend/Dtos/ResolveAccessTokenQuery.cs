using MessagePack;

namespace Meeko.Contracts.Demux.LlmBackend;

[MessagePackObject]
public sealed class ResolveAccessTokenQuery
{
    [Key(0)] public string KeyHash { get; set; } = string.Empty;
    [Key(1)] public string? ClientIp { get; set; }
}
