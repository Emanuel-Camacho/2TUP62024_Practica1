using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Ej6Controller : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(decimal precio, string formaPago, string numeroTarjeta = "")
        {
            // Verificar que el precio sea positivo
            if (precio <= 0)
            {
                return BadRequest("El precio debe ser un valor positivo.");
            }

            // Normalizar la forma de pago
            string formaPagoNormalizada = formaPago.Trim().ToLower();

            if (formaPagoNormalizada == "tarjeta")
            {
                // Verificar el número de tarjeta
                if (string.IsNullOrWhiteSpace(numeroTarjeta) || numeroTarjeta.Length != 16 || !long.TryParse(numeroTarjeta, out _))
                {
                    return BadRequest("Número de tarjeta inválido. Debe contener 16 dígitos.");
                }

                // Calcular el recargo del 10%
                decimal recargo = precio * 0.10m;
                decimal totalAPagar = precio + recargo;

                return Ok($"El total a pagar con tarjeta es: {totalAPagar:C2} (incluye un recargo del 10%).");
            }
            else if (formaPagoNormalizada == "efectivo")
            {
                return Ok($"El total a pagar en efectivo es: {precio:C2}.");
            }
            else
            {
                return BadRequest("Forma de pago no válida. Debe ser 'efectivo' o 'tarjeta'.");
            }
        }
    }
}
