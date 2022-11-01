using SMStore.Entities;

namespace SMStore.WebUIAPIUsing.Models
{
    public class HomePageViewModel
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<News> News { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
        public HomePageViewModel()
        {
            Sliders = new List<Slider>();
            Products = new List<Product>();
            News = new List<News>();
            Brands = new List<Brand>();
        }
    }
}
