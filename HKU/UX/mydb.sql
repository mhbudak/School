-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `mydb` DEFAULT CHARACTER SET utf8 ;
USE `mydb` ;

-- -----------------------------------------------------
-- Table `mydb`.`Genre`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Genre` (
  `idGenre` INT GENERATED ALWAYS AS (),
  `genreName` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idGenre`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Games`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Games` (
  `idGame` INT GENERATED ALWAYS AS (),
  `idGenre` INT NOT NULL,
  `gameName` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idGame`),
  INDEX `Genre_idx` (`idGenre` ASC),
  UNIQUE INDEX `idGame_UNIQUE` (`idGame` ASC),
  CONSTRAINT `Genre`
    FOREIGN KEY (`idGenre`)
    REFERENCES `mydb`.`Genre` (`idGenre`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Rolles`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Rolles` (
  `idRoles` INT GENERATED ALWAYS AS () VIRTUAL,
  `roleNames` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idRoles`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Users`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Users` (
  `idUsers` INT GENERATED ALWAYS AS (),
  `name` VARCHAR(45) NOT NULL,
  `password` LONGTEXT GENERATED ALWAYS AS (),
  `userRoll` INT NOT NULL,
  PRIMARY KEY (`idUsers`),
  UNIQUE INDEX `idUsers_UNIQUE` (`idUsers` ASC),
  INDEX `UserRolles_idx` (`userRoll` ASC),
  CONSTRAINT `UserRolles`
    FOREIGN KEY (`userRoll`)
    REFERENCES `mydb`.`Rolles` (`idRoles`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Games_Info`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Games_Info` (
  `idGameInfo` INT GENERATED ALWAYS AS (),
  `idGame` INT NOT NULL,
  `gameName` VARCHAR(45) NOT NULL,
  `gameImage` BLOB NULL,
  `information` LONGTEXT NULL,
  `idUsers` INT NOT NULL,
  `comments` LONGTEXT NOT NULL,
  PRIMARY KEY (`idGameInfo`),
  UNIQUE INDEX `idGame_UNIQUE` (`idGame` ASC),
  UNIQUE INDEX `idGameInfo_UNIQUE` (`idGameInfo` ASC),
  CONSTRAINT `Games`
    FOREIGN KEY (`idGame`)
    REFERENCES `mydb`.`Games` (`idGame`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `Users`
    FOREIGN KEY (`idGame`)
    REFERENCES `mydb`.`Users` (`idUsers`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
