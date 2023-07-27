using Microsoft.AspNetCore.Mvc;
using sipay_bootcamp_cohorts_homework1.Models;

namespace sipay_bootcamp_cohorts_homework1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private static List<Product> ProductList = new List<Product>()
    {
        new Product()
        {
            Id = 1,
            Title = "Asus",
            Model = "VivoBook X1502ZA-EJ1066AS",
            Price = 15099
        },
        new Product()
        {
            Id = 2,
            Title = "Lenovo",
            Model = "Thinkbook 16 21CY004RTX02",
            Price = 38008
        },
        new Product()
        {
            Id = 3,
            Title = "Dell",
            Model = "Latitude 3520-17",
            Price = 29806
        },
    };

    //GET
    [HttpGet]
    public List<Product> GetProducts()
    {
        return ProductList;
    }

    //GET
    [HttpGet("{id}")]
    public Product GetById(int id)
    {
        var product = ProductList.Where(product => product.Id == id).SingleOrDefault();
        return product;
    }

    //POST
    [HttpPost]
    public IActionResult AddProduct([FromBody] Product newProduct)
    {
        var product = ProductList.SingleOrDefault(x => x.Title == newProduct.Title);

        if (product != null)
            return BadRequest();

        ProductList.Add(newProduct);
        return Ok();
    }

    //PUT
    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, [FromBody] Product updatedProduct)
    {
        var product = ProductList.SingleOrDefault(x => x.Id == id);

        if (product == null)
            return BadRequest();

        product.Title = updatedProduct.Title != default ? updatedProduct.Title : product.Title;
        product.Model = updatedProduct.Model != default ? updatedProduct.Model : product.Model;
        product.Price = updatedProduct.Price != default ? updatedProduct.Price : product.Price;
        return Ok();
    }

    //PATCH
    [HttpPatch("{id}")]
    public IActionResult UpdateProductTitle(int id, [FromBody] Product updatedTitle)
    {
        var product = ProductList.SingleOrDefault(x => x.Id == id);

        if (product == null)
            return BadRequest();

        product.Title = updatedTitle.Title != default ? updatedTitle.Title : product.Title;
        return Ok();
    }

    //DELETE
    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var product = ProductList.SingleOrDefault(x => x.Id == id);

        if (product == null)
            return BadRequest();

        ProductList.Remove(product);
        return Ok();
    }

    //SORT
    [HttpGet("sort")]
    public IActionResult GetSortProduct(string sortBy)
    {
        if (sortBy == "title")
        {
            var sortedProduct = ProductList.OrderBy(title => title.Title); //ascending sort by title
            return Ok(sortedProduct);
        }
        else if (sortBy == "price")
        {
            var sortedProduct = ProductList.OrderBy(price => price.Price); //ascending sort by price
            return Ok(sortedProduct);
        }
        return BadRequest();
    }

    //FILTER
    [HttpGet("list")]
    public IActionResult GetProductByTitle([FromQuery] string title)
    {
        var filteredProduct = ProductList.Where(p => p.Title.Contains(title)).ToList();
        return Ok(filteredProduct);
    }
}