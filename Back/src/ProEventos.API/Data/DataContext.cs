using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.API.Models;

namespace ProEventos.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { } /*Remete a string de conexão do banco de dados mapeada no Startup.cs*/
        public DbSet<Evento> Eventos { get; set; } /*Mapemamento de uma classe para que se torne o banco de dados*/
    }
}