using MagicOnion;

namespace Meeko.Contracts.Demux.Internal;

/// <summary>
/// Internal Demux → Gateway invoke service.
/// Bypasses sk- token auth; AccountUid is trusted and passed directly.
/// Used by Meeko.Demux's PG (Playground) endpoint to forward chat requests
/// to LLM backends via NATS without going through AiTokenAuthMiddleware.
///
/// The Gateway must listen on an HTTP/2 port for this service.
/// Configure via gateway.yaml: Server.GrpcPort (default 0 = disabled).
/// </summary>
public interface IInternalLlmInvokeService : IService<IInternalLlmInvokeService>
{
    /// <summary>
    /// Non-streaming invoke: collects the complete response and returns it in one shot.
    /// Suitable for short prompts or function-calling flows.
    /// </summary>
    UnaryResult<InternalInvokeResult> InvokeAsync(InternalInvokeCommand command);

    /// <summary>
    /// Streaming invoke: yields <see cref="InternalStreamChunk"/> items as they arrive.
    /// The stream always ends with exactly one <see cref="InternalChunkType.Done"/> or
    /// one <see cref="InternalChunkType.Error"/> chunk.
    /// </summary>
    Task<ServerStreamingResult<InternalStreamChunk>> InvokeStreamingAsync(InternalInvokeCommand command);

    /// <summary>
    /// Control plane → Gateway: evict cached sk- token resolutions so token edits
    /// (channel/vendor-key, quota, status, expiry) take effect without waiting for the
    /// cache TTL. Because resolutions are cached in <b>shared Redis</b>, a single gateway
    /// node (reached via gwconsul round-robin) deleting the keys invalidates them fleet-wide,
    /// so no broadcast is required. Returns the number of cache entries removed (best-effort).
    /// </summary>
    UnaryResult<int> InvalidateTokenCacheAsync(InvalidateTokenCacheCommand command);
}
