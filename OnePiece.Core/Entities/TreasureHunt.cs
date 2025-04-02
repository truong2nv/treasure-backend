using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
namespace OnePiece.Core.Entities
{
    [Table("TreasureHunts")]
    public class TreasureHunt
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [Range(1, 500)]
        public int N { get; set; }

        [Required]
        [Range(1, 500)]
        public int M { get; set; }

        [Required]
        [Range(1, 250000)]
        public int P { get; set; }

        [Required]
        public string MatrixJson { get; set; }

        public double? Result { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Pending"; // Status: Pending, Solved, Failed
        
        // public int[][] GetMatrix()
        // {
        //     return JsonSerializer.Deserialize<int[][]>(MatrixJson);
        // }
        //
        // public void SetMatrix(int[][] matrix)
        // {
        //     MatrixJson = JsonSerializer.Serialize(matrix);
        // }
    }
}