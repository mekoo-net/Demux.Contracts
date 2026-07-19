using System.Text.Json.Serialization;
using Platform.Common.Web;
using Meeko.Contracts.Demux.Common;
using MessagePack;

namespace Meeko.Contracts.Demux.Admin;

[MessagePackObject]
public sealed class AiTokenDto
{
    [Key(0)]
    [JsonConverter(typeof(LongToStringConverter))]
    public long Id { get; set; }

    [Key(1)]
    [JsonConverter(typeof(LongToStringConverter))]
    public long AccountUid { get; set; }

    [Key(2)]  public string Name { get; set; } = string.Empty;
    [Key(3)]  public string KeyPrefix { get; set; } = string.Empty;
    [Key(4)]  public AiTokenStatus Status { get; set; }
    [Key(5)]  public bool UnlimitedQuota { get; set; }
    [Key(6)]  public decimal RemainQuota { get; set; }
    [Key(7)]  public decimal UsedQuota { get; set; }
    [Key(8)]  public string[] ModelLimits { get; set; } = [];
    [Key(9)]  public string[] AllowIpCidrs { get; set; } = [];
    [Key(10)] public DateTime? ExpiresAtUtc { get; set; }
    [Key(11)] public DateTime CreatedAtUtc { get; set; }
    [Key(12)] public DateTime? LastUsedAtUtc { get; set; }
    [Key(13)] public string ModelBillingScope { get; set; } = "all";
    [Key(14)] public string[] ModelVendorKeys { get; set; } = [];

    /// <summary>创建该 key 的 IamUser.Uid（子账号归属审计）；历史/无法归属时为 null。</summary>
    [Key(15)]
    [JsonConverter(typeof(NullableLongToStringConverter))]
    public long? IamUid { get; set; }
}

[MessagePackObject]
public sealed class IssueAiTokenCommand
{
    [Key(0)]
    [JsonConverter(typeof(LongToStringConverter))]
    public long AccountUid { get; set; }

    [Key(1)] public string Name { get; set; } = string.Empty;
    [Key(2)] public bool UnlimitedQuota { get; set; }
    [Key(3)] public decimal InitialQuota { get; set; }
    [Key(4)] public string[] ModelLimits { get; set; } = [];
    [Key(5)] public string[] AllowIpCidrs { get; set; } = [];
    [Key(6)] public DateTime? ExpiresAtUtc { get; set; }
    [Key(7)] public string ModelBillingScope { get; set; } = "all";
    [Key(8)] public string[] ModelVendorKeys { get; set; } = [];

    /// <summary>创建者 IamUser.Uid（子账号归属审计）；调用方从 X-Iam-Uid 解析，解析不到时为 null。</summary>
    [Key(9)]
    [JsonConverter(typeof(NullableLongToStringConverter))]
    public long? IamUid { get; set; }
}

[MessagePackObject]
public sealed class IssueAiTokenResult
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
public sealed class UpdateAiTokenCommand
{
    [Key(0)]
    [JsonConverter(typeof(LongToStringConverter))]
    public long Id { get; set; }

    [Key(1)] public string Name { get; set; } = string.Empty;
    [Key(2)] public AiTokenStatus Status { get; set; }
    [Key(3)] public string[] ModelLimits { get; set; } = [];
    [Key(4)] public string[] AllowIpCidrs { get; set; } = [];
    [Key(5)] public DateTime? ExpiresAtUtc { get; set; }
    [Key(6)] public bool UnlimitedQuota { get; set; }
    [Key(7)] public decimal RemainQuota { get; set; }
    [Key(8)] public string ModelBillingScope { get; set; } = "all";
    [Key(9)] public string[] ModelVendorKeys { get; set; } = [];
}

[MessagePackObject]
public sealed class AdjustAiTokenQuotaCommand
{
    [Key(0)]
    [JsonConverter(typeof(LongToStringConverter))]
    public long Id { get; set; }

    /// <summary>正数=增加；负数=扣减；0=切换 Unlimited 时校准。</summary>
    [Key(1)] public decimal Delta { get; set; }
    [Key(2)] public bool? SetUnlimited { get; set; }
    [Key(3)] public string Reason { get; set; } = string.Empty;
    [Key(4)] public string IdempotencyKey { get; set; } = string.Empty;
}

[MessagePackObject]
public sealed class ListAiTokensQuery
{
    [Key(0)]
    [JsonConverter(typeof(NullableLongToStringConverter))]
    public long? AccountUid { get; set; }

    [Key(1)] public AiTokenStatus? Status { get; set; }
    [Key(2)] public string? Keyword { get; set; }
    [Key(3)] public int Take { get; set; } = 50;
    [Key(4)] public int Skip { get; set; }
}

[MessagePackObject]
public sealed class ListAiTokensResult
{
    [Key(0)] public AiTokenDto[] Items { get; set; } = [];
    [Key(1)] public int Total { get; set; }
}
