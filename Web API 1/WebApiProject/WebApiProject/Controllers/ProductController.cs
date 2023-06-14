using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace WebApiProject.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Product>> GetAll()
        {
            return Products;
        }

        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id) 
        {
            return Products.Single(x => x.Id == id);
        }

        [HttpPost]
        public ActionResult Create(Product model)
        {            
            model.Id = Products.Count() + 1;
            Products.Add(model);

            //Este método 'CreatedAtAction' retorna:
            // 1. Un status code 201 de recurso creado
            // 2. En las cabeceras (headers) indica donde está el nuevo recurso en la propiedad location
            return CreatedAtAction(
                "Get",
                new { id = model.Id},
                model
                );
        }

        [HttpPut("{productId}")]
        public ActionResult Update(int productId, Product model)
        {
            var originalEntry = Products.Single(x => x.Id == productId);
            originalEntry.Name = model.Name;
            originalEntry.Description = model.Description;
            originalEntry.Price = model.Price;

            //Retorna un status code 204 (Se actualizó correctamente)
            //Los valores se actualizaron automáticamente porque son mutables. Los objetos están en memoria
            //y se está actualizando el mismo objeto consultado y retornado en la variable originalEntry (mutabilidad)
            return NoContent();
        }

        [HttpDelete ("{productId}")]
        public ActionResult Delete(int productId)
        {
            Products = Products.Where(x => x.Id != productId).ToList();
            return NoContent();
        }

        public static List<Product> Products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "Guitarra Electrica",
                Price = 1200,
                Description = "Ideal para tocar jazz, blues y rock"

            },
            new Product
            {
                Id = 2,
                Name = "Amplificador de guitarra Electrica",
                Price = 1200,
                Description = "Excelente amplificador con un sonido cálido"
            }
        };


    }
}
