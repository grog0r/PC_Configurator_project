using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCConfigurator.Models
{
    /// <summary>
    /// Модель корпуса
    /// </summary>
    [Table("korpusa")]
    public class Korpus
    {
        /// <summary>
        /// Модель корпуса (первичный ключ)
        /// </summary>
        [Key]
        [Column("Model")]
        [Display(Name = "Модель")]
        [Required(ErrorMessage = "Модель обязательна для заполнения")]
        [StringLength(100, ErrorMessage = "Модель не должна превышать 100 символов")]
        public string Model { get; set; } = string.Empty;

        /// <summary>
        /// Поддерживаемые форм-факторы материнских плат
        /// </summary>
        [Column("PodderzhivayemyyeFormFaktory")]
        [Display(Name = "Поддерживаемые форм-факторы")]
        [Required(ErrorMessage = "Поддерживаемые форм-факторы обязательны для заполнения")]
        [StringLength(200, ErrorMessage = "Форм-факторы не должны превышать 200 символов")]
        public string PodderzhivayemyyeFormFaktory { get; set; } = string.Empty;

        /// <summary>
        /// Размеры корпуса
        /// </summary>
        [Column("Razmery")]
        [Display(Name = "Размеры")]
        [Required(ErrorMessage = "Размеры обязательны для заполнения")]
        [StringLength(100, ErrorMessage = "Размеры не должны превышать 100 символов")]
        public string Razmery { get; set; } = string.Empty;

        /// <summary>
        /// Цена корпуса
        /// </summary>
        [Column("Tsena")]
        [Display(Name = "Цена")]
        [Required(ErrorMessage = "Цена обязательна для заполнения")]
        [Range(0, double.MaxValue, ErrorMessage = "Цена не может быть отрицательной")]
        public decimal Tsena { get; set; }

        /// <summary>
        /// Список сборок, использующих этот корпус
        /// </summary>
        public virtual ICollection<GotovayaSborka> GotovyyeSborki { get; set; } = new List<GotovayaSborka>();
    }
}