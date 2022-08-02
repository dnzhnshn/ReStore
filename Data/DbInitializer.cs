using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entity;

namespace API.Data
{
    public class DbInitializer
    {
        public static void Initializer(StoreContext context){

            if(context.Product.Any()) return; 
            
            List<Product> products=new List<Product>(){
                new Product{
                  Name="deneme1",
                  Description="deneme desc",
                  Price=2000,
                  PictureUrl="deneme URL",
                  Type="denem type",
                  Brand="deneme brand",
                  QuantityInStock=20
                },
                new Product{
                  Name="deneme2",
                  Description="deneme desc",
                  Price=2000,
                  PictureUrl="deneme URL",
                  Type="denem type",
                  Brand="deneme brand",
                  QuantityInStock=20
                },
                new Product{
                  Name="deneme3",
                  Description="deneme desc",
                  Price=2000,
                  PictureUrl="deneme URL",
                  Type="denem type",
                  Brand="deneme brand",
                  QuantityInStock=20
                },
                new Product{
                  Name="deneme4",
                  Description="deneme desc",
                  Price=2000,
                  PictureUrl="deneme URL",
                  Type="denem type",
                  Brand="deneme brand",
                  QuantityInStock=20
                },new Product{
                  Name="deneme5",
                  Description="deneme desc",
                  Price=2000,
                  PictureUrl="deneme URL",
                  Type="denem type",
                  Brand="deneme brand",
                  QuantityInStock=20
                },new Product{
                  Name="deneme6",
                  Description="deneme desc",
                  Price=2000,
                  PictureUrl="deneme URL",
                  Type="denem type",
                  Brand="deneme brand",
                  QuantityInStock=20
                },new Product{
                  Name="deneme7",
                  Description="deneme desc",
                  Price=2000,
                  PictureUrl="deneme URL",
                  Type="denem type",
                  Brand="deneme brand",
                  QuantityInStock=20
                },new Product{
                  Name="deneme8",
                  Description="deneme desc",
                  Price=2000,
                  PictureUrl="deneme URL",
                  Type="denem type",
                  Brand="deneme brand",
                  QuantityInStock=20
                },new Product{
                  Name="deneme9",
                  Description="deneme desc",
                  Price=2000,
                  PictureUrl="deneme URL",
                  Type="denem type",
                  Brand="deneme brand",
                  QuantityInStock=20
                }
            };

            foreach (var product in products)
            {
                context.Product.Add(product);
            }
            context.SaveChanges();        
             
        }
    }
}