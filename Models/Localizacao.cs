using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoSara.Models
{
    [Table("LOCALIZACAO")]
    public class Localizacao
    {
        [Key]
        [Column("ID_LOCALIZACAO")]
        public long Id { get; set; }

        [Column("CIDADE")]
        public string? Cidade { get; set; }

        [Column("ESTADO")]
        public string? Estado { get; set; }

        public virtual ICollection<Sensor>? Sensores { get; set; }
        public virtual ICollection<Alerta>? Alertas { get; set; }
    }
}