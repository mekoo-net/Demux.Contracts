using MagicOnion;
using Meeko.Contracts.Demux.Common;

namespace Meeko.Contracts.Demux.Admin;

public interface IRatioAdminService : IService<IRatioAdminService>
{
    UnaryResult<ListRatiosResult> ListAsync(ListRatiosQuery query);
    UnaryResult<AdminCommandResult> UpsertAsync(UpsertRatioCommand cmd);
    UnaryResult<AdminCommandResult> DeleteAsync(DeleteRatioCommand cmd);
    UnaryResult<RatioVersionDto[]> ListVersionsAsync(int take);
}
