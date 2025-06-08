using Microsoft.EntityFrameworkCore;
using ProjetoSara.Models;

namespace ProjetoSara.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Alerta> Alertas { get; set; }
        public DbSet<LeituraSensor> LeituraSensores { get; set; }
        public DbSet<Localizacao> Localizacoes { get; set; }
        public DbSet<NivelAlerta> NivelAlertas { get; set; }
        public DbSet<Notificacao> Notificacoes { get; set; }
        public DbSet<Sensor> Sensores { get; set; }
        public DbSet<StatusNotificacao> StatusNotificacoes { get; set; }
        public DbSet<TipoSensor> TipoSensores { get; set; }
        public DbSet<TipoUsuario> TipoUsuarios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasSequence<long>("SEQ_ALERTA");
            modelBuilder.HasSequence<long>("SEQ_LEITURA_SENSOR");
            modelBuilder.HasSequence<long>("SEQ_LOCALIZACAO");
            modelBuilder.HasSequence<long>("SEQ_NIVEL_ALERTA");
            modelBuilder.HasSequence<long>("SEQ_NOTIFICACAO");
            modelBuilder.HasSequence<long>("SEQ_SENSOR");
            modelBuilder.HasSequence<long>("SEQ_STATUS_NOTIFICACAO");
            modelBuilder.HasSequence<long>("SEQ_TIPO_SENSOR");
            modelBuilder.HasSequence<long>("SEQ_TIPO_USUARIO");
            modelBuilder.HasSequence<long>("SEQ_USUARIO");
        }
    }
}