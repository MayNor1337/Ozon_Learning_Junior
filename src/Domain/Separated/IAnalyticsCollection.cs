using Domain.Models;

namespace Domain.Separated;

public interface IAnalyticsCollection
{
    ReportModel GetReports();
}