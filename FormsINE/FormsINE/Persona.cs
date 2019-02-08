using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsINE
{
    public class Persona
    {
        public string ine_id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string CURP { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public Image Imagen { get; set; }
    }
}
