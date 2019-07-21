using Olive;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sneakify.Common
{
    public class Extentions
    {
        public static DirectoryInfo GetExecutionPath()
        {
            var appDomain = AppDomain.CurrentDomain;
            var basePath = appDomain.RelativeSearchPath ?? appDomain.BaseDirectory;
            return basePath.AsDirectory();
        }
    }
}
