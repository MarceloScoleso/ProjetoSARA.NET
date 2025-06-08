using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoSara.Models
{
    [Table("STATUSNOTIFICACAO")]
    public class StatusNotificacao
    {
        [Key]
        [Column("ID_STATUS_NOTIFICACAO")]
        public long Id { get; set; }

        [Column("CODIGO")]
        public string Codigo { get; set; } = string.Empty;

        [Column("DESCRICAO")]
        public string? Descricao { get; set; }

        public virtual ICollection<Notificacao>? Notificacoes { get; set; }
    }
}