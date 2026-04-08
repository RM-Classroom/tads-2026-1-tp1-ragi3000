using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Locadora.Models
{
    public class Fabricante
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string? Nome { get; set; }

        public ICollection<Veiculo>? Veiculos { get; set; }
    }

    public class Categoria 
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string? Nome { get; set; } // Ex: Econômico, SUV, Luxo

        [Required]
        public decimal ValorDiariaPadrao { get; set; }

        public ICollection<Veiculo>? Veiculos { get; set; }
    }

    public class Veiculo
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string? Modelo { get; set; }

        [Required]
        public int AnoFabricacao { get; set; }

        [Required]
        public int Quilometragem { get; set; }

        // Chaves Estrangeiras
        public int FabricanteId { get; set; }
        [ForeignKey("FabricanteId")]
        public Fabricante? Fabricante { get; set; }

        public int CategoriaId { get; set; }
        [ForeignKey("CategoriaId")]
        public Categoria? Categoria { get; set; }
    }

    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string? Nome { get; set; }

        [Required, MaxLength(11)] 
        public string? CPF { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
    }

    public class Aluguel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DataInicio { get; set; }

        public DateTime? DataDevolucao { get; set; }

        [Required]
        public int KmInicial { get; set; }

        public int? KmFinal { get; set; }

        [Required]
        public decimal ValorDiaria { get; set; } 

        public decimal? ValorTotal { get; set; }

        // Chaves Estrangeiras
        public int ClienteId { get; set; }
        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }

        public int VeiculoId { get; set; }
        [ForeignKey("VeiculoId")]
        public Veiculo? Veiculo { get; set; }
    }
}
