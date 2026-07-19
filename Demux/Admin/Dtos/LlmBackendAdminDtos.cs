using System.Text.Json.Serialization;
using Platform.Common.Web;
using MessagePack;

namespace Meeko.Contracts.Demux.Admin;

[MessagePackObject]
public sealed class IssueLlmBackendCommand
{
    [Key(0)] public string Name { get; set; } = string.Empty;
    [Key(1)] public string[] Scopes { get; set; } = [];
}

[MessagePackObject]
public sealed class IssueLlmBackendResult
{
    [Key(0)] public bool Success { get; set; }

    [Key(1)]
    [JsonConverter(typeof(LongToStringConverter))]
    public long BackendId { get; set; }

    [Key(2)] public string ClientId { get; set; } = string.Empty;
    /// <summary>明文 Secret（仅本次返回）。</summary>
    [Key(3)] public string ClientSecret { get; set; } = string.Empty;
    [Key(4)] public string? FailureCode { get; set; }
    [Key(5)] public string? FailureMessage { get; set; }
}
