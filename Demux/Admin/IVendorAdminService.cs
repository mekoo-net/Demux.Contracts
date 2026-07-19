using MagicOnion;
using Meeko.Contracts.Demux.Common;

namespace Meeko.Contracts.Demux.Admin;

public interface IVendorAdminService : IService<IVendorAdminService>
{
    UnaryResult<VendorDto[]> ListAsync();
    UnaryResult<VendorDto?> GetAsync(long id);
    UnaryResult<AdminCommandResult> UpsertAsync(UpsertVendorCommand cmd);
    UnaryResult<AdminCommandResult> DeleteAsync(long id);
}
