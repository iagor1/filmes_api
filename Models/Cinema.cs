﻿using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Models
{
    public class Cinema
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage ="O campo nome é obrigatório")]
        public string Nome { get; set; }
        public virtual Address Address { get; set; }
        public int AddressId { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
