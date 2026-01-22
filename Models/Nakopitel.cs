using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCConfigurator.Models
{
    /// <summary>
    /// Модель накопителя
    /// </summary>
    [Table("nakopiteli")]
    public class Nakopitel
    {
        /// <summary>
        /// Модель накопителя (первичный ключ)
        /// </summary>
        [Key]
        [Column("Model")]
        [Display(Name = "Модель")]
        [Required(ErrorMessage = "Модель обязательна для заполнения")]
        [StringLength(100, ErrorMessage = "Модель не должна превышать 100 символов")]
        public string Model { get; set; } = string.Empty;

        /// <summary>
        /// Тип накопителя (SSD, HDD, NVMe)
        /// </summary>
        [Column("Tip")]
        [Display(Name = "Тип")]
        [Required(ErrorMessage = "Тип обязателен для заполнения")]
        [StringLength(20, ErrorMessage = "Тип не должен превышать 20 символов")]
        public string Tip { get; set; } = string.Empty;

        /// <summary>
        /// Объем накопителя в ГБ
        /// </summary>
        [Column("ObyomGb")]
        [Display(Name = "Объем (ГБ)")]
        [Required(ErrorMessage = "Объем обязателен для заполнения")]
        [Range(1, int.MaxValue, ErrorMessage = "Объем должен быть больше 0")]
        public int ObyomGb { get; set; }

        /// <summary>
        /// Скорость чтения в МБ/с
        /// </summary>
        [Column("SkorostChteniyaMbS")]
        [Display(Name = "Скорость чтения (МБ/с)")]
        [Required(ErrorMessage = "Скорость чтения обязательна для заполнения")]
        [Range(1, int.MaxValue, ErrorMessage = "Скорость чтения должна быть больше 0")]
        public int SkorostChteniyaMbS { get; set; }

        /// <summary>
        /// Скорость записи в МБ/с
        /// </summary>
        [Column("SkorostZapisiMbS")]
        [Display(Name = "Скорость записи (МБ/с)")]
        [Required(ErrorMessage = "Скорость записи обязательна для заполнения")]
        [Range(1, int.MaxValue, ErrorMessage = "Скорость записи должна быть больше 0")]
        public int SkorostZapisiMbS { get; set; }

        /// <summary>
        /// Цена накопителя
        /// </summary>
        [Column("Tsena")]
        [Display(Name = "Цена")]
        [Required(ErrorMessage = "Цена обязательна для заполнения")]
        [Range(0, double.MaxValue, ErrorMessage = "Цена не может быть отрицательной")]
        public decimal Tsena { get; set; }

        /// <summary>
        /// Список сборок, использующих этот накопитель
        /// </summary>
        public virtual ICollection<GotovayaSborka> GotovyyeSborki { get; set; } = new List<GotovayaSborka>();
    }
}