﻿public enum TipoInformeEvol
{
    Diaria =1,
    Total =2
}

public enum TipoInforme
{
    Ingreso =1,
    EvolucionDia =2,
    EvolucionTotal=3,
    Alta = 4
}


public enum TipoAccion
{
    Alta = 1,
    Baja = 2,
    Modif = 3,
    Ver = 4,
    AltaConDatos=5
}

public enum TipoError
{
    SinError = 0,
    Terminal = 1,
    Informativo = 2
}

public enum Estados
{
    SinFiltrar = 0,
    Filtrado = 1
}

public enum Modulo
{
    Car = 1,
    Elf = 2
}

public enum OrdenSel
{
    Primera = 1,
    Ultima = 2
}

public enum TipoPermiso
{
    Todo = 1,
    Alta = 2,
    Baja = 3,
    Modificacion = 4,
    Ver = 5
}


public enum Formularios
{
    Paciente = 1,
    Procedimiento = 2,
    DatosClinicos = 3,
    EstudiosPrevios = 4,
    Tavi = 5,
    Atc = 6,
    Complicaciones = 7,
    _30Dias = 8
}

public enum TipoDeDatos
{
    ID = 1,
    Texto = 2,
    Tabla = 3,
    Numero = 4,
    Fecha = 5,
    NoSi = 6
}

public enum NoSi
{
    No = 2,
    Si = 3
}

public enum TipoCir
{
    MP=2,
    AA=4,
    FA=5,
    CDI=5,
    CDI_CRT=7,
    TV_EV=8
}

