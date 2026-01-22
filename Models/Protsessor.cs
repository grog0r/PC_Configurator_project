using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCConfigurator.Models
{
    /// <summary>
    /// Модель процессора
    /// </summary>
    [Table("protsessory")]
    public class Protsessor
    {
        /// <summary>
        /// Модель процессора (первичный ключ)
        /// </summary>
        [Key]
        [Column("Model")]
        [Display(Name = "Модель")]
        [Required(ErrorMessage = "Модель обязательна для заполнения")]
        [StringLength(100, ErrorMessage = "Модель не должна превышать 100 символов")]
        public string Model { get; set; } = string.Empty;

        /// <summary>
        /// Сокет процессора
        /// </summary>
        [Column("Soket")]
        [Display(Name = "Сокет")]
        [Required(ErrorMessage = "Сокет обязателен для заполнения")]
        [StringLength(50, ErrorMessage = "Сокет не должен превышать 50 символов")]
        public string Soket { get; set; } = string.Empty;

        /// <summary>
        /// Мощность процессора (TDP в ваттах)
        /// </summary>
        [Column("Moshchnost")]
        [Display(Name = "Мощность (Вт)")]
        [Required(ErrorMessage = "Мощность обязательна для заполнения")]
        [Range(1, int.MaxValue, ErrorMessage = "Мощность должна быть больше 0")]
        public int Moshchnost { get; set; }

        /// <summary>
        /// Тактовая частота в ГГц
        /// </summary>
        [Column("TaktovayaChastotaGz")]
        [Display(Name = "Тактовая частота (ГГц)")]
        [Required(ErrorMessage = "Тактовая частота обязательна для заполнения")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Тактовая частота должна быть больше 0")]
        public decimal TaktovayaChastotaGz { get; set; }

        /// <summary>
        /// Количество ядер процессора
        /// </summary>
        [Column("KolichestvoYader")]
        [Display(Name = "Количество ядер")]
        [Required(ErrorMessage = "Количество ядер обязательно для заполнения")]
        [Range(1, int.MaxValue, ErrorMessage = "Количество ядер должно быть больше 0")]
        public int KolichestvoYader { get; set; }

        /// <summary>
        /// Цена процессора
        /// </summary>
        [Column("Tsena")]
        [Display(Name = "Цена")]
        [Required(ErrorMessage = "Цена обязательна для заполнения")]
        [Range(0, double.MaxValue, ErrorMessage = "Цена не может быть отрицательной")]
        public decimal Tsena { get; set; }

        /// <summary>
        /// Список сборок, использующих этот процессор
        /// </summary>
        public virtual ICollection<GotovayaSborka> GotovyyeSborki { get; set; } = new List<GotovayaSborka>();
    }
}