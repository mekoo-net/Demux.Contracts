using MessagePack;

namespace Meeko.Contracts.Demux.LlmBackend;

[MessagePackObject]
public sealed class ReserveQuotaResult
{
    [Key(0)] public bool Success { get; set; }
    [Key(1)] public long? ReservationId { get; set; }
    [Key(2)] public long? BillingHoldId { get; set; }
    /// <summary>Demux 服务端用倍率算出的预扣金额（authoritative）。</summary>
    [Key(3)] public decimal? EstimatedQuota { get; set; }
    /// <summary>预扣后 Billing 钱包剩余可用余额。</summary>
    [Key(4)] public decimal? WalletAvailableAfter { get; set; }
    [Key(5)] public DateTime? ExpiresAtUtc { get; set; }
    /// <summary>失败原因。常见：ratio_missing / token_limit_exceeded / insufficient_funds / token_disabled / token_expired。</summary>
    [Key(6)] public string? FailureCode { get; set; }
    [Key(7)] public string? FailureMessage { get; set; }
}
