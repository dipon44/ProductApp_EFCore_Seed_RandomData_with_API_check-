using Bogus;
using EntityFrameworkHelpLine.ApplicationDBContext;
using EntityFrameworkHelpLine.Model;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkHelpLine.Services
{
    public interface ITestService
    {
        Task SeedDataAsync();
    }

    public class TestService : ITestService
    {
        private readonly ApplicationDbContext _dbContext;

        public TestService(ApplicationDbContext context)
        {
            _dbContext = context;
        }
        public async Task SeedDataAsync()
        {
            //if (_dbContext.Products.Count() > 0)
            //{
            //    return;
            //}
            var testCategory = new Faker<Category>()          
                .RuleFor(o => o.Name, f => f.Name.FullName())
                .RuleFor(o => o.Description, f => f.Lorem.Paragraph());

            var testReview = new Faker<Review>()
                .RuleFor(o => o.Name, f => f.Name.FullName())
                .RuleFor(o => o.Comment, f => f.Lorem.Paragraph())
                .RuleFor(o => o.Rating, f => f.Random.Int(1,5));

            var testCategoryProduct = new Faker<CategoryProduct>()
                .RuleFor(c => c.Category, f => testCategory.Generate());

            var testProduct = new Faker<Product>()
                .RuleFor(p => p.Name, f => f.Name.FullName())
                .RuleFor(p => p.Description, f => f.Lorem.Paragraph())
                .RuleFor(p => p.Price, f => f.Random.Decimal(100, 200000))
                .RuleFor(p => p.Reviews, f => testReview.Generate(5).ToList())
                .RuleFor(p => p.CategoryData, f => testCategoryProduct.Generate(2).ToList());

            var products = testProduct.Generate(100);
           await _dbContext.AddRangeAsync(products);
           await _dbContext.SaveChangesAsync();
        }
    }
}
