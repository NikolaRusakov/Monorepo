using Carter;

namespace ContactsApi.Modules;

public class ProductModule : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/api/products", GetProducts);
		app.MapGet("/api/products/{id:int}", GetProduct);
		app.MapPost("/api/products", CreateProduct);
		app.MapPut("/api/products/{id:int}", UpdateProduct);
		app.MapDelete("/api/products/{id:int}", DeleteProduct);
	}

	public static async Task<IResult> GetProducts(IProductService productService)
	{
		var products = await productService.GetAllProductsAsync();
		return Results.Ok(products);
	}

	public static async Task<IResult> GetProduct(int id, IProductService productService)
	{
		var product = await productService.GetProductByIdAsync(id);
		return product is not null ? Results.Ok(product) : Results.NotFound();
	}

	public static async Task<IResult> CreateProduct(Product product, IProductService productService)
	{
		var createdProduct = await productService.CreateProductAsync(product);
		return Results.Created($"/api/products/{createdProduct.Id}", createdProduct);
	}

	public static async Task<IResult> UpdateProduct(int id, Product product, IProductService productService)
	{
		var updatedProduct = await productService.UpdateProductAsync(product);
		return updatedProduct is not null ? Results.Ok(updatedProduct) : Results.NotFound();
	}

	public static async Task<IResult> DeleteProduct(int id, IProductService productService)
	{
		var success = await productService.DeleteProductAsync(id);
		return success ? Results.NoContent() : Results.NotFound();
	}
}
