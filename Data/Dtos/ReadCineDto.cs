﻿
using FilmesApi.Models;

namespace FilmesApi.Data.Dtos
{
    public class ReadCineDto
    {

        public int Id { get; set; }

        public string Nome { get; set; }
        public ReadAddressDto ReadAddressDto { get; set; }
        public virtual ICollection<ReadSessionDto> Sessions { get; set; }
    }
}
