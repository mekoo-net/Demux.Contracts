using MessagePack;

namespace Meeko.Contracts.Demux.LlmBackend;

/// <summary>
/// 平台级 IP 速率限制策略（与账户/令牌无关，对所有打到网关公开 AI 路径的请求生效）。
/// <see cref="Enabled"/> 为 false 或上限为 <c>0</c> 时对应维度不限；<see cref="Overrides"/>
/// 为针对具体 IP / CIDR 的覆盖，命中且启用时优先于默认策略。
/// </summary>
[MessagePackObject]
public sealed class IpRatePolicyDto
{
    /// <summary>总开关；关闭时网关不做任何 IP 级限制（含覆盖）。</summary>
    [Key(0)] public bool Enabled { get; set; }
    /// <summary>默认策略：请求数统计窗口（秒）。</summary>
    [Key(1)] public int WindowSeconds { get; set; } = 60;
    /// <summary>默认策略：单个 IP 在窗口内的最大请求数；<c>0</c> 表示不限。</summary>
    [Key(2)] public int MaxRequests { get; set; }
    /// <summary>默认策略：单个 IP 的同时在途请求数上限；<c>0</c> 表示不限。</summary>
    [Key(3)] public int MaxConcurrency { get; set; }
    /// <summary>针对具体 IP / CIDR 的覆盖；命中且启用时优先于默认策略。</summary>
    [Key(4)] public IpRateOverrideDto[] Overrides { get; set; } = [];
}

/// <summary>
/// <see cref="ILlmTokenService.SyncIpRatePolicyAsync"/> 的响应：版本号条件同步。
/// <see cref="Policy"/> 为 null 表示与调用方已知版本一致（未变更）；
/// 非 null 时调用方应整体替换本地缓存并记住 <see cref="Version"/>。
/// </summary>
[MessagePackObject]
public sealed class IpRatePolicySyncDto
{
    /// <summary>服务端当前配置版本（由配置内容派生，跨实例稳定）。</summary>
    [Key(0)] public long Version { get; set; }

    /// <summary>完整策略快照；null 表示未变更。</summary>
    [Key(1)] public IpRatePolicyDto? Policy { get; set; }
}

/// <summary>针对单个 IP 或 CIDR 网段的速率限制覆盖。</summary>
[MessagePackObject]
public sealed class IpRateOverrideDto
{
    /// <summary>精确 IP（如 <c>1.2.3.4</c>）或 CIDR 网段（如 <c>10.0.0.0/8</c>）。</summary>
    [Key(0)] public string Ip { get; set; } = string.Empty;
    /// <summary>该覆盖是否启用；关闭时该 IP 回退到默认策略。</summary>
    [Key(1)] public bool Enabled { get; set; }
    /// <summary>请求数统计窗口（秒）。</summary>
    [Key(2)] public int WindowSeconds { get; set; } = 60;
    /// <summary>单个 IP 在窗口内的最大请求数；<c>0</c> 表示不限。</summary>
    [Key(3)] public int MaxRequests { get; set; }
    /// <summary>单个 IP 的同时在途请求数上限；<c>0</c> 表示不限。</summary>
    [Key(4)] public int MaxConcurrency { get; set; }
}
