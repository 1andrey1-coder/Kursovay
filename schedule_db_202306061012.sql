--
-- Скрипт сгенерирован Devart dbForge Studio 2020 for MySQL, Версия 9.0.391.0
-- Домашняя страница продукта: http://www.devart.com/ru/dbforge/mysql/studio
-- Дата скрипта: 06.06.2023 10:12:41
-- Версия сервера: 8.0.31
-- Версия клиента: 4.1
--

-- 
-- Отключение внешних ключей
-- 
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;

-- 
-- Установить режим SQL (SQL mode)
-- 
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- 
-- Установка кодировки, с использованием которой клиент будет посылать запросы на сервер
--
SET NAMES 'utf8mb4';

--
-- Установка базы данных по умолчанию
--
USE schedule_db;

--
-- Удалить таблицу `pair1`
--
DROP TABLE IF EXISTS pair1;

--
-- Удалить таблицу `tbl_audit`
--
DROP TABLE IF EXISTS tbl_audit;

--
-- Удалить таблицу `tbl_pairs`
--
DROP TABLE IF EXISTS tbl_pairs;

--
-- Удалить таблицу `tbl_predmet`
--
DROP TABLE IF EXISTS tbl_predmet;

--
-- Удалить таблицу `tbl_predmet2`
--
DROP TABLE IF EXISTS tbl_predmet2;

--
-- Удалить таблицу `tbl_obpred`
--
DROP TABLE IF EXISTS tbl_obpred;

--
-- Удалить таблицу `tbl_name`
--
DROP TABLE IF EXISTS tbl_name;

--
-- Удалить таблицу `tbl_replacement`
--
DROP TABLE IF EXISTS tbl_replacement;

--
-- Удалить таблицу `tbl_weekdays`
--
DROP TABLE IF EXISTS tbl_weekdays;

--
-- Удалить таблицу `tbl_schedule_db`
--
DROP TABLE IF EXISTS tbl_schedule_db;

--
-- Удалить таблицу `tbl_group`
--
DROP TABLE IF EXISTS tbl_group;

--
-- Удалить таблицу `tbl_speciality`
--
DROP TABLE IF EXISTS tbl_speciality;

--
-- Удалить таблицу `tbl_semestr`
--
DROP TABLE IF EXISTS tbl_semestr;

--
-- Удалить таблицу `tbl_course`
--
DROP TABLE IF EXISTS tbl_course;

--
-- Установка базы данных по умолчанию
--
USE schedule_db;

--
-- Создать таблицу `tbl_course`
--
CREATE TABLE tbl_course (
  id int NOT NULL AUTO_INCREMENT,
  Course int DEFAULT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 5,
AVG_ROW_LENGTH = 4096,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать индекс `FK_tbl_course` для объекта типа таблица `tbl_course`
--
ALTER TABLE tbl_course
ADD INDEX FK_tbl_course (Course);

--
-- Создать таблицу `tbl_semestr`
--
CREATE TABLE tbl_semestr (
  id int NOT NULL AUTO_INCREMENT,
  CourseID int DEFAULT NULL,
  semesterNuber int DEFAULT NULL,
  semesterWeek int DEFAULT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 9,
AVG_ROW_LENGTH = 2048,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать индекс `FK_tbl_semestr_tbl_group_GroupID` для объекта типа таблица `tbl_semestr`
--
ALTER TABLE tbl_semestr
ADD INDEX FK_tbl_semestr_tbl_group_GroupID (semesterNuber);

--
-- Создать индекс `FK_tbl_semestr_tbl_obpred_id` для объекта типа таблица `tbl_semestr`
--
ALTER TABLE tbl_semestr
ADD INDEX FK_tbl_semestr_tbl_obpred_id (CourseID);

--
-- Создать внешний ключ
--
ALTER TABLE tbl_semestr
ADD CONSTRAINT FK_tbl_semestr_tbl_course_id FOREIGN KEY (CourseID)
REFERENCES tbl_course (id);

--
-- Создать таблицу `tbl_speciality`
--
CREATE TABLE tbl_speciality (
  specialityID int NOT NULL AUTO_INCREMENT,
  speciality varchar(255) DEFAULT NULL,
  PRIMARY KEY (specialityID)
)
ENGINE = INNODB,
AUTO_INCREMENT = 3,
AVG_ROW_LENGTH = 8192,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать таблицу `tbl_group`
--
CREATE TABLE tbl_group (
  GroupID int NOT NULL AUTO_INCREMENT,
  `Group` int NOT NULL,
  specialityID int DEFAULT NULL,
  courseID int DEFAULT NULL,
  semestrNuberID int DEFAULT NULL,
  semestrWeekID int DEFAULT NULL,
  PRIMARY KEY (GroupID)
)
ENGINE = INNODB,
AUTO_INCREMENT = 37,
AVG_ROW_LENGTH = 455,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать внешний ключ
--
ALTER TABLE tbl_group
ADD CONSTRAINT FK_tbl_group FOREIGN KEY (semestrNuberID)
REFERENCES tbl_semestr (id);

--
-- Создать внешний ключ
--
ALTER TABLE tbl_group
ADD CONSTRAINT FK_tbl_group_tbl_course_id FOREIGN KEY (courseID)
REFERENCES tbl_course (id);

--
-- Создать внешний ключ
--
ALTER TABLE tbl_group
ADD CONSTRAINT FK_tbl_group_tbl_semestr_id FOREIGN KEY (semestrWeekID)
REFERENCES tbl_semestr (id);

--
-- Создать внешний ключ
--
ALTER TABLE tbl_group
ADD CONSTRAINT FK_tbl_group_tbl_speciality_specialityID FOREIGN KEY (specialityID)
REFERENCES tbl_speciality (specialityID);

--
-- Создать таблицу `tbl_schedule_db`
--
CREATE TABLE tbl_schedule_db (
  id int NOT NULL AUTO_INCREMENT,
  Name varchar(255) DEFAULT '',
  Predmet varchar(255) DEFAULT '',
  Cabinet double DEFAULT NULL,
  Day varchar(255) DEFAULT '',
  Pair int DEFAULT NULL,
  Groupid int DEFAULT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 187,
AVG_ROW_LENGTH = 2730,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать внешний ключ
--
ALTER TABLE tbl_schedule_db
ADD CONSTRAINT FK_tbl_schedule_db_tbl_group_GroupID FOREIGN KEY (Groupid)
REFERENCES tbl_group (GroupID);

--
-- Создать таблицу `tbl_weekdays`
--
CREATE TABLE tbl_weekdays (
  id int NOT NULL AUTO_INCREMENT,
  Day varchar(255) DEFAULT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 7,
AVG_ROW_LENGTH = 2730,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать таблицу `tbl_replacement`
--
CREATE TABLE tbl_replacement (
  Id int NOT NULL AUTO_INCREMENT,
  `Group` int DEFAULT NULL,
  Pair int DEFAULT NULL,
  Cabinet int DEFAULT NULL,
  Name varchar(255) DEFAULT '',
  Predmet varchar(255) DEFAULT '',
  weekdaysId int DEFAULT NULL,
  Date datetime DEFAULT NULL,
  PRIMARY KEY (Id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 108,
AVG_ROW_LENGTH = 2730,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать внешний ключ
--
ALTER TABLE tbl_replacement
ADD CONSTRAINT FK_tbl_replacement FOREIGN KEY (weekdaysId)
REFERENCES tbl_weekdays (id);

--
-- Создать таблицу `tbl_name`
--
CREATE TABLE tbl_name (
  id int NOT NULL AUTO_INCREMENT,
  Name varchar(50) DEFAULT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 57,
AVG_ROW_LENGTH = 364,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать таблицу `tbl_obpred`
--
CREATE TABLE tbl_obpred (
  id int NOT NULL AUTO_INCREMENT,
  predmet varchar(255) DEFAULT NULL,
  courseID int DEFAULT NULL,
  groupid int DEFAULT NULL,
  nameid int DEFAULT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 101,
AVG_ROW_LENGTH = 167,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать внешний ключ
--
ALTER TABLE tbl_obpred
ADD CONSTRAINT FK_tbl_obpred_tbl_course_id FOREIGN KEY (courseID)
REFERENCES tbl_course (id);

--
-- Создать внешний ключ
--
ALTER TABLE tbl_obpred
ADD CONSTRAINT FK_tbl_obpred_tbl_group_GroupID FOREIGN KEY (groupid)
REFERENCES tbl_group (GroupID);

--
-- Создать внешний ключ
--
ALTER TABLE tbl_obpred
ADD CONSTRAINT FK_tbl_obpred_tbl_name_id FOREIGN KEY (nameid)
REFERENCES tbl_name (id);

--
-- Создать таблицу `tbl_predmet2`
--
CREATE TABLE tbl_predmet2 (
  id int NOT NULL AUTO_INCREMENT,
  predmet2 varchar(255) DEFAULT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 21,
AVG_ROW_LENGTH = 862,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать таблицу `tbl_predmet`
--
CREATE TABLE tbl_predmet (
  id int NOT NULL AUTO_INCREMENT,
  predmet varchar(255) DEFAULT NULL,
  factionid int DEFAULT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 2,
AVG_ROW_LENGTH = 16384,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать индекс `FK_tbl_predmet_tbl_faction_id` для объекта типа таблица `tbl_predmet`
--
ALTER TABLE tbl_predmet
ADD INDEX FK_tbl_predmet_tbl_faction_id (factionid);

--
-- Создать таблицу `tbl_pairs`
--
CREATE TABLE tbl_pairs (
  Id int NOT NULL,
  Pair int DEFAULT NULL,
  PRIMARY KEY (Id)
)
ENGINE = INNODB,
AVG_ROW_LENGTH = 4096,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать таблицу `tbl_audit`
--
CREATE TABLE tbl_audit (
  Id int NOT NULL AUTO_INCREMENT,
  Audit double DEFAULT NULL,
  PRIMARY KEY (Id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 61,
AVG_ROW_LENGTH = 277,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать таблицу `pair1`
--
CREATE TABLE pair1 (
  id int NOT NULL AUTO_INCREMENT,
  StartTime date DEFAULT NULL,
  EndTime date DEFAULT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

-- 
-- Вывод данных для таблицы tbl_course
--
INSERT INTO tbl_course VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4);

-- 
-- Вывод данных для таблицы tbl_speciality
--
INSERT INTO tbl_speciality VALUES
(1, 'Mechanical separation'),
(2, 'Department of Economy and Service');

-- 
-- Вывод данных для таблицы tbl_semestr
--
INSERT INTO tbl_semestr VALUES
(1, 1, 1, 12),
(2, 2, 1, 10),
(3, 3, 1, 13),
(4, 4, 1, 11),
(5, 1, 2, 16),
(6, 2, 2, 20),
(7, 3, 2, 20),
(8, 4, 2, 7);

-- 
-- Вывод данных для таблицы tbl_weekdays
--
INSERT INTO tbl_weekdays VALUES
(1, 'Понедельник'),
(2, 'Вторник'),
(3, 'Среда'),
(4, 'Четверг'),
(5, 'Пятница'),
(6, 'Суббота');

-- 
-- Вывод данных для таблицы tbl_name
--
INSERT INTO tbl_name VALUES
(1, 'Тарасова Е.Ф.'),
(2, 'Кротов Г.Д.'),
(3, 'Гашенко О.В.'),
(4, 'Сахно И.В.'),
(5, 'Рахматулаев М.Р.'),
(6, 'Саркисян С.Р.'),
(7, 'Жанаева Л.С.'),
(8, 'Пугачева Е.Л.'),
(9, 'Васильева Рябчикова'),
(10, 'Дитковская/Здобина'),
(11, 'Пасещнюк Н.Б.'),
(12, 'Довгань С.В.'),
(13, 'Пискунова А.В.'),
(14, 'Малыхина Е.А.'),
(15, 'Горелкина Л.М.'),
(16, 'Сивкова О.В.'),
(17, 'Половец/Рябчиков'),
(18, 'Дубинина Е.С.'),
(19, 'Оселедец/Половец'),
(20, 'Савина А.А.'),
(21, 'Васильева/Оселедец'),
(22, 'Нуянцева А.В.'),
(23, 'Кузьменко В.А.'),
(24, 'Воробьев Н.А.'),
(25, 'Чуприкова Н.Н.'),
(26, 'Чернухина М.П.'),
(27, 'Глушенко Л.А.'),
(28, 'Голоско А.П.'),
(29, 'Полунин А.Н.'),
(30, 'Новоложилова Кульменко'),
(31, 'Зернова М.И.'),
(32, 'Михайлюк П.К.'),
(33, 'Новожилова И.Г.'),
(34, 'Лебедека А.С.'),
(35, 'Здобина/Авизова'),
(36, 'Васильева/Оседелец'),
(37, 'Здобина Е.В.'),
(38, 'Морехина/Половец'),
(39, 'Малыхина Аркелин'),
(40, 'Абросимова М.М.'),
(41, 'Абросимова М.П.'),
(42, 'Морозова Рябчикова'),
(43, 'Аракелин А.С.'),
(44, 'Володичев А.В.'),
(45, 'Чиркова Н.Н.'),
(46, 'Домирукрая Васильская');

-- 
-- Вывод данных для таблицы tbl_group
--
INSERT INTO tbl_group VALUES
(1, 311, 2, 1, 1, 1),
(2, 321, 2, 2, 2, 2),
(3, 331, 2, 3, 3, 3),
(4, 341, 2, 4, 4, 4),
(5, 915, 2, 1, 1, 1),
(6, 925, 2, 2, 2, 2),
(7, 935, 2, 3, 3, 3),
(8, 945, 2, 4, 4, 4),
(9, 916, 2, 1, 1, 1),
(10, 926, 2, 2, 2, 2),
(11, 936, 2, 3, 3, 3),
(12, 946, 2, 4, 4, 4),
(13, 1011, 2, 1, 1, 1),
(14, 1021, 2, 2, 2, 2),
(15, 1031, 2, 3, 3, 3),
(16, 1041, 2, 4, 4, 4),
(17, 1115, 2, 1, 1, 1),
(18, 1125, 2, 2, 2, 2),
(19, 1135, 2, 3, 3, 3),
(20, 1145, 2, 4, 4, 4),
(21, 415, 1, 1, 1, 1),
(22, 425, 1, 2, 2, 2),
(23, 435, 1, 3, 3, 3),
(24, 445, 1, 4, 4, 4),
(25, 416, 1, 1, 1, 1),
(26, 426, 1, 2, 2, 2),
(27, 436, 1, 3, 3, 3),
(28, 446, 1, 4, 4, 4),
(29, 611, 1, 1, 1, 1),
(30, 621, 1, 2, 2, 2),
(31, 631, 1, 3, 3, 3),
(32, 641, 1, 4, 4, 4),
(33, 1711, 1, 1, 1, 1),
(34, 1721, 1, 2, 2, 2),
(35, 1731, 1, 3, 3, 3),
(36, 1741, 1, 4, 4, 4);

-- 
-- Вывод данных для таблицы tbl_schedule_db
--
INSERT INTO tbl_schedule_db VALUES
(147, 'Пушкин А.А.', 'ОАИП', 4, 'Понедельник', 1, 1),
(148, 'Довгань С.В.', 'Теория вероят.', 404, 'Понедельник', 2, 1),
(149, 'Побежимов М.В.', 'Осн.проект.бд', 1, 'Понедельник', 3, 1),
(150, 'Борсова О.И.', 'Осн.фин грам.', 304, 'Вторник', 1, 1),
(151, '', '', NULL, '1', NULL, 3),
(183, '', '', NULL, '', NULL, NULL),
(184, '', '', NULL, '', NULL, NULL),
(185, 'Кротов Г.Д.', 'Введение в сп', 101, 'Вторник', 3, 1);

-- 
-- Вывод данных для таблицы tbl_replacement
--
INSERT INTO tbl_replacement VALUES
(103, 311, 2, 202, 'Тарасова Е.Ф.', 'ДОУКул.стрАТР', 2, '2023-06-04 16:21:26'),
(104, 311, 1, 100, 'Гашенко О.В.', 'Физкультура', 1, '2023-06-05 19:02:04'),
(105, 311, 3, 202, 'Кротов Г.Д.', 'Англ.яз', 3, '2023-06-05 19:03:02'),
(106, 311, 2, 26, 'Сахно И.В.', 'История', 1, '2023-06-06 09:20:06'),
(107, 311, 3, 2, 'а', 'и', 1, '2023-06-05 00:00:00');

-- 
-- Вывод данных для таблицы tbl_predmet2
--
INSERT INTO tbl_predmet2 VALUES
(1, 'Физкультура'),
(2, 'Англ.яз'),
(3, 'Введение в сп'),
(4, 'ДОУ Инж.комп гр-ка'),
(5, 'ДОУ кор.курс Р.Я.'),
(6, 'ДОУ МХК'),
(7, 'ДОУ пр.матем'),
(8, 'ДОУ прикл.физика'),
(9, 'ДОУИстория ПК'),
(10, 'ДОУКул.стрАТР'),
(11, 'Информатика'),
(12, 'История'),
(13, 'Литература'),
(14, 'Математика'),
(15, 'ОБЖ'),
(16, 'Родн.литерат'),
(17, 'Русский язык'),
(18, 'Физика'),
(19, 'Экономика');

-- 
-- Вывод данных для таблицы tbl_predmet
--
INSERT INTO tbl_predmet VALUES
(1, 'History', 1);

-- 
-- Вывод данных для таблицы tbl_pairs
--
INSERT INTO tbl_pairs VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5);

-- 
-- Вывод данных для таблицы tbl_obpred
--
INSERT INTO tbl_obpred VALUES
(1, 'Физкультура', 1, 1, 1),
(2, 'Англ.яз', 1, 1, 9),
(3, 'История', 1, 1, 8),
(4, 'ОБЖ', 1, 1, 15),
(5, 'Литература', 1, 1, 7),
(6, 'Информатика', 1, 1, 10),
(7, 'Экономика', 1, 1, 11),
(8, 'История', 1, 5, 8),
(9, 'Введение в сп', 1, 1, 13),
(10, 'Родн.литерат', 1, 1, 7),
(11, 'Русский язык', 1, 1, 7),
(12, 'ДОУКул.стрАТР', 1, 5, 11),
(13, 'ДОУИстория ПК', 1, 5, 8),
(14, 'ДОУКул.стрАТР', 1, 9, 11),
(15, 'Физкультура', 1, 5, 2),
(16, 'Математика', 1, 1, 12),
(17, 'Математика', 1, 5, 12),
(18, 'ДОУИстория ПК', 1, 9, 8),
(19, 'Русский язык', 1, 5, 7),
(20, 'Физкультура', 1, 21, 4),
(21, 'Физкультура', 1, 25, 6),
(22, 'Физкультура', 1, 9, 1),
(23, 'Физкультура', 1, 17, 3),
(24, 'Физкультура', 1, 13, 1),
(25, 'Физкультура', 1, 29, 4),
(26, 'Физкультура', 1, 33, 2),
(27, 'Русский язык', 1, 21, 25),
(28, 'Русский язык', 1, 25, 25),
(29, 'Русский язык', 1, 9, 7),
(30, 'Русский язык', 1, 17, 25),
(31, 'Русский язык', 1, 13, 7),
(32, 'Русский язык', 1, 29, 1),
(33, 'Русский язык', 1, 33, 7),
(34, 'Математика', 1, 21, 31),
(35, 'Математика', 1, 25, 31),
(36, 'Математика', 1, 9, 12),
(37, 'Математика', 1, 17, 22),
(38, 'Математика', 1, 13, 12),
(39, 'Математика', 1, 29, 31),
(40, 'Математика', 1, 33, 31),
(41, 'ОБЖ', 1, 5, 15),
(42, 'ОБЖ', 1, 21, 29),
(43, 'ОБЖ', 1, 25, 29),
(44, 'ОБЖ', 1, 9, 15),
(46, 'ОБЖ', 1, 13, 15),
(47, 'ОБЖ', 1, 29, 23),
(48, 'История', 1, 21, 27),
(49, 'История', 1, 25, 27),
(50, 'История', 1, 9, 8),
(51, 'История', 1, 17, 8),
(52, 'История', 1, 13, 8),
(53, 'История', 1, 29, 8),
(54, 'История', 1, 33, 27),
(55, 'Информатика', 1, 5, 14),
(56, 'Информатика', 1, 21, 30),
(57, 'Информатика', 1, 25, 14),
(58, 'Информатика', 1, 9, 14),
(59, 'Информатика', 1, 17, 10),
(60, 'Информатика', 1, 13, 35),
(61, 'Информатика', 1, 29, 39),
(62, 'Информатика', 1, 33, 30),
(63, 'Родн.литерат', 1, 5, 7),
(64, 'Родн.литерат', 1, 21, 25),
(65, 'Родн.литерат', 1, 25, 25),
(66, 'Родн.литерат', 1, 9, 7),
(67, 'Родн.литерат', 1, 17, 25),
(68, 'Родн.литерат', 1, 13, 7),
(69, 'Родн.литерат', 1, 29, 45),
(70, 'Англ.яз', 1, 5, 46),
(71, 'Англ.яз', 1, 21, 19),
(72, 'Англ.яз', 1, 25, 38),
(73, 'Англ.яз', 1, 9, 17),
(74, 'Англ.яз', 1, 17, 21),
(75, 'Англ.яз', 1, 13, 19),
(76, 'Англ.яз', 1, 29, 42),
(77, 'Англ.яз', 1, 33, 42),
(78, 'Введение в сп', 1, 5, 16),
(79, 'Введение в сп', 1, 21, 28),
(80, 'Введение в сп', 1, 25, 44),
(81, 'Введение в сп', 1, 9, 16),
(82, 'Введение в сп', 1, 17, 24),
(83, 'Введение в сп', 1, 13, 18),
(84, 'Введение в сп', 1, 29, 34),
(85, 'Введение в сп', 1, 33, 32),
(86, 'ДОУ кор.курс Р.Я.', 1, 13, 20),
(87, 'Физика', 1, 17, 5),
(88, 'Физика', 1, 21, 5),
(89, 'Физика', 1, 25, 5),
(90, 'Физика', 1, 33, 5),
(91, 'ДОУ МХК', 1, 13, 15),
(92, 'ДОУ Инж.комп.г', 1, 17, 23),
(93, 'ДОУ пр.математика', 1, 17, 37),
(94, 'ДОУ Инж.комп.г', 1, 21, 33),
(95, 'ДОУ прикл.физика', 1, 21, 26),
(96, 'ДОУ пр.матем', 1, 29, 41),
(97, 'ДОУ Инж.комп.г', 1, 29, 43),
(98, 'ДОУ пр.математика', 1, 33, 31),
(99, 'ДОУ Инж.комп.г', 1, 33, 33);

-- 
-- Вывод данных для таблицы tbl_audit
--
INSERT INTO tbl_audit VALUES
(1, 100),
(2, 101),
(3, 102),
(4, 103),
(5, 104),
(6, 201),
(7, 202),
(8, 301),
(9, 302),
(10, 303),
(11, 304),
(12, 305),
(13, 306),
(14, 401),
(15, 402),
(16, 403),
(17, 404),
(18, 405),
(19, 406),
(20, 1),
(21, 4),
(22, 5.102),
(23, 5.8),
(24, 1.301),
(25, 7.8),
(26, 7),
(27, 302.305),
(28, 301.103),
(29, 101.8),
(30, 101.5),
(31, 101.301),
(32, 7.101),
(33, 103.305),
(34, 102.8),
(35, 202.1),
(36, 21),
(37, 24),
(38, 25),
(39, 26),
(40, 27),
(41, 28),
(42, 29),
(43, 30),
(44, 21.26),
(45, 8.403),
(46, 25.404),
(47, 26.406),
(48, 102.402),
(49, 7.5),
(50, 20),
(51, 31),
(52, 32),
(53, 301.302),
(54, 7.102),
(55, 402.102),
(56, 0);

-- 
-- Вывод данных для таблицы pair1
--
-- Таблица schedule_db.pair1 не содержит данных

-- 
-- Восстановить предыдущий режим SQL (SQL mode)
--
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;

-- 
-- Включение внешних ключей
-- 
/*!40014 SET FOREIGN_KEY_CHECKS = @OLD_FOREIGN_KEY_CHECKS */;