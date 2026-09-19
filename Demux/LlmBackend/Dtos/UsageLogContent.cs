using System.Text.Json;
using System.Text.Json.Serialization;
using MessagePack;
using Platform.Common.Json;

namespace Meeko.Contracts.Demux.LlmBackend;

/// <summary>
/// 一次调用的上下文快照，整体落到 <c>usage_logs.content</c>（jsonb）。
/// 把协议 / 响应码 / 会话 / 错误 / 耗时 / 来源 IP 这些"描述请求本身"的字段收在一处，
/// 避免每加一个维度就往宽表上挂一列。
///
/// <para>TraceId 不在此列：它是计费幂等键，须保持独立列 + 唯一索引。</para>
///
/// <para>reserve 阶段只有 <see cref="Protocol"/> / <see cref="Streamed"/> / <see cref="ClientIp"/>
/// 有值；其余字段要等上游回报，commit 时补齐。</para>
///
/// <para>jsonb 里的 key 一律 camelCase（<see cref="JsonOptions"/>），
/// Admin 侧的 jsonb 查询依赖这个约定，改名前先搜全库。</para>
/// </summary>
[MessagePackObject]
public sealed class UsageLogContent
{
    /// <summary>调用协议（<c>anthropic_messages</c> / <c>openai_chat</c> / <c>gemini</c> ...）。</summary>
    [Key(0)] public string? Protocol { get; set; }

    /// <summary>上游 HTTP 响应码；null 表示未抵达上游。计费据此判定成功与否。</summary>
    [Key(1)] public int? StatusCode { get; set; }

    [Key(2)] public bool Streamed { get; set; }

    /// <summary>网关瀑布解析出的 conversationId。</summary>
    [Key(3)] public string? ConvId { get; set; }

    [Key(4)] public string? ErrorCode { get; set; }
    [Key(5)] public string? ErrorMessage { get; set; }

    /// <summary>
    /// 调用耗时（流式为首字延迟 TTFT）。null 表示未知——reserve 阶段还没有耗时可言，
    /// 统计（AVG / P95）也据此跳过这些行，故不要用 0 代替 null。
    /// </summary>
    [Key(6)] public int? LatencyMs { get; set; }

    /// <summary>调用方来源 IP（已由网关的 ForwardedHeaders 还原为真实用户 IP）。</summary>
    [Key(7)] public string? ClientIp { get; set; }

    public static readonly JsonSerializerOptions JsonOptions = BuildJsonOptions();

    private static JsonSerializerOptions BuildJsonOptions()
    {
        var o = new JsonSerializerOptions(PlatformJson.Web)
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };
        o.MakeReadOnly(populateMissingResolver: true);
        return o;
    }

    public string ToJson() => JsonSerializer.Serialize(this, JsonOptions);

    public static UsageLogContent? FromJson(string? json)
    {
        if (string.IsNullOrWhiteSpace(json))
            return null;
        try
        {
            return JsonSerializer.Deserialize<UsageLogContent>(json, JsonOptions);
        }
        catch (JsonException)
        {
            return null;
        }
    }
}
