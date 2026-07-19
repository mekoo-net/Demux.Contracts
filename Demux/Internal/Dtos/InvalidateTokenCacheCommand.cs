using MessagePack;

namespace Meeko.Contracts.Demux.Internal;

/// <summary>
/// Control plane → Gateway token-cache eviction request.
/// </summary>
[MessagePackObject]
public sealed class InvalidateTokenCacheCommand
{
    /// <summary>
    /// Plaintext credential forms a client could present for the changed token — typically the
    /// bare secret and its <c>sk-</c>-prefixed form. The gateway derives each Redis cache key from
    /// these and deletes them, so whichever form clients send gets invalidated.
    /// </summary>
    [Key(0)] public string[] Credentials { get; set; } = [];
}
