using System.Text.Json.Serialization;
using Platform.Common.Web;
using Meeko.Contracts.Demux.Common;
using MessagePack;

namespace Meeko.Contracts.Demux.Admin;

[MessagePackObject]
public sealed class RedemptionStaffDto
{
    // Staff 是「用户」语义，按 docs §3 保留 `uid`；long → string 序列化。
    [Key(0)]
    [JsonConverter(typeof(LongToStringConverter))]
    public long Uid { get; set; }

    [Key(1)] public string DisplayName { get; set; } = string.Empty;
    [Key(2)] public string? Username { get; set; }
}

[MessagePackObject]
public sealed class RedemptionDto
{
    // 业务实体（兑换码行）主键：JSON 字段 `id`。
    [Key(0)]
    [JsonConverter(typeof(LongToStringConverter))]
    public long Id { get; set; }

    [Key(1)] public string Name { get; set; } = string.Empty;
    [Key(2)] public string KeyMasked { get; set; } = string.Empty;
    [Key(3)] public RedemptionStatus Status { get; set; }
    [Key(4)] public decimal Quota { get; set; }
    [Key(10)] public int MaxRedemptions { get; set; } = 1;
    [Key(11)] public int RedeemedCount { get; set; }

    // 账户用户域主键（userId）：JSON `usedByAccountUid`。
    [Key(5)]
    [JsonConverter(typeof(NullableLongToStringConverter))]
    public long? UsedByAccountUid { get; set; }

    [Key(6)] public DateTime CreatedAtUtc { get; set; }
    [Key(7)] public DateTime? RedeemedAtUtc { get; set; }
    [Key(8)] public DateTime? ExpiresAtUtc { get; set; }
    [Key(9)] public RedemptionStaffDto CreatedBy { get; set; } = new();
}

[MessagePackObject]
public sealed class GenerateRedemptionsCommand
{
    [Key(0)] public string Name { get; set; } = string.Empty;
    [Key(1)] public decimal Quota { get; set; }
    [Key(2)] public int Count { get; set; } = 1;
    [Key(4)] public int MaxRedemptions { get; set; } = 1;
    [Key(3)] public DateTime? ExpiresAtUtc { get; set; }
}

[MessagePackObject]
public sealed class GenerateRedemptionsResult
{
    [Key(0)] public bool Success { get; set; }
    [Key(1)] public string[] PlainKeys { get; set; } = [];

    // 生成的兑换码业务实体主键列表（与 PlainKeys 同序）。
    [Key(2)]
    [JsonPropertyName("ids")]
    public long[] Ids { get; set; } = [];

    [Key(3)] public string? FailureCode { get; set; }
    [Key(4)] public string? FailureMessage { get; set; }
}

[MessagePackObject]
public sealed class RedeemCommand
{
    // 兑换的发起人是 account（用户域），保留 Uid 后缀。
    [Key(0)] public long AccountUid { get; set; }
    [Key(1)] public string Key { get; set; } = string.Empty;
    [Key(2)] public string IdempotencyKey { get; set; } = string.Empty;
}

[MessagePackObject]
public sealed class RedeemResult
{
    [Key(0)] public bool Success { get; set; }
    [Key(1)] public decimal? CreditedAmount { get; set; }

    // 兑换码业务实体的主键（命中的 Redemption.Id）。
    [Key(2)]
    [JsonConverter(typeof(NullableLongToStringConverter))]
    public long? RedemptionId { get; set; }

    [Key(3)] public string? FailureCode { get; set; }
    [Key(4)] public string? FailureMessage { get; set; }
}

[MessagePackObject]
public sealed class ListRedemptionsQuery
{
    [Key(0)] public RedemptionStatus? Status { get; set; }
    [Key(1)] public string? Keyword { get; set; }
    [Key(2)] public int Take { get; set; } = 50;
    [Key(3)] public int Skip { get; set; }
}

[MessagePackObject]
public sealed class ListRedemptionsResult
{
    [Key(0)] public RedemptionDto[] Items { get; set; } = [];
    [Key(1)] public int Total { get; set; }
}

[MessagePackObject]
public sealed class RedemptionStatsDto
{
    [Key(0)] public int Total { get; set; }
    [Key(1)] public int Claimable { get; set; }
    [Key(2)] public int InProgress { get; set; }
    [Key(3)] public int Exhausted { get; set; }
    [Key(4)] public int Expired { get; set; }
}
