using Workshop.Api.Bll.Models;
using Workshop.Api.Bll.Services.Interfaces;

namespace Workshop.Api.Bll.Services;

public class PriceCalculator : IPriceCalculator
{
    public double CalculatePrice(IEnumerable<GoodModels> goods)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<CalculationLogModel> QueryLog(int take)
    {
        throw new NotImplementedException();
    }
}