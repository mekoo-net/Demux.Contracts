using MagicOnion;

namespace Meeko.Contracts.Demux.Admin;

/// <summary>AI 用量日志查询：Admin 全量；用户面只能查自己 Account 下的日志（由 BFF 强制）。</summary>
public interface IAiLogQueryService : IService<IAiLogQueryService>
{
    UnaryResult<ListAiLogsResult> ListAsync(ListAiLogsQuery query);
    UnaryResult<AiUsageLogDto?> GetAsync(long id);

    /// <summary>
    /// 批量把账单号（Commit 流水号）解析为调用日志号（UsageLog.Id）。供 BFF 据账单自身流水号
    /// 反查发起它的调用日志，组装账单详情「业务号」；未命中的账单号不出现在结果里。
    /// </summary>
    UnaryResult<LogBillRefDto[]> ResolveLogIdsByBillSerialsAsync(string[] billSerialNos);

    /// <summary>
    /// 批量把账单号（Commit 流水号）解析为调用日志摘要（渠道 / 模型 / 计费 / 用量 / 耗时）。
    /// 供 BFF 把日志侧字段回填进账户自助账单列表；未命中的账单号不出现在结果里。
    /// </summary>
    UnaryResult<LogBillSummaryDto[]> ResolveLogSummariesByBillSerialsAsync(string[] billSerialNos);
    UnaryResult<AiLogStatDto[]> StatHourlyAsync(AiLogStatQuery query);
    UnaryResult<AiLogStatTotalsDto> StatTotalsAsync(AiLogStatQuery query);
    UnaryResult<AiVendorStatDto[]> StatByVendorAsync(AiVendorStatQuery query);

    UnaryResult<ReverseAiLogResult> ReverseAsync(ReverseAiLogCommand cmd);
}
