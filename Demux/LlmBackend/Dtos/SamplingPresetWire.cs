using MessagePack;

namespace Meeko.Contracts.Demux.LlmBackend;

/// <summary>
/// 令牌解析时随绑定一起下发的那一条采样参数预设。
///
/// <para>规则集合按 <see cref="RulesJson"/> 原文携带而不是拆成结构化字段：
/// 可调参数白名单会随上游能力增减，拆开的话每加一个参数就要动一次线上契约，
/// 而这个 blob 的消费方只有网关自己。</para>
/// </summary>
[MessagePackObject]
public sealed class SamplingPresetWire
{
    /// <summary>预设主键（UUID）。日志和 <c>x-meeko-preset</c> 用它。</summary>
    [Key(0)] public Guid Id { get; set; }

    /// <summary>展示名（日志）。响应头 <c>x-meeko-preset</c> 写的是 <see cref="Id"/>——HTTP 头只能 ASCII。</summary>
    [Key(1)] public string Name { get; set; } = string.Empty;

    /// <summary>规则集合 JSON 原文；字段语义见网关侧 <c>SamplingRuleSet</c>。</summary>
    [Key(2)] public string RulesJson { get; set; } = "{}";
}
