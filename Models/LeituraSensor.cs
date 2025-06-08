using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoSara.Models
{
    [Table("LEITURASENSOR")]
    public class LeituraSensor
    {
        [Key]
        [Column("ID_LEITURA")]
        public long Id { get; set; }

        [Column("SENSOR_ID")]
        public long? SensorId { get; set; }

        [ForeignKey("SensorId")]
        public virtual Sensor? Sensor { get; set; }

        [Column("VALOR")]
        public double? Valor { get; set; }

        [Column("UNIDADE")]
        public string? Unidade { get; set; }

        [Column("DATA_HORA")]
        public DateTime DataHora { get; set; }
    }
}