using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoSara.Models
{
    [Table("NIVELALERTA")]
    public class NivelAlerta
    {
        [Key]
        [Column("ID_NIVEL_ALERTA")]
        public long Id { get; set; }

        [Column("CODIGO")]
        public string? Codigo { get; set; }

        [Column("DESCRICAO")]
        public string? Descricao { get; set; }

        public virtual ICollection<Alerta>? Alertas { get; set; }
    }
}