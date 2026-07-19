using System.Text.Json.Serialization;
using Platform.Common.Web;
using MessagePack;

namespace Meeko.Contracts.Demux.Common;

[MessagePackObject]
public sealed class AdminCommandResult
{
    [Key(0)] public bool Success { get; set; }

    // 新建/更新成功时返回的实体主键。console docs §3：业务实体一律用 `id` 字段名，
    // 同时 long 经 LongToStringConverter 序列化为 string 以避免 JS Number 精度丢失。
    [Key(1)]
    [JsonConverter(typeof(LongToStringConverter))]
    public long Id { get; set; }

    [Key(2)] public string? FailureCode { get; set; }
    [Key(3)] public string? FailureMessage { get; set; }

    public static AdminCommandResult Ok(long id) => new() { Success = true, Id = id };
    public static AdminCommandResult Fail(string code, string msg) => new() { FailureCode = code, FailureMessage = msg };
}
