﻿using api_vet_app.Models.Persona;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_vet_app.Models.Service
{
    public class Lab
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string? Name { get; set; }
        [ForeignKey(nameof(VetId)), Required]
        public string? VetId { get; set; }
        public User? User { get; set; }
    }
}
