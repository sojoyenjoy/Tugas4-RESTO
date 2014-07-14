-- phpMyAdmin SQL Dump
-- version 3.5.8.1
-- http://www.phpmyadmin.net
--
-- Inang: localhost
-- Waktu pembuatan: 14 Jul 2014 pada 11.15
-- Versi Server: 5.5.16
-- Versi PHP: 5.3.8

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Basis data: `resto`
--

-- --------------------------------------------------------

--
-- Struktur dari tabel `kategori`
--

CREATE TABLE IF NOT EXISTS `kategori` (
  `ID` int(2) NOT NULL AUTO_INCREMENT,
  `Nama` varchar(10) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=3 ;

--
-- Dumping data untuk tabel `kategori`
--

INSERT INTO `kategori` (`ID`, `Nama`) VALUES
(1, 'Makanan'),
(2, 'Minuman');

-- --------------------------------------------------------

--
-- Struktur dari tabel `level`
--

CREATE TABLE IF NOT EXISTS `level` (
  `ID` int(5) NOT NULL AUTO_INCREMENT,
  `Level` varchar(10) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=3 ;

--
-- Dumping data untuk tabel `level`
--

INSERT INTO `level` (`ID`, `Level`) VALUES
(1, 'Pegawai'),
(2, 'Member');

-- --------------------------------------------------------

--
-- Struktur dari tabel `tabelmenu`
--

CREATE TABLE IF NOT EXISTS `tabelmenu` (
  `ID` varchar(10) NOT NULL,
  `Kategori_id` int(1) NOT NULL,
  `Nama` varchar(15) NOT NULL,
  `Harga` int(7) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data untuk tabel `tabelmenu`
--

INSERT INTO `tabelmenu` (`ID`, `Kategori_id`, `Nama`, `Harga`) VALUES
('IDMN-1', 1, 'Lasagna', 11000),
('IDMN-2', 1, 'Spagethi', 12000),
('IDMN-3', 1, 'Kwetiau', 8000),
('IDMN-4', 1, 'NasiBakar', 7500),
('IDMN-5', 1, 'Nasi Goreng', 10000),
('IDMN-6', 2, 'Jus Melon', 7000);

-- --------------------------------------------------------

--
-- Struktur dari tabel `transaksi`
--

CREATE TABLE IF NOT EXISTS `transaksi` (
  `No_Trans` varchar(10) NOT NULL,
  `Order_id` varchar(10) NOT NULL,
  `JumlahBeli` int(5) NOT NULL,
  `JmlItem` int(5) NOT NULL,
  `TotalBayar` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data untuk tabel `transaksi`
--

INSERT INTO `transaksi` (`No_Trans`, `Order_id`, `JumlahBeli`, `JmlItem`, `TotalBayar`) VALUES
(' TRKD-1', 'IDMN-1', 1, 3, 'Rp.35000'),
(' TRKD-1', 'IDMN-2', 2, 3, 'Rp.35000'),
(' TRKD-2', 'IDMN-2', 2, 9, 'Rp.79500'),
(' TRKD-2', 'IDMN-4', 1, 9, 'Rp.79500'),
(' TRKD-2', 'IDMN-5', 2, 9, 'Rp.79500'),
(' TRKD-2', 'IDMN-6', 4, 9, 'Rp.79500');

-- --------------------------------------------------------

--
-- Struktur dari tabel `user`
--

CREATE TABLE IF NOT EXISTS `user` (
  `id` int(5) NOT NULL AUTO_INCREMENT,
  `username` varchar(15) NOT NULL,
  `password` varchar(15) NOT NULL,
  `level_id` int(5) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- Dumping data untuk tabel `user`
--

INSERT INTO `user` (`id`, `username`, `password`, `level_id`) VALUES
(1, 'adham', 'hayukalbu', 1);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
