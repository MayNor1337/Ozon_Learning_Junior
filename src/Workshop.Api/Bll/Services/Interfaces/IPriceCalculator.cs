using Workshop.Api.Bll.Models;

namespace Workshop.Api.Bll.Services.Interfaces;

public interface IPriceCalculator
{
    decimal CalculatePrice(IEnumerable<GoodModels> goods, decimal distance);

    IEnumerable<CalculationLogModel> QueryLog(int take);

    void ClearLogs();
}