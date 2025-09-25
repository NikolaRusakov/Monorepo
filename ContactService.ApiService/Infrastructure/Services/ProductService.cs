public class ProductService : IProductService
{
	private static readonly List<Product> _products = new()
	{
		new(1, "Laptop", 999.99m, "High-performance laptop"),
		new(2, "Mouse", 29.99m, "Wireless mouse"),
		new(3, "Keyboard", 79.99m, "Mechanical keyboard")
	};

	public Task<IEnumerable<Product>> GetAllProductsAsync()
	{
		return Task.FromResult<IEnumerable<Product>>(_products);
	}

	public Task<Product?> GetProductByIdAsync(int id)
	{
		var product = _products.FirstOrDefault(p => p.Id == id);
		return Task.FromResult(product);
	}

	public Task<Product> CreateProductAsync(Product product)
	{
		var newId = _products.Max(p => p.Id) + 1;
		var newProduct = product with { Id = newId };
		_products.Add(newProduct);
		return Task.FromResult(newProduct);
	}

	public Task<Product?> UpdateProductAsync(Product product)
	{
		var index = _products.FindIndex(p => p.Id == product.Id);
		if (index == -1) return Task.FromResult<Product?>(null);

		_products[index] = product;
		return Task.FromResult<Product?>(product);
	}

	public Task<bool> DeleteProductAsync(int id)
	{
		var product = _products.FirstOrDefault(p => p.Id == id);
		if (product is null) return Task.FromResult(false);

		_products.Remove(product);
		return Task.FromResult(true);
	}
}

