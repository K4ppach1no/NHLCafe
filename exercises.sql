-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 03, 2022 at 03:58 PM
-- Server version: 10.4.22-MariaDB
-- PHP Version: 8.1.2

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `exercises`
--

-- --------------------------------------------------------

--
-- Table structure for table `category`
--

CREATE TABLE `category` (
  `CategoryId` int(11) NOT NULL,
  `Name` varchar(128) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `category`
--

INSERT INTO `category` (`CategoryId`, `Name`) VALUES
(1, 'Frisdranken'),
(2, 'Bier'),
(3, 'Wijnen en aperitieven'),
(4, 'Warme dranken'),
(5, 'Speciaal bier');

-- --------------------------------------------------------

--
-- Table structure for table `product`
--

CREATE TABLE `product` (
  `ProductId` int(11) NOT NULL,
  `Name` varchar(128) CHARACTER SET utf8 NOT NULL,
  `CategoryId` int(11) NOT NULL,
  `Price` decimal(10,2) NOT NULL CHECK (`Price` > 0)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `product`
--

INSERT INTO `product` (`ProductId`, `Name`, `CategoryId`, `Price`) VALUES
(1, 'Dommelsch 0.22', 2, '2.30'),
(2, 'Dommelsch 0.25', 2, '2.50'),
(3, 'Dommelsch 0.50', 2, '4.50'),
(4, 'Jupiler N/A 0.0%', 2, '2.50'),
(5, 'Palm', 5, '3.40'),
(6, 'Hoegaarden witbier', 5, '3.40'),
(7, 'Hoegaarden Radler 0.0%', 5, '3.40'),
(8, 'Hoegaarden Radler 2.0%', 5, '3.40'),
(9, 'Leffe dubbel', 5, '3.75'),
(10, 'Leffe blond', 5, '3.75'),
(11, 'Leffe trippel', 5, '4.25'),
(12, 'Hoegaarden rosé', 5, '3.50'),
(13, 'Liefmans fruitesse', 5, '3.50'),
(14, 'Oud bruin', 5, '2.50'),
(15, 'Biestheuvel blond 6%', 5, '4.00'),
(16, 'Biestheuvel IPA 7%', 5, '4.50'),
(17, 'Biestheuvel Tripel 9%', 5, '4.50'),
(18, 'Coca-cola regular', 1, '2.30'),
(19, 'Coca-cola light', 1, '2.30'),
(20, 'Coca-cola zero', 1, '2.30'),
(21, 'Sprite', 1, '2.30'),
(22, 'Fanta orange', 1, '2.30'),
(23, 'Bitter Lemon', 1, '2.30'),
(24, 'Tonic', 1, '2.30'),
(25, 'Fanta Cassis', 1, '2.30'),
(26, 'Chaudfontainte still', 1, '2.30'),
(27, 'Chaudfontainte sparkling', 1, '2.30'),
(28, 'Lipton-ice tea regular', 1, '2.50'),
(29, 'Lipton-ice green', 1, '2.50'),
(30, 'Appelsap', 1, '2.50'),
(31, 'Jus d’orange', 1, '2.50'),
(32, 'Rivella', 1, '2.50'),
(33, 'Tomatensap', 1, '2.50'),
(34, 'Chocomel', 1, '2.50'),
(35, 'Fristi', 1, '2.50'),
(36, 'Huiswijnen Rood', 3, '3.75'),
(37, 'Huiswijnen Wit', 3, '3.75'),
(38, 'Huiswijnen Rose', 3, '3.75'),
(39, 'Port', 3, '3.75'),
(40, 'Sherry', 3, '3.75'),
(41, 'Vermouth', 3, '3.75'),
(42, 'Kop koffie', 4, '2.30'),
(43, 'Thee (Lipton)', 4, '2.30'),
(44, 'Cappuccino', 4, '2.50'),
(45, 'Latte Macchiato', 4, '2.50'),
(46, 'Koffie verkeerd', 4, '2.50'),
(47, 'Espresso', 4, '2.50'),
(48, 'Warme chocomel', 4, '3.00'),
(49, 'Warme chocomel met slagroom', 4, '3.50');

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE `user` (
  `UserId` int(11) NOT NULL,
  `UserName` varchar(255) NOT NULL,
  `Password` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `category`
--
ALTER TABLE `category`
  ADD PRIMARY KEY (`CategoryId`);

--
-- Indexes for table `product`
--
ALTER TABLE `product`
  ADD PRIMARY KEY (`ProductId`),
  ADD UNIQUE KEY `Name` (`Name`),
  ADD KEY `FK_ProductCategory` (`CategoryId`);

--
-- Indexes for table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`UserId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `category`
--
ALTER TABLE `category`
  MODIFY `CategoryId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `product`
--
ALTER TABLE `product`
  MODIFY `ProductId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=50;

--
-- AUTO_INCREMENT for table `user`
--
ALTER TABLE `user`
  MODIFY `UserId` int(11) NOT NULL AUTO_INCREMENT;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `product`
--
ALTER TABLE `product`
  ADD CONSTRAINT `FK_ProductCategory` FOREIGN KEY (`CategoryId`) REFERENCES `category` (`CategoryId`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
