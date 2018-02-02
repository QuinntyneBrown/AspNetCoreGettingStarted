using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreGettingStarted.IntegrationTests
{
    public class ServerFixture<TStartup> : IDisposable
        where TStartup : class
    {
        private IWebHost _host;

        private IApplicationLifetime _lifetime;

        public string WebSocketsUrl => Url.Replace("http", "ws");

        public string Url { get; private set; }

        public ServerFixture()
        {
            Url = "http://localhost:" + GetNextPort();
            StartServer(Url);
        }
        private void StartServer(string url) {
            _host = new WebHostBuilder()
                .UseStartup(typeof(TStartup))
                .UseKestrel()
                .UseUrls(url)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .Build();

            var t = Task.Run(() => _host.Start());

            _lifetime = _host.Services.GetRequiredService<IApplicationLifetime>();
            if (!_lifetime.ApplicationStarted.WaitHandle.WaitOne(TimeSpan.FromSeconds(5)))
            {
                // t probably faulted
                if (t.IsFaulted)
                {
                    throw t.Exception.InnerException;
                }
                throw new TimeoutException("Timed out waiting for application to start.");
            }


            _lifetime.ApplicationStopped.Register(() =>
            {

            });
        }

        public void Dispose()
        {
            _host.Dispose();
        }

        private static int GetNextPort()
        {
            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                // Let the OS assign the next available port. Unless we cycle through all ports
                // on a test run, the OS will always increment the port number when making these calls.
                // This prevents races in parallel test runs where a test is already bound to
                // a given port, and a new test is able to bind to the same port due to port
                // reuse being enabled by default by the OS.
                socket.Bind(new IPEndPoint(IPAddress.Loopback, 0));
                return ((IPEndPoint)socket.LocalEndPoint).Port;
            }
        }

    }
}
