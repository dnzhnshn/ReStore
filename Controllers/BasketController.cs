using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using API.Data;
using API.DTO;
using API.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly StoreContext storeContext;
        public BasketController(StoreContext storeContext)
        {
            this.storeContext = storeContext;
        }
        [HttpGet(Name="GetBasket")]
        public async Task<ActionResult<BasketDto>> GetBasket()
        {
            var basket = await RetrieveBasket();
            if (basket == null)
                return NotFound();

            return MapBasketToDto(basket);
        }
        [HttpPost]
        public async Task<ActionResult<BasketDto>> AddItemToBasket(int productId, int quantity)
        {
            Basket basket= await RetrieveBasket();
            if(basket==null)
            basket=CreateBasket();
            Product product = await storeContext.Product.FindAsync(productId);
            if (product == null) {
                return NotFound();
            }
            basket.AddItem(product, quantity);
            bool result = await storeContext.SaveChangesAsync() > 0;
            if (result)
                return CreatedAtRoute("GetBasket",MapBasketToDto(basket));

            return BadRequest(new ProblemDetails { Title = "There is problem while save to basket" });
        }
        [HttpDelete]
        public async Task<ActionResult> RemoveItemToBasket(int productId, int quantity)
        {
            var basket=await RetrieveBasket();
            if(basket==null){
                return NotFound();
            }
            basket.RemoveItem(productId,quantity);
            storeContext.SaveChanges();
            return Ok();
        }

        private BasketDto MapBasketToDto(Basket basket){
            return new BasketDto()
            {
                Id = basket.Id,
                BuyerId = basket.BuyerId,
                Items=basket.Items.Select(i=>new BasketItemDto{
                    ProductId=i.ProductId,
                    Name=i.Product.Name,
                    Price=i.Product.Price,
                    PictureUrl=i.Product.PictureUrl,
                    Type=i.Product.Type,
                    Brand=i.Product.Brand,
                    Quantity=i.Quantity
                }).ToList()
            };
        }

        private async Task<Basket> RetrieveBasket()
        {
            return await storeContext.Basket.
            Include(i => i.Items).
            ThenInclude(p => p.Product).
            FirstOrDefaultAsync(x => x.BuyerId == Request.Cookies["buyerId"]);
        }
        private Basket CreateBasket()
        {
            var buyerId = Guid.NewGuid().ToString();
            var cookieOption = new CookieOptions() { IsEssential = true, Expires = DateTime.Now.AddDays(30) };
            Response.Cookies.Append("buyerId", buyerId, cookieOption);
            Basket basket = new Basket() { BuyerId = buyerId };
            storeContext.Basket.Add(basket);
            return basket;
        }
    }
}