using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCConfigurator.Models
{
    /// <summary>
    /// Модель материнской платы
    /// </summary>
    [Table("materinskiyeplaty")]
    public class MaterinskayaPlata
    {
        /// <summary>
        /// Модель материнской платы (первичный ключ)
        /// </summary>
        [Key]
        [Column("Model")]
        [Display(Name = "Модель")]
        [Required(ErrorMessage = "Модель обязательна для заполнения")]
        [StringLength(100, ErrorMessage = "Модель не должна превышать 100 символов")]
        public string Model { get; set; } = string.Empty;

        /// <summary>
        /// Форм-фактор материнской платы
        /// </summary>
        [Column("FormFaktor")]
        [Display(Name = "Форм-фактор")]
        [Required(ErrorMessage = "Форм-фактор обязателен для заполнения")]
        [StringLength(50, ErrorMessage = "Форм-фактор не должен превышать 50 символов")]
        public string FormFaktor { get; set; } = string.Empty;

        /// <summary>
        /// Сокет процессора
        /// </summary>
        [Column("Soket")]
        [Display(Name = "Сокет")]
        [Required(ErrorMessage = "Сокет обязателен для заполнения")]
        [StringLength(50, ErrorMessage = "Сокет не должен превышать 50 символов")]
        public string Soket { get; set; } = string.Empty;

        /// <summary>
        /// Тип поддерживаемой оперативной памяти
        /// </summary>
        [Column("TipPodderzhivayemoyOzu")]
        [Display(Name = "Тип поддерживаемой ОЗУ")]
        [Required(ErrorMessage = "Тип ОЗУ обязателен для заполнения")]
        [StringLength(50, ErrorMessage = "Тип ОЗУ не должен превышать 50 символов")]
        public string TipPodderzhivayemoyOzu { get; set; } = string.Empty;

        /// <summary>
        /// Максимальный объем оперативной памяти в ГБ
        /// </summary>
        [Column("MaksimalnyyObyomOzuGb")]
        [Display(Name = "Макс. объем ОЗУ (ГБ)")]
        [Required(ErrorMessage = "Максимальный объем ОЗУ обязателен для заполнения")]
        [Range(1, int.MaxValue, ErrorMessage = "Объем ОЗУ должен быть больше 0")]
        public int MaksimalnyyObyomOzuGb { get; set; }

        /// <summary>
        /// Тип поддерживаемых накопителей
        /// </summary>
        [Column("TipPodderzhivayemixNakopiteli")]
        [Display(Name = "Поддерживаемые накопители")]
        [Required(ErrorMessage = "Тип накопителей обязателен для заполнения")]
        [StringLength(100, ErrorMessage = "Тип накопителей не должен превышать 100 символов")]
        public string TipPodderzhivayemixNakopiteli { get; set; } = string.Empty;

        /// <summary>
        /// Цена материнской платы
        /// </summary>
        [Column("Tsena")]
        [Display(Name = "Цена")]
        [Required(ErrorMessage = "Цена обязательна для заполнения")]
        [Range(0, double.MaxValue, ErrorMessage = "Цена не может быть отрицательной")]
        public decimal Tsena { get; set; }

        /// <summary>
        /// Список сборок, использующих эту материнскую плату
        /// </summary>
        public virtual ICollection<GotovayaSborka> GotovyyeSborki { get; set; } = new List<GotovayaSborka>();
    }
}