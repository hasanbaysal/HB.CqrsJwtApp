﻿namespace HB.CqrsJwtApp.Core.Domain
{
    public class Category
    {
        public int Id { get; set; }
        public string? Definition { get; set; }

        public List<Product>? Products { get; set; }

       
    }

}

