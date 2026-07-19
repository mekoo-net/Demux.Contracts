using MessagePack;

namespace Meeko.Contracts.Demux.LlmBackend;

/// <summary>
/// 令牌解析时下发的账户级速率限制（账户覆盖优先于产品默认）。
/// 所有上限为 <c>0</c> 表示该维度不限；窗口为请求数 / 成功数共用。
/// </summary>
[MessagePackObject]
public sealed class AiTokenRateLimit
{
    /// <summary>统计窗口（秒）；请求数 / 成功数共用。</summary>
    [Key(0)] public int WindowSeconds { get; set; } = 60;
    /// <summary>单个窗口内的最大请求数；<c>0</c> 表示不限。</summary>
    [Key(1)] public int MaxRequests { get; set; }
    /// <summary>单个窗口内的最大成功响应数（成功 = 实际计费成功）；<c>0</c> 表示不限。</summary>
    [Key(2)] public int MaxSuccesses { get; set; }
    /// <summary>同时在途请求数上限；<c>0</c> 表示不限。</summary>
    [Key(3)] public int MaxConcurrency { get; set; }
}
