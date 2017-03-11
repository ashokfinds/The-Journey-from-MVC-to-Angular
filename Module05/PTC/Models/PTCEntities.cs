using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace PTC.Models
{
  public partial class PTCEntities
  {
    protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items) {
      List<DbValidationError> list = new List<DbValidationError>();
      
      if (entityEntry.Entity is Product) {
        Product entity = entityEntry.Entity as Product;

        // Check ProductName field
        if (string.IsNullOrEmpty(entity.ProductName)) {
          list.Add(new DbValidationError("ProductName",
              "Product Name must be filled in."));
        }
        else { 
          if (entity.ProductName.ToLower() ==
              entity.ProductName) {
            list.Add(new DbValidationError("ProductName",
              "Product Name must not be all lower case."));
          }
          if (entity.ProductName.Length < 4 || 
              entity.ProductName.Length > 150) {
            list.Add(new DbValidationError("ProductName",
              "Product Name must have between 4 and 150 characters."));
          }
        }

        // Check IntroductionDate field
        if (entity.IntroductionDate.HasValue) {
          if (entity.IntroductionDate < DateTime.Now.AddYears(-2)) {
            list.Add(new DbValidationError("IntroductionDate",
              "Introduction date must be within the last two years."));
          }
        }
        else {
          list.Add(new DbValidationError("IntroductionDate",
              "Introduction date must be filled in."));
        }

        // Check Price field
        if (entity.Price.HasValue) {
          if (entity.Price < Convert.ToDecimal(0.1) || 
              entity.Price.Value > Convert.ToDecimal(9999.99)) {
            list.Add(new DbValidationError("Price",
              "Price must be between $0.1 and less than $9,999.99."));
          }
        }
        else {
          list.Add(new DbValidationError("Price",
              "Price must be filled in."));
        }

        // Check Url field
        if (string.IsNullOrEmpty(entity.Url)) {
          list.Add(new DbValidationError("Url",
              "Url must be filled in."));
        }
        else {
          if (entity.Url.Length < 5 ||
              entity.ProductName.Length > 255) {
            list.Add(new DbValidationError("Url",
              "Product Name must have between 5 and 255 characters."));
          }
        }

        if (list.Count > 0) {
          return new DbEntityValidationResult(entityEntry, list);
        }
      }

      return base.ValidateEntity(entityEntry, items);
    }
  }
}