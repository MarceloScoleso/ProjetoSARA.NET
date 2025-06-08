using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoSara.Models
{
    [Table("TIPOSENSOR")]
    public class TipoSensor
    {
        [Key]
        [Column("ID_TIPO_SENSOR")]
        public long Id { get; set; }

        [Column("CODIGO")]
        public string? Codigo { get; set; }

        [Column("DESCRICAO")]
        public string? Descricao { get; set; }

        public virtual ICollection<Sensor>? Sensores { get; set; }
    }
}