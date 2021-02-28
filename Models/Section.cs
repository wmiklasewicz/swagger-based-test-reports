using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace test_reports.Models
{
    public class Section
    {
        [JsonProperty("depth")]
        public long Depth { get; set; }

        [JsonProperty("display_order")]
        public long DisplayOrder { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("parent_id")]
        public object ParentId { get; set; }

        [JsonProperty("suite_id")]
        public long SuiteId { get; set; }
    }
}
