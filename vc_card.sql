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
-- Table structure for table `card`
--

DROP TABLE IF EXISTS `card`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `card` (
  `ID_Doctor` int NOT NULL,
  `ID_Treatment` mediumint DEFAULT NULL,
  `ID_Card` mediumint NOT NULL AUTO_INCREMENT,
  `Test_Date` datetime DEFAULT NULL,
  `Complaints` text NOT NULL,
  `ID_Child` mediumint NOT NULL,
  `Dat` date NOT NULL,
  `Tim` varchar(20) NOT NULL,
  PRIMARY KEY (`ID_Card`),
  KEY `R_37` (`ID_Doctor`),
  KEY `R_38` (`Test_Date`),
  KEY `R_39` (`ID_Treatment`),
  KEY `R_44` (`ID_Child`),
  CONSTRAINT `card_ibfk_4` FOREIGN KEY (`ID_Child`) REFERENCES `child` (`ID_Child`),
  CONSTRAINT `card_ibfk_5` FOREIGN KEY (`ID_Treatment`) REFERENCES `treatment` (`ID_Treatment`),
  CONSTRAINT `card_ibfk_6` FOREIGN KEY (`Test_Date`) REFERENCES `analysis` (`Test_Date`)
) ENGINE=InnoDB AUTO_INCREMENT=65 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `card`
--

LOCK TABLES `card` WRITE;
/*!40000 ALTER TABLE `card` DISABLE KEYS */;
INSERT INTO `card` VALUES (1,NULL,41,NULL,'Температура',37,'2022-12-06','10:00:00'),(2,NULL,42,NULL,'Сутулится',38,'2022-12-06','09:00:00'),(3,NULL,43,NULL,'Сыпь',38,'2022-12-07','10:00:00'),(1,NULL,45,NULL,'Температура',40,'2022-12-07','13:30:00'),(2,NULL,49,NULL,'Болит живот',43,'2023-01-02','11:30:00'),(2,NULL,50,NULL,'Температура',43,'2022-12-12','10:30:00'),(1,NULL,51,'2022-12-12 09:00:00','Сильная головная боль',35,'2022-12-12','11:30:00'),(1,NULL,52,NULL,'Колики в животе',44,'2022-12-12','11:00:00'),(1,NULL,53,NULL,'Колики',44,'2022-12-12','10:30:00'),(1,NULL,54,NULL,'Колики',44,'2022-12-12','13:00:00'),(1,NULL,55,NULL,'Головная боль',35,'2022-12-13','11:00:00'),(3,NULL,56,NULL,'Колики',44,'2022-12-12','16:00:00'),(3,NULL,57,NULL,'Боль в животе',45,'2022-12-12','12:00:00'),(3,NULL,58,NULL,'Боль в животе',38,'2022-12-12','12:30:00'),(3,NULL,59,NULL,'Болит живот',45,'2022-12-12','09:00:00'),(3,NULL,60,NULL,'Боль',45,'2022-12-12','09:30:00'),(1,18,61,'2022-12-15 13:30:00','Боль в голове',35,'2022-12-12','16:30:00'),(3,NULL,62,NULL,'озноб',46,'2022-12-15','14:00:00'),(1,NULL,63,NULL,'температура 49',46,'2022-12-12','15:30:00'),(2,NULL,64,'2022-12-13 11:00:00','Колики',37,'2022-12-12','16:30:00');
/*!40000 ALTER TABLE `card` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `card_AFTER_DELETE` AFTER DELETE ON `card` FOR EACH ROW BEGIN
DELETE FROM Analysis WHERE Test_Date=Old.Test_Date;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-12-14 18:35:24
