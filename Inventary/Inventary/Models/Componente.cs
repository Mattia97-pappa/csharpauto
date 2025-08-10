using System.ComponentModel.DataAnnotations;

namespace Inventary.Models
{
    public class Componente
    {
    
        
        public int id { get; set; }
        public string nome { get; set; }
        public string descrizione { get; set; }

        public string immagine { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "La giacenza non può essere negativa")]
        public int giacenza { get; set; }

        public double prezzo { get; set; }
        public string categoria { get; set; }

        public string codice { get; set; }
    
    
    
    
    }
}
