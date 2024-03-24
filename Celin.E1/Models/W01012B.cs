namespace Celin.Models.W01012B;

public static class Actions
{
    public const string Select = "14";
    public const string Find = "15";
}
public record Row(
    int z_AN8_19,
    string z_ALPH_20,
    string z_ADD1_40,
    string z_CTY1_44,
    string z_AR1_81,
    string z_PH3_46,
    string z_PHTP_47,
    string z_ALKY_48,
    string z_SIC_49,
    string z_AT1_50,
    string z_TAX_51,
    int z_SYNCS_91,
    string z_DL01_93,
    int z_CAAD_92,
    string z_DL01_94);
public record Data : AIS.FormData<Row>
{
    public required AIS.FormField<string> z_AT1_54 { get; init; }
    public required AIS.FormField<string> z_ALPH_58 { get; init; }
    public required AIS.FormField<string>? z_EV01_62 { get; init; }
    public required AIS.FormField<string>? z_EV01_63 { get; init; }
}
public class Request : AIS.FormRequest
{
    public Request(AIS.Query q) : this()
    {
        query = q;
    }
    public Request()
    {
        formName = Helpers.FormName(nameof(W01012B));
    }
}
