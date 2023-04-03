namespace JavidElectronics.Areas.Client.ViewModels.OrderProducts
{
    public class OrderProductsViewModel
    {
        public List<ListItem>? Products { get; set; }
        public SummaryTotal? Summary { get; set; }

        public class SummaryTotal
        {
            public decimal Total { get; set; }
        }

        public class ListItem
        {
            public string? Name { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public decimal Total { get; set; }
        }
    }
}
