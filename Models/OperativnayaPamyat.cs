using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCConfigurator.Models
{
    /// <summary>
    /// Модель оперативной памяти
    /// </summary>
    [Table("operativnayapamyat")]
    public class OperativnayaPamyat
    {
        /// <summary>
        /// Модель оперативной памяти (первичный ключ)
        /// </summary>
        [Key]
        [Column("Model")]
        [Display(Name = "Модель")]
        [Required(ErrorMessage = "Модель обязательна для заполнения")]
        [StringLength(100, ErrorMessage = "Модель не должна превышать 100 символов")]
        public string Model { get; set; } = string.Empty;

        /// <summary>
        /// Тип оперативной памяти (DDR4, DDR5 и т.д.)
        /// </summary>
        [Column("Tip")]
        [Display(Name = "Тип")]
        [Required(ErrorMessage = "Тип обязателен для заполнения")]
        [StringLength(20, ErrorMessage = "Тип не должен превышать 20 символов")]
        public string Tip { get; set; } = string.Empty;

        /// <summary>
        /// Объем одного модуля в ГБ
        /// </summary>
        [Column("ObyomModulyaGb")]
        [Display(Name = "Объем модуля (ГБ)")]
        [Required(ErrorMessage = "Объем обязателен для заполнения")]
        [Range(1, int.MaxValue, ErrorMessage = "Объем должен быть больше 0")]
        public int ObyomModulyaGb { get; set; }

        /// <summary>
        /// Частота памяти в МГц
        /// </summary>
        [Column("ChastotaMgts")]
        [Display(Name = "Частота (МГц)")]
        [Required(ErrorMessage = "Частота обязательна для заполнения")]
        [Range(1, int.MaxValue, ErrorMessage = "Частота должна быть больше 0")]
        public int ChastotaMgts { get; set; }

        /// <summary>
        /// Цена оперативной памяти
        /// </summary>
        [Column("Tsena")]
        [Display(Name = "Цена")]
        [Required(ErrorMessage = "Цена обязательна для заполнения")]
        [Range(0, double.MaxValue, ErrorMessage = "Цена не может быть отрицательной")]
        public decimal Tsena { get; set; }

        /// <summary>
        /// Список сборок, использующих эту оперативную память
        /// </summary>
        public virtual ICollection<GotovayaSborka> GotovyyeSborki { get; set; } = new List<GotovayaSborka>();
    }
}