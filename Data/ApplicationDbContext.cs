using Microsoft.EntityFrameworkCore;
using PCConfigurator.Models;

namespace PCConfigurator.Data
{
    /// <summary>
    /// Контекст базы данных для приложения конфигуратора ПК
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Конструктор контекста базы данных
        /// </summary>
        /// <param name="options">Параметры контекста базы данных</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Таблица материнских плат
        /// </summary>
        public DbSet<MaterinskayaPlata> MaterinskiyePlaty { get; set; } = null!;

        /// <summary>
        /// Таблица процессоров
        /// </summary>
        public DbSet<Protsessor> Protsessory { get; set; } = null!;

        /// <summary>
        /// Таблица оперативной памяти
        /// </summary>
        public DbSet<OperativnayaPamyat> OperativnayaPamyat { get; set; } = null!;

        /// <summary>
        /// Таблица видеокарт
        /// </summary>
        public DbSet<Videokarta> Videokarty { get; set; } = null!;

        /// <summary>
        /// Таблица накопителей
        /// </summary>
        public DbSet<Nakopitel> Nakopiteli { get; set; } = null!;

        /// <summary>
        /// Таблица блоков питания
        /// </summary>
        public DbSet<BlokPitaniya> BlokiPitaniya { get; set; } = null!;

        /// <summary>
        /// Таблица корпусов
        /// </summary>
        public DbSet<Korpus> Korpusa { get; set; } = null!;

        /// <summary>
        /// Таблица готовых сборок
        /// </summary>
        public DbSet<GotovayaSborka> GotovyyeSborki { get; set; } = null!;

        /// <summary>
        /// Конфигурация модели базы данных
        /// </summary>
        /// <param name="modelBuilder">Построитель модели</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Конфигурация таблицы материнских плат
            modelBuilder.Entity<MaterinskayaPlata>()
                .HasKey(mp => mp.Model);

            // Конфигурация таблицы процессоров
            modelBuilder.Entity<Protsessor>()
                .HasKey(p => p.Model);

            // Конфигурация таблицы оперативной памяти
            modelBuilder.Entity<OperativnayaPamyat>()
                .HasKey(op => op.Model);

            // Конфигурация таблицы видеокарт
            modelBuilder.Entity<Videokarta>()
                .HasKey(v => v.Model);

            // Конфигурация таблицы накопителей
            modelBuilder.Entity<Nakopitel>()
                .HasKey(n => n.Model);

            // Конфигурация таблицы блоков питания
            modelBuilder.Entity<BlokPitaniya>()
                .HasKey(bp => bp.Model);

            // Конфигурация таблицы корпусов
            modelBuilder.Entity<Korpus>()
                .HasKey(k => k.Model);

            // Конфигурация таблицы готовых сборок
            modelBuilder.Entity<GotovayaSborka>()
                .HasKey(gs => gs.IdSborki);

            // Настройка внешних ключей для готовых сборок
            modelBuilder.Entity<GotovayaSborka>()
                .HasOne(gs => gs.MaterinskayaPlata)
                .WithMany(mp => mp.GotovyyeSborki)
                .HasForeignKey(gs => gs.MaterinskayaPlataModel)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GotovayaSborka>()
                .HasOne(gs => gs.Protsessor)
                .WithMany(p => p.GotovyyeSborki)
                .HasForeignKey(gs => gs.ProtsessorModel)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GotovayaSborka>()
                .HasOne(gs => gs.OperativnayaPamyat)
                .WithMany(op => op.GotovyyeSborki)
                .HasForeignKey(gs => gs.OperativnayaPamyatModel)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GotovayaSborka>()
                .HasOne(gs => gs.Videokarta)
                .WithMany(v => v.GotovyyeSborki)
                .HasForeignKey(gs => gs.VideokartaModel)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GotovayaSborka>()
                .HasOne(gs => gs.Nakopitel)
                .WithMany(n => n.GotovyyeSborki)
                .HasForeignKey(gs => gs.NakopitelModel)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GotovayaSborka>()
                .HasOne(gs => gs.BlokPitaniya)
                .WithMany(bp => bp.GotovyyeSborki)
                .HasForeignKey(gs => gs.BlokPitaniyaModel)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GotovayaSborka>()
                .HasOne(gs => gs.Korpus)
                .WithMany(k => k.GotovyyeSborki)
                .HasForeignKey(gs => gs.KorpusModel)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}