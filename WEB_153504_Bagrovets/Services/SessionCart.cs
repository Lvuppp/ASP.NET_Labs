using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using Web_153504_Bagrovets.Domain.Entities;
using Web_153504_Bagrovets_Lab1.Extension;

namespace Web_153504_Bagrovets_Lab1.Services
{
    public class SessionCart : Cart
    { 
        public static Cart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>().HttpContext?.Session;
            SessionCart cart = session?.Get<SessionCart>("CartViewComponent") ?? new SessionCart();
            cart.Session = session;
            return cart;
        }

        [JsonIgnore]
        public ISession? Session { get; set; }

        public override void AddItem(Product product, int count)
        {
            base.AddItem(product, count);
            Session?.Set("CartViewComponent", this);
        }

        public override void RemoveItem(Product product)
        {
            base.RemoveItem(product);
            Session?.Set("CartViewComponent", this);
        }

        public override void Clear()
        {
            base.Clear();
            Session?.Remove("CartViewComponent");
        }
    }
}
