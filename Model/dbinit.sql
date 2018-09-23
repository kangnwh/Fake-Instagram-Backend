﻿-- MySQL Script generated by MySQL Workbench
-- Sat Sep 22 21:26:44 2018
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema sns
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema sns
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `sns` DEFAULT CHARACTER SET utf8 ;
USE `sns` ;

-- -----------------------------------------------------
-- Table `sns`.`User`
-- -----------------------------------------------------
DROP TABLE IF EXISTS  `sns`.`User` ;
CREATE TABLE IF NOT EXISTS `sns`.`User` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `username` VARCHAR(45) NOT NULL,
  `password` VARCHAR(100) NOT NULL,
  `name` VARCHAR(45) NOT NULL,
  `email` VARCHAR(45) NULL,
  `phone` VARCHAR(45) NULL,
  `dob` DATETIME NULL,
  `gender` CHAR(1) NULL,
  `createDate` DATETIME NULL,
  `avatarURL` VARCHAR(45) NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  UNIQUE INDEX `username_UNIQUE` (`username` ASC))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `sns`.`Post`
-- -----------------------------------------------------
DROP TABLE IF EXISTS  `sns`.`Post`;
CREATE TABLE IF NOT EXISTS `sns`.`Post` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `userId` INT NOT NULL,
  `content` VARCHAR(45) NULL,
  `location` VARCHAR(45) NULL,
  `createDate` DATETIME NOT NULL,
  `logi` DECIMAL(8,3) NULL,
  `lati` DECIMAL(8,3) NULL,
  PRIMARY KEY (`id`, `userId`),
  INDEX `fk_Post_User1_idx` (`userId` ASC),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  CONSTRAINT `fk_Post_User1`
    FOREIGN KEY (`userId`)
    REFERENCES `sns`.`User` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `sns`.`Image`
-- -----------------------------------------------------
DROP TABLE IF  EXISTS `sns`.`Image` ;
CREATE TABLE IF NOT EXISTS `sns`.`Image` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `postId` INT NOT NULL,
  `userId` INT NOT NULL,
  `imageURL` VARCHAR(45) NULL,
  `createDate` DATETIME NULL,
  PRIMARY KEY (`id`, `postId`, `userId`),
  INDEX `fk_Image_Post1_idx` (`postId` ASC),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  INDEX `fk_Image_User1_idx` (`userId` ASC),
  CONSTRAINT `fk_Image_Post1`
    FOREIGN KEY (`postId`)
    REFERENCES `sns`.`Post` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Image_User1`
    FOREIGN KEY (`userId`)
    REFERENCES `sns`.`User` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `sns`.`FollowRelation`
-- -----------------------------------------------------
DROP TABLE IF  EXISTS `sns`.`FollowRelation`;
CREATE TABLE IF NOT EXISTS `sns`.`FollowRelation` (
  `from` INT NOT NULL,
  `to` INT NOT NULL,
  `createDate` DATETIME NULL,
  PRIMARY KEY (`from`, `to`),
  INDEX `fk_User_has_User_User2_idx` (`to` ASC),
  INDEX `fk_User_has_User_User1_idx` (`from` ASC),
  CONSTRAINT `fk_User_has_User_User1`
    FOREIGN KEY (`from`)
    REFERENCES `sns`.`User` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_User_has_User_User2`
    FOREIGN KEY (`to`)
    REFERENCES `sns`.`User` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `sns`.`comment`
-- -----------------------------------------------------
DROP TABLE IF  EXISTS `sns`.`comment` ;
CREATE TABLE IF NOT EXISTS `sns`.`comment` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `postId` INT NOT NULL,
  `userId` INT NOT NULL,
  `content` VARCHAR(100) NULL,
  `createDate` DATETIME NOT NULL,
  PRIMARY KEY (`id`, `postId`, `userId`),
  INDEX `fk_comment_Post1_idx` (`postId` ASC),
  INDEX `fk_comment_User1_idx` (`userId` ASC),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  CONSTRAINT `fk_comment_Post1`
    FOREIGN KEY (`postId`)
    REFERENCES `sns`.`Post` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_comment_User1`
    FOREIGN KEY (`userId`)
    REFERENCES `sns`.`User` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `sns`.`UserLikePost`
-- -----------------------------------------------------
DROP TABLE IF  EXISTS `sns`.`UserLikePost`;
CREATE TABLE IF NOT EXISTS `sns`.`UserLikePost` (
  `userId` INT NOT NULL,
  `postId` INT NOT NULL,
  `Post_userId` INT NOT NULL,
  `createDate` DATETIME NULL,
  PRIMARY KEY (`userId`, `postId`, `Post_userId`),
  INDEX `fk_User_has_Post_Post1_idx` (`postId` ASC, `Post_userId` ASC),
  INDEX `fk_User_has_Post_User1_idx` (`userId` ASC),
  CONSTRAINT `fk_User_has_Post_User1`
    FOREIGN KEY (`userId`)
    REFERENCES `sns`.`User` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_User_has_Post_Post1`
    FOREIGN KEY (`postId` , `Post_userId`)
    REFERENCES `sns`.`Post` (`id` , `userId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;