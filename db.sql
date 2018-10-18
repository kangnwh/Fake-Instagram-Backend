-- MySQL dump 10.13  Distrib 5.5.61, for debian-linux-gnu (x86_64)
--
-- Host: localhost    Database: sns
-- ------------------------------------------------------
-- Server version	5.5.61-0ubuntu0.14.04.1

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `FollowRelation`
--

DROP TABLE IF EXISTS `FollowRelation`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `FollowRelation` (
  `from` int(11) NOT NULL,
  `to` int(11) NOT NULL,
  `createDate` datetime DEFAULT NULL,
  PRIMARY KEY (`from`,`to`),
  KEY `fk_User_has_User_User2_idx` (`to`),
  KEY `fk_User_has_User_User1_idx` (`from`),
  CONSTRAINT `fk_User_has_User_User1` FOREIGN KEY (`from`) REFERENCES `User` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_User_has_User_User2` FOREIGN KEY (`to`) REFERENCES `User` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `FollowRelation`
--

LOCK TABLES `FollowRelation` WRITE;
/*!40000 ALTER TABLE `FollowRelation` DISABLE KEYS */;
INSERT INTO `FollowRelation` VALUES (3,4,'2018-10-17 23:53:54'),(3,6,'2018-10-17 23:53:51'),(3,8,'2018-10-18 00:36:49'),(4,3,'2018-10-11 15:31:58'),(4,8,'2018-10-18 01:19:56'),(4,9,'2018-10-18 01:19:58'),(5,4,'2018-10-16 22:49:11'),(7,4,'2018-10-16 22:46:33'),(8,3,'2018-10-17 14:47:36'),(8,4,'2018-10-17 14:52:12'),(8,5,'2018-10-17 14:52:10'),(9,3,'2018-10-18 04:14:45'),(9,4,'2018-10-18 00:32:24'),(9,8,'2018-10-18 03:57:54');
/*!40000 ALTER TABLE `FollowRelation` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Image`
--

DROP TABLE IF EXISTS `Image`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Image` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `postId` int(11) NOT NULL,
  `userId` int(11) NOT NULL,
  `imageURL` varchar(45) DEFAULT NULL,
  `createDate` datetime DEFAULT NULL,
  PRIMARY KEY (`id`,`postId`,`userId`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `fk_Image_Post1_idx` (`postId`),
  KEY `fk_Image_User1_idx` (`userId`),
  CONSTRAINT `fk_Image_Post1` FOREIGN KEY (`postId`) REFERENCES `Post` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_Image_User1` FOREIGN KEY (`userId`) REFERENCES `User` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Image`
--

LOCK TABLES `Image` WRITE;
/*!40000 ALTER TABLE `Image` DISABLE KEYS */;
INSERT INTO `Image` VALUES (7,7,3,'32018_10_02T23_48_26mypic.png','2018-10-02 23:48:26'),(8,8,3,'32018_10_09T16_52_08mypic.png','2018-10-09 16:52:08'),(11,11,3,'32018_10_15T05_39_02mypic.png','2018-10-15 05:39:02'),(12,12,3,'32018_10_16T17_53_50mypic.png','2018-10-16 17:53:50'),(15,15,3,'32018_10_16T07_11_44mypic.png','2018-10-16 07:11:44'),(16,16,3,'32018_10_16T07_12_07mypic.png','2018-10-16 07:12:07'),(18,18,3,'32018_10_17T04_28_24mypic.png','2018-10-17 04:28:24'),(19,19,3,'32018_10_17T06_45_22mypic.png','2018-10-17 06:45:22'),(20,20,3,'32018_10_17T06_46_02mypic.png','2018-10-17 06:46:02'),(21,21,3,'32018_10_17T06_55_20mypic.png','2018-10-17 06:55:20'),(22,22,3,'32018_10_17T10_12_37mypic.png','2018-10-17 10:12:37'),(23,23,8,'82018_10_17T14_48_12mypic.png','2018-10-17 14:48:12'),(25,25,9,'92018_10_18T04_01_19mypic.png','2018-10-18 04:01:19'),(26,26,9,'92018_10_18T04_02_48mypic.png','2018-10-18 04:02:48'),(27,27,9,'92018_10_18T04_05_09mypic.png','2018-10-18 04:05:09'),(28,28,9,'92018_10_18T04_09_21mypic.png','2018-10-18 04:09:21');
/*!40000 ALTER TABLE `Image` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Post`
--

DROP TABLE IF EXISTS `Post`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Post` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `userId` int(11) NOT NULL,
  `content` varchar(45) DEFAULT NULL,
  `location` varchar(45) DEFAULT NULL,
  `createDate` datetime NOT NULL,
  `logi` decimal(8,3) DEFAULT NULL,
  `lati` decimal(8,3) DEFAULT NULL,
  PRIMARY KEY (`id`,`userId`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `fk_Post_User1_idx` (`userId`),
  CONSTRAINT `fk_Post_User1` FOREIGN KEY (`userId`) REFERENCES `User` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Post`
--

LOCK TABLES `Post` WRITE;
/*!40000 ALTER TABLE `Post` DISABLE KEYS */;
INSERT INTO `Post` VALUES (7,4,'Test Comment','29 Guildford Lane,Melbourne,Australia,','2018-10-02 23:48:26',144.960,-37.811),(8,3,'Test Comment','Not Provided','2018-10-09 16:52:08',NULL,NULL),(9,4,'Test Comment','Not Provided','2018-10-09 21:09:30',NULL,NULL),(10,4,'Test Comment','Banbridge Place,Carlton,Australia','2018-10-15 04:32:38',144.962,-37.801),(11,3,'东拼西凑','Banbridge Place,Carlton,Australia','2018-10-15 05:39:02',144.962,-37.801),(12,3,'','','2018-10-16 17:53:50',NULL,NULL),(13,5,'Ning2 Account Post 1','Not Provided','2018-10-16 17:56:51',NULL,NULL),(14,5,'','','2018-10-16 17:57:40',NULL,NULL),(15,3,'Waterfall',NULL,'2018-10-16 07:11:44',NULL,NULL),(16,3,'Waterfall',NULL,'2018-10-16 07:12:07',NULL,NULL),(17,4,'Test Comment','29 Guildford Lane,Melbourne,Australia','2018-10-16 10:43:10',144.960,-37.811),(18,3,'','','2018-10-17 04:28:24',NULL,NULL),(19,3,'Test Comment','墨尔本大学,斯帕克维尔,澳大利亚','2018-10-17 06:45:22',144.963,-37.799),(20,3,'咸鱼～','墨尔本大学,斯帕克维尔,澳大利亚','2018-10-17 06:46:02',144.963,-37.799),(21,3,'Biubiubiu','Redmond Barry,斯帕克维尔,澳大利亚','2018-10-17 06:55:20',144.963,-37.797),(22,3,'～','5 Courtney Street,North Melbourne,澳大利亚','2018-10-17 10:12:37',144.955,-37.803),(23,8,'PUBG','5 Courtney Street,North Melbourne,澳大利亚','2018-10-17 14:48:12',144.955,-37.803),(24,4,'','','2018-10-18 00:34:31',NULL,NULL),(25,9,'Test Comment','427-433 Swanston Street,墨尔本,澳大利亚','2018-10-18 04:01:19',144.963,-37.808),(26,9,'Test Comment','427-433 Swanston Street,墨尔本,澳大利亚','2018-10-18 04:02:48',144.963,-37.808),(27,9,'','','2018-10-18 04:05:09',NULL,NULL),(28,9,'诡异的鼠标','427-433 Swanston Street,墨尔本,澳大利亚','2018-10-18 04:09:21',144.963,-37.808);
/*!40000 ALTER TABLE `Post` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `User`
--

DROP TABLE IF EXISTS `User`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `User` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(45) NOT NULL,
  `password` varchar(100) NOT NULL,
  `name` varchar(45) NOT NULL,
  `email` varchar(45) DEFAULT NULL,
  `phone` varchar(45) DEFAULT NULL,
  `dob` datetime DEFAULT NULL,
  `gender` char(1) DEFAULT NULL,
  `createDate` datetime DEFAULT NULL,
  `avatarURL` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  UNIQUE KEY `username_UNIQUE` (`username`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `User`
--

LOCK TABLES `User` WRITE;
/*!40000 ALTER TABLE `User` DISABLE KEYS */;
INSERT INTO `User` VALUES (3,'abc','AQAAAAEAACcQAAAAEFnN5RXPrXLS1mCfOgkkiyUY2bB09LfLFn9jFDdbcCV/oAa3ZrL8mvW3oE6JqW4D9g==','abc','a@hn.com','123','2018-09-22 12:24:23','M',NULL,'32018_10_17T04_28_24mypic.png'),(4,'Ning','AQAAAAEAACcQAAAAEBn3aCUBHZO+E6SVHnnQ5B1YktKpSXgoGbdGRdhfSM778ZfkfiLOsLzvn5YuF/tDlg==','Ning','a@ad.com','111111','2018-09-24 00:00:00','F',NULL,'42018_10_18T00_34_31mypic.png'),(5,'Ning2','AQAAAAEAACcQAAAAEAlkzoKUkwL0Qc+PrKnoHQ0ZGESXZAGisy+XeVglBYMZ7fv0h4KmEGKTaHGiX3ylUw==','Ning2','a@ad.com','111111','2018-09-24 00:00:00','F',NULL,'52018_10_16T17_57_39mypic.png'),(6,'Ning32','AQAAAAEAACcQAAAAECtlHfQoApDU07+nsogXTU4/lezMumXFMB1ci8dpgHtlb6nZZKAduocBUVPYUFqPbg==','Ning32','a@ad.com','111111','2018-09-24 00:00:00','F',NULL,NULL),(7,'Ning23','AQAAAAEAACcQAAAAEJG3qfQSaUQb1XHdGxz1ezlz+IFJp4/tRLhHzyOB9vUkSiFwzIgSMeWlItWebX3rdQ==','Ning23','a@ad.com','111111','2018-09-24 00:00:00','F',NULL,NULL),(8,'Raelyn_Lyu','AQAAAAEAACcQAAAAEOYdtIhdoEf3y1qe4OGOmVJwCuH7w9xq69M01h5RxwSfJQe7DMJWTHUCxLo03hNbNg==','Raelyn_Lyu','a@ad.com','111111','2018-10-18 00:00:00','F',NULL,NULL),(9,'xinwuwh','AQAAAAEAACcQAAAAEHvPm1ZLszomt77rnW4yj9KcPom9hobZOt5XrE9pxMyJdOvkDz8HaKdC38SGbTULmw==','Candice','a@ad.com','111111','2018-10-18 00:00:00','F',NULL,'92018_10_18T04_05_09mypic.png');
/*!40000 ALTER TABLE `User` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `UserLikePost`
--

DROP TABLE IF EXISTS `UserLikePost`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `UserLikePost` (
  `userId` int(11) NOT NULL,
  `postId` int(11) NOT NULL,
  `Post_userId` int(11) NOT NULL,
  `createDate` datetime DEFAULT NULL,
  PRIMARY KEY (`userId`,`postId`,`Post_userId`),
  KEY `fk_User_has_Post_Post1_idx` (`postId`,`Post_userId`),
  KEY `fk_User_has_Post_User1_idx` (`userId`),
  CONSTRAINT `fk_User_has_Post_User1` FOREIGN KEY (`userId`) REFERENCES `User` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_User_has_Post_Post1` FOREIGN KEY (`postId`, `Post_userId`) REFERENCES `Post` (`id`, `userId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `UserLikePost`
--

LOCK TABLES `UserLikePost` WRITE;
/*!40000 ALTER TABLE `UserLikePost` DISABLE KEYS */;
INSERT INTO `UserLikePost` VALUES (3,10,4,'2018-10-18 01:00:58'),(3,14,5,'2018-10-17 23:07:24'),(4,10,4,'2018-10-15 21:26:45'),(4,20,3,'2018-10-18 01:20:40'),(4,23,8,'2018-10-18 01:20:25'),(9,22,3,'2018-10-18 04:15:28'),(9,24,4,'2018-10-18 03:55:26');
/*!40000 ALTER TABLE `UserLikePost` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `comment`
--

DROP TABLE IF EXISTS `comment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `comment` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `postId` int(11) NOT NULL,
  `userId` int(11) NOT NULL,
  `content` varchar(100) DEFAULT NULL,
  `createDate` datetime NOT NULL,
  PRIMARY KEY (`id`,`postId`,`userId`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `fk_comment_Post1_idx` (`postId`),
  KEY `fk_comment_User1_idx` (`userId`),
  CONSTRAINT `fk_comment_Post1` FOREIGN KEY (`postId`) REFERENCES `Post` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_comment_User1` FOREIGN KEY (`userId`) REFERENCES `User` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `comment`
--

LOCK TABLES `comment` WRITE;
/*!40000 ALTER TABLE `comment` DISABLE KEYS */;
INSERT INTO `comment` VALUES (1,7,3,'Nicholas is a handsome boy!','2018-10-07 00:11:35'),(2,7,3,'hello world','2018-10-07 00:12:25'),(3,7,3,' ','2018-10-07 00:15:08'),(4,7,3,' ','2018-10-07 00:15:18'),(5,7,3,'','2018-10-07 00:15:54'),(6,13,3,'dasdasdasd','2018-10-17 16:30:24'),(7,14,3,'great picture!','2018-10-17 23:10:28'),(8,14,3,'Amazing photo!','2018-10-17 23:21:38'),(9,10,3,'Amazing!','2018-10-17 13:43:11'),(10,10,3,'阔爱','2018-10-18 01:00:44'),(11,23,4,'Chicken tonight','2018-10-18 01:20:23'),(12,24,9,'So cute','2018-10-18 03:54:29');
/*!40000 ALTER TABLE `comment` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-10-18  4:52:25
