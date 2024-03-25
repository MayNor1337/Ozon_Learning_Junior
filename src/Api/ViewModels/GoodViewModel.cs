namespace Api.ViewModels;

public record GoodsPageViewModel(
    IEnumerable<GoodViewModel> Goods);

public record GoodViewModel(
    int Id,
    string Name, 
    int Lenght,
    int Width,
    int Height,
    decimal Weight,
    int Count,
    decimal Price
);