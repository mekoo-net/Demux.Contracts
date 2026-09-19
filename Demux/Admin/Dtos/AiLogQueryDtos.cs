using Meeko.Contracts.Demux.Common;
using MessagePack;

namespace Meeko.Contracts.Demux.Admin;

[MessagePackObject]
public sealed class LogAccountDto
{
    [Key(0)]
    [System.Text.Json.Serialization.JsonConverter(typeof(Platform.Common.Web.LongToStringConverter))]
    public long Uid { get; set; }

    [Key(1)]
    [System.Text.Json.Serialization.JsonConverter(typeof(Platform.Common.Web.NullableLongToStringConverter))]
    public long? IamUserUid { get; set; }

    [Key(2)] public string? DisplayName { get; set; }
    [Key(3)] public string? Email { get; set; }
    [Key(4)] public string? Phone { get; set; }
}

[MessagePackObject]
public sealed class LogUsageInputDto
{
    [Key(0)] public int Tokens { get; set; }
    [Key(1)] public int CachedReadTokens { get; set; }
    [Key(2)] public int CachedWriteTokens { get; set; }
    [Key(3)] public int AudioTokens { get; set; }
}

[MessagePackObject]
public sealed class LogUsageOutputDto
{
    [Key(0)] public int Tokens { get; set; }
    [Key(1)] public int ReasoningTokens { get; set; }
    [Key(2)] public int AudioTokens { get; set; }
}

[MessagePackObject]
public sealed class LogUsageDto
{
    [Key(0)] public int TotalTokens { get; set; }
    [Key(1)] public LogUsageInputDto Input { get; set; } = new();
    [Key(2)] public LogUsageOutputDto Output { get; set; } = new();
}

[MessagePackObject]
public sealed class LogDimCostDto
{
    [Key(0)] public decimal PerMToken { get; set; }
    [Key(1)] public decimal Amount { get; set; }
}

[MessagePackObject]
public sealed class LogCostInputDto
{
    [Key(0)] public decimal PerMToken { get; set; }
    [Key(1)] public decimal Amount { get; set; }
    [Key(2)] public LogDimCostDto CachedRead { get; set; } = new();
    [Key(3)] public LogDimCostDto CachedWrite { get; set; } = new();
    [Key(4)] public LogDimCostDto Audio { get; set; } = new();
}

[MessagePackObject]
public sealed class LogCostOutputDto
{
    [Key(0)] public decimal PerMToken { get; set; }
    [Key(1)] public decimal Amount { get; set; }
    [Key(2)] public LogDimCostDto Reasoning { get; set; } = new();
    [Key(3)] public LogDimCostDto Audio { get; set; } = new();
}

[MessagePackObject]
public sealed class LogCostDto
{
    [Key(0)] public LogCostInputDto Input { get; set; } = new();
    [Key(1)] public LogCostOutputDto Output { get; set; } = new();
    [Key(2)] public decimal Total { get; set; }
}

[MessagePackObject]
public sealed class LogPerCallUsageDto
{
    /// <summary>
    /// 按次计费的调用同样会消耗 token（如 function call / moderation 仍是 LLM 调用），
    /// 仅记录上游回报的输入 / 输出 / 缓存 token 原始明细，供观测 / 对账，
    /// 不参与计费（计费走 <see cref="LogPerCallCostDto.PricePerCall"/>）。
    /// </summary>
    [Key(0)] public LogUsageInputDto Input { get; set; } = new();
    [Key(1)] public LogUsageOutputDto Output { get; set; } = new();
}

[MessagePackObject]
public sealed class LogPerCallCostDto
{
    [Key(0)] public decimal PricePerCall { get; set; }
    [Key(1)] public decimal CachedPricePerCall { get; set; }
    [Key(2)] public decimal Total { get; set; }
}

[MessagePackObject]
public sealed class LogPerImageUsageTierDto
{
    [Key(0)] public string Size { get; set; } = string.Empty;
    [Key(1)] public string Quality { get; set; } = string.Empty;
}

[MessagePackObject]
public sealed class LogPerImageUsageDto
{
    [Key(0)] public LogPerImageUsageTierDto Tier { get; set; } = new();
    [Key(1)] public int Count { get; set; }
}

[MessagePackObject]
public sealed class LogPerImageCostDto
{
    [Key(0)] public decimal PricePerImage { get; set; }
    [Key(1)] public decimal Total { get; set; }
}

[MessagePackObject]
public sealed class LogPerVideoUsageTierDto
{
    [Key(0)] public string Resolution { get; set; } = string.Empty;
}

[MessagePackObject]
public sealed class LogPerVideoUsageDto
{
    [Key(0)] public LogPerVideoUsageTierDto Tier { get; set; } = new();
    [Key(1)] public decimal Seconds { get; set; }
}

[MessagePackObject]
public sealed class LogPerVideoCostDto
{
    [Key(0)] public decimal PricePerSecond { get; set; }
    [Key(1)] public decimal Total { get; set; }
}

[MessagePackObject]
public sealed class LogPerAudioMinuteUsageDto
{
    [Key(0)] public decimal Minutes { get; set; }
}

[MessagePackObject]
public sealed class LogPerAudioMinuteCostDto
{
    [Key(0)] public decimal PricePerMinute { get; set; }
    [Key(1)] public decimal Total { get; set; }
}

[MessagePackObject]
public sealed class LogPerCharacterUsageDto
{
    [Key(0)] public int Characters { get; set; }
}

[MessagePackObject]
public sealed class LogPerCharacterCostDto
{
    [Key(0)] public decimal PricePerKChar { get; set; }
    [Key(1)] public decimal Total { get; set; }
}

/// <summary>
/// 失败原因，折叠于 <see cref="LogContentDto.Error"/>。
/// 上游 HTTP 码不在此列——它是 <see cref="LogContentDto.StatusCode"/>，同一层级不重复下发。
/// </summary>
[MessagePackObject]
public sealed class LogErrorDto
{
    [Key(0)] public string? Code { get; set; }
    [Key(1)] public string? Message { get; set; }
}

/// <summary>
/// 请求上下文，是 <c>usage_logs.content</c>（jsonb）的下发镜像：协议 / 响应码 / 流式 /
/// 会话 / 耗时 / 来源 IP / 错误。字段与 <c>Meeko.Contracts.Demux.LlmBackend.UsageLogContent</c>
/// 一一对应，只把存储层的 <c>errorCode</c> + <c>errorMessage</c> 收成嵌套的 <see cref="Error"/>。
/// <para>
/// 结算状态（<see cref="AiUsageLogDto.Status"/>）不在此列：它是独立的 <c>status</c> 列，
/// 描述「这笔账算成什么样」，与「请求本身长什么样」是两件事，查询也分别走列索引与 GIN 索引。
/// </para>
/// </summary>
[MessagePackObject]
public sealed class LogContentDto
{
    /// <summary>多轮对话的会话 ID；无会话上下文时为 null。</summary>
    [Key(0)] public string? ConvId { get; set; }

    /// <summary>调用协议（<c>anthropic_messages</c> / <c>openai_chat</c> / <c>gemini</c> ...）。</summary>
    [Key(1)] public string? Protocol { get; set; }

    /// <summary>调用方来源 IP（点分字符串，与 jsonb 原文一致）。</summary>
    [Key(2)] public string? ClientIp { get; set; }

    /// <summary>上游 HTTP 响应码；null 表示未抵达上游。</summary>
    [Key(3)] public int? StatusCode { get; set; }

    [Key(4)] public bool Streamed { get; set; }

    /// <summary>
    /// 调用耗时（流式为首字延迟 TTFT）。原样透传存储值：null 表示未知
    /// （reserve 阶段尚无耗时），失败行仍会带上"失败前耗了多久"。
    /// </summary>
    [Key(5)] public int? LatencyMs { get; set; }

    /// <summary>失败原因；成功行为 null。</summary>
    [Key(6)] public LogErrorDto? Error { get; set; }
}

[MessagePackObject]
public sealed class LogBillReversalDto
{
    [Key(0)] public string? By { get; set; }
    [Key(1)] public string? Code { get; set; }
    [Key(2)] public string? Remark { get; set; }
    [Key(3)] public DateTime AtUtc { get; set; }
}

[MessagePackObject]
public sealed class LogBillDto
{
    [Key(0)] public string? Id { get; set; }
    [Key(1)] public string? Status { get; set; }
    [Key(2)] public LogBillReversalDto? Reversal { get; set; }
}

[MessagePackObject]
public sealed class LogTokenDto
{
    [Key(0)]
    [System.Text.Json.Serialization.JsonConverter(typeof(Platform.Common.Web.LongToStringConverter))]
    public long Id { get; set; }

    [Key(1)] public string Name { get; set; } = string.Empty;
}

/// <summary>
/// Admin 日志行。三块折叠对象各自对应一列 jsonb / 一组快照：
/// <see cref="Content"/> = <c>usage_logs.content</c>（请求上下文），
/// <see cref="Usage"/> = <c>usage_logs.usage</c>（用量明细），
/// <see cref="Cost"/> = 按定价快照算出的费用明细。
/// 顺序：身份 → 主体 → 模型/渠道 → 结算态 → 请求上下文 → 计费 → 时间。
/// </summary>
[MessagePackObject]
public sealed class AiUsageLogDto
{
    [Key(0)]
    [System.Text.Json.Serialization.JsonConverter(typeof(Platform.Common.Web.LongToStringConverter))]
    public long Id { get; set; }

    /// <summary>请求链路 TraceId（幂等键 / 账单 idempotency_key）。</summary>
    [Key(1)] public string? TraceId { get; set; }

    [Key(2)] public LogAccountDto Account { get; set; } = new();
    /// <summary>sk- 令牌快照；PG 直发（无令牌）时为 null。</summary>
    [Key(3)] public LogTokenDto? Token { get; set; }

    [Key(4)] public string ModelName { get; set; } = string.Empty;
    /// <summary>请求命中的供应商（供应商组 / 内部 QueueGroup）。来自定价快照绑定，历史不丢。</summary>
    [Key(5)] public string? VendorKey { get; set; }
    /// <summary>对外公开通道 slug（如 nai / pa），由 <see cref="VendorKey"/> 反查 Vendor.VendorSlug 得到；供前端映射渠道展示名。未配置 slug 时为 null。</summary>
    [Key(6)] public string? VendorPlug { get; set; }
    /// <summary>请求命中的上游真实模型名（vendor_model）。来自别名快照绑定，历史不丢。</summary>
    [Key(7)] public string? VendorModel { get; set; }

    /// <summary>
    /// 结算状态：<c>pending</c> | <c>success</c> | <c>failure</c> | <c>cancelled</c>。
    /// 成败的唯一真源——失败原因见 <see cref="LogContentDto.Error"/>。
    /// </summary>
    [Key(8)] public string Status { get; set; } = "success";
    [Key(9)] public string BillingType { get; set; } = string.Empty;

    /// <summary>请求上下文（<c>usage_logs.content</c> jsonb 镜像）。</summary>
    [Key(10)] public LogContentDto Content { get; set; } = new();
    [Key(11)] public object Usage { get; set; } = new LogUsageDto();
    [Key(12)] public object Cost { get; set; } = new LogCostDto();
    [Key(13)] public LogBillDto? Bill { get; set; }
    [Key(14)] public DateTime CreateAt { get; set; }
}

[MessagePackObject]
public sealed class ListAiLogsQuery
{
    /// <summary>调用日志号（UsageLog.Id）精确匹配。</summary>
    [Key(0)] public long? LogId { get; set; }
    [Key(1)] public long? AccountUid { get; set; }
    [Key(2)] public long? AccessTokenId { get; set; }
    [Key(3)] public long? IamUserUid { get; set; }

    [Key(4)] public string? ModelName { get; set; }
    /// <summary>按渠道过滤；匹配 <c>usage_logs.vendor_key</c>（外键指向 vendors.queue_group）。</summary>
    [Key(5)] public string? VendorKey { get; set; }
    [Key(6)] public string? Protocol { get; set; }
    [Key(7)] public string? ConvId { get; set; }
    /// <summary>TraceId 精确匹配（UsageLog.trace_id）。</summary>
    [Key(8)] public string? TraceId { get; set; }
    /// <summary>账单 UID（Commit 流水号 / bill.id）精确匹配。</summary>
    [Key(9)] public string? BillSerialNo { get; set; }
    /// <summary>邮箱 / 手机 / 昵称模糊匹配；先经 Keystone 解析为 account_uid 集合再筛日志。</summary>
    [Key(10)] public string? ContactKeyword { get; set; }
    [Key(11)] public string? ErrorCode { get; set; }

    [Key(12)] public Meeko.Contracts.Demux.Common.AiUsageStatus? Status { get; set; }
    /// <summary>仅返回失败行（Success=false）；与 Status 可叠加。</summary>
    [Key(13)] public bool? ErrorOnly { get; set; }

    [Key(14)] public DateTime? FromUtc { get; set; }
    [Key(15)] public DateTime? ToUtc { get; set; }
    [Key(16)] public PageQuery Page { get; set; } = new();
}

[MessagePackObject]
public sealed class ListAiLogsResult
{
    [Key(0)] public AiUsageLogDto[] Items { get; set; } = [];
    [Key(1)] public int Total { get; set; }
}

/// <summary>
/// 时间序列分桶聚合行（按查询跨度自适应桶宽：≤48h 按小时，否则按天）。
/// 每个桶含成功 / 失败计数，便于前端绘制成功+失败叠加趋势。
/// </summary>
[MessagePackObject]
public sealed class AiLogStatDto
{
    /// <summary>桶起点（Unix 毫秒 UTC）。桶宽见 <see cref="BucketSeconds"/>。</summary>
    [Key(0)] public DateTime BucketStartUtc { get; set; }
    /// <summary>桶宽（秒）：3600=按小时，86400=按天。前端据此格式化横轴刻度。</summary>
    [Key(1)] public int BucketSeconds { get; set; }
    /// <summary>该桶总调用数（成功 + 失败）。</summary>
    [Key(2)] public int RequestCount { get; set; }
    /// <summary>该桶失败调用数（Status != Success）。</summary>
    [Key(3)] public int ErrorCount { get; set; }
    /// <summary>成功调用累计输入 token。</summary>
    [Key(4)] public long TotalPromptTokens { get; set; }
    /// <summary>成功调用累计输出 token。</summary>
    [Key(5)] public long TotalCompletionTokens { get; set; }
    /// <summary>成功调用累计扣费（元）。</summary>
    [Key(6)] public decimal TotalQuota { get; set; }
}

[MessagePackObject]
public sealed class AiLogStatQuery
{
    [Key(0)] public long? AccountUid { get; set; }
    [Key(1)] public long? AccessTokenId { get; set; }
    /// <summary>IAM 子账户 userId；空 = 不按操作者过滤。</summary>
    [Key(2)] public long? IamUserUid { get; set; }
    [Key(3)] public string? ModelName { get; set; }
    [Key(4)] public DateTime FromUtc { get; set; }
    [Key(5)] public DateTime ToUtc { get; set; }
}

/// <summary>时间窗内调用总量（quota / tokens / 请求数），供 legacy stat 端点单条聚合。</summary>
[MessagePackObject]
public sealed class AiLogStatTotalsDto
{
    [Key(0)] public int RequestCount { get; set; }
    [Key(1)] public long TotalQuota { get; set; }
    [Key(2)] public long TotalTokens { get; set; }
}

/// <summary>按供应商（供应商组）聚合的消费统计行。</summary>
[MessagePackObject]
public sealed class AiVendorStatDto
{
    [Key(0)] public string VendorKey { get; set; } = string.Empty;
    [Key(1)] public int RequestCount { get; set; }
    /// <summary>该供应商下出现过的上游真实模型数（去重）。</summary>
    [Key(2)] public int UpstreamModelCount { get; set; }
    [Key(3)] public long TotalPromptTokens { get; set; }
    [Key(4)] public long TotalCompletionTokens { get; set; }
    [Key(5)] public decimal TotalQuota { get; set; }
}

[MessagePackObject]
public sealed class AiVendorStatQuery
{
    [Key(0)] public long? AccountUid { get; set; }
    [Key(1)] public long? AccessTokenId { get; set; }
    /// <summary>仅统计指定供应商；空 = 全部供应商。</summary>
    [Key(2)] public string? VendorKey { get; set; }
    /// <summary>对外模型名模糊匹配（routeKey）；空 = 全部模型。</summary>
    [Key(3)] public string? ModelName { get; set; }
    [Key(4)] public DateTime FromUtc { get; set; }
    [Key(5)] public DateTime ToUtc { get; set; }
}

/// <summary>按对外模型别名（routeKey）聚合的消费统计行。</summary>
[MessagePackObject]
public sealed class AiModelConsumptionDto
{
    [Key(0)] public string ModelName { get; set; } = string.Empty;
    [Key(1)] public int RequestCount { get; set; }
    /// <summary>该模型出现过的渠道数（去重 vendor_key）。</summary>
    [Key(2)] public int VendorCount { get; set; }
    [Key(3)] public long TotalPromptTokens { get; set; }
    [Key(4)] public long TotalCompletionTokens { get; set; }
    [Key(5)] public decimal TotalQuota { get; set; }
}

/// <summary>
/// 灵活消耗报表。TimeBucket：hour / day / none；Breakdown：vendor / model（默认 vendor）。
/// VendorKeys / ModelNames 空 = 不筛；多个为精确匹配。
/// </summary>
[MessagePackObject]
public sealed class AiConsumptionReportQuery
{
    [Key(0)] public string[] VendorKeys { get; set; } = [];
    [Key(1)] public string[] ModelNames { get; set; } = [];
    [Key(2)] public string TimeBucket { get; set; } = "hour";
    [Key(3)] public string Breakdown { get; set; } = "vendor";
    [Key(4)] public DateTime FromUtc { get; set; }
    [Key(5)] public DateTime ToUtc { get; set; }
}

[MessagePackObject]
public sealed class AiConsumptionCellDto
{
    /// <summary>拆分键：vendorKey / routeKey；Breakdown=none 时为空串。</summary>
    [Key(0)] public string GroupKey { get; set; } = "";
    /// <summary>时间桶起点；TimeBucket=none 时为 Unix epoch（0 毫秒）。</summary>
    [Key(1)] public DateTime BucketStartUtc { get; set; }
    [Key(2)] public int RequestCount { get; set; }
    [Key(3)] public long TotalPromptTokens { get; set; }
    [Key(4)] public long TotalCompletionTokens { get; set; }
    [Key(5)] public decimal TotalQuota { get; set; }
}

[MessagePackObject]
public sealed class AiConsumptionReportDto
{
    [Key(0)] public string TimeBucket { get; set; } = "hour";
    [Key(1)] public string Breakdown { get; set; } = "vendor";
    [Key(2)] public int BucketSeconds { get; set; }
    [Key(3)] public AiConsumptionCellDto[] Cells { get; set; } = [];
}

/// <summary>按对外模型别名（routeKey）聚合的 Top 排行行。</summary>
[MessagePackObject]
public sealed class AiModelStatDto
{
    [Key(0)] public string ModelName { get; set; } = string.Empty;
    [Key(1)] public int RequestCount { get; set; }
    [Key(2)] public decimal TotalQuota { get; set; }
    [Key(3)] public int ErrorCount { get; set; }
}

/// <summary>按模型渠道（vendors.queue_group）聚合的 Top 排行行。</summary>
[MessagePackObject]
public sealed class AiProviderStatDto
{
    /// <summary>渠道键（vendors.queue_group）。</summary>
    [Key(0)] public string VendorKey { get; set; } = string.Empty;
    /// <summary>渠道展示名（VendorSlug 优先，否则 QueueGroup）。</summary>
    [Key(1)] public string? ProviderName { get; set; }
    [Key(2)] public int RequestCount { get; set; }
    [Key(3)] public int ErrorCount { get; set; }
    /// <summary>平均首字延迟（ms）；仅 streamed + success 样本入均值。</summary>
    [Key(4)] public int AvgTokenLatencyMs { get; set; }
}

/// <summary>错误码分布行（仅失败调用，按出现次数降序）。</summary>
[MessagePackObject]
public sealed class AiErrorCodeStatDto
{
    /// <summary>上游 / 网关错误码；缺失时为 <c>unknown</c>。</summary>
    [Key(0)] public string Code { get; set; } = "unknown";
    [Key(1)] public int Count { get; set; }
}

/// <summary>区间内首字延迟（TTFT）汇总：仅 streamed + success 样本入聚合。</summary>
[MessagePackObject]
public sealed class AiLatencyStatDto
{
    /// <summary>平均首字延迟（ms）。无样本时为 0。</summary>
    [Key(0)] public int AvgTokenLatencyMs { get; set; }
    /// <summary>P95 首字延迟（ms）。无样本时为 0。</summary>
    [Key(1)] public int P95TokenLatencyMs { get; set; }
}

[MessagePackObject]
public sealed class ReverseAiLogCommand
{
    [Key(0)] public long LogId { get; set; }
    [Key(1)] public long? OperatorIamUserUid { get; set; }
    [Key(2)] public required string Code { get; set; }
    [Key(3)] public string? Remark { get; set; }
}

[MessagePackObject]
public sealed class ReverseAiLogResult
{
    [Key(0)] public bool Success { get; set; }
    [Key(1)] public string? BillId { get; set; }
    [Key(2)] public string? ReversedBy { get; set; }
    [Key(3)] public string? ReversedCode { get; set; }
    [Key(4)] public DateTime? ReversedAtUtc { get; set; }
    /// <summary>失败原因；成功为 null。</summary>
    [Key(5)] public LogErrorDto? Error { get; set; }

    public static ReverseAiLogResult Ok(
        string billId,
        DateTime reversedAtUtc,
        string? reversedBy,
        string reversedCode) =>
        new()
        {
            Success = true,
            BillId = billId,
            ReversedAtUtc = reversedAtUtc,
            ReversedBy = reversedBy,
            ReversedCode = reversedCode,
        };

    public static ReverseAiLogResult Fail(string code, string message) =>
        new() { Success = false, Error = new LogErrorDto { Code = code, Message = message } };
}

/// <summary>
/// 账单号 → 用量日志号映射行。BFF 据账单自身的流水号（账单号）反查发起它的调用日志，
/// 把日志号组装进账单详情的「业务号」。基于 UsageLog.BillSerialNo（Commit 时单向落库）。
/// </summary>
[MessagePackObject]
public sealed class LogBillRefDto
{
    /// <summary>账单域 Commit 流水号（账单号，= UsageLog.BillSerialNo）。</summary>
    [Key(0)] public required string BillSerialNo { get; set; }

    /// <summary>发起该账单扣费的调用日志号（UsageLog.Id，snowflake）。对外以字符串返回，避免 JS 精度丢失。</summary>
    [Key(1)]
    [System.Text.Json.Serialization.JsonConverter(typeof(Platform.Common.Web.LongToStringConverter))]
    public long LogId { get; set; }
}

/// <summary>
/// 账单号 → 调用日志摘要行。供 BFF 据账单流水号反查发起它的调用日志，把渠道 / 模型 / 计费 /
/// 用量 / 耗时等日志侧字段回填进账户自助账单列表（账单域不感知这些产品域字段）。
/// 基于 UsageLog.BillSerialNo（Commit 时单向落库）；一个账单号对应一条日志，重复取最新。
/// </summary>
[MessagePackObject]
public sealed class LogBillSummaryDto
{
    /// <summary>账单域 Commit 流水号（账单号，= UsageLog.BillSerialNo）。</summary>
    [Key(0)] public required string BillSerialNo { get; set; }

    /// <summary>发起该账单扣费的调用日志号（UsageLog.Id，snowflake）。对外以字符串返回，避免 JS 精度丢失。</summary>
    [Key(1)]
    [System.Text.Json.Serialization.JsonConverter(typeof(Platform.Common.Web.LongToStringConverter))]
    public long LogId { get; set; }

    /// <summary>对外模型别名（routeKey）；快照缺失时为 null。</summary>
    [Key(2)] public string? ModelName { get; set; }

    /// <summary>对外渠道 slug（VendorSlug，如 nai / pa），供前端映射渠道展示名；未配置时为 null。</summary>
    [Key(3)] public string? VendorPlug { get; set; }

    /// <summary>计费类型：per_token / per_call / per_image / per_video / per_audio_minute / per_character。</summary>
    [Key(4)] public string BillingType { get; set; } = "unknown";

    [Key(5)] public int TotalTokens { get; set; }
    [Key(6)] public int InputTokens { get; set; }
    [Key(7)] public int CachedReadTokens { get; set; }
    [Key(8)] public int CachedWriteTokens { get; set; }
    [Key(9)] public int OutputTokens { get; set; }
    [Key(10)] public int ReasoningTokens { get; set; }

    /// <summary>调用耗时（ms）；仅成功调用有值。</summary>
    [Key(11)] public int? LatencyMs { get; set; }
}
