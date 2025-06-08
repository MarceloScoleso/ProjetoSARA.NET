using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoSara.Models
{
    [Table("USUARIO")]
    public class Usuario
    {
        [Key]
        [Column("ID_USUARIO")]
        public long Id { get; set; }

        [Column("NOME")]
        public string Nome { get; set; } = string.Empty;

        [Column("EMAIL")]
        public string Email { get; set; } = string.Empty;

        [Column("SENHA")]
        public string Senha { get; set; } = string.Empty;

        [Column("CPF")]
        public string Cpf { get; set; } = string.Empty;

        [Column("TIPO_USUARIO_ID")]
        public long? TipoUsuarioId { get; set; }

        [ForeignKey("TipoUsuarioId")]
        public virtual TipoUsuario? TipoUsuario { get; set; }

        public virtual ICollection<Alerta>? Alertas { get; set; }
        public virtual ICollection<Notificacao>? Notificacoes { get; set; }
    }
}