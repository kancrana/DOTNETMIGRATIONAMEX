using eShop.Domain.ProductCatalog;
using eShop.Domain.ProductCatalog.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace eShop.Repository.SqlClient.ProductCatalog
{
    public class ProductRepository : IProductRepository
    {
        private string connectionString = GetConnection();

        private static string GetConnection()
        {
            return DBHelper.GetConnection(DB.Product);
        }

        public void Add(Product entity)
        {
            throw new NotImplementedException();
        }

        public void BulkInsert(IEnumerable<Product> products)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> FindAll()
        {
            IList<Product> productList = new List<Product>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Product]", connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        productList.Add(ReadProduct(reader));
                    }
                }
                return productList;
            }
        }

        public async Task<IEnumerable<Product>> FindAllAsync()
        {
            IList<Product> productList = new List<Product>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Product]", connection);
                await connection.OpenAsync();
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        productList.Add(ReadProduct(reader));
                    }
                }
                return productList;
            }
        }

        public Product FindBy(int id)
        {
            Product product = new Product();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Product] WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        product = (ReadProduct(reader));
                    }
                }
                return product;
            }
        }

        public async Task<Product> FindByIdAsync(int id)
        {
            Product product = new Product();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Product] WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                await connection.OpenAsync();
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        product = (ReadProduct(reader));
                    }
                }
                return product;
            }
        }

        public async Task<IEnumerable<ProductBrand>> GetProductBrandsAsync()
        {
            IList<ProductBrand> brands = new List<ProductBrand>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[ProductBrand]", connection);
                await connection.OpenAsync();
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        brands.Add(new ProductBrand
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"]
                        });
                    }
                }
                return brands;
            }
        }

        public async Task<IEnumerable<ProductType>> GetProductTypesAsync()
        {
            IList<ProductType> productTypes = new List<ProductType>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[ProductType]", connection);
                await connection.OpenAsync();
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        productTypes.Add(new ProductType
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"]
                        });
                    }
                }
                return productTypes;
            }
        }

        public void Save(Product entity)
        {
            throw new NotImplementedException();
        }

        public PaginatedViewModel<Product> SearchByName(string name, int pageSize, int pageIndex)
        {
            IList<Product> productList = new List<Product>();
            long totalItems = 0;
            string query = "SELECT * FROM[dbo].[Product] WHERE [Name] LIKE '%' + @SearchKey +'%' ORDER BY[Id] OFFSET(@PageIndex) * @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SearchKey", name);
                command.Parameters.AddWithValue("@PageIndex", pageIndex);
                command.Parameters.AddWithValue("@PageSize", pageSize);

                SqlCommand command2 = new SqlCommand("SELECT Count(1) [Count] FROM [dbo].[Product]", connection);
                connection.Open();
                using (SqlDataReader reader = command2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        totalItems = Convert.ToInt64(reader["Count"]);
                    }
                }

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        productList.Add(ReadProduct(reader));
                    }
                }
                return new PaginatedViewModel<Product>(pageIndex, pageSize, totalItems, productList);
            }
        }

        public async Task<PaginatedViewModel<Product>> SearchByNameAsync(string name, int pageSize, int pageIndex)
        {
            IList<Product> productList = new List<Product>();
            long totalItems = 0;
            string query = "SELECT * FROM[dbo].[Product] WHERE [Name] LIKE '%' + @SearchKey +'%' ORDER BY[Id] OFFSET(@PageIndex) * @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SearchKey", name);
                command.Parameters.AddWithValue("@PageIndex", pageIndex);
                command.Parameters.AddWithValue("@PageSize", pageSize);

                SqlCommand command2 = new SqlCommand("SELECT Count(1) [Count] FROM [dbo].[Product]", connection);
                await connection.OpenAsync();
                using (SqlDataReader reader = await command2.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        totalItems = Convert.ToInt64(reader["Count"]);
                    }
                }

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        productList.Add(ReadProduct(reader));
                    }
                }
                return new PaginatedViewModel<Product>(pageIndex, pageSize, totalItems, productList);
            }
        }

        public PaginatedViewModel<Product> SearchByTypeAndBrand(int typeId, int? brandId, int pageSize, int pageIndex)
        {
            IList<Product> productList = new List<Product>();
            long totalItems = 0;
            string query = "SELECT * FROM[dbo].[Product] WHERE [ProductTypeId] = @SearchKey  AND ([ProductBrandId] IS NOT NULL OR [ProductBrandId] = @BrandId) ORDER BY[Id] OFFSET(@PageIndex) * @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SearchKey", typeId);
                command.Parameters.AddWithValue("@PageIndex", pageIndex);
                command.Parameters.AddWithValue("@PageSize", pageSize);
                command.Parameters.AddWithValue("@BrandId", brandId);

                SqlCommand command2 = new SqlCommand("SELECT Count(1) [Count] FROM [dbo].[Product]", connection);
                connection.Open();
                using (SqlDataReader reader = command2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        totalItems = Convert.ToInt64(reader["Count"]);
                    }
                }

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        productList.Add(ReadProduct(reader));
                    }
                }
                return new PaginatedViewModel<Product>(pageIndex, pageSize, totalItems, productList);
            }
        }

        public async Task<PaginatedViewModel<Product>> SearchByTypeAndBrandAsync(int typeId, int? brandId, int pageSize, int pageIndex)
        {
            IList<Product> productList = new List<Product>();
            long totalItems = 0;
            string query = "SELECT * FROM[dbo].[Product] WHERE [ProductTypeId] = @SearchKey  AND ([ProductBrandId] IS NOT NULL OR [ProductBrandId] = @BrandId) ORDER BY[Id] OFFSET(@PageIndex) * @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SearchKey", typeId);
                command.Parameters.AddWithValue("@PageIndex", pageIndex);
                command.Parameters.AddWithValue("@PageSize", pageSize);
                command.Parameters.AddWithValue("@BrandId", brandId);

                SqlCommand command2 = new SqlCommand("SELECT Count(1) [Count] FROM [dbo].[Product]", connection);
                await connection.OpenAsync();
                using (SqlDataReader reader = await command2.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        totalItems = Convert.ToInt64(reader["Count"]);
                    }
                }

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        productList.Add(ReadProduct(reader));
                    }
                }
                return new PaginatedViewModel<Product>(pageIndex, pageSize, totalItems, productList);
            }
        }

        private Product ReadProduct(SqlDataReader reader)
        {
            return new Product
            {
                Id = (int)reader["Id"],
                Name = (string)reader["Name"],
                Description = (string)reader["Description"],
                Price = (decimal)reader["Price"],
                OldPrice = (decimal)reader["OldPrice"],
                PictureUri = (string)reader["PictureFileName"],
                ProductTypeId = (int)reader["ProductTypeId"],
                ProductBrandId = (int)reader["ProductBrandId"],
            };
        }
    }
}
