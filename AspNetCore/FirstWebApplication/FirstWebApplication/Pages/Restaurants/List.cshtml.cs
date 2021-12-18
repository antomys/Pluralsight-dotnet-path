using FirstWebApplication.Dal;
using FirstWebApplication.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FirstWebApplication.Pages.Restaurants;

public class List : PageModel
{
    private readonly IConfiguration _configuration;
    private readonly IRestaurantService _restaurantService;

    public List(IConfiguration configuration, IRestaurantService restaurantService, IEnumerable<Restaurant> restaurants)
    {
        _configuration = configuration
            ?? throw new ArgumentNullException(nameof(configuration));
        _restaurantService = restaurantService
                             ?? throw new ArgumentNullException(nameof(restaurantService));
    }

    public string Message { get; set; }
    public IEnumerable<Restaurant> Restaurants { get; private set; }
    public void OnGet()
    {
        Message = _configuration[nameof(Message)];
        Restaurants = _restaurantService.GetRestaurants();
    }
}