using Domain.Models;

namespace Domain.Services.Interfaces;

public interface IPriceCalculator
{
    decimal CalculatePrice(IEnumerable<GoodModels> goods, decimal distance);

    IEnumerable<CalculationLogModel> QueryLog(int take);

    void ClearLogs();
}