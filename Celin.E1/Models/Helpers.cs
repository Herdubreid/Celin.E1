using System.Text;
using System.Text.RegularExpressions;

namespace Celin.Models;

public static partial class Helpers
{
    [GeneratedRegex("_(W[^_]+)")]
    public static partial Regex FORM();
    public static string FormName(string name)
        => $"P{name[1..^1]}_{name}";
    public static StringBuilder FormatErrorMsg(IEnumerable<AIS.ErrorWarning> errors)
        => errors.Aggregate(new StringBuilder(), (s, e)
            => s.AppendLine($"{e.TITLE} ({e.CODE})").AppendLine(Regex.Unescape(e.DESC)));
    public static string ErrorMsg(AIS.AisException ex)
        => ex.ErrorResponse?.errorDetails is null
        ? ex.ErrorResponse?.message ?? ex.Message
        : ex.ErrorResponse.errorDetails.errors.Any()
            ? FormatErrorMsg(ex.ErrorResponse.errorDetails.errors).ToString()
            : string.Format("Unknown Error (Http Status Code {0})", ex.HttpStatusCode);
}
