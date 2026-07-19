using MagicOnion;

namespace Meeko.Contracts.Demux.LlmBackend;

/// <summary>
/// LLM 后端 → DemuxAi：按量计量。
/// Demux 内部转译为 Billing 的 Hold/Commit/ReportUsage，并在 ai_usage_logs 落审计行。
/// </summary>
public interface ILlmBillingService : IService<ILlmBillingService>
{
    /// <summary>预扣（流式 / 大额场景；非流式可省略，直接走 <see cref="CommitAsync"/> + ReservationUid=null）。</summary>
    UnaryResult<ReserveQuotaResult> ReserveAsync(ReserveQuotaCommand command);

    /// <summary>提交实际用量。RequestId 幂等：同一键多次调用只落账一次。</summary>
    UnaryResult<CommitQuotaResult> CommitAsync(CommitQuotaCommand command);

    /// <summary>取消预扣（上游失败 / 客户端断开 / 超时）。</summary>
    UnaryResult<bool> CancelAsync(long reservationUid, string reason);
}
