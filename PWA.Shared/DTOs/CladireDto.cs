using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWA.Shared.DTOs
{
	public sealed class CladireDto
	{
		[Required]
		public string Tip_Strada { get; set; }
        [Required]
		public string Denumire_Strada { get; set; }
		[Required]
		public string Numar { get; set; }

        [Required]
        public string Bloc { get; set; }
        [Required]
        public string Scara { get; set; }
        [Required]
        public string Stadiul_Blocului { get; set; }
        [Required]
        public string Anul_Construirii { get; set; }
        [Required]
        public string Regim_Inaltime { get; set; }
        [Required]
        public string Sistemul_constructiv { get; set; }
        [Required]
        public int Numar_apartamente { get; set; }
    }
}
