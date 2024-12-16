using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;

namespace Web_Demo.Controllers
{
    public class ViewController : Controller
    {
        // GET: View
        public IActionResult Reports()
        {
            return View();
        }

        public IActionResult GetReport(string id = "SimpleList")
        {
            // Create the report object
            var report = new StiReport();

            report.Load(StiNetCoreHelper.MapPath(this, "/Reports/" + id + ".mrt"));

            Stopwatch watch = Stopwatch.StartNew();

            var actionResult = StiNetCoreViewer.GetReportResult(this, report);

            watch.Stop();
            LogStopwatch(watch, "[ViewController]", "GetReport", "GetReportResult");

            return actionResult;
        }

        public IActionResult ViewerEvent()
        {
            return StiNetCoreViewer.ViewerEventResult(this);
        }

        public IActionResult Design(string id)
        {
            return RedirectToAction("Reports", "Design", new { id });
        }

        /// <summary>
        /// Performance trace.
        /// </summary>
        private void LogStopwatch(Stopwatch watch, params string[] sections)
        {
            System.Console.WriteLine(
                "STOPWATCH {0}: {1:n2} ms",
                string.Join(" > ", sections),
                watch.Elapsed.TotalSeconds);
        }

    }
}