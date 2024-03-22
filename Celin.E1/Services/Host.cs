using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Celin.Services;

public class Host : AIS.Server
{
    public Host(IConfiguration config, ILogger<Host> logger)
        : base(config.GetValue<string>("BaseUrl"), logger)
    { }
}
