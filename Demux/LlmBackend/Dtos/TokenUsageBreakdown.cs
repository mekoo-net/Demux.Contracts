using MessagePack;

namespace Meeko.Contracts.Demux.LlmBackend;

[MessagePackObject]
public sealed class TokenUsageBreakdown
{
    [Key(0)] public int Prompt { get; set; }
    [Key(1)] public int Completion { get; set; }
    [Key(2)] public int Cached { get; set; }
    [Key(3)] public int Reasoning { get; set; }
    [Key(4)] public int Image { get; set; }
    [Key(5)] public int Audio { get; set; }
}
