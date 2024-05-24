﻿using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace OnlineStore.Models
{
    public class Product
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Brand { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Category { get; set; } = string.Empty;
        [Precision(16,2)]
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        [MaxLength(100)] 
        public string ImageFileName { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; }
    }
}
