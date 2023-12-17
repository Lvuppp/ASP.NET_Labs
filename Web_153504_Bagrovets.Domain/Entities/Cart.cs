using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Web_153504_Bagrovets.Domain.Entities
{
    public class Cart
    {
        /// <summary>
        /// Список объектов в корзине
        /// key - идентификатор объекта
        /// </summary>
        public Dictionary<int, CartItem> CartItems { get; set; } = new();
        /// <summary>
        /// Добавить объект в корзину
        /// </summary>
        /// <param name="dish">Добавляемый объект</param>
        public virtual void AddItem(Product product, int count)
        {
            CartItem? item = CartItems.Values
            .Where(p => p.Item.Id == product.Id)
            .FirstOrDefault();
            if (item == null)
            {
                CartItems.Add(product.Id,new CartItem
                {
                    Item = product,
                    Count = count
                });
            }
            else
            {
                item.Count += count;
            }
        }
        public virtual void RemoveItem(Product product) =>
        CartItems.Remove(product.Id);
        public virtual void Clear() => CartItems.Clear();
        public double Count {
            get => CartItems.Sum(item => item.Value.Count); }
        /// <summary>
        /// Общее количество калорий
        /// </summary>
        public double TotalPrice
        {
            get => CartItems.Sum(item => item.Value.Item.Price * item.Value.Count);
        }
    }
}
