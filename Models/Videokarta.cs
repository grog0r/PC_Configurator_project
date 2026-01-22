using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCConfigurator.Models
{
    /// <summary>
    /// Модель видеокарты
    /// </summary>
    [Table("videokarty")]
    public class Videokarta
    {
        /// <summary>
        /// Модель видеокарты (первичный ключ)
        /// </summary>
        [Key]
        [Column("Model")]
        [Display(Name = "Модель")]
        [Required(ErrorMessage = "Модель обязательна для заполнения")]
        [StringLength(100, ErrorMessage = "Модель не должна превышать 100 символов")]
        public string Model { get; set; } = string.Empty;

        /// <summary>
        /// Вычислительная мощность (например, RTX 3070, RX 6700 XT)
        /// </summary>
        [Column("VychislitelnayaMoshchnost")]
        [Display(Name = "Вычислительная мощность")]
        [Required(ErrorMessage = "Вычислительная мощность обязательна для заполнения")]
        [StringLength(50, ErrorMessage = "Вычислительная мощность не должна превышать 50 символов")]
        public string VychislitelnayaMoshchnost { get; set; } = string.Empty;

        /// <summary>
        /// Объем видеопамяти в ГБ
        /// </summary>
        [Column("ObyemPamyatiGb")]
        [Display(Name = "Объем памяти (ГБ)")]
        [Required(ErrorMessage = "Объем памяти обязателен для заполнения")]
        [Range(1, int.MaxValue, ErrorMessage = "Объем памяти должен быть больше 0")]
        public int ObyemPamyatiGb { get; set; }

        /// <summary>
        /// Мощность видеокарты (TDP в ваттах)
        /// </summary>
        [Column("Moshchnost")]
        [Display(Name = "Мощность (Вт)")]
        [Required(ErrorMessage = "Мощность обязательна для заполнения")]
        [Range(1, int.MaxValue, ErrorMessage = "Мощность должна быть больше 0")]
        public int Moshchnost { get; set; }

        /// <summary>
        /// Цена видеокарты
        /// </summary>
        [Column("Tsena")]
        [Display(Name = "Цена")]
        [Required(ErrorMessage = "Цена обязательна для заполнения")]
        [Range(0, double.MaxValue, ErrorMessage = "Цена не может быть отрицательной")]
        public decimal Tsena { get; set; }

        /// <summary>
        /// Список сборок, использующих эту видеокарту
        /// </summary>
        public virtual ICollection<GotovayaSborka> GotovyyeSborki { get; set; } = new List<GotovayaSborka>();
    }
}