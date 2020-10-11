﻿using Elencar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Elencar.Domain.Interfaces.Repositories
{
    public interface IGenreRepository
    {
        int Insert(Genre genre);
        Task<Genre> GetByIdAsync(int id);
        IEnumerable<Genre> Get();
    }
}
