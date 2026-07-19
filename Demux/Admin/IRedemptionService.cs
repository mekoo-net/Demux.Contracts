using MagicOnion;
using Meeko.Contracts.Demux.Common;

namespace Meeko.Contracts.Demux.Admin;

/// <summary>兑换码：Admin 生成/查询；用户走 RedeemAsync 兑换并写回 Billing 钱包。</summary>
public interface IRedemptionService : IService<IRedemptionService>
{
    UnaryResult<ListRedemptionsResult> ListAsync(ListRedemptionsQuery query);
    UnaryResult<GenerateRedemptionsResult> GenerateAsync(GenerateRedemptionsCommand cmd);
    UnaryResult<AdminCommandResult> DisableAsync(long uid);
    UnaryResult<RedeemResult> RedeemAsync(RedeemCommand cmd);
}
