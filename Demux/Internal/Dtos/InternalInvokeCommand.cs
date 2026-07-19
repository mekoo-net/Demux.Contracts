using MessagePack;

namespace Meeko.Contracts.Demux.Internal;

[MessagePackObject]
public sealed class InternalInvokeCommand
{
    /// <summary>Trusted caller's account UID — replaces sk- token auth.</summary>
    [Key(0)] public long AccountUid { get; set; }

    /// <summary>Bare alias (e.g. "claude-opus-4.5"). Paired with <see cref="VendorKey"/> since aliases may repeat across channels.</summary>
    [Key(1)] public string ModelName { get; set; } = "";

    /// <summary>Idempotency key correlating with metering reservation on the Demux side.</summary>
    [Key(2)] public string RequestId { get; set; } = "";

    /// <summary>NATS dispatch caller format: "openai.chat", "anthropic", "gemini", etc.</summary>
    [Key(3)] public string CallerFormat { get; set; } = "openai.chat";

    /// <summary>Raw UTF-8 JSON of the LLM API request with "model" already rewritten to ModelName.</summary>
    [Key(4)] public byte[] PayloadJson { get; set; } = [];

    /// <summary>Channel key (NATS queue group). With <see cref="ModelName"/> (alias) it pins the exact route/pricing row.</summary>
    [Key(5)] public string VendorKey { get; set; } = "";
}
