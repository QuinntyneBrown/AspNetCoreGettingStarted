using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AspNetCoreGettingStarted.Tests.Utilities
{
    public class TestHelpers
    {
        public static IConfigurationRoot GetAppSettings() {
            return new ConfigurationBuilder()
                .SetBasePath(Path.GetFullPath(@"../../../../../src/AspNetCoreGettingStarted/"))
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
        }
    }
}
