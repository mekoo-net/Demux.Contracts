using MagicOnion;

namespace Meeko.Contracts.Demux.Admin;

/// <summary>LLM 后端凭据管理：颁发 client_id / client_secret 给 LLM 后端实例使用。</summary>
public interface ILlmBackendAdminService : IService<ILlmBackendAdminService>
{
    UnaryResult<IssueLlmBackendResult> IssueAsync(IssueLlmBackendCommand cmd);
}
