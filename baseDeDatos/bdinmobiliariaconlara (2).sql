-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 02-10-2025 a las 00:48:54
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
  `cuotasPagas` int(100) NOT NULL,
  `mesInicio` int(10) NOT NULL,
  `existe` tinyint(1) NOT NULL,
  `usuarioAlta` int(100) NOT NULL,
  `usuarioBaja` int(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `contratos`
--

INSERT INTO `contratos` (`idContrato`, `idInquilino`, `idInmuebles`, `monto`, `fechaDesde`, `fechaHasta`, `vigente`, `cantidadCuotas`, `cuotasPagas`, `mesInicio`, `existe`, `usuarioAlta`, `usuarioBaja`) VALUES
(26, 4, 9, 190000, '2026-01-01', '2026-05-01', 1, 5, 1, 1, 1, 1, NULL),
(27, 4, 3, 130000, '2025-10-01', '2026-02-01', 1, 5, 5, 10, 1, 1, NULL),
(28, 15, 5, 360000, '2025-11-01', '2026-01-01', 1, 3, 0, 11, 1, 1, NULL);

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
  `Habilitado` tinyint(1) NOT NULL,
  `existe` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inmuebles`
--

INSERT INTO `inmuebles` (`idInmuebles`, `direccion`, `ambientes`, `superficie`, `latitud`, `longitud`, `idPropietario`, `idTipoInmueble`, `precio`, `Habilitado`, `existe`) VALUES
(2, 'Las heras 344', 5, 460, 254, 226, 1, 5, 160000, 1, 1),
(3, 'Belgrano 123', 4, 298, 35, 56, 4, 4, 130000, 1, 1),
(4, 'Belgrano1287', 7, 360, 35, 56, 1, 5, 200000, 1, 1),
(5, 'Las heras 2377', 5, 350, 35, 34, 2, 5, 360000, 1, 1),
(7, 'Las Heras47586', 5, 3456, 25, 46, 1, 2, 120000, 1, 1),
(8, 'San Martin 345', 5, 2387, 33, 37, 6, 4, 125000, 0, 1),
(9, 'San Martin 123', 7, 347, 22, 55, 2, 5, 190000, 1, 1),
(10, 'Sarmiento 332', 4, 3645, 34, 56, 1, 4, 145000, 0, 1),
(11, 'España305', 3, 350, 332, 332, 2, 5, 100000, 0, 0);

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
  `eMail` varchar(100) NOT NULL,
  `existe` int(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inquilino`
--

INSERT INTO `inquilino` (`idInquilino`, `apellido`, `nombre`, `dni`, `telefono`, `eMail`, `existe`) VALUES
(1, 'Perez', 'Juan', '16234876', '265465783', 'arubira60@gmail.com', 1),
(2, 'Veronelli', 'Antonio', '32456374', '2654345637', 'arubira60@gmail.com', 1),
(4, 'Lopez', 'Adriana', '25364785', '2635874658', 'arubira60@gmail.com', 1),
(6, 'Lusero', 'Joaquin', '27398456', '26354988', 'arubira60@gmail.com', 1),
(7, 'Perez', 'Alberto', '347823487', '236478233', 'arubira60@gmail.com', 1),
(8, 'Perez', 'Carmela', '546373748', '546373747', 'arubira60@gmail.com', 1),
(9, 'PEREZ', 'Alicia Carmen', '546373746', '435464657', 'arubira60@gmail.com', 1),
(10, 'Gonzalez', 'Miriam del Carmen', '4334649823', '87543857349', 'arubira60@gmail.com', 1),
(11, 'Gonzales', 'Jose', '45734539', '4358340958', 'arubira60@gmail.com', 1),
(12, 'Garro', 'Luis', '346236478', '32493374', 'arubira60@gmail.com', 1),
(13, 'Garro', 'Alicia', '437646376', '84768346', 'arubira60@gmail.com', 1),
(14, 'ALTAMIRANO', 'Luisa', '133636728', '34723478239', 'arubira60@gmail.com', 1),
(15, 'lopes', 'Oscar', '34632462', '8438937', 'arubira60@gmail.com', 1),
(16, 'Lopez', 'Juan', '25364545', '3454646', 'arubira60@gmail.com', 1),
(17, 'Lopez', 'Juan', '25364545', '3454646', 'arubira60@gmail.com', 1),
(18, 'Rosetti', 'Juan Alberto', '3546465', '53646474', 'arubira60@gmail.com', 1),
(19, 'Castro', 'Bruno', '7647734377', '87468764', 'arubira60@gmail.com', 1),
(20, 'PEREYRA', 'Luisa', '765875687', '54654654', 'arubira60@gmail.com', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `multa`
--

CREATE TABLE `multa` (
  `idMulta` int(100) NOT NULL,
  `idContrato` int(100) NOT NULL,
  `fechaMulta` date NOT NULL,
  `fechaHastaContrato` date NOT NULL,
  `nuevaFechaHastaContrato` date NOT NULL,
  `importeCuota` decimal(60,0) NOT NULL,
  `importeMulta` decimal(60,0) NOT NULL,
  `cuotasAdeudadas` int(100) NOT NULL,
  `pagada` tinyint(1) NOT NULL,
  `existe` int(100) NOT NULL,
  `usuarioAlta` int(100) NOT NULL,
  `usuarioBaja` int(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pagos`
--

CREATE TABLE `pagos` (
  `idPagos` int(100) NOT NULL,
  `idContratos` int(100) NOT NULL,
  `fechaPago` date NOT NULL,
  `importe` decimal(60,0) NOT NULL,
  `concepto` varchar(100) NOT NULL,
  `numeroCuota` int(100) NOT NULL,
  `mesPago` int(100) NOT NULL,
  `existe` tinyint(1) NOT NULL,
  `usuarioAlta` int(100) NOT NULL,
  `usuarioBaja` int(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `pagos`
--

INSERT INTO `pagos` (`idPagos`, `idContratos`, `fechaPago`, `importe`, `concepto`, `numeroCuota`, `mesPago`, `existe`, `usuarioAlta`, `usuarioBaja`) VALUES
(33, 27, '2025-10-01', 130000, 'Alquiler mes :Octubre', 1, 10, 1, 1, NULL),
(34, 26, '2025-10-01', 190000, 'Alquiler mes :Enero', 1, 1, 1, 1, NULL),
(35, 27, '2025-10-01', 130000, 'Alquiler mes :Noviembre', 2, 11, 1, 1, NULL),
(36, 27, '2025-10-01', 130000, 'Alquiler mes :Diciembre', 3, 12, 1, 1, NULL),
(37, 27, '2025-10-01', 130000, 'Alquiler mes :Enero', 4, 1, 1, 1, NULL),
(38, 27, '2025-10-01', 130000, 'Alquiler mes :Febrero', 5, 2, 1, 1, NULL);

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
  `clave` varchar(100) NOT NULL,
  `existe` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `propietario`
--

INSERT INTO `propietario` (`idPropietario`, `apellido`, `nombre`, `dni`, `telefono`, `eMail`, `clave`, `existe`) VALUES
(1, 'Rubira', 'Alejandro', '26833093', '2664313126', 'arubira60@gmail.com', '123', 1),
(2, 'Barzola', 'Deolinda', '26347384', '1111111111', 'arubira60@gmail.com', '123', 1),
(4, 'rodreiguez', 'Albert', '26374874', '2536677', 'arubira60@gmail.com', '123', 1),
(5, 'Rodregues', 'Analia', '29765345', '24354675', 'arubira60@gmail.com', '123', 1),
(6, 'Lusero', 'Daniel', '284675909', '35467284', 'arubira60@gmail.com', '123', 1),
(7, 'Andrada', 'juan', '536464757', '746743674374', 'arubira60@gmail.com', '1234', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipoinmueble`
--

CREATE TABLE `tipoinmueble` (
  `idTipoInmueble` int(100) NOT NULL,
  `nombre` varchar(100) NOT NULL,
  `existe` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `tipoinmueble`
--

INSERT INTO `tipoinmueble` (`idTipoInmueble`, `nombre`, `existe`) VALUES
(1, 'Local', 1),
(2, 'Deposito', 1),
(4, 'Departamento', 1),
(5, 'Casa', 1);

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
  `rol` int(100) NOT NULL,
  `existe` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `usuario`
--

INSERT INTO `usuario` (`idUsuario`, `nombre`, `apellido`, `eMail`, `clave`, `avatar`, `rol`, `existe`) VALUES
(1, 'Angel Ismael Ezequia', 'Orozco Pauli', 'ismaelaorozco0@gmail.com', 'E7NP32UITW3oT3ltbw/t+1LoAL0RABdnyCWz59qI7kQ=', '/Uploads\\avatar_1.jpg', 1, 1),
(2, 'empleado', 'Prueba', 'ep@gmail.com', 'E7NP32UITW3oT3ltbw/t+1LoAL0RABdnyCWz59qI7kQ=', '/Uploads/avatar_0.png', 2, 1),
(4, 'Alejando Gabriel', 'Rubira', 'arubira60@gmail.com', 'E7NP32UITW3oT3ltbw/t+1LoAL0RABdnyCWz59qI7kQ=', '/Uploads/avatar_0.png', 1, 1);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `contratos`
--
ALTER TABLE `contratos`
  ADD PRIMARY KEY (`idContrato`),
  ADD KEY `contratos_ibfk_1` (`idInmuebles`),
  ADD KEY `contratos_ibfk_2` (`idInquilino`),
  ADD KEY `contratos_ibfk_3` (`usuarioAlta`);

--
-- Indices de la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  ADD PRIMARY KEY (`idInmuebles`),
  ADD KEY `inmuebles_ibfk_1` (`idTipoInmueble`),
  ADD KEY `inmuebles_ibfk_2` (`idPropietario`);

--
-- Indices de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  ADD PRIMARY KEY (`idInquilino`);

--
-- Indices de la tabla `multa`
--
ALTER TABLE `multa`
  ADD PRIMARY KEY (`idMulta`),
  ADD KEY `multa_ibfk_1` (`idContrato`),
  ADD KEY `multa_ibfk_2` (`usuarioAlta`);

--
-- Indices de la tabla `pagos`
--
ALTER TABLE `pagos`
  ADD PRIMARY KEY (`idPagos`),
  ADD KEY `pagos_ibfk_1` (`idContratos`),
  ADD KEY `pagos_ibfk_2` (`usuarioAlta`);

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
  MODIFY `idContrato` int(100) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=29;

--
-- AUTO_INCREMENT de la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  MODIFY `idInmuebles` int(100) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  MODIFY `idInquilino` int(100) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT de la tabla `multa`
--
ALTER TABLE `multa`
  MODIFY `idMulta` int(100) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `pagos`
--
ALTER TABLE `pagos`
  MODIFY `idPagos` int(100) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=39;

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
  MODIFY `idUsuario` int(100) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `contratos`
--
ALTER TABLE `contratos`
  ADD CONSTRAINT `contratos_ibfk_1` FOREIGN KEY (`idInmuebles`) REFERENCES `inmuebles` (`idInmuebles`),
  ADD CONSTRAINT `contratos_ibfk_2` FOREIGN KEY (`idInquilino`) REFERENCES `inquilino` (`idInquilino`),
  ADD CONSTRAINT `contratos_ibfk_3` FOREIGN KEY (`usuarioAlta`) REFERENCES `usuario` (`idUsuario`);

--
-- Filtros para la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  ADD CONSTRAINT `inmuebles_ibfk_1` FOREIGN KEY (`idTipoInmueble`) REFERENCES `tipoinmueble` (`idTipoInmueble`),
  ADD CONSTRAINT `inmuebles_ibfk_2` FOREIGN KEY (`idPropietario`) REFERENCES `propietario` (`idPropietario`);

--
-- Filtros para la tabla `multa`
--
ALTER TABLE `multa`
  ADD CONSTRAINT `multa_ibfk_1` FOREIGN KEY (`idContrato`) REFERENCES `contratos` (`idContrato`),
  ADD CONSTRAINT `multa_ibfk_2` FOREIGN KEY (`usuarioAlta`) REFERENCES `usuario` (`idUsuario`);

--
-- Filtros para la tabla `pagos`
--
ALTER TABLE `pagos`
  ADD CONSTRAINT `pagos_ibfk_1` FOREIGN KEY (`idContratos`) REFERENCES `contratos` (`idContrato`),
  ADD CONSTRAINT `pagos_ibfk_2` FOREIGN KEY (`usuarioAlta`) REFERENCES `usuario` (`idUsuario`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
