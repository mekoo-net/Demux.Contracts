using MagicOnion;

namespace Meeko.Contracts.Demux.LlmBackend;

/// <summary>
/// LLM 后端 → DemuxAi：拉取倍率/模型/分组的版本化快照。
/// 建议 LLM 后端定时（1–5 min）拉一次；服务端按 sinceVersion 返回增量。
/// </summary>
public interface ILlmCatalogService : IService<ILlmCatalogService>
{
    UnaryResult<RatioSnapshot> GetRatioSnapshotAsync(int sinceVersion);

    UnaryResult<ModelMetaSnapshot> GetModelSnapshotAsync(int sinceVersion);
}
