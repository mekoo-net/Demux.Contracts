using MagicOnion;
using Meeko.Contracts.Demux.Common;

namespace Meeko.Contracts.Demux.Admin;

/// <summary>
/// AI 令牌（sk-）管理 RPC：用户自助 + Admin 共用，权限由 BFF 强制（用户只能改自己 Account 下的令牌）。
/// </summary>
public interface IAiTokenAdminService : IService<IAiTokenAdminService>
{
    UnaryResult<ListAiTokensResult> ListAsync(ListAiTokensQuery query);
    UnaryResult<AiTokenDto?> GetAsync(long id);
    UnaryResult<IssueAiTokenResult> IssueAsync(IssueAiTokenCommand cmd);
    UnaryResult<AdminCommandResult> UpdateAsync(UpdateAiTokenCommand cmd);
    UnaryResult<AdminCommandResult> AdjustQuotaAsync(AdjustAiTokenQuotaCommand cmd);
    UnaryResult<AdminCommandResult> DeleteAsync(long id);
}
