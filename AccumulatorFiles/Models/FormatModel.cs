using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AccumulatorFiles.Models
{
    public class FormatModel
    {
        public Dictionary<int, Regex> Filters { get; set; }
        public string[] ColumnSortingSequence { get; set; }
        public bool Descending { get; set; }
        public FormatModel() { }
        public FormatModel(JToken filters, JToken ColumnSortingSequence, JToken Descending)
        {
            Filters = new Dictionary<int, Regex>();
            if (filters == null ? false : filters.HasValues)
                foreach (var item in filters.ToObject<Dictionary<int, string>>())
                {
                    this.Filters.Add(item.Key, new Regex(item.Value));
                }
            this.ColumnSortingSequence = ColumnSortingSequence == null ? new string[0] : !ColumnSortingSequence.HasValues ? new string[0] : ColumnSortingSequence.ToObject<string[]>();
            this.Descending = Descending == null ? true : !Descending.HasValues ? true : Descending.ToObject<bool>();
        }
    }
}
