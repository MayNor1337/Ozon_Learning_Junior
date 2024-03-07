﻿using Workshop.Api.Bll.Models;

namespace Workshop.Api.Bll.Services.Interfaces;

public interface IPriceCalculator
{
    double CalculatePrice(IEnumerable<GoodModels> goods);

    IEnumerable<CalculationLogModel> QueryLog(int take);
}