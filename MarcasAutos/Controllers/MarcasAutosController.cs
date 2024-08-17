using MarcasAutos.Data;
using MarcasAutos.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarcasAutos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcasAutosController : ControllerBase
    {
        //Dependency injection del MarcaAutosDbContext para
        //tener accesso a la base de datos.
        private readonly MarcasAutosDbContext _marcasContext;
        public MarcasAutosController(MarcasAutosDbContext context)
        {
            _marcasContext = context;
        }

        /** 
        Regresa una lista de todos las marcas encontradas bajo la tabla 
        MarcasAutos
        ---------------------------------------------------------------
        Method: 'GET'
        Route: 'http://localhost:5233/api/MarcaAutos'
        Return: [
                    {
                        "id": 0,
                        "nombre": "string",
                        "creador": "string"
                    }
                ]
        */
        [HttpGet]
        public async Task<ActionResult<List<MarcaAuto>>> GetTodasMarcas()
        {
            var marcas = await _marcasContext.MarcasAutos.ToListAsync();
            return Ok(marcas);
        }
    }
}