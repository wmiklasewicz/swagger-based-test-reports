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

        public void GeneratetestHtml()
        {
            var table =_dataManager.GetTestCasesByReferences(_endpointsMap.testEndpointsMap());
            string sectionNames = "'Config', 'Documents', 'Payments', 'Policy', 'Quote','Tobes', 'Reporting', 'Report Schedules', 'Stripe', 'User', 'test User Management','test Organization Management', 'Notes', 'Command', 'Portal', 'Webhooks', 'Subscription', 'Policy Search'";
            var testCasesNumber = _dataManager.CountCoveredEndpoints(_endpointsMap.testEndpointsMap(), "/v1/");
            var allEndpoints = _endpointsMap.testEndpointsMap().Keys.Count;
            var nonCoveredTestCasesNumber = allEndpoints - testCasesNumber;
            var coveredTestCasesNumber = allEndpoints - nonCoveredTestCasesNumber;
            var testCasesPerSections = _dataManager.ReturnTestCasesForSubsections("2453", "/v1/");
            var classDiv = "test";
            var navbarDivID = "testID";
            var body = PopulateBody(table, "test", nonCoveredTestCasesNumber.ToString(), coveredTestCasesNumber.ToString(), testCasesPerSections, classDiv, navbarDivID, sectionNames);
            using StreamWriter outputFile = new StreamWriter(Path.Combine("Reports/index.html"));
            outputFile.WriteLine(body);
        }

        public void GenerateBaseHtml()
        {
            var table = _dataManager.GetTestCasesByReferences(_endpointsMap.BaseEndpointsMap());
            var testCasesNumber = _dataManager.CountCoveredEndpoints(_endpointsMap.BaseEndpointsMap(), "/v2/");
            string sectionNames = "'Config', 'Line Of Business', 'Payments', 'Policies','Product', 'Quick Quotes', 'Stripe', 'User', 'Tobes','Quotes', 'Subscriptions', 'Documents'";
            var allEndpoints = _endpointsMap.BaseEndpointsMap().Keys.Count;
            var nonCoveredTestCasesNumber = allEndpoints - testCasesNumber;
            var coveredTestCasesNumber = allEndpoints - nonCoveredTestCasesNumber;
            var testCasesPerSections = _dataManager.ReturnTestCasesForSubsections("2471", "/v2/");
            var classDiv = "Base";
            var navbarDivID = "platfromID";
            var body = PopulateBody(table, "Base", nonCoveredTestCasesNumber.ToString(), coveredTestCasesNumber.ToString(), testCasesPerSections, classDiv, navbarDivID, sectionNames);
            using StreamWriter outputFile = new StreamWriter(Path.Combine("Reports/Base.html"));
            outputFile.WriteLine(body);
        }

    }
}
