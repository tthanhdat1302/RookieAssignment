using API.Controllers;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Shared;

namespace UnitTest
{
    public class ProductUnitTest
    {
        private ApplicationDbContext _dbContext;
        private SqliteConnection _connection;

        public ProductUnitTest()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            _connection = new SqliteConnection(connectionStringBuilder.ToString());
            _connection.Open();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(_connection)
                .Options;
            _dbContext = new ApplicationDbContext(options);
            _dbContext.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetAllProducts_Ok_3Items()
        {
            _dbContext.Categories.Add(new Category{Name="Android"});
            await _dbContext.SaveChangesAsync();
            for(int i=16;i<19;i++)
            {
                _dbContext.Products.Add(new Product
                {
                    Name = "Android "+i,
                    Price=1000,
                    Description="Kill Goku",
                    Image="Android.jpg",
                    RatingAVG=5,
                    CategoryId=1,
                });
                await _dbContext.SaveChangesAsync();
            }   
            var controller = new ProductController(_dbContext);

            var result = await controller.GetProducts();

            Assert.IsType<ActionResult<IEnumerable<ProductVm>>>(result);
            var products=result.Value as List<ProductVm>;
            Assert.Equal(3,products.Count);
        }

        [Fact]
        public async Task GetProductById_Ok()
        {
            _dbContext.Categories.Add(new Category{Name="Android"});
            await _dbContext.SaveChangesAsync();
            for(int i=16;i<19;i++)
            {
                _dbContext.Products.Add(new Product
                {
                    Name = "Android "+i,
                    Price=1000,
                    Description="Kill Goku",
                    Image="Android.jpg",
                    RatingAVG=5,
                    CategoryId=1,
                });
                await _dbContext.SaveChangesAsync();
            }   
            var controller = new ProductController(_dbContext);

            var result = await controller.GetProduct(1);

            Assert.IsType<ActionResult<ProductVm>>(result);
            var pro=result.Value.Name;
            Assert.Equal("Android 16",pro);
        }
        
        [Fact]
        public async Task GetProductById_NotOk()
        {
            _dbContext.Categories.Add(new Category{Name="Android"});
            await _dbContext.SaveChangesAsync();
            for(int i=16;i<19;i++)
            {
                _dbContext.Products.Add(new Product
                {
                    Name = "Android "+i,
                    Price=1000,
                    Description="Kill Goku",
                    Image="Android.jpg",
                    RatingAVG=5,
                    CategoryId=1,
                });
                await _dbContext.SaveChangesAsync();
            }   
            var controller = new ProductController(_dbContext);

            var result = await controller.GetProduct(10);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateProduct_Ok()
        {
             _dbContext.Categories.Add(new Category{Name="Ninja"});
            await _dbContext.SaveChangesAsync();
            var product = new ProductCreateRequest
            {
                Name = "Naruto",
                Price=24,
                Description="7th Hokage",
                Image="Naruto.jpg",
                ImageFile=null,
                RatingAVG=5,
                CategoryId=1,
            };
            var controller = new ProductController(_dbContext);
            var result = await controller.PostProduct(product);

            Assert.IsType<OkResult>(result);
            // var returnValue = Assert.IsType<Category>(acceptedResult);
            var pro=await controller.GetProduct(1);
            Assert.NotNull(pro);
            Assert.Equal(product.Name,pro.Value.Name);
            Assert.Equal(product.Price,pro.Value.Price);
            Assert.Equal(product.Description,pro.Value.Description);
            Assert.Equal(product.Image,pro.Value.Image);
            Assert.Equal(product.RatingAVG,pro.Value.RatingAVG);
            Assert.Equal(product.CategoryId,pro.Value.CategoryId);
        }

        [Fact]
        public async Task UpdateProduct_Ok()
        {
            _dbContext.Categories.Add(new Category{Name="Android"});
            await _dbContext.SaveChangesAsync();
            for(int i=16;i<20;i++)
            {
                _dbContext.Products.Add(new Product
                {
                    Name = "Android "+i,
                    Price=1000,
                    Description="Kill Goku",
                    Image="Android.jpg",
                    RatingAVG=3,
                    CategoryId=1,
                });
                await _dbContext.SaveChangesAsync();
            }
             var product = new ProductCreateRequest
            {
                Name = "Cell",
                Price=3000,
                Description="Kill Goku",
                Image="Cell.jpg",
                ImageFile=null,
                RatingAVG=5,
                CategoryId=1,
            };   
            var controller = new ProductController(_dbContext);
            var updatePro=await controller.PutProduct(4,product);
            Assert.IsType<AcceptedResult>(updatePro);

            var pro=await controller.GetProduct(4);
            Assert.Equal(product.Name,pro.Value.Name);
            Assert.Equal(product.Price,pro.Value.Price);
            Assert.Equal(product.Description,pro.Value.Description);
            Assert.Equal(product.Image,pro.Value.Image);
            Assert.Equal(product.RatingAVG,pro.Value.RatingAVG);
            Assert.Equal(product.CategoryId,pro.Value.CategoryId);
        }

        [Fact]
        public async Task UpdateProduct_NotFound()
        {
             _dbContext.Categories.Add(new Category{Name="Android"});
            await _dbContext.SaveChangesAsync();
            for(int i=16;i<20;i++)
            {
                _dbContext.Products.Add(new Product
                {
                    Name = "Android "+i,
                    Price=1000,
                    Description="Kill Goku",
                    Image="Android.jpg",
                    RatingAVG=3,
                    CategoryId=1,
                });
                await _dbContext.SaveChangesAsync();
            }
             var product = new ProductCreateRequest
            {
                Name = "Cell",
                Price=3000,
                Description="Kill Goku",
                Image="Cell.jpg",
                ImageFile=null,
                RatingAVG=5,
                CategoryId=1,
            };   
            var controller = new ProductController(_dbContext);
            var updatePro=await controller.PutProduct(10,product);
            Assert.IsType<NotFoundResult>(updatePro);
        }
        
         [Fact]
        public async Task DeleteProduct_Ok()
        {
            
             _dbContext.Categories.Add(new Category{Name="Android"});
            await _dbContext.SaveChangesAsync();
            for(int i=16;i<20;i++)
            {
                _dbContext.Products.Add(new Product
                {
                    Name = "Android "+i,
                    Price=1000,
                    Description="Kill Goku",
                    Image="Android.jpg",
                    RatingAVG=3,
                    CategoryId=1,
                });
                await _dbContext.SaveChangesAsync();
            }  
            var controller = new ProductController(_dbContext);
            var deletePro=await controller.DeleteProduct(4);
            Assert.IsType<AcceptedResult>(deletePro);

            var proDelete=await controller.GetProduct(4);
            Assert.IsType<NotFoundResult>(proDelete.Result);

            
        }
         [Fact]
        public async Task DeleteProduct_NotOk()
        {
              _dbContext.Categories.Add(new Category{Name="Android"});
            await _dbContext.SaveChangesAsync();
            for(int i=16;i<20;i++)
            {
                _dbContext.Products.Add(new Product
                {
                    Name = "Android "+i,
                    Price=1000,
                    Description="Kill Goku",
                    Image="Android.jpg",
                    RatingAVG=3,
                    CategoryId=1,
                });
                await _dbContext.SaveChangesAsync();
            }  
            var controller = new ProductController(_dbContext);
            var deletePro=await controller.DeleteProduct(10);
            Assert.IsType<NotFoundResult>(deletePro);
        }

    }
}