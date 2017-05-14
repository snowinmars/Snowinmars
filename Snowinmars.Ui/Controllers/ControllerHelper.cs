using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snowinmars.Ui.Controllers
{
    internal static class ControllerHelper
    {
        internal static string Convert(string str) => str?.Trim() ?? "";
    }
}