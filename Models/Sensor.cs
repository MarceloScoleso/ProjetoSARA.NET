using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoSara.Models
{
    [Table("SENSOR")]
    public class Sensor
    {
        [Key]
        [Column("ID_SENSOR")]
        public long Id { get; set; }

        [Column("TIPO_SENSOR_ID")]
        public long? TipoSensorId { get; set; }

        [ForeignKey("TipoSensorId")]
        public virtual TipoSensor? TipoSensor { get; set; }

        [Column("LOCALIZACAO_ID")]
        public long? LocalizacaoId { get; set; }

        [ForeignKey("LocalizacaoId")]
        public virtual Localizacao? Localizacao { get; set; }

        public virtual ICollection<LeituraSensor>? Leituras { get; set; }
    }
}