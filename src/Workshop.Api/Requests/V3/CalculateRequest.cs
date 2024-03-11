using Workshop.Api.Responses.V3;

namespace Workshop.Api.Requests.V3;

public record CalculateRequest(GoodProperties[] Goods, int Distance);