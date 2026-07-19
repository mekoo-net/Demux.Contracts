using MessagePack;

namespace Meeko.Contracts.Demux.LlmBackend;

[MessagePackObject]
public sealed class CommitQuotaCommand
{
    /// <summary>非 null 走"预扣 → 提交"路径；为 null 走"无预扣直接落账"路径。</summary>
    [Key(0)]  public long? ReservationId { get; set; }
    [Key(1)]  public long TokenId { get; set; }
    [Key(2)]  public long AccountUid { get; set; }
    [Key(3)]  public string ModelName { get; set; } = string.Empty;
    /// <summary>实际 token 用量。Demux 用 ratios[model] × 该用量算 actual quota（authoritative）。</summary>
    [Key(4)]  public TokenUsageBreakdown Tokens { get; set; } = new();
    [Key(5)]  public int? UpstreamStatusCode { get; set; }
    [Key(6)]  public int LatencyMs { get; set; }
    [Key(7)]  public int? ChannelIdExternal { get; set; }
    [Key(8)]  public string? ClientIp { get; set; }
    /// <summary>
    /// W3C trace id（32 位十六进制），与 reserve 阶段同一值（同一请求内 trace 不变）。
    /// 充当幂等键：同一 trace 多次 commit 只落账一次；commit 按 ReservationId 更新 reserve 落的同一行。
    /// </summary>
    [Key(9)]  public string TraceId { get; set; } = string.Empty;
    /// <summary>上游返回的额外字段（model_returned 等），原文 JSON 透传到 usage_logs.extra。</summary>
    [Key(10)] public string? ExtraJson { get; set; }
    [Key(11)] public int? ProviderId { get; set; }
    [Key(12)] public string? ApiType { get; set; }
    [Key(13)] public bool Streamed { get; set; }
    [Key(14)] public string? ConvId { get; set; }
    [Key(15)] public long? IamUserUid { get; set; }
    [Key(16)] public string? ErrorCode { get; set; }
    [Key(17)] public string? ErrorMessage { get; set; }
    // Key(18) 曾是 TraceId：已下沉到 reserve 阶段（ReserveQuotaCommand.TraceId）随预扣行落库，
    // commit 按 ReservationId 更新同一行即可，无需重复上报。保留空位避免 key 重排。

    /// <summary>渠道键（NATS 队列组）。与 <see cref="ModelName"/>(别名) 共同定位定价行——别名可跨渠道重名。</summary>
    [Key(19)] public string VendorKey { get; set; } = string.Empty;
}
