﻿namespace Workshop.Api.Bll.Models;

public record ReportModel(
    decimal MaxWeight,
    decimal MaxVolume,
    decimal MaxDistanceForHeaviestGood,
    decimal MaxDistanceForLargestGood,
    decimal WavgPrice);