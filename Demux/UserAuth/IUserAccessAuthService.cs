using MagicOnion;
using Meeko.Contracts.Gateway.Auth;

namespace Meeko.Contracts.Demux.UserAuth;

/// <summary>
/// Hosted Demux.Gateway → Meeko.Demux：JWT 验签公钥 + 撤销查询（Keystone 仅在平台内可见）。
/// </summary>
public interface IUserAccessAuthService : IService<IUserAccessAuthService>
{
    /// <summary>ES256 验签公钥；平台内转发 Keystone InternalRpc，网关不走 HTTP JWKS。</summary>
    UnaryResult<JwtSigningKeySet> GetJwtSigningKeysAsync();

    UnaryResult<bool> IsAccessTokenRevokedAsync(string jti);

    /// <summary>Keystone account 域 RBAC（如 inference:invoke）。</summary>
    UnaryResult<bool> HasAccountPermissionAsync(string roleName, string permissionCode);

    /// <summary>Keystone staff 域 RBAC 点查（Demux.Gateway /api/admin/*，仅返回 bool，不下发全量 permissions）。</summary>
    UnaryResult<bool> HasStaffPermissionAsync(string staffRoleName, string permissionCode);
}
