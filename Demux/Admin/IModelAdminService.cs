using MagicOnion;
using Meeko.Contracts.Demux.Common;

namespace Meeko.Contracts.Demux.Admin;

public interface IModelAdminService : IService<IModelAdminService>
{
    UnaryResult<ModelMetaAdminDto[]> ListAsync();
    UnaryResult<ModelMetaAdminDto?> GetAsync(long id);
    UnaryResult<AdminCommandResult> UpsertAsync(UpsertModelMetaCommand cmd);
    UnaryResult<AdminCommandResult> DeleteAsync(long id);
}
