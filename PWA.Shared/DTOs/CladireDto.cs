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
		public string Strada { get; set; }
        [Required]
        public string Numar { get; set; }

        public string Bloc { get; set; }
        public string Scara { get; set; }
        [Required]
		public string Oras { get; set; }
		[Required]
		public string Judet { get; set; }
	}
}
