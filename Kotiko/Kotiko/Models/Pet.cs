using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Kotiko.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PetTypes PetType { get; set; }
        public Gender Gender { get; set; }
        public float? Age { get; set; }
        public PetSize PetSize { get; set; }
        public string Town { get; set; }
        public string Description { get; set; }
        public byte[] Avatar { get; set; }
        public bool IsPicked { get; set; }
        public string Phone { get; set; }
    }
}
