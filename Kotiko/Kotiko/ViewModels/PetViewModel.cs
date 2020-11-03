using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kotiko.ViewModels
{
    public class PetViewModel
    {
        public string Name { get; set; }
        public PetTypes PetType { get; set; }                                                   
        public Gender Gender { get; set; }
        public float? Age { get; set; }
        [Required]
        public string Town { get; set; }
        public string Description { get; set; }
        [Required]
        public IFormFile Avatar { get; set; }
        public PetSize PetSize { get; set; }
        
    }
}
