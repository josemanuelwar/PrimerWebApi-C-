using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPIAutores.Validaciones;
namespace WebAPIAutores.Entidades
{
    public class Autor : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 5,ErrorMessage = "El campo {0} no debe tenet mas de {1} carateres")]
        //[PrimeraLetraMayusculaAttribute]
        public string Nombre { get; set; }
        
        [Range(18,120)]
        [NotMapped]
        public int Edad { get; set; }
        
        [CreditCard]
        [NotMapped]
        public string TarjetaDeCredito { get; set; }

        [Url]
        [NotMapped]
        public string url { get; set; }
        public List<Libro> Libros { get; set; }
        
        [NotMapped]
        public int Menor { get; set; }
        
        [NotMapped]
        public int Mayor { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext){
            if(!string.IsNullOrEmpty(this.Nombre)){
                var primeraLetra = this.Nombre[0].ToString();
                if(primeraLetra != primeraLetra.ToUpper()){
                    yield return new ValidationResult("La primera letra debe ser mayuscula",
                    new string[]{nameof(Nombre)});
                }
            }

            if(this.Menor > this.Mayor){
                yield return new ValidationResult("El numero menor no puede ser mayor que el numero mayor",
                new string []{nameof(this.Menor)});
            }
        }

    }
}