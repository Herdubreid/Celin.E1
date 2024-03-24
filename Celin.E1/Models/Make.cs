namespace Celin.Models;

// AIS Element Factories
public static class Extensions
{
    public static IEnumerable<AIS.Condition> Equal(
        this IEnumerable<AIS.Condition> list, string controlId, string value)
        => list.Append(Make.Equal(controlId, value));
    public static IEnumerable<AIS.Condition> In(
        this IEnumerable<AIS.Condition> list, string controlId, string[] values)
        => list.Append(Make.In(controlId, values));
    public static string Name(this string name)
        => name.Replace('_', '.');
    public static string Table(this string name)
        => name.Split('_').FirstOrDefault() ?? string.Empty;
    public static string Alias(this string name)
        => name.Split('_').LastOrDefault() ?? string.Empty;
}
public class Make
{
    // Generic list converter
    public static IEnumerable<T> List<T>(params T[] items)
        => items;
    // Literal value
    public static AIS.Value Literal(string content)
        => new AIS.Value
        {
            content = content,
            specialValueId = AIS.Value.LITERAL
        };
    // Equal condition
    public static AIS.Condition Equal(string controlId, string value)
        => new AIS.Condition
        {
            controlId = controlId,
            @operator = AIS.Condition.EQUAL,
            value = List(Literal(value))
        };
    // Like condition
    public static AIS.Condition Like(string controlId, string value)
        => new AIS.Condition
        {
            controlId = controlId,
            @operator = AIS.Condition.STR_CONTAIN,
            value = List(Literal(value))
        };
    // In condition
    public static AIS.Condition In(string controlId, string[] values)
        => new AIS.Condition
        {
            controlId = controlId,
            @operator = AIS.Condition.LIST,
            value = values.Select(v => Literal(v))
        };
    // QBE FormAction
    public static AIS.FormAction QBE(string controlId, string value)
        => new AIS.FormAction
        {
            controlID = $"1[{controlId}]",
            command = AIS.FormAction.SetQBEValue,
            value = value
        };
    // Set FormAction
    public static AIS.FormAction Set(string controlId, string value)
        => new AIS.FormAction
        {
            controlID = controlId,
            command = AIS.FormAction.SetControlValue,
            value = value
        };
    // Select FormAction
    public static AIS.FormAction Select(int row, int grid = 1)
        => new AIS.FormAction
        {
            controlID = $"{grid}.{row}",
            command = AIS.FormAction.SelectRow
        };
    // SelectAll FormAction
    public static AIS.FormAction SelectAll(int grid = 1)
        => new AIS.FormAction
        {
            controlID = $"{grid}",
            command = AIS.FormAction.SelectAllRows
        };
    // UnSelect FormAction
    public static AIS.FormAction UnSelect(int row, int grid = 1)
        => new AIS.FormAction
        {
            controlID = $"{grid}.{row}",
            command = AIS.FormAction.UnSelectRow
        };
    // UnSelectAll FormAction
    public static AIS.FormAction UnSelectAll(int row, int grid = 1)
        => new AIS.FormAction
        {
            controlID = $"{grid}",
            command = AIS.FormAction.UnSelectAllRows
        };
    // Do FormAction
    public static AIS.FormAction Do(string controlId)
        => new AIS.FormAction
        {
            controlID = controlId,
            command = AIS.FormAction.DoAction
        };
    public static AIS.FormAction Do(int controlId)
        => Do(controlId.ToString());
    // Query
    public static AIS.Query Query(IEnumerable<AIS.Condition> condition)
        => new AIS.Query
        {
            matchType = AIS.Query.MATCH_ALL,
            autoFind = true,
            condition = condition
        };
    // Open Stack Form
    public static AIS.StackFormRequest Open(AIS.FormRequest fm)
        => new AIS.StackFormRequest
        {
            action = AIS.StackFormRequest.open,
            formRequest = fm
        };
    // Execute Stack Form
    public static AIS.StackFormRequest Execute(AIS.FormResponse rs, AIS.ActionRequest rq)
    {
        rq.formOID = Helpers.FORM().Match(rs.currentApp).Groups[1].Value;
        return new AIS.StackFormRequest
        {
            action = AIS.StackFormRequest.execute,
            stackId = rs.stackId,
            stateId = rs.stateId,
            rid = rs.rid,
            actionRequest = rq
        };
    }
    // Close Stack Form
    public static AIS.StackFormRequest Close(AIS.FormResponse rs)
        => new AIS.StackFormRequest
        {
            action = AIS.StackFormRequest.close,
            stackId = rs.stackId,
            stateId = rs.stateId,
            rid = rs.rid
        };
}
