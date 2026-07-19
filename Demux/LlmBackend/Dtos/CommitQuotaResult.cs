using MessagePack;

namespace Meeko.Contracts.Demux.LlmBackend;

[MessagePackObject]
public sealed class CommitQuotaResult
{
    [Key(0)] public bool Success { get; set; }
    /// <summary>Demux 用倍率算出的实际扣款金额（authoritative，调用方应据此打日志）。</summary>
    [Key(1)] public decimal? ChargedQuota { get; set; }
    /// <summary>提交后 Billing 钱包剩余可用余额。</summary>
    [Key(2)] public decimal? WalletAvailableAfter { get; set; }
    [Key(3)] public long? AiUsageLogId { get; set; }
    /// <summary>失败原因。常见：reservation_not_found / reservation_expired / ratio_missing / billing_commit_failed。</summary>
    [Key(4)] public string? FailureCode { get; set; }
    [Key(5)] public string? FailureMessage { get; set; }
}
