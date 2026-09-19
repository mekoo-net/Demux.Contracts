using MessagePack;

namespace Meeko.Contracts.Demux.LlmBackend;

[MessagePackObject]
public sealed class ReserveQuotaCommand
{
    /// <summary>调用方 sk- 访问令牌行 id（<c>demux.access_tokens.id</c>）；PG 账户级直发时为 0。</summary>
    [Key(0)] public long AccessTokenId { get; set; }
    [Key(1)] public long AccountUid { get; set; }
    [Key(2)] public string ModelName { get; set; } = string.Empty;
    /// <summary>调用方估算的 prompt token 数；Demux 用 ratios[model] 自己换算成预扣金额。</summary>
    [Key(3)] public int EstimatedPromptTokens { get; set; }
    /// <summary>
    /// W3C trace id（32 位十六进制），由网关自生成（忽略入站 traceparent，服务端可控且唯一）。
    /// 同时充当幂等键：同一 trace 多次调用返回同一 ReservationUid，并随预扣行落库，
    /// 便于凭日志里的 trace id 反查计费记录、定位报错。
    /// </summary>
    [Key(4)] public string TraceId { get; set; } = string.Empty;
    // Key(5) 曾是 Streamed：已并入 Content.Streamed。保留空位避免 key 重排。

    /// <summary>可选：估算 completion token 数。缺省时 Demux 用 prompt × 默认系数兜底。</summary>
    [Key(6)] public int? EstimatedCompletionTokens { get; set; }
    [Key(7)] public TimeSpan? Ttl { get; set; }
    // Key(8) 曾是 ClientIp：已并入 Content.ClientIp。保留空位避免 key 重排。
    // Key(9) 曾是 ApiType：已并入 Content.Protocol。保留空位避免 key 重排。

    /// <summary>渠道键（NATS 队列组）。与 <see cref="ModelName"/>(别名) 共同定位定价行——别名可跨渠道重名；
    /// 同时作为 <c>usage_logs.vendor_key</c> 落库，外键关联 <c>vendors.queue_group</c>。</summary>
    [Key(10)] public string VendorKey { get; set; } = string.Empty;

    /// <summary>调用上下文；reserve 阶段只有 Protocol / Streamed / ClientIp 有值，其余等 commit 补齐。</summary>
    [Key(11)] public UsageLogContent Content { get; set; } = new();
}
