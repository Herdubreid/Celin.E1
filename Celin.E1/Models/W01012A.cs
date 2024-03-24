namespace Celin.Models.W01012A;

public record Data : AIS.FormData<object>
{
    public required AIS.FormField<int> z_AN8_21 { get; init; }
    public required AIS.FormField<string> z_ALPH_28 { get; init; }
    public required AIS.FormField<string> z_ALKY_32 { get; init; }
    public required AIS.FormField<string> z_TAX_34 { get; init; }
    public required AIS.FormField<string> z_AT1_36 { get; init; }
    public required AIS.FormField<string> z_MCU_62 { get; init; }
    public required AIS.FormField<string> z_MLNM_38 { get; init; }
    public required AIS.FormField<string> z_ADD1_40 { get; init; }
    public required AIS.FormField<string> z_ADD2_42 { get; init; }
    public required AIS.FormField<string> z_ADD3_44 { get; init; }
    public required AIS.FormField<string> z_ADD4_46 { get; init; }
    public required AIS.FormField<string> z_CTY1_52 { get; init; }
    public required AIS.FormField<string> z_ADDS_54 { get; init; }
    public required AIS.FormField<string> z_ADDZ_50 { get; init; }
    public required AIS.FormField<string> z_CTR_56 { get; init; }
}
