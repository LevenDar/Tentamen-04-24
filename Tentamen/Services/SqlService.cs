using Microsoft.EntityFrameworkCore;
using Tentamen.Models;

namespace Tentamen.Services
{
    public interface IDataAccess
    {
        public Task<Product> CreateProductAsync(ProductRequest request);
        public Task<Product> UpdateProductAsync(int id, ProductRequest request);
        public Task<bool> DeleteProductAsync(int id);
        public Task<Product> GetProductAsync(int id);
        public Task<IEnumerable<Product>> GetAllProductAsync();
        public Task<User> CreateUserAsync(UserRequest request);
        public Task<User> UpdateUserAsync(int id, UserRequest request);
        public Task<bool> DeleteUserAsync(int id);
        public Task<User> GetUserAsync(int id);
        public Task<IEnumerable<User>> GetAllUserAsync();
        public Task<Order> CreateOrderAsync(OrderRequest request);
        public Task<Order> UpdateOrderAsync(int id, OrderRequest request);
        public Task<bool> DeleteOrderAsync(int id);
        public Task<Order> GetOrderAsync(int id);
        public Task<IEnumerable<Order>> GetAllOrderAsync();
    }
    public class SqlService : IDataAccess
    {

        private readonly DataContext _context;

        public SqlService(DataContext context)
        {
            _context = context; 
        }
        public async Task<Order> CreateOrderAsync(OrderRequest request)
        {
            if (!await _context.Orders.AnyAsync(x => x.UserId == request.UserId)) //Måste göra om hela min order och orderrequest
            {
                var orderEntity = new OrderEntity
                {
                    AmountOfProducts = request.AmountOfProducts,
                    UserId = request.UserId,
                    OrderStatus = request.OrderStatus,
                };
                _context.Orders.Add(orderEntity);
                await _context.SaveChangesAsync();
            }
            return null!;           
           
        }
        public async Task<Product> CreateProductAsync(ProductRequest request)
        {
            if (!await _context.Products.AnyAsync(x => x.Name == request.Name))
            {
                var categoryEntity = await _context.Categories.FirstOrDefaultAsync(x => x.Name == request.Categoryname);
                if (categoryEntity == null)
                {
                    categoryEntity = new CategoryEntity { Name = request.Categoryname };
                    _context.Categories.Add(categoryEntity);
                    await _context.SaveChangesAsync();
                }

                var productEntity = new ProductEntity
                {
                    Name = request.Name,
                    Description = request.Description,
                    Price = request.Price,
                    Articalnumber = request.Articalnumber,
                    CategoryId = categoryEntity.Id,
                };
                _context.Products.Add(productEntity);
                await _context.SaveChangesAsync();


                return new Product
                {
                    Id = productEntity.Id,
                    Description = productEntity.Description,
                    Price = productEntity.Price,
                    Articalnumber = productEntity.Articalnumber,
                    Categoryname = productEntity.Category.Name
                };
            }
            return null!;
         }

        public async Task<User> CreateUserAsync(UserRequest request)
        {
            if (!await _context.Users.AnyAsync(x => x.Email == request.Email))
            {
                var userEntity = new UserEntity
                {
                    Email = request.Email,
                    Firstname = request.Firstname,
                    Lastname = request.Lastname,
                    Password = request.Password,
                    Address = request.Address,
                };
                _context.Users.Add(userEntity);
                await _context.SaveChangesAsync();
            }
            return null!;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var orderEntity = await _context.Orders.FindAsync(id);
            if (orderEntity != null)
            {
                _context.Orders.Remove(orderEntity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var productEntity = await _context.Products.FindAsync(id);
            if (productEntity != null)
            {
                _context.Products.Remove(productEntity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var userEntity = await _context.Users.FindAsync(id);
            if (userEntity != null)
            {
                _context.Users.Remove(userEntity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Order>> GetAllOrderAsync()
        {
            var items = new List<Order>();
            foreach (var order in await _context.Orders.ToListAsync())
                items.Add(new Order
                {
                    Id = order.Id,
                    OrderDate = order.OrderDate,
                    AmountOfProducts = order.AmountOfProducts,
                    UserId = order.UserId,
                });
            return items;
        }

        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {
           var items = new List<Product>();
            foreach (var item in await _context.Products.Include(x => x.Category).ToListAsync())
                items.Add(new Product
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    Articalnumber = item.Articalnumber,
                    Description = item.Description,
                    Categoryname = item.Category.Name,
                });
            return items;
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            var items = new List<User>();
            foreach (var item in await _context.Users.ToListAsync())
                items.Add(new User
                {
                    Id = item.Id,
                    Firstname = item.Firstname,
                    Lastname = item.Lastname,
                    Address = item.Address,
                    Email = item.Email,
                   
                });
            return items;
        }

        public async Task<Order> GetOrderAsync(int id)
        {
            var orderEntity = await _context.Orders.FindAsync(id);

            return new Order
            {
                Id = orderEntity.Id,
                AmountOfProducts = orderEntity.AmountOfProducts,
                OrderDate = orderEntity.OrderDate,
                OrderStatus = orderEntity.OrderStatus,
                UserId = orderEntity.UserId,
            };
        }

        public async Task<Product> GetProductAsync(int id)
        {
            var productEntity = await _context.Products.FindAsync(id);

            return new Product
            {
                Id = productEntity.Id,
                Name = productEntity.Name,
                Price = productEntity.Price,
                Articalnumber = productEntity.Articalnumber,
                Description = productEntity.Description,
                Categoryname = productEntity.Category.Name
            };
        }

        public async Task<User> GetUserAsync(int id)
        {
            var userEntity = await _context.Users.FindAsync(id);

            return new User
            {
                Id = userEntity.Id,
                Firstname = userEntity.Firstname,
                Lastname = userEntity.Lastname,
                Address = userEntity.Address,
                Email = userEntity.Email,
            };
        }

        public async Task<Order> UpdateOrderAsync(int id, OrderRequest request)
        {
            var orderEntity = await _context.Orders.FindAsync(id);
            if(orderEntity != null)
            {
                if (orderEntity.AmountOfProducts != request.AmountOfProducts)
                    orderEntity.AmountOfProducts = request.AmountOfProducts;
                if (orderEntity.OrderStatus != request.OrderStatus && !string.IsNullOrEmpty(request.OrderStatus))
                    orderEntity.OrderStatus = request.OrderStatus;

                _context.Entry(orderEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return null!;
        }

        public async Task<Product> UpdateProductAsync(int id, ProductRequest request)
        {
            var productEntity = await _context.Products.FindAsync(id);
            if (productEntity != null)
            {
                if (productEntity.Name != request.Name && !string.IsNullOrEmpty(request.Name))
                    productEntity.Name = request.Name;

                if (productEntity.Name != request.Name && !string.IsNullOrEmpty(request.Name))
                    productEntity.Description = request.Description;

                if (productEntity.Price != request.Price)
                    productEntity.Price = request.Price;

                if (productEntity.Articalnumber != request.Articalnumber)
                    productEntity.Articalnumber = request.Articalnumber;

                if(productEntity.Category.Name != request.Categoryname && !string.IsNullOrEmpty(request.Categoryname)) 
                {
                    var categoryEntity = await _context.Categories.FirstOrDefaultAsync(c => c.Name == request.Categoryname);
                    if (categoryEntity == null)
                    {
                        categoryEntity = new CategoryEntity { Name = request.Categoryname };
                        _context.Categories.Add(categoryEntity);
                        await _context.SaveChangesAsync();
                    }
                    productEntity.CategoryId = categoryEntity.Id;
                }
                   
                _context.Entry(productEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return null!;
        }

        public async Task<User> UpdateUserAsync(int id, UserRequest request)
        {
            var userEntity = await _context.Users.FindAsync(id);
            if (userEntity != null)
            {
                if (userEntity.Firstname != request.Firstname && !string.IsNullOrEmpty(request.Firstname))
                    userEntity.Firstname = request.Firstname;
                if (userEntity.Lastname != request.Lastname && !string.IsNullOrEmpty(request.Lastname))
                    userEntity.Lastname = request.Lastname;
                if (userEntity.Address != request.Address && !string.IsNullOrEmpty(request.Address))
                    userEntity.Address = request.Address;
                if (userEntity.Password != request.Password && !string.IsNullOrEmpty(request.Password))
                    userEntity.Password = request.Password;
                if (userEntity.Email != request.Email && !string.IsNullOrEmpty(request.Email))
                    userEntity.Email = request.Email;
                
                _context.Entry(userEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return null!;
        }
    }
}
