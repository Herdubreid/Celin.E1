namespace Celin.Models;

public record Form : AIS.FormResponse
{
    public AIS.Form<W01012B.Data>? fs_P01012_W01012B { get; init; }
    public AIS.Form<W01012B.Data> W01012B
        => fs_P01012_W01012B ?? throw new ArgumentNullException(nameof(W01012B));
    public AIS.Form<W01012A.Data>? fs_P01012_W01012A { get; init; }
    public AIS.Form<W01012A.Data> W01012A
        => fs_P01012_W01012A ?? throw new ArgumentNullException(nameof(W01012A));
}
