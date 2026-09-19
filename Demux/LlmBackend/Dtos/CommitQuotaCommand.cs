using MessagePack;

namespace Meeko.Contracts.Demux.LlmBackend;

[MessagePackObject]
public sealed class CommitQuotaCommand
{
    /// <summary>非 null 走"预扣 → 提交"路径；为 null 走"无预扣直接落账"路径。</summary>
    [Key(0)]  public long? ReservationId { get; set; }
    /// <summary>调用方 sk- 访问令牌行 id（<c>demux.access_tokens.id</c>）；PG 账户级直发时为 0。</summary>
    [Key(1)]  public long AccessTokenId { get; set; }
    [Key(2)]  public long AccountUid { get; set; }
    [Key(3)]  public string ModelName { get; set; } = string.Empty;
    /// <summary>实际 token 用量。Demux 用 ratios[model] × 该用量算 actual quota（authoritative）。</summary>
    [Key(4)]  public TokenUsageBreakdown Tokens { get; set; } = new();
    // Key(5) 曾是 UpstreamStatusCode：已并入 Content.StatusCode。保留空位避免 key 重排。
    // Key(6) 曾是 LatencyMs：已并入 Content.LatencyMs。保留空位避免 key 重排。
    // Key(7) 曾是 ChannelIdExternal（认领 worker 实例 id）：无分析价值，已随 usage_logs.channel_id_external 一并移除。保留空位避免 key 重排。
    // Key(8) 曾是 ClientIp：已并入 Content.ClientIp。保留空位避免 key 重排。
    /// <summary>
    /// W3C trace id（32 位十六进制），与 reserve 阶段同一值（同一请求内 trace 不变）。
    /// 充当幂等键：同一 trace 多次 commit 只落账一次；commit 按 ReservationId 更新 reserve 落的同一行。
    /// </summary>
    [Key(9)]  public string TraceId { get; set; } = string.Empty;
    // Key(10) 曾是 ExtraJson（上游额外字段原文透传）：从未被写入，已随 usage_logs.extra 一并移除。保留空位避免 key 重排。
    // Key(11) 曾是 ProviderId：改用 VendorKey（Key 19）外键关联 vendors.queue_group。保留空位避免 key 重排。
    // Key(12) 曾是 ApiType：已并入 Content.Protocol。保留空位避免 key 重排。
    // Key(13) 曾是 Streamed：已并入 Content.Streamed。保留空位避免 key 重排。
    // Key(14) 曾是 ConvId：已并入 Content.ConvId。保留空位避免 key 重排。
    [Key(15)] public long? IamUserUid { get; set; }
    // Key(16) 曾是 ErrorCode：已并入 Content.ErrorCode。保留空位避免 key 重排。
    // Key(17) 曾是 ErrorMessage：已并入 Content.ErrorMessage。保留空位避免 key 重排。
    // Key(18) 曾是重复的 TraceId 槽位：trace 统一走 Key(9)。保留空位避免 key 重排。

    /// <summary>渠道键（NATS 队列组）。与 <see cref="ModelName"/>(别名) 共同定位定价行——别名可跨渠道重名；
    /// 同时作为 <c>usage_logs.vendor_key</c> 落库，外键关联 <c>vendors.queue_group</c>。</summary>
    [Key(19)] public string VendorKey { get; set; } = string.Empty;

    /// <summary>
    /// 调用上下文（协议 / 响应码 / 会话 / 错误 / 耗时 / 来源 IP），整体落到 <c>usage_logs.content</c>。
    /// <see cref="UsageLogContent.StatusCode"/> 是计费成败判定依据。
    /// </summary>
    [Key(20)] public UsageLogContent Content { get; set; } = new();
}
