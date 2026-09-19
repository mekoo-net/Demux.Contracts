using MessagePack;

namespace Meeko.Contracts.Demux.Internal;

/// <summary>
/// Key 按「ChunkType 分组（discriminator → text/done/error → usage → function call）+ 组内名字字典序」排列。
/// 新增字段插入所属组的正确位置并整体重编号，不要追加到末尾。
/// </summary>
[MessagePackObject]
public sealed class InternalStreamChunk
{
    /// <summary>One of the <see cref="InternalChunkType"/> constants.</summary>
    [Key(0)] public byte ChunkType { get; set; }

    /// <summary>Incremental text delta (ChunkType = <see cref="InternalChunkType.Text"/>).</summary>
    [Key(1)] public string? ContentDelta { get; set; }

    /// <summary>Error message (ChunkType = <see cref="InternalChunkType.Error"/>).</summary>
    [Key(2)] public string? ErrorMessage { get; set; }

    /// <summary>Finish reason (ChunkType = <see cref="InternalChunkType.Done"/>), e.g. "stop".</summary>
    [Key(3)] public string? FinishReason { get; set; }

    // ── usage (ChunkType = InternalChunkType.Usage) ───────────────────────────

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

    // ── function call (ChunkType = InternalChunkType.FunctionCall) ────────────

    /// <summary>Function arguments JSON (possibly a partial delta).</summary>
    [Key(9)] public string? FunctionArguments { get; set; }

    /// <summary>Provider-assigned tool call id.</summary>
    [Key(10)] public string? FunctionCallId { get; set; }

    /// <summary>
    /// Function/tool name. Non-null on the first chunk of a call; subsequent chunks
    /// carry only <see cref="FunctionArguments"/> deltas (OpenAI-style incremental tool calls).
    /// </summary>
    [Key(11)] public string? FunctionName { get; set; }
}
