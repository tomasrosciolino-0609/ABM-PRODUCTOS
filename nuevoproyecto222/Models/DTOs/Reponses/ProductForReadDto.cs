using System;
using System.Collections.Generic;
using System.Text;

namespace nuevoproyecto222.Models.DTOs.Reponses
{
    public class ProductForReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
