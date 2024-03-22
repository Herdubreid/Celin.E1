namespace Celin.Services;

public static class Routes
{
    public const string Login = "login";
}

public class NavigationService
{
    public Task NavigateToAsync(string route, IDictionary<string, object>? routeParameters = null)
    {
        var shellNavigation = new ShellNavigationState(route);

        return routeParameters != null
            ? Shell.Current.GoToAsync(shellNavigation, routeParameters)
            : Shell.Current.GoToAsync(shellNavigation);
    }
    public NavigationService()
    {
    }
}