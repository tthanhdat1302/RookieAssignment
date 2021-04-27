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
    public class CategoryUnitTest
    {
        private ApplicationDbContext _dbContext;
        private SqliteConnection _connection;

        public CategoryUnitTest()
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
        public async Task GetAllCategories_Ok_3Items()
        {
            for(int i=0;i<3;i++)
            {
                _dbContext.Categories.Add(new Category
                {
                    Name = "Songoku",
                });
                await _dbContext.SaveChangesAsync();
            }   
            
            var controller = new CategoryController(_dbContext);

            var result = await controller.GetCategories();

            Assert.IsType<ActionResult<IEnumerable<CategoryVm>>>(result);
            var cates=result.Value as List<CategoryVm>;
            Assert.Equal(3,cates.Count);
        }

        [Fact]
        public async Task GetCategoryById_Ok()
        {
            for(int i=1;i<4;i++)
            {
                _dbContext.Categories.Add(new Category
                {
                    Name = "Category"+i,
                });
                await _dbContext.SaveChangesAsync();
            }   
            
            var controller = new CategoryController(_dbContext);

            var result = await controller.GetCategory(2);

            Assert.IsType<ActionResult<CategoryVm>>(result);
            var cate=result.Value.Name;
            Assert.Equal("Category2",cate);
        }
        
        [Fact]
        public async Task GetCategoryById_NotOk()
        {
            for(int i=1;i<4;i++)
            {
                _dbContext.Categories.Add(new Category
                {
                    Name = "Category"+i,
                });
                await _dbContext.SaveChangesAsync();
            }   
            
            var controller = new CategoryController(_dbContext);

            var result = await controller.GetCategory(10);

            Assert.IsType<NotFoundResult>(result.Result);    
        }

        [Fact]
        public async Task CreateCategory_ReturnCategoryModel()
        {
            var category = new CategoryCreateRequest
            {
                Name = "Naruto"
            };
            var controller = new CategoryController(_dbContext);
            var result = await controller.PostCategory(category);

            var acceptedResult = Assert.IsType<OkObjectResult>(result);
            // var returnValue = Assert.IsType<Category>(acceptedResult);

            Assert.Equal("Naruto", acceptedResult.Value);
        }

        [Fact]
        public async Task UpdateCategory_Ok()
        {
             for(int i=0;i<4;i++)
            {
                _dbContext.Categories.Add(new Category
                {
                    Name = "Dragon",
                });
                await _dbContext.SaveChangesAsync();
            }   
    
            var controller = new CategoryController(_dbContext);
            var category=new CategoryCreateRequest{
                Name="Tiger"
            };
            var result=await controller.PutCategory(3,category);
            Assert.IsType<AcceptedResult>(result);    

            var cate=await controller.GetCategory(3);
            Assert.Equal("Tiger",cate.Value.Name);
        }

        [Fact]
        public async Task UpdateCategory_NotFound()
        {
             for(int i=0;i<4;i++)
            {
                _dbContext.Categories.Add(new Category
                {
                    Name = "Dragon",
                });
                await _dbContext.SaveChangesAsync();
            }   
    
            var controller = new CategoryController(_dbContext);
            var category=new CategoryCreateRequest{
                Name="Tiger"
            };
            var result=await controller.PutCategory(10,category);

            Assert.IsType<NotFoundResult>(result);    
        }
        
         [Fact]
        public async Task DeleteCategory_Ok()
        {
             for(int i=0;i<4;i++)
            {
                _dbContext.Categories.Add(new Category
                {
                    Name = "Rocky Balboa",
                });
                await _dbContext.SaveChangesAsync();
            }   
    
            var controller = new CategoryController(_dbContext);
            var result=await controller.DeleteCategory(4);
            Assert.IsType<AcceptedResult>(result);    

            var getAllCates=await controller.GetCategories();
            var categories=getAllCates.Value as List<CategoryVm>;
            Assert.Equal(3,categories.Count);

            var getCateDelete=await controller.GetCategory(4);
            Assert.IsType<NotFoundResult>(getCateDelete.Result);
            
        }
         [Fact]
        public async Task DeleteCategory_NotOk()
        {
             for(int i=0;i<4;i++)
            {
                _dbContext.Categories.Add(new Category
                {
                    Name = "Rocky Balboa",
                });
                await _dbContext.SaveChangesAsync();
            }   
    
            var controller = new CategoryController(_dbContext);
            var result=await controller.DeleteCategory(10);
            Assert.IsType<NotFoundResult>(result);        
        }

    }
}