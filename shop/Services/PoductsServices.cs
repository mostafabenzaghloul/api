using Microsoft.EntityFrameworkCore;
using shop.Models;

namespace shop.Services
{
    public class PoductsServices:IProductsServices
    {
        private readonly ApplicationDbContext _context;

        public PoductsServices(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Product> Add(Product movie)
        {
            await _context.AddAsync(movie);
            _context.SaveChanges();

            return movie;
        }

        public Product Delete(Product movie)
        {
            _context.Remove(movie);
            _context.SaveChanges();

            return movie;
        }

        public async Task<IEnumerable<Product>> GetAll(int genreId = 0)
        {
            return await _context.products
                .Where(m => m.CategoryId == genreId || genreId == 0)
                .OrderByDescending(m => m.Rate)
                .Include(m => m.Category)
                .ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.products.Include(m => m.Category).SingleOrDefaultAsync(m => m.Id == id);
        }

        public Product Update(Product movie)
        {
            _context.Update(movie);
            _context.SaveChanges();

            return movie;
        }
    }
}
