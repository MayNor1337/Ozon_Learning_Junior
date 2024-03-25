using Api.Responses.V3;

namespace Api.Requests.V3;

public record CalculateRequest(GoodProperties[] Goods, int Distance);