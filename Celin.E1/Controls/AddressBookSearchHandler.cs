using System.Diagnostics;

namespace Celin.Controls;

public class AddressBookSearchHandler : SearchHandler
{
    protected override void OnQueryConfirmed()
    {
        base.OnQueryConfirmed();
        Debug.WriteLine("Query Confirmed!");
    }
}
