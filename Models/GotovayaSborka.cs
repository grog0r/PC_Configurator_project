using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCConfigurator.Models
{
    /// <summary>
    /// Модель готовой сборки компьютера
    /// </summary>
    [Table("gotovyyesborki")]
    public class GotovayaSborka
    {
        /// <summary>
        /// Идентификатор сборки (первичный ключ)
        /// </summary>
        [Key]
        [Column("IdSborki")]
        [Display(Name = "ID сборки")]
        [Required(ErrorMessage = "ID сборки обязателен для заполнения")]
        [StringLength(50, ErrorMessage = "ID сборки не должен превышать 50 символов")]
        public string IdSborki { get; set; } = string.Empty;

        /// <summary>
        /// Модель материнской платы (внешний ключ)
        /// </summary>
        [Column("MaterinskayaPlataModel")]
        [Display(Name = "Материнская плата")]
        [Required(ErrorMessage = "Материнская плата обязательна для заполнения")]
        public string MaterinskayaPlataModel { get; set; } = string.Empty;

        /// <summary>
        /// Модель процессора (внешний ключ)
        /// </summary>
        [Column("ProtsessorModel")]
        [Display(Name = "Процессор")]
        [Required(ErrorMessage = "Процессор обязателен для заполнения")]
        public string ProtsessorModel { get; set; } = string.Empty;

        /// <summary>
        /// Модель оперативной памяти (внешний ключ)
        /// </summary>
        [Column("OperativnayaPamyatModel")]
        [Display(Name = "Оперативная память")]
        [Required(ErrorMessage = "Оперативная память обязательна для заполнения")]
        public string OperativnayaPamyatModel { get; set; } = string.Empty;

        /// <summary>
        /// Модель видеокарты (внешний ключ)
        /// </summary>
        [Column("VideokartaModel")]
        [Display(Name = "Видеокарта")]
        [Required(ErrorMessage = "Видеокарта обязательна для заполнения")]
        public string VideokartaModel { get; set; } = string.Empty;

        /// <summary>
        /// Модель накопителя (внешний ключ)
        /// </summary>
        [Column("NakopitelModel")]
        [Display(Name = "Накопитель")]
        [Required(ErrorMessage = "Накопитель обязателен для заполнения")]
        public string NakopitelModel { get; set; } = string.Empty;

        /// <summary>
        /// Модель блока питания (внешний ключ)
        /// </summary>
        [Column("BlokPitaniyaModel")]
        [Display(Name = "Блок питания")]
        [Required(ErrorMessage = "Блок питания обязателен для заполнения")]
        public string BlokPitaniyaModel { get; set; } = string.Empty;

        /// <summary>
        /// Модель корпуса (внешний ключ)
        /// </summary>
        [Column("KorpusModel")]
        [Display(Name = "Корпус")]
        [Required(ErrorMessage = "Корпус обязателен для заполнения")]
        public string KorpusModel { get; set; } = string.Empty;

        /// <summary>
        /// Вычислительная мощность сборки
        /// </summary>
        [Column("VychislitelnayaMoshchnost")]
        [Display(Name = "Вычислительная мощность")]
        [Required(ErrorMessage = "Вычислительная мощность обязательна для заполнения")]
        [StringLength(50, ErrorMessage = "Вычислительная мощность не должна превышать 50 символов")]
        public string VychislitelnayaMoshchnost { get; set; } = string.Empty;

        /// <summary>
        /// Общая стоимость сборки
        /// </summary>
        [Column("ObshchayaStoimost")]
        [Display(Name = "Общая стоимость")]
        [Required(ErrorMessage = "Общая стоимость обязательна для заполнения")]
        [Range(0, double.MaxValue, ErrorMessage = "Стоимость не может быть отрицательной")]
        public decimal ObshchayaStoimost { get; set; }

        /// <summary>
        /// Общая мощность сборки в ваттах
        /// </summary>
        [Column("MoshnostVt")]
        [Display(Name = "Мощность (Вт)")]
        [Required(ErrorMessage = "Мощность обязательна для заполнения")]
        [Range(1, int.MaxValue, ErrorMessage = "Мощность должна быть больше 0")]
        public int MoshnostVt { get; set; }

        // Навигационные свойства

        /// <summary>
        /// Материнская плата сборки
        /// </summary>
        [ForeignKey("MaterinskayaPlataModel")]
        public virtual MaterinskayaPlata? MaterinskayaPlata { get; set; }

        /// <summary>
        /// Процессор сборки
        /// </summary>
        [ForeignKey("ProtsessorModel")]
        public virtual Protsessor? Protsessor { get; set; }

        /// <summary>
        /// Оперативная память сборки
        /// </summary>
        [ForeignKey("OperativnayaPamyatModel")]
        public virtual OperativnayaPamyat? OperativnayaPamyat { get; set; }

        /// <summary>
        /// Видеокарта сборки
        /// </summary>
        [ForeignKey("VideokartaModel")]
        public virtual Videokarta? Videokarta { get; set; }

        /// <summary>
        /// Накопитель сборки
        /// </summary>
        [ForeignKey("NakopitelModel")]
        public virtual Nakopitel? Nakopitel { get; set; }

        /// <summary>
        /// Блок питания сборки
        /// </summary>
        [ForeignKey("BlokPitaniyaModel")]
        public virtual BlokPitaniya? BlokPitaniya { get; set; }

        /// <summary>
        /// Корпус сборки
        /// </summary>
        [ForeignKey("KorpusModel")]
        public virtual Korpus? Korpus { get; set; }

        /// <summary>
        /// Расчетная стоимость сборки (сумма всех компонентов)
        /// </summary>
        [NotMapped]
        [Display(Name = "Расчетная стоимость")]
        public decimal RasschitannayaStoimost
        {
            get
            {
                decimal total = 0;
                if (MaterinskayaPlata != null) total += MaterinskayaPlata.Tsena;
                if (Protsessor != null) total += Protsessor.Tsena;
                if (OperativnayaPamyat != null) total += OperativnayaPamyat.Tsena;
                if (Videokarta != null) total += Videokarta.Tsena;
                if (Nakopitel != null) total += Nakopitel.Tsena;
                if (BlokPitaniya != null) total += BlokPitaniya.Tsena;
                if (Korpus != null) total += Korpus.Tsena;
                return total;
            }
        }

        /// <summary>
        /// Расчетная мощность сборки (процессор + видеокарта + 20% запас)
        /// </summary>
        [NotMapped]
        [Display(Name = "Расчетная мощность")]
        public int RasschitannayaMoshchnost
        {
            get
            {
                int total = 0;
                if (Protsessor != null) total += Protsessor.Moshchnost;
                if (Videokarta != null) total += Videokarta.Moshchnost;
                // Добавляем 20% запаса на другие компоненты
                return (int)(total * 1.2m);
            }
        }

        /// <summary>
        /// Разница между фактической и расчетной стоимостью
        /// </summary>
        [NotMapped]
        [Display(Name = "Разница в стоимости")]
        public decimal RaznitsaStoimosti => ObshchayaStoimost - RasschitannayaStoimost;

        /// <summary>
        /// Разница между фактической и расчетной мощностью
        /// </summary>
        [NotMapped]
        [Display(Name = "Разница в мощности")]
        public int RaznitsaMoshchnosti => MoshnostVt - RasschitannayaMoshchnost;

        /// <summary>
        /// Проверяет, совпадает ли фактическая стоимость с расчетной
        /// </summary>
        [NotMapped]
        [Display(Name = "Стоимость корректна")]
        public bool StoimostKorrektna => Math.Abs(RaznitsaStoimosti) < 0.01m;

        /// <summary>
        /// Проверяет, совпадает ли фактическая мощность с расчетной
        /// </summary>
        [NotMapped]
        [Display(Name = "Мощность корректна")]
        public bool MoshchnostKorrektna => Math.Abs(RaznitsaMoshchnosti) <= 10;

        /// <summary>
        /// Возвращает список всех компонентов сборки
        /// </summary>
        [NotMapped]
        public List<object> VseKomponenty
        {
            get
            {
                var list = new List<object>();
                if (MaterinskayaPlata != null) list.Add(new { Type = "Материнская плата", Component = MaterinskayaPlata });
                if (Protsessor != null) list.Add(new { Type = "Процессор", Component = Protsessor });
                if (OperativnayaPamyat != null) list.Add(new { Type = "Оперативная память", Component = OperativnayaPamyat });
                if (Videokarta != null) list.Add(new { Type = "Видеокарта", Component = Videokarta });
                if (Nakopitel != null) list.Add(new { Type = "Накопитель", Component = Nakopitel });
                if (BlokPitaniya != null) list.Add(new { Type = "Блок питания", Component = BlokPitaniya });
                if (Korpus != null) list.Add(new { Type = "Корпус", Component = Korpus });
                return list;
            }
        }

        /// <summary>
        /// Возвращает общую информацию о сборке
        /// </summary>
        [NotMapped]
        public string ObshchayaInformatsiya
        {
            get
            {
                return $"{VychislitelnayaMoshchnost} сборка стоимостью {ObshchayaStoimost:N0}₽ с мощностью {MoshnostVt}Вт";
            }
        }

        /// <summary>
        /// Проверяет, достаточно ли мощности блока питания
        /// </summary>
        [NotMapped]
        public bool BlokPitaniyaDostatochno
        {
            get
            {
                if (BlokPitaniya == null) return false;
                return BlokPitaniya.MoshchnostVt >= MoshnostVt;
            }
        }

        /// <summary>
        /// Запас мощности блока питания в процентах
        /// </summary>
        [NotMapped]
        public int ZapasMoshchnostiProcent
        {
            get
            {
                if (BlokPitaniya == null || MoshnostVt == 0) return 0;
                return (int)((BlokPitaniya.MoshchnostVt - MoshnostVt) * 100 / (double)MoshnostVt);
            }
        }

        /// <summary>
        /// Проверяет совместимость сокета процессора и материнской платы
        /// </summary>
        [NotMapped]
        public bool SoketSovmestim
        {
            get
            {
                if (Protsessor == null || MaterinskayaPlata == null) return false;
                return Protsessor.Soket == MaterinskayaPlata.Soket;
            }
        }
    }
}