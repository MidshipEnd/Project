using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Domain;
using Infrastructure;

namespace Presentation.WebApp.Controllers;

public class VentasController : Controller
{
    private readonly VentasDbContext _ventasDbContext;
    private readonly ClientesDbContext _clientesDbContext;
    private readonly ProductosDbContext _productosDbContext;
    public VentasController(IConfiguration configuration)
    {
        _ventasDbContext = new VentasDbContext(configuration.GetConnectionString("DefaultConnection"));
        _clientesDbContext = new ClientesDbContext(configuration.GetConnectionString("DefaultConnection"));
         _productosDbContext = new ProductosDbContext(configuration.GetConnectionString("DefaultConnection"));
    }

    public IActionResult Index()
    {
        var data = _ventasDbContext.List();
        return View(data);
    }

    public IActionResult Details(Guid id)
    {
        var data = _ventasDbContext.Details(id);
        return View(data);
    }

    public IActionResult Create()
    {
        ViewBag.Cliente = _clientesDbContext.List();
        ViewBag.Producto = _productosDbContext.List();
        return View();
    }
    [HttpPost]
    public IActionResult Create(Venta data)
    {
        _ventasDbContext.Create(data);
        //SmtpClientEmailService.SendEmail(data.Cliente.Correo, "Asunto", $"<h4>Hola {data.Cliente.Nombre}</h1>", true);
        return RedirectToAction("Index");
    }

    public IActionResult Edit(Guid id)
    {
        var data = _ventasDbContext.Details(id);
        ViewBag.Cliente = _clientesDbContext.List();
        ViewBag.Producto = _productosDbContext.List();
        return View(data);
    }
    [HttpPost]
    public IActionResult Edit(Venta data)
    {
        _ventasDbContext.Edit(data);
        ViewBag.Cliente = _clientesDbContext.List();
        ViewBag.Producto = _productosDbContext.List();
        return RedirectToAction("Index");
    }

    public IActionResult Delete(Guid id)
    {
        _ventasDbContext.Delete(id);
        return RedirectToAction("Index");
    }
}