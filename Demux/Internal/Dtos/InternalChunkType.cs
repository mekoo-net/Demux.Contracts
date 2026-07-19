using MessagePack;

namespace Meeko.Contracts.Demux.Internal;

/// <summary>Well-known values for <see cref="InternalStreamChunk.ChunkType"/>.</summary>
public static class InternalChunkType
{
    /// <summary>Incremental text delta. Read <see cref="InternalStreamChunk.ContentDelta"/>.</summary>
    public const byte Text  = 0;
    /// <summary>Stream completed normally. Read <see cref="InternalStreamChunk.FinishReason"/>.</summary>
    public const byte Done  = 1;
    /// <summary>Token usage summary. Read Prompt/Completion/CachedTokens.</summary>
    public const byte Usage = 2;
    /// <summary>Upstream or gateway error. Read <see cref="InternalStreamChunk.ErrorMessage"/>.</summary>
    public const byte Error = 3;

    /// <summary>
    /// Function/tool call (possibly incremental). Read FunctionName / FunctionCallId /
    /// FunctionArguments. A chunk with a non-null FunctionName starts a new call;
    /// chunks with only FunctionArguments append to the current call.
    /// The stream still ends with one <see cref="Done"/> chunk after tool calls.
    /// </summary>
    public const byte FunctionCall = 4;
}
