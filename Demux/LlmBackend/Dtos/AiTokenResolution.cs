using Meeko.Contracts.Demux.Common;
using MessagePack;

namespace Meeko.Contracts.Demux.LlmBackend;

[MessagePackObject]
public sealed class AiTokenResolution
{
    [Key(0)] public long TokenId { get; set; }
    [Key(1)] public long AccountUid { get; set; }
    [Key(2)] public string TokenName { get; set; } = string.Empty;
    [Key(3)] public AiTokenStatus Status { get; set; }
    [Key(4)] public bool UnlimitedQuota { get; set; }
    [Key(5)] public decimal RemainQuota { get; set; }
    [Key(6)] public string[] ModelLimits { get; set; } = [];
    [Key(7)] public string[] AllowIpCidrs { get; set; } = [];
    [Key(8)] public DateTime? ExpiresAtUtc { get; set; }
    /// <summary>模型计费范围：<c>all</c> | <c>per_call</c> | <c>metered</c>。</summary>
    [Key(9)] public string ModelBillingScope { get; set; } = "all";
    /// <summary>允许的 vendor slug 列表；空表示不限制通道。</summary>
    [Key(10)] public string[] ModelVendorKeys { get; set; } = [];
    /// <summary>账户级速率限制（窗口 / 请求数 / 成功数 / 并发数）；账户覆盖优先于产品默认。</summary>
    [Key(11)] public AiTokenRateLimit Rate { get; set; } = new();
}
