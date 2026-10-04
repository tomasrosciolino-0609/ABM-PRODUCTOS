namespace nuevoproyecto222.Models.DTOs.Reponses
{
    public class ProductStatsDto
    {
        public int Total { get; set; }
        public decimal AveragePrice { get; set; }
        public string MostExpensiveName { get; set; } = string.Empty;
    }
}
