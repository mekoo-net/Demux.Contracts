using MessagePack;

namespace Meeko.Contracts.Demux.Internal;

/// <summary>
/// Key 按「类型分组（result envelope → usage）+ 组内名字字典序」排列。
/// usage 分桶语义与 <see cref="InternalStreamChunk"/> 一致。
/// </summary>
[MessagePackObject]
public sealed class InternalInvokeResult
{
    // ── result envelope ───────────────────────────────────────────────────────

    /// <summary>MessagePack-serialised List&lt;StreamingChunkDto&gt; (same as Gateway non-streaming BodyBytes).</summary>
    [Key(0)] public byte[]? BodyBytes { get; set; }

    [Key(1)] public string? ErrorMessage { get; set; }

    [Key(2)] public bool Success { get; set; }

    [Key(3)] public int UpstreamStatusCode { get; set; }

    // ── usage ─────────────────────────────────────────────────────────────────

    /// <summary>Cache-read prompt token count.</summary>
    [Key(4)] public long CachedReadTokens { get; set; }

    /// <summary>Cache-creation (write) prompt token count.</summary>
    [Key(5)] public long CachedWriteTokens { get; set; }

    /// <summary>Completion token count, GROSS — includes <see cref="ReasoningTokens"/>.</summary>
    [Key(6)] public long CompletionTokens { get; set; }

    /// <summary>Prompt token count, NET — excludes both cache buckets.</summary>
    [Key(7)] public long PromptTokens { get; set; }

    /// <summary>Reasoning / thinking output token count.</summary>
    [Key(8)] public long ReasoningTokens { get; set; }
}
