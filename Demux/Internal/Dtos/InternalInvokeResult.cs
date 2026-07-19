using MessagePack;

namespace Meeko.Contracts.Demux.Internal;

[MessagePackObject]
public sealed class InternalInvokeResult
{
    [Key(0)] public bool Success { get; set; }
    [Key(1)] public int UpstreamStatusCode { get; set; }
    /// <summary>MessagePack-serialised List&lt;StreamingChunkDto&gt; (same as Gateway non-streaming BodyBytes).</summary>
    [Key(2)] public byte[]? BodyBytes { get; set; }
    [Key(3)] public string? ErrorMessage { get; set; }
    [Key(4)] public long PromptTokens { get; set; }
    [Key(5)] public long CompletionTokens { get; set; }
    [Key(6)] public long CachedTokens { get; set; }
}
