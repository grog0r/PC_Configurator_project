-- 1. Удаление существующих таблиц (если они есть)
DROP TABLE IF EXISTS "gotovyyesborki" CASCADE;
DROP TABLE IF EXISTS "materinskiyeplaty" CASCADE;
DROP TABLE IF EXISTS "protsessory" CASCADE;
DROP TABLE IF EXISTS "operativnayapamyat" CASCADE;
DROP TABLE IF EXISTS "videokarty" CASCADE;
DROP TABLE IF EXISTS "nakopiteli" CASCADE;
DROP TABLE IF EXISTS "blokipitaniya" CASCADE;
DROP TABLE IF EXISTS "korpusa" CASCADE;

-- 2. Создание таблиц

-- Материнские платы
CREATE TABLE "materinskiyeplaty" (
    "Model" VARCHAR(100) PRIMARY KEY,
    "FormFaktor" VARCHAR(50) NOT NULL,
    "Soket" VARCHAR(50) NOT NULL,
    "TipPodderzhivayemoyOzu" VARCHAR(50) NOT NULL,
    "MaksimalnyyObyomOzuGb" INTEGER NOT NULL CHECK ("MaksimalnyyObyomOzuGb" > 0),
    "TipPodderzhivayemixNakopiteli" VARCHAR(100) NOT NULL,
    "Tsena" DECIMAL(10, 2) NOT NULL CHECK ("Tsena" >= 0)
);

-- Процессоры
CREATE TABLE "protsessory" (
    "Model" VARCHAR(100) PRIMARY KEY,
    "Soket" VARCHAR(50) NOT NULL,
    "Moshchnost" INTEGER NOT NULL CHECK ("Moshchnost" > 0),
    "TaktovayaChastotaGz" DECIMAL(4, 2) NOT NULL CHECK ("TaktovayaChastotaGz" > 0),
    "KolichestvoYader" INTEGER NOT NULL CHECK ("KolichestvoYader" > 0),
    "Tsena" DECIMAL(10, 2) NOT NULL CHECK ("Tsena" >= 0)
);

-- Оперативная память
CREATE TABLE "operativnayapamyat" (
    "Model" VARCHAR(100) PRIMARY KEY,
    "Tip" VARCHAR(20) NOT NULL,
    "ObyomModulyaGb" INTEGER NOT NULL CHECK ("ObyomModulyaGb" > 0),
    "ChastotaMgts" INTEGER NOT NULL CHECK ("ChastotaMgts" > 0),
    "Tsena" DECIMAL(10, 2) NOT NULL CHECK ("Tsena" >= 0)
);

-- Видеокарты
CREATE TABLE "videokarty" (
    "Model" VARCHAR(100) PRIMARY KEY,
    "VychislitelnayaMoshchnost" VARCHAR(50) NOT NULL,
    "ObyemPamyatiGb" INTEGER NOT NULL CHECK ("ObyemPamyatiGb" > 0),
    "Moshchnost" INTEGER NOT NULL CHECK ("Moshchnost" > 0),
    "Tsena" DECIMAL(10, 2) NOT NULL CHECK ("Tsena" >= 0)
);

-- Накопители
CREATE TABLE "nakopiteli" (
    "Model" VARCHAR(100) PRIMARY KEY,
    "Tip" VARCHAR(20) NOT NULL,
    "ObyomGb" INTEGER NOT NULL CHECK ("ObyomGb" > 0),
    "SkorostChteniyaMbS" INTEGER NOT NULL CHECK ("SkorostChteniyaMbS" > 0),
    "SkorostZapisiMbS" INTEGER NOT NULL CHECK ("SkorostZapisiMbS" > 0),
    "Tsena" DECIMAL(10, 2) NOT NULL CHECK ("Tsena" >= 0)
);

-- Блоки питания
CREATE TABLE "blokipitaniya" (
    "Model" VARCHAR(100) PRIMARY KEY,
    "MoshchnostVt" INTEGER NOT NULL CHECK ("MoshchnostVt" > 0),
    "Sertifikatsiya" VARCHAR(50) NOT NULL,
    "Tsena" DECIMAL(10, 2) NOT NULL CHECK ("Tsena" >= 0)
);

-- Корпуса
CREATE TABLE "korpusa" (
    "Model" VARCHAR(100) PRIMARY KEY,
    "PodderzhivayemyyeFormFaktory" VARCHAR(200) NOT NULL,
    "Razmery" VARCHAR(100) NOT NULL,
    "Tsena" DECIMAL(10, 2) NOT NULL CHECK ("Tsena" >= 0)
);

-- Готовые сборки
CREATE TABLE "gotovyyesborki" (
    "IdSborki" VARCHAR(50) PRIMARY KEY,
    "MaterinskayaPlataModel" VARCHAR(100) NOT NULL,
    "ProtsessorModel" VARCHAR(100) NOT NULL,
    "OperativnayaPamyatModel" VARCHAR(100) NOT NULL,
    "VideokartaModel" VARCHAR(100) NOT NULL,
    "NakopitelModel" VARCHAR(100) NOT NULL,
    "BlokPitaniyaModel" VARCHAR(100) NOT NULL,
    "KorpusModel" VARCHAR(100) NOT NULL,
    "VychislitelnayaMoshchnost" VARCHAR(50) NOT NULL,
    "ObshchayaStoimost" DECIMAL(10, 2) NOT NULL CHECK ("ObshchayaStoimost" >= 0),
    "MoshnostVt" INTEGER NOT NULL CHECK ("MoshnostVt" > 0),
    
    -- Внешние ключи
    CONSTRAINT "fk_materinskaya_plata" 
        FOREIGN KEY ("MaterinskayaPlataModel") 
        REFERENCES "materinskiyeplaty"("Model")
        ON DELETE CASCADE,
    
    CONSTRAINT "fk_protsessor" 
        FOREIGN KEY ("ProtsessorModel") 
        REFERENCES "protsessory"("Model")
        ON DELETE CASCADE,
    
    CONSTRAINT "fk_operativnaya_pamyat" 
        FOREIGN KEY ("OperativnayaPamyatModel") 
        REFERENCES "operativnayapamyat"("Model")
        ON DELETE CASCADE,
    
    CONSTRAINT "fk_videokarta" 
        FOREIGN KEY ("VideokartaModel") 
        REFERENCES "videokarty"("Model")
        ON DELETE CASCADE,
    
    CONSTRAINT "fk_nakopitel" 
        FOREIGN KEY ("NakopitelModel") 
        REFERENCES "nakopiteli"("Model")
        ON DELETE CASCADE,
    
    CONSTRAINT "fk_blok_pitaniya" 
        FOREIGN KEY ("BlokPitaniyaModel") 
        REFERENCES "blokipitaniya"("Model")
        ON DELETE CASCADE,
    
    CONSTRAINT "fk_korpus" 
        FOREIGN KEY ("KorpusModel") 
        REFERENCES "korpusa"("Model")
        ON DELETE CASCADE
);

-- 3. Создание индексов для ускорения поиска
CREATE INDEX "idx_protsessory_soket" ON "protsessory"("Soket");
CREATE INDEX "idx_operativnaya_pamyat_tip" ON "operativnayapamyat"("Tip");
CREATE INDEX "idx_nakopiteli_tip" ON "nakopiteli"("Tip");
CREATE INDEX "idx_gotovyye_sborki_components" ON "gotovyyesborki"(
    "MaterinskayaPlataModel", 
    "ProtsessorModel", 
    "OperativnayaPamyatModel"
);

-- 4. Вставка тестовых данных

-- Материнские платы
INSERT INTO "materinskiyeplaty" ("Model", "FormFaktor", "Soket", "TipPodderzhivayemoyOzu", "MaksimalnyyObyomOzuGb", "TipPodderzhivayemixNakopiteli", "Tsena") VALUES
('ASUS ROG Strix Z790-E', 'ATX', 'LGA1700', 'DDR5', 128, 'M.2 NVMe, SATA III', 34999.00),
('Gigabyte B760M DS3H', 'Micro-ATX', 'LGA1700', 'DDR5', 96, 'M.2 NVMe, SATA III', 12499.00),
('MSI MAG B550 Tomahawk', 'ATX', 'AM4', 'DDR4', 128, 'M.2 NVMe, SATA III', 15999.00),
('ASRock B660M Pro RS', 'Micro-ATX', 'LGA1700', 'DDR4', 64, 'M.2 NVMe, SATA III', 8999.00),
('ASUS TUF Gaming A620M-PLUS', 'Micro-ATX', 'AM5', 'DDR5', 96, 'M.2 NVMe, SATA III', 12999.00);

-- Процессоры
INSERT INTO "protsessory" ("Model", "Soket", "Moshchnost", "TaktovayaChastotaGz", "KolichestvoYader", "Tsena") VALUES
('Intel Core i9-13900K', 'LGA1700', 125, 3.00, 24, 56999.00),
('AMD Ryzen 9 7950X', 'AM5', 170, 4.50, 16, 59999.00),
('Intel Core i5-13400F', 'LGA1700', 65, 2.50, 10, 22999.00),
('AMD Ryzen 5 7600X', 'AM5', 105, 4.70, 6, 24999.00),
('Intel Core i3-12100', 'LGA1700', 60, 3.30, 4, 11999.00);

-- Оперативная память
INSERT INTO "operativnayapamyat" ("Model", "Tip", "ObyomModulyaGb", "ChastotaMgts", "Tsena") VALUES
('Kingston Fury Beast 32GB', 'DDR5', 32, 6000, 8999.00),
('Corsair Vengeance RGB 16GB', 'DDR4', 16, 3600, 5999.00),
('G.Skill Trident Z5 64GB', 'DDR5', 64, 6400, 19999.00),
('Crucial Basics 8GB', 'DDR4', 8, 3200, 2999.00),
('TeamGroup T-Force Delta 16GB', 'DDR5', 16, 5200, 4999.00);

-- Видеокарты
INSERT INTO "videokarty" ("Model", "VychislitelnayaMoshchnost", "ObyemPamyatiGb", "Moshchnost", "Tsena") VALUES
('NVIDIA GeForce RTX 4090', 'Высокая', 24, 450, 169999.00),
('AMD Radeon RX 7900 XTX', 'Высокая', 24, 355, 109999.00),
('NVIDIA GeForce RTX 4070 Ti', 'Средняя', 12, 285, 79999.00),
('AMD Radeon RX 7700 XT', 'Средняя', 12, 245, 44999.00),
('NVIDIA GeForce RTX 4060', 'Низкая', 8, 115, 32999.00);

-- Накопители
INSERT INTO "nakopiteli" ("Model", "Tip", "ObyomGb", "SkorostChteniyaMbS", "SkorostZapisiMbS", "Tsena") VALUES
('Samsung 990 Pro 2TB', 'NVMe', 2000, 7450, 6900, 17999.00),
('WD Black SN850X 1TB', 'NVMe', 1000, 7300, 6300, 9999.00),
('Crucial P3 Plus 500GB', 'NVMe', 500, 5000, 3600, 3999.00),
('Seagate BarraCuda 2TB', 'HDD', 2000, 210, 210, 5999.00),
('Kingston NV2 1TB', 'NVMe', 1000, 3500, 2100, 4999.00);

-- Блоки питания
INSERT INTO "blokipitaniya" ("Model", "MoshchnostVt", "Sertifikatsiya", "Tsena") VALUES
('Be quiet! Straight Power 11 1000W', 1000, '80 Plus Platinum', 15999.00),
('Corsair RM850x 850W', 850, '80 Plus Gold', 12999.00),
('Chieftec GPE-700S 700W', 700, '80 Plus Bronze', 5999.00),
('AeroCool VX Plus 600W', 600, '80 Plus Bronze', 3999.00),
('DeepCool PQ850M 850W', 850, '80 Plus Gold', 8999.00);

-- Корпуса
INSERT INTO "korpusa" ("Model", "PodderzhivayemyyeFormFaktory", "Razmery", "Tsena") VALUES
('Lian Li O11 Dynamic EVO', 'ATX, Micro-ATX, Mini-ITX', '465 x 285 x 459 мм', 14999.00),
('NZXT H5 Flow', 'ATX, Micro-ATX, Mini-ITX', '480 x 210 x 466 мм', 8999.00),
('DeepCool MATREXX 55 MESH', 'ATX, Micro-ATX, Mini-ITX', '440 x 210 x 480 мм', 4999.00),
('Fractal Design Pop Air', 'ATX, Micro-ATX', '473 x 215 x 454 мм', 7999.00),
('Cooler Master MasterBox Q300L', 'Micro-ATX, Mini-ITX', '387 x 230 x 381 мм', 3999.00);

-- 5. Вставка тестовых сборок (5 записей)
INSERT INTO "gotovyyesborki" ("IdSborki", "MaterinskayaPlataModel", "ProtsessorModel", "OperativnayaPamyatModel", "VideokartaModel", "NakopitelModel", "BlokPitaniyaModel", "KorpusModel", "VychislitelnayaMoshchnost", "ObshchayaStoimost", "MoshnostVt") VALUES
('GAMING-ULTRA', 'ASUS ROG Strix Z790-E', 'Intel Core i9-13900K', 'G.Skill Trident Z5 64GB', 'NVIDIA GeForce RTX 4090', 'Samsung 990 Pro 2TB', 'Be quiet! Straight Power 11 1000W', 'Lian Li O11 Dynamic EVO', 'Максимальная', 367990.00, 575),
('GAMING-PRO', 'Gigabyte B760M DS3H', 'Intel Core i5-13400F', 'Corsair Vengeance RGB 16GB', 'NVIDIA GeForce RTX 4070 Ti', 'WD Black SN850X 1TB', 'Corsair RM850x 850W', 'NZXT H5 Flow', 'Высокая', 158990.00, 350),
('OFFICE-BASIC', 'ASRock B660M Pro RS', 'Intel Core i3-12100', 'Crucial Basics 8GB', 'NVIDIA GeForce RTX 4060', 'Crucial P3 Plus 500GB', 'Chieftec GPE-700S 700W', 'DeepCool MATREXX 55 MESH', 'Низкая', 67990.00, 175),
('GAMING-AMD', 'MSI MAG B550 Tomahawk', 'AMD Ryzen 5 7600X', 'TeamGroup T-Force Delta 16GB', 'AMD Radeon RX 7700 XT', 'Kingston NV2 1TB', 'DeepCool PQ850M 850W', 'Fractal Design Pop Air', 'Средняя', 112990.00, 350),
('BUDGET-MATX', 'ASUS TUF Gaming A620M-PLUS', 'AMD Ryzen 5 7600X', 'Kingston Fury Beast 32GB', 'AMD Radeon RX 7700 XT', 'Seagate BarraCuda 2TB', 'AeroCool VX Plus 600W', 'Cooler Master MasterBox Q300L', 'Средняя', 114990.00, 350);

-- 6. Проверка данных
SELECT 'Таблицы успешно созданы!' as status;