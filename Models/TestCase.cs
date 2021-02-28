using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace test_reports.Models
{
    public class TestCase
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("section_id")]
        public long SectionId { get; set; }

        [JsonProperty("template_id")]
        public long TemplateId { get; set; }

        [JsonProperty("type_id")]
        public long TypeId { get; set; }

        [JsonProperty("priority_id")]
        public long PriorityId { get; set; }

        [JsonProperty("milestone_id")]
        public object MilestoneId { get; set; }

        [JsonProperty("refs")]
        public string Refs { get; set; }

        [JsonProperty("created_by")]
        public long CreatedBy { get; set; }

        [JsonProperty("created_on")]
        public long CreatedOn { get; set; }

        [JsonProperty("updated_by")]
        public long UpdatedBy { get; set; }

        [JsonProperty("updated_on")]
        public long UpdatedOn { get; set; }

        [JsonProperty("estimate")]
        public object Estimate { get; set; }

        [JsonProperty("estimate_forecast")]
        public object EstimateForecast { get; set; }

        [JsonProperty("suite_id")]
        public long SuiteId { get; set; }

        [JsonProperty("display_order")]
        public long DisplayOrder { get; set; }

        [JsonProperty("custom_automation_type")]
        public long CustomAutomationType { get; set; }

        [JsonProperty("custom_preconds")]
        public object CustomPreconds { get; set; }

        [JsonProperty("custom_steps")]
        public object CustomSteps { get; set; }

        [JsonProperty("custom_expected")]
        public object CustomExpected { get; set; }

        [JsonProperty("custom_steps_separated")]
        public object CustomStepsSeparated { get; set; }

        [JsonProperty("custom_mission")]
        public object CustomMission { get; set; }

        [JsonProperty("custom_goals")]
        public object CustomGoals { get; set; }
    }
}
