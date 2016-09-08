using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Generales
{
    public static class Mensajes
    {
        static String strCancel = "Hay datos  nuevo ingresados.\n Los perderá. Esta seguro";
        static String strError = "Error en ";
        static String strHoraInvalida = "Hora inválida";
        static String strValorInvalido = "Valor inválido";
        static String strValorInvalidoNumero = "Valor inválido para este Campo. \n Ingrese un número";
        static String strValorInvalidoCantidadEnteros = "La cantidad de dígitos enteros no es valida para este Campo.";
        static String strValorInvalidoCantidadDecimal = "La cantidad de dígitos decimales no es valida para este Campo.";

        static String strValorInvalidoEntero = "Valor inválido para este Campo. \n Ingrese un entero";
        static String strValorInvalidoFecha = "Valor inválido para este Campo. \n Ingrese una fecha válida dd/mm/aaaa";
        static String strValorInvalidoHora = "Valor inválido para este Campo. \n Ingrese una hora válida hh:mm";
        static string strErrorPacientesConProc = "No se puede borrar el paciente\n Tiene procedimietos asociados";
        static String strNoTienePermisos = "No tiene permiso para realizar esta acción";
        static String strlistaCorregirPacientes ="¿En la Vista SOLO tiene el Paciente que queda  \n y el/los que va a corregir?";
        static String strConfirmarSalida = "Si sale ahora perderá los datos que haya cambiado. Está seguro?";
        static String strEsperarTerProceso = "Proceso en curso. Espere a que termine";
        public static void msgProcesoEnCurso()
        {
            MessageBox.Show(strEsperarTerProceso, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        static String strErrorPacienteNextLab = "No se localiza Paciente\n Verifique Apellido, Nombre y Fecha de Nacimiento\n Corrija y reintente";


        public static void msgErrorPacienteNextLab()
        {
            MessageBox.Show(strErrorPacienteNextLab, "Error de Paciente", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult msgConfirmarSalida(string rutina)
        {
            return MessageBox.Show(strConfirmarSalida, rutina, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
        }
        public static void msgNoSePuedeBorrarProc(string rutina)
        {
            MessageBox.Show("No se puede Borrar procedimento. Tiene registros derivados", rutina, MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }
        public static void msgProcConExito(string rutina)
        {
            MessageBox.Show("Procedieminto finalizado con éxito", rutina, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void msgProcesoCancelado(string rutina)
        {
            MessageBox.Show("Procedieminto cancelado", rutina, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static DialogResult msgListacorregitPacientes()
        {
            return MessageBox.Show(strlistaCorregirPacientes, "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button2);
        }

        public static void msgNoTienePermisos()
        {
            MessageBox.Show(strNoTienePermisos, "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }
        public static void msgNoTienePermisos(string rutina)
        {
            MessageBox.Show(strNoTienePermisos, rutina, MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }
        public static void msgErrorBorrarPac(string rutina)
        {
            MessageBox.Show(strErrorPacientesConProc, rutina, MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }
        public static DialogResult msgCancelar()
        {
            return MessageBox.Show(strCancel, "Cancelar", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
        }

        public static void msgError(String rutina, Exception ex)
        {
             MessageBox.Show(strError + rutina + ": " +ex.Message, rutina, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void msgError(Exception ex)
        {
             MessageBox.Show(strError + ": " +ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void msgHoraInvalida()
        {
            MessageBox.Show(strHoraInvalida , "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void msgValorInvalido()
        {
            MessageBox.Show(strValorInvalido, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void msgValorInvalidoNumero()
        {
            MessageBox.Show(strValorInvalidoNumero, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void msgValorInvalidoCantidadEnteros()
        {
            MessageBox.Show(strValorInvalidoCantidadEnteros, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void msgValorInvalidoCantidadDecimal()
        {
            MessageBox.Show(strValorInvalidoCantidadDecimal, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void msgValorInvalidoEntero()
        {
            MessageBox.Show(strValorInvalidoEntero, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void msgValorInvalidoFecha()
        {
            MessageBox.Show(strValorInvalidoFecha, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult msgEliminarResgistro()
        {
            return(MessageBox.Show("Se eliminara el registro actual de forma permanente \n Está Seguro ?", "Borrar registro", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation));

        }

        public static void msgRegistroBorrado()
        {
            MessageBox.Show("Registro borrado", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ErrorBorrar()
        {
            MessageBox.Show("Error al borrar registro", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult msgActualizarRegistro()
        {
            return (MessageBox.Show("Se modificará el registro actual de forma permanente \n Está Seguro ?", "Modificar procedimiento", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation));
        }

        public static void msgRegistroActualizado()
        {
            MessageBox.Show("Registro actualizado", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ErrorActualizar()
        {
            MessageBox.Show("Error al actualizar", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        public static void msgRegistroInsertado()
        {
            MessageBox.Show("Registro insertado", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ErrorInsertar()
        {
            MessageBox.Show("Error al insertar registro", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        public static void NoTieneFiltro()
        {
            MessageBox.Show("Debe filtrar primero el paciente que quiere reparar", "ArreglarPaciente", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        public static void msgFormatoNoValido()
        {
            MessageBox.Show("Formato de archivo no válido", "",MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void msgFormatoNoValido(string func)
        {
            MessageBox.Show("Formato de archivo no válido", func, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void msgErrorDebeSeleccionarCampo(string rutina)
        {
            MessageBox.Show("Debe Seleccionar al menos un campo", rutina, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        internal static void msgValorInvalidoHora()
        {
            MessageBox.Show(strValorInvalidoHora, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ErrorActualizarObito()
        {
            MessageBox.Show(strErrorActualizarObito, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static string strErrorActualizarObito = "Error al actulizar Obito";

        public static void msgPacienteObitado()
        {
            MessageBox.Show("Paciente Obitado", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
