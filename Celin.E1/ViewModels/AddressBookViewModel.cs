using Celin.Models;
using Celin.Services;
using Celin.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace Celin.ViewModels;

public static class ColumnWidth
{
    public static int AN8 { get; set; }
    public static int ALPH { get; set; }
}
public partial class AddressBookViewModel : ObservableObject
{
    [ObservableProperty]
    List<Models.W01012B.Row> _rows = [];
    [ObservableProperty]
    string? _searchMessage;
    [RelayCommand]
    async Task SelectAsync(Models.W01012B.Row item)
    {
        if (item == null) return;
        var dlg = new RequestPage("Address Detail...");
        await Shell.Current.Navigation.PushModalAsync(dlg);
        try
        {
            var rs = await _host
            .RequestAsync<Form>(new Models.W01012B
                    .Request(Make
                        .Query(Make.List(Make
                            .Equal("1[19]", item.z_AN8_19.ToString()))))
            {
                formActions = Make.List(
                            Make.Select(0), Make.Do(Models.W01012B.Actions.Select))
            });

            await Shell.Current.Navigation.PopModalAsync();
            await Shell.Current.Navigation.PushAsync(new AddressDetailPage(rs.W01012A.data));
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.Navigation.PopModalAsync();
        }
    }
    [RelayCommand]
    async Task CustomerSearchAsync(string txt)
    {
        _cancel.Cancel();
        _cancel = new CancellationTokenSource();
        try
        {
            SearchMessage = "Searching...";

            var tokens = txt.Split(' ');
            var q = int.TryParse(txt, out int an8)
            ? Make.Query(Make.List(Make.Equal("1[19]", an8.ToString())))
            : Make.Query(tokens
                .Select(t => Make.Like("1[20]", t)));
            var rs = await _host.RequestAsync<Form>(
                new Models.W01012B.Request(q));
            Rows = rs.W01012B.data.gridData.rowset.ToList();

            if (Rows.Count > 0)
            {
                SearchMessage = null;
                const int w = 9;
                ColumnWidth.AN8 = Rows.Max(r => r.z_AN8_19.ToString().Length) * w;
                ColumnWidth.ALPH = Rows.Max(r => r.z_ALPH_20.Length) * w;
            }
            else
            {
                SearchMessage = "No Matching Result";
            }
        }
        catch (OperationCanceledException) { }
    }
    CancellationTokenSource _cancel = new CancellationTokenSource();
    readonly Host _host;
    public AddressBookViewModel(Host host)
    {
        _host = host;
    }
}
