using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCConfigurator.Models
{
    /// <summary>
    /// Модель блока питания
    /// </summary>
    [Table("blokipitaniya")]
    public class BlokPitaniya
    {
        /// <summary>
        /// Модель блока питания (первичный ключ)
        /// </summary>
        [Key]
        [Column("Model")]
        [Display(Name = "Модель")]
        [Required(ErrorMessage = "Модель обязательна для заполнения")]
        [StringLength(100, ErrorMessage = "Модель не должна превышать 100 символов")]
        public string Model { get; set; } = string.Empty;

        /// <summary>
        /// Мощность блока питания в ваттах
        /// </summary>
        [Column("MoshchnostVt")]
        [Display(Name = "Мощность (Вт)")]
        [Required(ErrorMessage = "Мощность обязательна для заполнения")]
        [Range(1, int.MaxValue, ErrorMessage = "Мощность должна быть больше 0")]
        public int MoshchnostVt { get; set; }

        /// <summary>
        /// Сертификация блока питания (80 Plus Bronze, Gold и т.д.)
        /// </summary>
        [Column("Sertifikatsiya")]
        [Display(Name = "Сертификация")]
        [Required(ErrorMessage = "Сертификация обязательна для заполнения")]
        [StringLength(50, ErrorMessage = "Сертификация не должна превышать 50 символов")]
        public string Sertifikatsiya { get; set; } = string.Empty;

        /// <summary>
        /// Цена блока питания
        /// </summary>
        [Column("Tsena")]
        [Display(Name = "Цена")]
        [Required(ErrorMessage = "Цена обязательна для заполнения")]
        [Range(0, double.MaxValue, ErrorMessage = "Цена не может быть отрицательной")]
        public decimal Tsena { get; set; }

        /// <summary>
        /// Список сборок, использующих этот блок питания
        /// </summary>
        public virtual ICollection<GotovayaSborka> GotovyyeSborki { get; set; } = new List<GotovayaSborka>();
    }
}