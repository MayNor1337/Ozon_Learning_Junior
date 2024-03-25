using Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Domain.Separated.Repositories;

namespace Api.Controllers.V1;

public class GoodsViewController : Controller
{
    private readonly IGoodRepository _repository;

    public GoodsViewController(IGoodRepository repository)
    {
        _repository = repository;
    }

    public IActionResult Index()
    {
        var entities = _repository.GetAll();
        var viewModel = new GoodsPageViewModel(entities.Select(x => 
            new GoodViewModel(
                x.Id,
                x.Name,
                x.Lenght,
                x.Width,
                x.Height,
                x.Weight,
                x.Count,
                x.Price)).ToArray());

        return View("/Views/GoodsPage.cshtml", viewModel);
    }
}