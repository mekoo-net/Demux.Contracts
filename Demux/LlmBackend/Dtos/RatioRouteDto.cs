using MessagePack;

namespace Meeko.Contracts.Demux.LlmBackend;

/// <summary>
/// 对外别名到真实渠道的绑定（来自 active <c>model_aliases</c> 快照）。
/// 网关用此表把客户端请求的 <see cref="Alias"/> 解析为 NATS 队列组与上游模型名。
/// </summary>
[MessagePackObject]
public sealed class RatioRouteDto
{
    [Key(0)] public string Alias { get; set; } = string.Empty;
    [Key(1)] public string VendorKey { get; set; } = string.Empty;
    [Key(2)] public string VendorModel { get; set; } = string.Empty;
    [Key(3)] public string BillingType { get; set; } = string.Empty;
    [Key(4)] public string VendorSlug { get; set; } = string.Empty;
}
