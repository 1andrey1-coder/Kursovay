--
-- Скрипт сгенерирован Devart dbForge Studio 2020 for MySQL, Версия 9.0.391.0
-- Домашняя страница продукта: http://www.devart.com/ru/dbforge/mysql/studio
-- Дата скрипта: 25.05.2023 20:25:31
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
-- Удалить таблицу `tbl_predmet`
--
DROP TABLE IF EXISTS tbl_predmet;

--
-- Удалить таблицу `tbl_semester`
--
DROP TABLE IF EXISTS tbl_semester;

--
-- Удалить таблицу `tbl_day`
--
DROP TABLE IF EXISTS tbl_day;

--
-- Удалить таблицу `tbl_pairs`
--
DROP TABLE IF EXISTS tbl_pairs;

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
-- Удалить таблицу `tbl_obpred`
--
DROP TABLE IF EXISTS tbl_obpred;

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
-- Создать таблицу `tbl_obpred`
--
CREATE TABLE tbl_obpred (
  id int NOT NULL AUTO_INCREMENT,
  predmet varchar(255) DEFAULT NULL,
  courseID int DEFAULT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 17,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать внешний ключ
--
ALTER TABLE tbl_obpred
ADD CONSTRAINT FK_tbl_obpred_tbl_course_id FOREIGN KEY (courseID)
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
  Cabinet int DEFAULT NULL,
  Day varchar(255) DEFAULT '',
  Pair int DEFAULT NULL,
  Groupid int DEFAULT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 154,
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
AUTO_INCREMENT = 101,
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
-- Создать таблицу `tbl_pairs`
--
CREATE TABLE tbl_pairs (
  Id int NOT NULL,
  Pairid int DEFAULT NULL,
  PRIMARY KEY (Id)
)
ENGINE = INNODB,
AVG_ROW_LENGTH = 4096,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать таблицу `tbl_day`
--
CREATE TABLE tbl_day (
  Id int NOT NULL AUTO_INCREMENT,
  DaySId int DEFAULT NULL,
  PairId int DEFAULT NULL,
  PRIMARY KEY (Id)
)
ENGINE = INNODB,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать внешний ключ
--
ALTER TABLE tbl_day
ADD CONSTRAINT FK_tbl_day_tbl_pairs_Id FOREIGN KEY (PairId)
REFERENCES tbl_pairs (Id);

--
-- Создать таблицу `tbl_semester`
--
CREATE TABLE tbl_semester (
  ID int NOT NULL,
  semesterNumber int DEFAULT NULL,
  semesterWeek int DEFAULT NULL,
  specialityID int DEFAULT NULL,
  PRIMARY KEY (ID)
)
ENGINE = INNODB,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать индекс `FK_tbl_predmetid_tbl_speciality_specialityID` для объекта типа таблица `tbl_semester`
--
ALTER TABLE tbl_semester
ADD INDEX FK_tbl_predmetid_tbl_speciality_specialityID (specialityID);

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
-- Вывод данных для таблицы tbl_pairs
--
INSERT INTO tbl_pairs VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4);

-- 
-- Вывод данных для таблицы tbl_semester
--
-- Таблица schedule_db.tbl_semester не содержит данных

-- 
-- Вывод данных для таблицы tbl_schedule_db
--
INSERT INTO tbl_schedule_db VALUES
(147, 'Пушкин А.А.', 'ОАИП', 4, 'Понедельник', 1, 1),
(148, 'Довгань С.В.', 'Теория вероят.', 404, 'Понедельник', 2, 1),
(149, 'Побежимов М.В.', 'Осн.проект.бд', 1, 'Понедельник', 3, 1),
(150, 'Борсова О.И.', 'Осн.фин грам.', 304, 'Вторник', 1, 1),
(151, '', '', NULL, '1', NULL, 3),
(152, '', '', NULL, 'пу', NULL, 5);

-- 
-- Вывод данных для таблицы tbl_replacement
--
INSERT INTO tbl_replacement VALUES
(92, 311, 2, 403, 'Довгань С.В.', 'Математика', 1, '2023-05-22 00:00:00'),
(93, 321, 2, 403, 'Глушенко', 'История', 1, '2023-05-23 00:00:00'),
(96, 311, 2, 403, 'Довгань С.В.', 'Математика', 1, '2023-05-23 00:00:00'),
(98, 311, 2, 403, 'LJdufym', 'Алля', 1, '2023-05-24 00:00:00'),
(99, 1225, 3, 33, 'СУ', 'аа', 1, '2023-05-24 18:25:24'),
(100, 311, NULL, NULL, '', '', 2, '2023-05-23 19:52:02');

-- 
-- Вывод данных для таблицы tbl_predmet
--
INSERT INTO tbl_predmet VALUES
(1, 'History', 1);

-- 
-- Вывод данных для таблицы tbl_obpred
--
INSERT INTO tbl_obpred VALUES
(1, 'Физкультура', 1),
(2, 'Англ.яз', 1),
(3, 'История', 1),
(4, NULL, NULL),
(5, NULL, NULL),
(6, NULL, NULL),
(7, NULL, NULL),
(8, NULL, NULL),
(9, NULL, NULL),
(10, NULL, NULL),
(11, NULL, NULL),
(12, NULL, NULL),
(13, NULL, NULL),
(14, NULL, NULL),
(15, NULL, NULL),
(16, NULL, NULL);

-- 
-- Вывод данных для таблицы tbl_day
--
-- Таблица schedule_db.tbl_day не содержит данных

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