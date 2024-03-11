using Workshop.Api.Bll.Models;

namespace Workshop.Api.Bll.Services.Interfaces;

public interface IAnalyticsCollection
{
    ReportModel GetReports();
}