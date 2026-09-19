using Meeko.Contracts.Demux.Common;
using MessagePack;

namespace Meeko.Contracts.Demux.LlmBackend;

[MessagePackObject]
public sealed class AccessTokenResolution
{
    [Key(0)] public long TokenId { get; set; }
    [Key(1)] public long AccountUid { get; set; }
    [Key(2)] public string TokenName { get; set; } = string.Empty;
    [Key(3)] public AccessTokenStatus Status { get; set; }
    /// <summary>计费范围：<c>all</c> | <c>per_call</c> | <c>metered</c>。</summary>
    [Key(4)] public string BillingScope { get; set; } = "all";
    /// <summary>允许的渠道 slug；空表示不限制。</summary>
    [Key(5)] public string[] VendorSlugs { get; set; } = [];
    /// <summary>允许的模型（routeKey）白名单；空表示不限制。</summary>
    [Key(6)] public string[] AllowModels { get; set; } = [];
    /// <summary>剩余额度；<b>负数 = 不限额</b>，见 <c>AccessToken.RemainQuota</c>。</summary>
    [Key(7)] public decimal RemainQuota { get; set; }
    [Key(8)] public string[] AllowIpCidrs { get; set; } = [];
    [Key(9)] public DateTime? ExpiresAtUtc { get; set; }
    /// <summary>账户级速率限制（窗口 / 请求数 / 成功数 / 并发数）；账户覆盖优先于产品默认。</summary>
    [Key(10)] public AccessTokenRateLimit Rate { get; set; } = new();
    /// <summary>
    /// 令牌绑定的那一条预设本体。随解析一起下发蹭缓存，热路径不再查采样表。
    /// 未绑定或预设已删则为 null。改这条预设必须显式失效解析缓存。
    /// </summary>
    [Key(11)] public SamplingPresetWire? SamplingPreset { get; set; }
}
