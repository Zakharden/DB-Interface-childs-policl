-- MySQL dump 10.13  Distrib 8.0.28, for Win64 (x86_64)
--
-- Host: localhost    Database: vc
-- ------------------------------------------------------
-- Server version	8.0.28

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `child`
--

DROP TABLE IF EXISTS `child`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `child` (
  `ID_Client` mediumint NOT NULL,
  `Child_Name` varchar(30) NOT NULL,
  `ID_Child` mediumint NOT NULL AUTO_INCREMENT,
  `Age` tinyint NOT NULL,
  `ID_Gender` int NOT NULL,
  `ID_Type` int DEFAULT NULL,
  PRIMARY KEY (`ID_Child`),
  KEY `R_21` (`ID_Client`),
  KEY `R_35` (`ID_Gender`),
  KEY `R_43` (`ID_Type`),
  CONSTRAINT `child_ibfk_1` FOREIGN KEY (`ID_Client`) REFERENCES `client` (`ID_Client`)
) ENGINE=InnoDB AUTO_INCREMENT=48 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `child`
--

LOCK TABLES `child` WRITE;
/*!40000 ALTER TABLE `child` DISABLE KEYS */;
INSERT INTO `child` VALUES (15,'Никифоров М.А.',35,2,1,1),(15,'Васильцов М.А.',37,18,1,3),(16,'Романенко С.И.',38,10,2,2),(17,'Нефедова С.Р.',39,18,2,3),(17,'Надбитов Н.В.',40,16,1,3),(18,'Булаткин Н.Е.',41,4,1,2),(18,'Савинцева З.С.',42,1,2,1),(19,'Карасиков М.И.',43,11,4,7),(19,'Двуреченская С.З.',44,1,5,6),(16,'Вакуленко А.В.',45,12,5,7),(20,'Чучаев А.О.',46,3,4,8),(15,'Ларченко',47,2,4,6);
/*!40000 ALTER TABLE `child` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-12-14 18:35:24
