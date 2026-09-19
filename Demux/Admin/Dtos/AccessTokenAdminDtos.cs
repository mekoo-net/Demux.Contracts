using System.Text.Json.Serialization;
using Platform.Common.Web;
using Meeko.Contracts.Demux.Common;
using MessagePack;

namespace Meeko.Contracts.Demux.Admin;

[MessagePackObject]
public sealed class AccessTokenDto
{
    [Key(0)]
    [JsonConverter(typeof(LongToStringConverter))]
    public long Id { get; set; }

    [Key(1)]
    [JsonConverter(typeof(LongToStringConverter))]
    public long AccountUid { get; set; }

    [Key(2)] public string Name { get; set; } = string.Empty;
    [Key(3)] public string KeyPrefix { get; set; } = string.Empty;
    [Key(4)] public AccessTokenStatus Status { get; set; }
    [Key(5)] public string BillingScope { get; set; } = "all";
    [Key(6)] public string[] VendorSlugs { get; set; } = [];
    [Key(7)] public string[] AllowModels { get; set; } = [];
    /// <summary>剩余额度；<b>负数 = 不限额</b>。</summary>
    [Key(8)] public decimal RemainQuota { get; set; }
    [Key(9)] public decimal UsedQuota { get; set; }
    [Key(10)] public string[] AllowIpCidrs { get; set; } = [];
    [Key(11)] public DateTime? ExpiresAtUtc { get; set; }
    [Key(12)] public DateTime CreatedAtUtc { get; set; }
    [Key(13)] public DateTime? LastUsedAtUtc { get; set; }

    /// <summary>创建该 key 的 IamUser.Uid（子账号归属审计）；历史/无法归属时为 null。</summary>
    [Key(14)]
    [JsonConverter(typeof(NullableLongToStringConverter))]
    public long? IamUid { get; set; }
}

[MessagePackObject]
public sealed class IssueAccessTokenCommand
{
    [Key(0)]
    [JsonConverter(typeof(LongToStringConverter))]
    public long AccountUid { get; set; }

    [Key(1)] public string Name { get; set; } = string.Empty;
    [Key(2)] public string BillingScope { get; set; } = "all";
    [Key(3)] public string[] VendorSlugs { get; set; } = [];
    [Key(4)] public string[] AllowModels { get; set; } = [];
    /// <summary>初始额度；<b>负数 = 不限额</b>。</summary>
    [Key(5)] public decimal InitialQuota { get; set; }
    [Key(6)] public string[] AllowIpCidrs { get; set; } = [];
    [Key(7)] public DateTime? ExpiresAtUtc { get; set; }

    /// <summary>创建者 IamUser.Uid（子账号归属审计）；调用方从 X-Iam-Uid 解析，解析不到时为 null。</summary>
    [Key(8)]
    [JsonConverter(typeof(NullableLongToStringConverter))]
    public long? IamUid { get; set; }
}

[MessagePackObject]
public sealed class IssueAccessTokenResult
{
    [Key(0)] public bool Success { get; set; }

    [Key(1)]
    [JsonConverter(typeof(LongToStringConverter))]
    public long TokenId { get; set; }

    [Key(2)] public string KeyPrefix { get; set; } = string.Empty;
    /// <summary>secret 本体（无 sk- 前缀，仅本次返回）。</summary>
    [Key(3)] public string PlainKey { get; set; } = string.Empty;
    [Key(4)] public string? FailureCode { get; set; }
    [Key(5)] public string? FailureMessage { get; set; }
}

[MessagePackObject]
public sealed class UpdateAccessTokenCommand
{
    [Key(0)]
    [JsonConverter(typeof(LongToStringConverter))]
    public long Id { get; set; }

    [Key(1)] public string Name { get; set; } = string.Empty;
    [Key(2)] public AccessTokenStatus Status { get; set; }
    [Key(3)] public string BillingScope { get; set; } = "all";
    [Key(4)] public string[] VendorSlugs { get; set; } = [];
    [Key(5)] public string[] AllowModels { get; set; } = [];
    /// <summary>剩余额度；<b>负数 = 不限额</b>。</summary>
    [Key(6)] public decimal RemainQuota { get; set; }
    [Key(7)] public string[] AllowIpCidrs { get; set; } = [];
    [Key(8)] public DateTime? ExpiresAtUtc { get; set; }
}

[MessagePackObject]
public sealed class AdjustAccessTokenQuotaCommand
{
    [Key(0)]
    [JsonConverter(typeof(LongToStringConverter))]
    public long Id { get; set; }

    /// <summary>正数=增加；负数=扣减。不限额令牌不走增量，改用 <see cref="SetRemaining"/>。</summary>
    [Key(1)] public decimal Delta { get; set; }
    /// <summary>直接设定剩余额度（忽略 <see cref="Delta"/>）；<b>负数 = 不限额</b>。</summary>
    [Key(2)] public decimal? SetRemaining { get; set; }
    [Key(3)] public string Reason { get; set; } = string.Empty;
    [Key(4)] public string IdempotencyKey { get; set; } = string.Empty;
}

[MessagePackObject]
public sealed class ListAccessTokensQuery
{
    [Key(0)]
    [JsonConverter(typeof(NullableLongToStringConverter))]
    public long? AccountUid { get; set; }

    [Key(1)] public string? Keyword { get; set; }
    [Key(2)] public AccessTokenStatus? Status { get; set; }
    [Key(3)] public PageQuery Page { get; set; } = new();
}

[MessagePackObject]
public sealed class ListAccessTokensResult
{
    [Key(0)] public AccessTokenDto[] Items { get; set; } = [];
    [Key(1)] public int Total { get; set; }
}
