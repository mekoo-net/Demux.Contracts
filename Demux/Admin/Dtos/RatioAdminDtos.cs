using System.Text.Json.Serialization;
using Platform.Common.Web;
using Meeko.Contracts.Demux.Common;
using MessagePack;

namespace Meeko.Contracts.Demux.Admin;

[MessagePackObject]
public sealed class RatioRowDto
{
    [Key(0)]
    [JsonConverter(typeof(LongToStringConverter))]
    public long Id { get; set; }

    [Key(1)] public string ModelName { get; set; } = string.Empty;
    [Key(2)] public int Version { get; set; }
    [Key(3)] public DateTime UpdatedAtUtc { get; set; }
}

[MessagePackObject]
public sealed class UpsertRatioCommand
{
    [Key(0)] public string ModelName { get; set; } = string.Empty;
    [Key(1)] public string? Reason { get; set; }
}

[MessagePackObject]
public sealed class DeleteRatioCommand
{
    [Key(0)] public long Id { get; set; }
    [Key(1)] public string? Reason { get; set; }
}

[MessagePackObject]
public sealed class RatioVersionDto
{
    [Key(0)] public int Version { get; set; }
    [Key(1)] public DateTime CreatedAtUtc { get; set; }
    [Key(2)] public string? OperatorUserId { get; set; }
    [Key(3)] public string? Reason { get; set; }
    [Key(4)] public int ChangedRowCount { get; set; }
}

[MessagePackObject]
public sealed class ListRatiosQuery
{
    [Key(0)] public string? Keyword { get; set; }
    [Key(1)] public PageQuery Page { get; set; } = new() { Take = 200 };
}

[MessagePackObject]
public sealed class ListRatiosResult
{
    [Key(0)] public RatioRowDto[] Items { get; set; } = [];
    [Key(1)] public int Total { get; set; }
    [Key(2)] public int CurrentVersion { get; set; }
}
