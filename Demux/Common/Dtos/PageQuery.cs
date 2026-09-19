using MessagePack;

namespace Meeko.Contracts.Demux.Common;

/// <summary>列表查询分页：<see cref="Take"/> 条、<see cref="Skip"/> 条偏移。</summary>
[MessagePackObject]
public sealed class PageQuery
{
    [Key(0)] public int Take { get; set; } = 50;
    [Key(1)] public int Skip { get; set; }
}
