using MagicOnion;
using Meeko.Contracts.Demux.Common;

namespace Meeko.Contracts.Demux.Admin;

/// <summary>
/// AI 令牌（sk-）管理 RPC：用户自助 + Admin 共用，权限由 BFF 强制（用户只能改自己 Account 下的令牌）。
/// </summary>
public interface IAccessTokenAdminService : IService<IAccessTokenAdminService>
{
    UnaryResult<ListAccessTokensResult> ListAsync(ListAccessTokensQuery query);
    UnaryResult<AccessTokenDto?> GetAsync(long id);
    UnaryResult<IssueAccessTokenResult> IssueAsync(IssueAccessTokenCommand cmd);
    UnaryResult<AdminCommandResult> UpdateAsync(UpdateAccessTokenCommand cmd);
    UnaryResult<AdminCommandResult> AdjustQuotaAsync(AdjustAccessTokenQuotaCommand cmd);
    UnaryResult<AdminCommandResult> DeleteAsync(long id);
}
