using System;
using System.Collections.Generic;
using System.Linq;

namespace TrainApp.Domain
{
    public class ProductManager
    {
        public List<KeyValuePair<string, string>> ValidationErrors { get; set; }

        public ProductManager()
        {
            ValidationErrors = new List<KeyValuePair<string, string>>();
        }

        public bool IsValid(Product product) {
            ValidationErrors.Clear();

            if(!String.IsNullOrWhiteSpace(product.Name)) {
                if(product.Name.ToLower() == product.Name) {
                    ValidationErrors.Add(new KeyValuePair<string, string>(nameof(product.Name), "Product Name must not be all lower case."));
                }
            }

            return ValidationErrors.Count == 0;
        }

        public bool Insert(Product product) {
            bool isValid = IsValid(product);

            if(isValid) {
                product.Id = _mockData.Max(p => p.Id) + 1;
                _mockData.Add(product);
            }

            return isValid;
        }
        public bool Update(Product product)
        {
            bool isValid = IsValid(product);

            if (isValid) {
                Delete(product.Id);
                _mockData.Add(product);
            }

            return isValid;
        }
        public void Delete(Product product) {
            Delete(product.Id);
        }
        public void Delete(int productId) {
            var product = _mockData.Find(p => p.Id == productId);
            _mockData.Remove(product);
            product = null;
        }


        public List<Product> Get(Product product = null) {
            var products = new List<Product>();
            products = CreateMockData();

            if (product == null) {
                return products;
            }

            if (!string.IsNullOrWhiteSpace(product.Name)) {
                products = products.FindAll(p => p.Name.ToLower().Contains(product.Name.ToLower()));
            }

            return products;
        }
        public Product GetById(int id) {
            return CreateMockData().FirstOrDefault(p => p.Id == id);
        }

        private static List<Product> _mockData;
        private List<Product> CreateMockData()
        {
            if(_mockData == null) {
                _mockData = new List<Product> {
                    new Product {
                        Id = 1,
                        Name = "Consolidating MVC Views Using Single Page Techniques",
                        IntroducedAt = new DateTime(2015, 10, 10),
                        Price = Convert.ToDecimal(10.00),
                        Url = "https://app.pluralsight.com/library/courses/mvc-application-techniques/table-of-contents"
                    },
                    new Product {
                        Id = 2,
                        Name = "A Comparison of Microsoft Web Technologies",
                        IntroducedAt = new DateTime(2016, 04, 12),
                        Price = Convert.ToDecimal(9.95),
                        Url = "https://app.pluralsight.com/library/courses/microsoft-web-technology-comparison/table-of-contents"
                    },
                    new Product {
                        Id = 3,
                        Name = "ASP.NET MVC 5 Fundamentals",
                        IntroducedAt = new DateTime(2013, 11, 05),
                        Price = Convert.ToDecimal(5.89),
                        Url = "https://app.pluralsight.com/library/courses/aspdotnet-mvc5-fundamentals/table-of-contents"
                    }
                };  
            }

            return _mockData;
        }
    }
}
