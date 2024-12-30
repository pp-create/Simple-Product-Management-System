using Microsoft.EntityFrameworkCore;
using Simple_Product_Management_System.Data.DBContext;
using Simple_Product_Management_System.Dto;

namespace Simple_Product_Management_System.BLL.Product
{
    public class ProductReo : IProdect
    {
        private readonly ProductDbContext _context;

        public ProductReo(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            return _context.Products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price
            }).ToList();
        }

        public async Task<ProductDTO> GetByID(int Id)
        {
            var p = await _context.Products.FindAsync(Id);
            if (p == null) return null;
            ProductDTO data = new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price
            };
            return data;
        }

        public async Task<IEnumerable<ProductDTO>> Search(string search)
        {
            return _context.Products
                .Where(p => p.Name.Contains(search))
                .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price
                }).ToList();
        }

        public async Task<IEnumerable<ProductDTO>> Sort(string ColumName)
        {
            var query = _context.Products.AsQueryable();
            query = ColumName switch
            {
                "Name" => query.OrderBy(p => p.Name),
                "Price" => query.OrderBy(p => p.Price),
                "Description" => query.OrderBy(p => p.Description),

                _ => query.OrderBy(p => p.Id)
            };
            var data = await query.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price
            }).ToListAsync();
            return data;
        }

        public async Task<bool> Add(ProductDTO productDTO)
        {
            var product = new Data.Entity.Product
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price,

            };

            _context.Products.Add(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(ProductDTO productDTO)
        {
            var product = await _context.Products.FindAsync(productDTO.Id);
            if (product == null) return false;

            product.Name = productDTO.Name;
            product.Description = productDTO.Description;
            product.Price = productDTO.Price;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(int Id)
        {
            var product = await _context.Products.FindAsync(Id);
            if (product == null) return false;

            _context.Products.Remove(product);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
