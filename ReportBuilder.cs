using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using test_reports.APITestCoverageReport;

namespace test_reports
{
    class ReportBuilder
    {
        private readonly DataManager _dataManager;
        private readonly EndpointsMap _endpointsMap;

        public ReportBuilder()
        {
            _dataManager = new DataManager();
            _endpointsMap = new EndpointsMap();
        }
        public string PopulateBody(string table, string title, string nonCoveredTestCasesNumber, string coveredTestCasesNumber, string testCasesPerSection, string classDiv,
            string navbarDivID, string sectionNames)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader("Templates/TestCoverageTemplate.html"))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Table}", table);
            body = body.Replace("{Title}", title);
            body = body.Replace("{NonCoveredTestCasesNumber}", nonCoveredTestCasesNumber);
            body = body.Replace("{CoveredTestCasesNumber}", coveredTestCasesNumber);
            body = body.Replace("{TestCasesPerSection}", testCasesPerSection);
            body = body.Replace("{ClassDiv}", classDiv);
            body = body.Replace("{NavbarDivID}", navbarDivID);
            body = body.Replace("{SectionNames}", sectionNames);
            return body;
        }

        public void GenerateOpusHtml()
        {
            var table =_dataManager.GetTestCasesByReferences(_endpointsMap.OpusEndpointsMap());
            string sectionNames = "'Config', 'Documents', 'Payments', 'Policy', 'Quote','Tobes', 'Reporting', 'Report Schedules', 'Stripe', 'User', 'Opus User Management','Opus Organization Management', 'Notes', 'Command', 'Portal', 'Webhooks', 'Subscription', 'Policy Search'";
            var testCasesNumber = _dataManager.CountCoveredEndpoints(_endpointsMap.OpusEndpointsMap(), "/v1/");
            var allEndpoints = _endpointsMap.OpusEndpointsMap().Keys.Count;
            var nonCoveredTestCasesNumber = allEndpoints - testCasesNumber;
            var coveredTestCasesNumber = allEndpoints - nonCoveredTestCasesNumber;
            var testCasesPerSections = _dataManager.ReturnTestCasesForSubsections("2453", "/v1/");
            var classDiv = "opus";
            var navbarDivID = "opusID";
            var body = PopulateBody(table, "OPUS", nonCoveredTestCasesNumber.ToString(), coveredTestCasesNumber.ToString(), testCasesPerSections, classDiv, navbarDivID, sectionNames);
            using StreamWriter outputFile = new StreamWriter(Path.Combine("Reports/index.html"));
            outputFile.WriteLine(body);
        }

        public void GeneratePlatformHtml()
        {
            var table = _dataManager.GetTestCasesByReferences(_endpointsMap.PlatformEndpointsMap());
            var testCasesNumber = _dataManager.CountCoveredEndpoints(_endpointsMap.PlatformEndpointsMap(), "/v2/");
            string sectionNames = "'Config', 'Line Of Business', 'Payments', 'Policies','Product', 'Quick Quotes', 'Stripe', 'User', 'Tobes','Quotes', 'Subscriptions', 'Documents'";
            var allEndpoints = _endpointsMap.PlatformEndpointsMap().Keys.Count;
            var nonCoveredTestCasesNumber = allEndpoints - testCasesNumber;
            var coveredTestCasesNumber = allEndpoints - nonCoveredTestCasesNumber;
            var testCasesPerSections = _dataManager.ReturnTestCasesForSubsections("2471", "/v2/");
            var classDiv = "platform";
            var navbarDivID = "platfromID";
            var body = PopulateBody(table, "PLATFORM", nonCoveredTestCasesNumber.ToString(), coveredTestCasesNumber.ToString(), testCasesPerSections, classDiv, navbarDivID, sectionNames);
            using StreamWriter outputFile = new StreamWriter(Path.Combine("Reports/platform.html"));
            outputFile.WriteLine(body);
        }

    }
}
