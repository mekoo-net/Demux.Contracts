using MessagePack;

namespace Meeko.Contracts.Demux.LlmBackend;

/// <summary>
/// 一次调用的计费用量分桶。各桶必须两两不相交（<c>RatioResolver.ComputeAmount</c> 按桶累加计费）：
/// input = <see cref="Prompt"/> + <see cref="CachedRead"/> + <see cref="CachedWrite"/>，
/// output = <see cref="Completion"/> + <see cref="Reasoning"/>。
/// <para>
/// Key 按「类型分组（input → output）+ 组内名字字典序」排列。新增字段插入所属组的正确位置并整体重编号，
/// 不要图省事追加到末尾——两端同版本部署，Key 连续性比兼容旧序更值钱。
/// </para>
/// </summary>
[MessagePackObject]
public sealed class TokenUsageBreakdown
{
    // ── input ────────────────────────────────────────────────────────────────

    /// <summary>命中缓存读取的 input token（不含缓存写入）。</summary>
    [Key(0)] public int CachedRead { get; set; }

    /// <summary>写入（创建）缓存的 input token。</summary>
    [Key(1)] public int CachedWrite { get; set; }

    /// <summary>未命中缓存的净 input token。</summary>
    [Key(2)] public int Prompt { get; set; }

    // ── output ───────────────────────────────────────────────────────────────

    [Key(3)] public int Audio { get; set; }

    /// <summary>正文 output token（已剔除 <see cref="Reasoning"/>）。</summary>
    [Key(4)] public int Completion { get; set; }

    [Key(5)] public int Image { get; set; }

    /// <summary>思考 / reasoning output token。</summary>
    [Key(6)] public int Reasoning { get; set; }
}
