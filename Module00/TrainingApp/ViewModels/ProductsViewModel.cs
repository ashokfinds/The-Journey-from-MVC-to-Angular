using System;
using System.Collections.Generic;
using TrainApp.Domain;
using TrainingApp.Common.ViewModel;

namespace TrainingApp.ViewModels
{
    public class ProductsViewModel : ViewModelBase
    {
        public List<Product> Products { get; set; }
        public Product SearchProduct { get; set; }
        public Product EntityProduct { get; set; }

        public ProductsViewModel() : base() { }      

        protected override void ResetSearch() {
            SearchProduct = new Product();
            base.ResetSearch();
        }

        protected override void Get() {
            var productManager = new ProductManager();
            Products = productManager.Get(SearchProduct);
            base.Get();
        }

        protected override void Add() {
            IsEntityValid = true;
            EntityProduct = new Product {
                IntroducedAt = DateTime.UtcNow,
                Url = "http://",
                Price = Convert.ToDecimal(10)
            };
            base.Add();
        }
        protected override void Edit() {
            var productManager = new ProductManager();
            EntityProduct = productManager.GetById(Convert.ToInt32(EventArgument));                        
            base.Edit();
        }
        protected override void Delete()
        {
            var productManager = new ProductManager();
            productManager.Delete(Convert.ToInt32(EventArgument));
            base.Delete();
        }
        protected override void Save() {
            var productManager = new ProductManager();

            if (Mode == "Add") {
                productManager.Insert(EntityProduct);
            }
            if (Mode == "Edit") {
                productManager.Update(EntityProduct);
            }

            ValidationErrors = productManager.ValidationErrors;

            base.Save();
        }

        protected override void Init()
        {            
            Products = new List<Product>();
            EntityProduct = new Product();
            base.Init();
        }

    }
}