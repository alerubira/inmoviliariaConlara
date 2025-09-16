-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 15-09-2025 a las 17:14:38
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `bdinmobiliariaconlara`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `contratos`
--

CREATE TABLE `contratos` (
  `idContrato` int(100) NOT NULL,
  `idInquilino` int(100) NOT NULL,
  `idInmuebles` int(100) NOT NULL,
  `monto` decimal(60,0) NOT NULL,
  `fechaDesde` date NOT NULL,
  `fechaHasta` date NOT NULL,
  `vigente` tinyint(1) NOT NULL,
  `cantidadCuotas` int(100) NOT NULL,
  `cuotasPagas` int(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `contratos`
--

INSERT INTO `contratos` (`idContrato`, `idInquilino`, `idInmuebles`, `monto`, `fechaDesde`, `fechaHasta`, `vigente`, `cantidadCuotas`, `cuotasPagas`) VALUES
(3, 6, 2, 960000, '2025-08-31', '2026-01-30', 1, 0, 0),
(4, 2, 4, 130000, '2025-11-13', '2025-11-12', 1, 0, 0),
(5, 15, 4, 200000, '2025-09-15', '2025-11-15', 1, 3, 0),
(6, 15, 7, 120000, '2025-09-15', '2026-01-15', 1, 5, 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmuebles`
--

CREATE TABLE `inmuebles` (
  `idInmuebles` int(100) NOT NULL,
  `direccion` varchar(100) NOT NULL,
  `ambientes` int(100) NOT NULL,
  `superficie` int(100) NOT NULL,
  `latitud` decimal(60,0) NOT NULL,
  `longitud` decimal(60,0) NOT NULL,
  `idPropietario` int(100) NOT NULL,
  `idTipoInmueble` int(100) NOT NULL,
  `precio` decimal(60,0) NOT NULL,
  `Habilitado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inmuebles`
--

INSERT INTO `inmuebles` (`idInmuebles`, `direccion`, `ambientes`, `superficie`, `latitud`, `longitud`, `idPropietario`, `idTipoInmueble`, `precio`, `Habilitado`) VALUES
(2, 'Las heras 344', 5, 460, 254, 226, 1, 5, 160000, 1),
(3, 'Belgrano 123', 4, 298, 35, 56, 4, 4, 130000, 1),
(4, 'Belgrano1287', 7, 360, 35, 56, 1, 5, 200000, 1),
(5, 'Las heras 2377', 5, 350, 35, 34, 2, 5, 360000, 1),
(7, 'Las Heras47586', 5, 3456, 25, 46, 1, 2, 120000, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inquilino`
--

CREATE TABLE `inquilino` (
  `idInquilino` int(100) NOT NULL,
  `apellido` varchar(100) NOT NULL,
  `nombre` varchar(100) NOT NULL,
  `dni` varchar(100) NOT NULL,
  `telefono` varchar(100) NOT NULL,
  `eMail` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inquilino`
--

INSERT INTO `inquilino` (`idInquilino`, `apellido`, `nombre`, `dni`, `telefono`, `eMail`) VALUES
(1, 'Perez', 'Juan', '16234876', '265465783', 'arubira60@gmail.com'),
(2, 'Veronelli', 'Antonio', '32456374', '2654345637', 'arubira60@gmail.com'),
(4, 'Lopez', 'Adriana', '25364785', '2635874658', 'arubira60@gmail.com'),
(6, 'Lusero', 'Joaquin', '27398456', '26354988', 'arubira60@gmail.com'),
(7, 'Perez', 'Alberto', '347823487', '236478233', 'arubira60@gmail.com'),
(8, 'Perez', 'Carmela', '546373748', '546373747', 'arubira60@gmail.com'),
(9, 'PEREZ', 'Alicia Carmen', '546373746', '435464657', 'arubira60@gmail.com'),
(10, 'Gonzalez', 'Miriam del Carmen', '4334649823', '87543857349', 'arubira60@gmail.com'),
(11, 'Gonzales', 'Jose', '45734539', '4358340958', 'arubira60@gmail.com'),
(12, 'Garro', 'Luis', '346236478', '32493374', 'arubira60@gmail.com'),
(13, 'Garro', 'Alicia', '437646376', '84768346', 'arubira60@gmail.com'),
(14, 'ALTAMIRANO', 'Luisa', '133636728', '34723478239', 'arubira60@gmail.com'),
(15, 'lopes', 'Oscar', '34632462', '8438937', 'arubira60@gmail.com'),
(16, 'Lopez', 'Juan', '25364545', '3454646', 'arubira60@gmail.com'),
(17, 'Lopez', 'Juan', '25364545', '3454646', 'arubira60@gmail.com'),
(18, 'Rosetti', 'Juan Alberto', '3546465', '53646474', 'arubira60@gmail.com'),
(19, 'Castro', 'Bruno', '7647734377', '87468764', 'arubira60@gmail.com'),
(20, 'PEREYRA', 'Luisa', '765875687', '54654654', 'arubira60@gmail.com');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pagos`
--

CREATE TABLE `pagos` (
  `idPagos` int(100) NOT NULL,
  `idContratos` int(100) NOT NULL,
  `fechaPago` date NOT NULL,
  `importe` decimal(60,0) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `pagos`
--

INSERT INTO `pagos` (`idPagos`, `idContratos`, `fechaPago`, `importe`) VALUES
(1, 4, '2025-09-05', 200000),
(2, 4, '2025-09-05', 200000);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `propietario`
--

CREATE TABLE `propietario` (
  `idPropietario` int(100) NOT NULL,
  `apellido` varchar(100) NOT NULL,
  `nombre` varchar(100) NOT NULL,
  `dni` varchar(100) NOT NULL,
  `telefono` varchar(100) NOT NULL,
  `eMail` varchar(100) NOT NULL,
  `clave` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `propietario`
--

INSERT INTO `propietario` (`idPropietario`, `apellido`, `nombre`, `dni`, `telefono`, `eMail`, `clave`) VALUES
(1, 'Rubira', 'Alejandro', '26833093', '2664313126', 'arubira60@gmail.com', '123'),
(2, 'Barzola', 'Deolinda', '26347384', '1111111111', 'arubira60@gmail.com', '123'),
(4, 'rodreiguez', 'Albert', '26374874', '2536677', 'arubira60@gmail.com', '123'),
(5, 'Rodregues', 'Analia', '29765345', '24354675', 'arubira60@gmail.com', '123'),
(6, 'Lucero', 'Daniel', '284675909', '35467284', 'arubira60@gmail.com', '123'),
(7, 'Andrada', 'juan', '536464757', '746743674374', 'arubira60@gmail.com', '1234');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipoinmueble`
--

CREATE TABLE `tipoinmueble` (
  `idTipoInmueble` int(100) NOT NULL,
  `nombre` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `tipoinmueble`
--

INSERT INTO `tipoinmueble` (`idTipoInmueble`, `nombre`) VALUES
(1, 'Local'),
(2, 'Deposito'),
(4, 'Departamento'),
(5, 'Casa');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuario`
--

CREATE TABLE `usuario` (
  `idUsuario` int(100) NOT NULL,
  `nombre` varchar(100) NOT NULL,
  `apellido` varchar(100) NOT NULL,
  `eMail` varchar(100) NOT NULL,
  `clave` varchar(100) NOT NULL,
  `avatar` varchar(100) NOT NULL,
  `rol` int(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `contratos`
--
ALTER TABLE `contratos`
  ADD PRIMARY KEY (`idContrato`);

--
-- Indices de la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  ADD PRIMARY KEY (`idInmuebles`);

--
-- Indices de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  ADD PRIMARY KEY (`idInquilino`);

--
-- Indices de la tabla `pagos`
--
ALTER TABLE `pagos`
  ADD PRIMARY KEY (`idPagos`);

--
-- Indices de la tabla `propietario`
--
ALTER TABLE `propietario`
  ADD PRIMARY KEY (`idPropietario`);

--
-- Indices de la tabla `tipoinmueble`
--
ALTER TABLE `tipoinmueble`
  ADD PRIMARY KEY (`idTipoInmueble`);

--
-- Indices de la tabla `usuario`
--
ALTER TABLE `usuario`
  ADD PRIMARY KEY (`idUsuario`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `contratos`
--
ALTER TABLE `contratos`
  MODIFY `idContrato` int(100) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT de la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  MODIFY `idInmuebles` int(100) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  MODIFY `idInquilino` int(100) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT de la tabla `pagos`
--
ALTER TABLE `pagos`
  MODIFY `idPagos` int(100) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `propietario`
--
ALTER TABLE `propietario`
  MODIFY `idPropietario` int(100) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT de la tabla `tipoinmueble`
--
ALTER TABLE `tipoinmueble`
  MODIFY `idTipoInmueble` int(100) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de la tabla `usuario`
--
ALTER TABLE `usuario`
  MODIFY `idUsuario` int(100) NOT NULL AUTO_INCREMENT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
