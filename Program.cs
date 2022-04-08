using Gurock.TestRail;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using test_reports.APITestCoverageReport;

namespace test_reports
{
    class Program
    {
        static void Main(string[] args)
        {
            var html = new ReportBuilder();
            html.GeneratetestHtml();
            html.GenerateBaseHtml();
        }
    }
}
