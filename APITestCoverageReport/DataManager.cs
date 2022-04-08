using Gurock.TestRail;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using test_reports.Models;

namespace test_reports.APITestCoverageReport
{
    class DataManager
    {
        private readonly APIClient _client;
        private int _covered;

        public DataManager()
        {
            _client = new APIClient("https://test.testrail.io/");
            _client.User = "";
            _client.Password = "";
        }

        public object GetListAllTestCases()
        {
            JArray listAllBackendTestCases = (JArray)_client.SendGet("get_cases/8");
            return listAllBackendTestCases;
        }

        public List<string> ReturnListOfTheSubsections(string sectionID, string version)
        {
            JArray getTestCasesBySection = (JArray)_client.SendGet("get_cases/8&suite_id =:" + sectionID + "");
            var obj = JsonConvert.DeserializeObject<List<TestCase>>(getTestCasesBySection.ToString());
            List<string> subsectionsList = new List<string>();
            foreach (var i in obj)
            {
                if (!(subsectionsList.Contains(i.SectionId.ToString())) && i.Refs.Contains(version))
                {
                    subsectionsList.Add(i.SectionId.ToString());
                }
            }
            return subsectionsList;
        }

        public string ReturnSectionNames(string sectionID)
        {
            JArray getTestCasesBySection = (JArray)_client.SendGet("get_sections/8&suite_id =:" + sectionID + "");
            var obj = JsonConvert.DeserializeObject<List<Section>>(getTestCasesBySection.ToString());
            List<string> sectionNamesList = new List<string>();
            foreach (var i in obj)
            {
                sectionNamesList.Add(i.Name.ToString());
            }

            StringBuilder builder = new StringBuilder();
            foreach (var item in sectionNamesList)
            {
                builder.Append(item).Append(",");
            }
            var g = builder.ToString();
            string replaced = "'" + g.Replace(",", "','") + "'";
            return replaced;
        }

        public string GetTestCasesByReferences(Dictionary<string, Tuple<string, string>> endpointsMap)
        {
            StringBuilder sb = new StringBuilder();
            var obj = JsonConvert.DeserializeObject<List<TestCase>>(GetListAllTestCases().ToString());
            using (Html.Table table = new Html.Table(sb, id: "tableFilter"))
            {
                table.StartHead();
                using (var thead = table.AddRow())
                {
                    thead.AddTh("Group", "1", "true");
                    thead.AddTh("Name", "2", "false");
                    thead.AddTh("Covered Endpoint", "3", "false");
                    thead.AddTh("Number of tests", "4", "true");
                    thead.AddTh("Test Name", "5", "true");
                }
                table.EndHead();
                table.StartBody();
                foreach (KeyValuePair<string, Tuple<string, string>> value in endpointsMap)
                {

                        using (var tr = table.AddRow(classAttributes: "someattributes"))
                        {
                        tr.AddCell("<p style='font-size: 12px;font-weight: bold'>" + value.Value.Item2 + "</p>");
                        tr.AddCell("<p style='font-size: 12px;font-weight: bold'>" + value.Key + "</p>");
                        tr.AddCell("<p style='font-size: 12px;font-weight: bold'>" + value.Value.Item1 + "</p>");
                        string testName = string.Empty;
                        int testNumber = 0;
                        foreach (var i in obj)
                        {
                            var referenceString = i.Refs;
                            if (referenceString == value.Value.Item1)
                            {
                                testName +=  "<p style='font-size: 12px'><a href=https://test.testrail.io/index.php?/cases/view/"
                                    + i.Id.ToString() + "&group_by=cases:section_id&group_order=asc&group_id="
                                    + i.SectionId.ToString() + ">#" + i.Id.ToString() + "</a>: " + i.Title + "</p>\n";
                                testNumber += 1;
                            }
                        }
                        tr.AddCell(testNumber.ToString());
                        tr.AddCell(testName);
                    }
                }
                table.EndBody();
            }
            return sb.ToString();
        }

        public int CountTestCasesBySection(string sectionID, string version)
        {
            JArray getTestCasesBySection = (JArray)_client.SendGet("get_cases/8&suite_id =:" + sectionID + "");
            var obj = JsonConvert.DeserializeObject<List<TestCase>>(getTestCasesBySection.ToString());
            List<string> testCasesList = new List<string>();
            foreach (var i in obj)
            {
                if (!(testCasesList.Contains(i.Id.ToString())) && i.Refs.Contains(version))
                {
                    testCasesList.Add(i.SectionId.ToString());
                }
            }
            return testCasesList.Count;
        }

        public int CountCoveredEndpoints(Dictionary<string, Tuple<string, string>> endpointsMap, string version)
        {
            var obj = JsonConvert.DeserializeObject<List<TestCase>>(GetListAllTestCases().ToString());
            List<string> coveredEndpointsList = new List<string>();
            foreach (KeyValuePair<string, Tuple<string, string>> value in endpointsMap)
                {
                    foreach (var i in obj)
                    {
                        var referenceString = i.Refs;
                        if (referenceString == value.Value.Item1 && i.Refs.Contains(version))
                        {
                            if (!(coveredEndpointsList.Contains(referenceString))) 
                            { 
                                coveredEndpointsList.Add(i.Refs); 
                            }
                        }
                    }
              _covered =  coveredEndpointsList.Count;
            }
            return _covered;
        }


        public string ReturnTestCasesForSubsections(string sectionID, string version)
        {
            List<string> testCasesSubsectionsList = new List<string>();
            for (var listItem = 0; listItem < ReturnListOfTheSubsections(sectionID, version).Count; listItem++)
            {
                var numberTestCases = ReturnListOfTheSubsections(sectionID, version)[listItem];
                JArray getTestCasesBySubsection = (JArray)_client.SendGet("get_cases/8&suite_id =:" + sectionID + "&section_id=" + numberTestCases + "");
                var obj = JsonConvert.DeserializeObject<List<TestCase>>(getTestCasesBySubsection.ToString());
                var testCasesNumber = obj.Count();
                testCasesSubsectionsList.Add(testCasesNumber.ToString());
            }
            StringBuilder builder = new StringBuilder();
            foreach (var item in testCasesSubsectionsList)
            {
                builder.Append(item).Append(",");
            }
            return builder.ToString();       
        }

    }
}
