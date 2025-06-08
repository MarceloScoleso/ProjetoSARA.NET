using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoSara.Models
{
    [Table("NOTIFICACAO")]
    public class Notificacao
    {
        [Key]
        [Column("ID_NOTIFICACAO")]
        public long Id { get; set; }

        [Column("USUARIO_ID")]
        public long? UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual Usuario? Usuario { get; set; }

        [Column("ALERTA_ID")]
        public long? AlertaId { get; set; }

        [ForeignKey("AlertaId")]
        public virtual Alerta? Alerta { get; set; }

        [Column("STATUS_ID")]
        public long? StatusId { get; set; }

        [ForeignKey("StatusId")]
        public virtual StatusNotificacao? Status { get; set; }

        [Column("DATA_ENVIO")]
        public DateTime? DataEnvio { get; set; }
    }
}