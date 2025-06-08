using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoSara.Models
{
    [Table("ALERTA")]
    public class Alerta
    {
        [Key]
        [Column("ID_ALERTA")]
        public long Id { get; set; }

        [Column("MENSAGEM")]
        public string? Mensagem { get; set; }

        [Column("DATA_HORA")]
        public DateTime DataHora { get; set; }

        [Column("USUARIO_ID")]
        public long? UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual Usuario? Usuario { get; set; }

        [Column("NIVEL_ALERTA_ID")]
        public long? NivelAlertaId { get; set; }

        [ForeignKey("NivelAlertaId")]
        public virtual NivelAlerta? NivelAlerta { get; set; }

        [Column("LOCALIZACAO_ID")]
        public long? LocalizacaoId { get; set; }

        [ForeignKey("LocalizacaoId")]
        public virtual Localizacao? Localizacao { get; set; }
    }
}