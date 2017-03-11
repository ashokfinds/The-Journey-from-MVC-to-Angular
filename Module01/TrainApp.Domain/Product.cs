using System;
using System.ComponentModel.DataAnnotations;

namespace TrainApp.Domain
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Name must be filled in.")]
        [Display(Description ="Product Name")]
        [StringLength(150, MinimumLength = 4, ErrorMessage = "Product Name must be greater than {2} characters and less than {1} characters.")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Introduction Date must be filled in.")]
        [Display(Description = "Introduction Date")]
        public DateTime IntroducedAt { get; set; }

        [Required(ErrorMessage = "URL must be filled in.")]
        [Display(Description = "URL")]
        [StringLength(2000, MinimumLength = 5, ErrorMessage = "URL must be greater than {2} characters and less than {1} characters.")]
        public string Url { get; set; }

        [Range(1, 9999, ErrorMessage = "Price must be between {1} and {2}.")]
        [Display(Description = "Price")]
        public decimal Price { get; set; }
    }
}
