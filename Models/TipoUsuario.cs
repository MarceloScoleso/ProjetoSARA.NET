using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoSara.Models
{
    [Table("TIPOUSUARIO")]
    public class TipoUsuario
    {
        [Key]
        [Column("ID_TIPO_USUARIO")]
        public long Id { get; set; }

        [Column("CODIGO")]
        public string? Codigo { get; set; }

        [Column("DESCRICAO")]
        public string? Descricao { get; set; }

        public virtual ICollection<Usuario>? Usuarios { get; set; }
    }
}