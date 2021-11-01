using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities = BlogEngine.Domain.Entities;

namespace BlogEngine.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Entities.Author> Author { get; set; }
        DbSet<Entities.Post> Post { get; set; }
        DbSet<Entities.Comment> Comment { get; set; }

        Task<int> SaveChangesAsync();
    }
}
