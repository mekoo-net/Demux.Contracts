using MagicOnion;

namespace Meeko.Contracts.Demux.LlmBackend;

/// <summary>
/// LLM 后端 → DemuxAi：sk- 令牌解析（hot path）。
/// 调用方在 MagicOnion Metadata 头携带 LLM 后端凭据，由 Demux 内部 Filter 校验。
/// </summary>
public interface ILlmTokenService : IService<ILlmTokenService>
{
    /// <summary>用 SHA-256(sk- 原文) 解析令牌；返回 null 表示不存在 / 已撤销 / 已过期。</summary>
    UnaryResult<AiTokenResolution?> ResolveAsync(ResolveAiTokenQuery query);

    /// <summary>批量预热（LLM 后端启动 / 缓存大面积失效时一次拉回）。</summary>
    UnaryResult<AiTokenResolution[]> ResolveBatchAsync(string[] keyHashes);

    /// <summary>
    /// 同步平台级 IP 速率限制策略（版本号条件拉取）：网关带上已知版本号，
    /// 未变更时仅回版本号（<see cref="IpRatePolicySyncDto.Policy"/> 为 null），
    /// 变更时下发完整快照。首次同步传 <c>0</c> 即得全量。
    /// </summary>
    UnaryResult<IpRatePolicySyncDto> SyncIpRatePolicyAsync(long knownVersion);
}
