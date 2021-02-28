using System;
using System.Text;

namespace test_reports.APITestCoverageReport
{
    public static class Html
    {
        public class Table : HtmlBase, IDisposable
        {
            public Table(StringBuilder sb, string classAttributes = "table table-striped", string id = "tableFilter") : base(sb)
            {
                Append($"<table id=\"{id}\" class=\"{classAttributes}\",data-toggle=\"table\", data-show-columns=\"false\", data-show-toggle=\"false\", " +
                    $"data-show-pagination-switch=\"false\", data-show-refresh=\"false\", data-search=\"true\", data-pagination=\"false\", data-key-events=\"true\",data-url=\"x\">\n");
            }

            public void StartHead()
            {
                Append("<thead>");
            }

            public void EndHead()
            {
                Append("</thead>");
            }

            public void StartFoot()
            {
                Append("<tfoot");
            }

            public void EndFoot()
            {
                Append("</tfoot>");
            }

            public void StartBody()
            {
                Append("<tbody>");
            }

            public void EndBody()
            {
                Append("</tbody>");
            }

            public void Dispose()
            {
                Append("</table>");
            }

            public Row AddRow(string classAttributes = "", string id = "")
            {
                return new Row(GetBuilder(), classAttributes, id);
            }
        }

        public class Row : HtmlBase, IDisposable
        {
            public Row(StringBuilder sb, string classAttributes = "", string id = "") : base(sb)
            {
                Append("\t<tr>\n");
            }
            public void Dispose()
            {
                Append("\t</tr>\n");
            }
            public void AddCell(string innerText)
            {
                Append("\t\t<td>\n");
                Append(innerText);
                Append("\t\t</td>\n");
            }

            public void AddTh(string innerText, string id, string dataSortable)
            {
                Append($"<th data-field=\"{id}\" data-sortable=\"{dataSortable}\">");
                Append(innerText); 
                Append("\t\t</th>\n");
            }
        }

        public abstract class HtmlBase
        {
            private StringBuilder _sb;

            protected HtmlBase(StringBuilder sb)
            {
                _sb = sb;
            }

            public StringBuilder GetBuilder()
            {
                return _sb;
            }

            protected void Append(string toAppend)
            {
                _sb.Append(toAppend);
            }
        }
    }
}
