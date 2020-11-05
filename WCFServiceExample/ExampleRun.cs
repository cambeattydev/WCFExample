using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SsrsService;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WCFServiceExample
{
    class ExampleRun
    {
        private const int MaxReceivedMessageSize = 20_971_529;
        private readonly ReportExecutionServiceSoapClient _reportClient;

        public ExampleRun(IConfiguration configuration)
        {
            _reportClient = new ReportExecutionServiceSoapClient(new BasicHttpBinding()
            {
                MaxReceivedMessageSize = MaxReceivedMessageSize,

            }, new EndpointAddress(configuration["ReportExecutionServiceSoapUrl"]));
            _reportClient.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Delegation;

        }

        public async Task TestAsync()
        {
            try
            {
                var trustedUserHeader = new TrustedUserHeader();
                var reportPath = "replace with path to report in SSRS";
                var report = await _reportClient.LoadReportAsync(trustedUserHeader, reportPath, null);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An excpetion occurred: {ex.Message}");
                throw;
            }
        }
    }
}
