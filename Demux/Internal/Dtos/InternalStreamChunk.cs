using MessagePack;

namespace Meeko.Contracts.Demux.Internal;

[MessagePackObject]
public sealed class InternalStreamChunk
{
    /// <summary>One of the <see cref="InternalChunkType"/> constants.</summary>
    [Key(0)] public byte ChunkType { get; set; }

    /// <summary>Incremental text delta (ChunkType = <see cref="InternalChunkType.Text"/>).</summary>
    [Key(1)] public string? ContentDelta { get; set; }

    /// <summary>Finish reason (ChunkType = <see cref="InternalChunkType.Done"/>), e.g. "stop".</summary>
    [Key(2)] public string? FinishReason { get; set; }

    /// <summary>Error message (ChunkType = <see cref="InternalChunkType.Error"/>).</summary>
    [Key(3)] public string? ErrorMessage { get; set; }

    /// <summary>Prompt token count (ChunkType = <see cref="InternalChunkType.Usage"/>).</summary>
    [Key(4)] public long PromptTokens { get; set; }

    /// <summary>Completion token count (ChunkType = <see cref="InternalChunkType.Usage"/>).</summary>
    [Key(5)] public long CompletionTokens { get; set; }

    /// <summary>Cache-read prompt token count (ChunkType = <see cref="InternalChunkType.Usage"/>).</summary>
    [Key(6)] public long CachedTokens { get; set; }

    /// <summary>
    /// Function/tool name (ChunkType = <see cref="InternalChunkType.FunctionCall"/>).
    /// Non-null on the first chunk of a call; subsequent chunks carry only
    /// <see cref="FunctionArguments"/> deltas (OpenAI-style incremental tool calls).
    /// </summary>
    [Key(7)] public string? FunctionName { get; set; }

    /// <summary>Provider-assigned tool call id (ChunkType = <see cref="InternalChunkType.FunctionCall"/>).</summary>
    [Key(8)] public string? FunctionCallId { get; set; }

    /// <summary>Function arguments JSON (possibly a partial delta; ChunkType = <see cref="InternalChunkType.FunctionCall"/>).</summary>
    [Key(9)] public string? FunctionArguments { get; set; }
}
