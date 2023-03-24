﻿namespace HB.CqrsJwtApp.UI.Models
{
    public class ProductCreateModel
    {
        public string? Name { get; set; }

        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
