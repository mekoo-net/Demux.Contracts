using MessagePack;

namespace Meeko.Contracts.Demux.LlmBackend;

[MessagePackObject]
public sealed class ReserveQuotaCommand
{
    [Key(0)] public long TokenId { get; set; }
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
    /// <summary>是否流式请求。reserve 时即上报，便于追踪未 commit 的预扣记录。</summary>
    [Key(5)] public bool Streamed { get; set; }

    /// <summary>可选：估算 completion token 数。缺省时 Demux 用 prompt × 默认系数兜底。</summary>
    [Key(6)] public int? EstimatedCompletionTokens { get; set; }
    [Key(7)] public TimeSpan? Ttl { get; set; }
    [Key(8)] public string? ClientIp { get; set; }
    /// <summary>调用协议（anthropic_messages / openai_chat / ...）。reserve 时即上报，便于追踪未 commit 的预扣记录。</summary>
    [Key(9)] public string? ApiType { get; set; }

    /// <summary>渠道键（NATS 队列组）。与 <see cref="ModelName"/>(别名) 共同定位定价行——别名可跨渠道重名。</summary>
    [Key(10)] public string VendorKey { get; set; } = string.Empty;
}
