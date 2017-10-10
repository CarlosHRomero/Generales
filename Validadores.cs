using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Generales
{
    public class Validadores
    {
        public static Boolean ValidaSingle(object sender, EventArgs e, int intLength, int fracLength)
        {
            float x = 0;

            if (!String.IsNullOrEmpty(((TextBox)sender).Text) && !Single.TryParse(((TextBox)sender).Text, out x))
            {
                Mensajes.msgValorInvalidoNumero();
                ((TextBox)sender).Text = null;
                ((Control)sender).Focus();
                return false;
            }

            decimal mWhole = Math.Truncate(Convert.ToDecimal(x));
            decimal mFraction = Convert.ToDecimal(x) - mWhole;

            if (((TextBox)sender).Text.Split(',').Length == 2)
            {
                if (((TextBox)sender).Text.Split(',')[1] == string.Empty)
                {
                    Mensajes.msgValorInvalidoNumero();
                    ((TextBox)sender).Text = null;
                    ((Control)sender).Focus();
                    return false;
                }
            }

            if (mFraction != 0)
                if (mFraction.ToString().Split(',')[1].Length > fracLength)
                {
                    Mensajes.msgValorInvalidoCantidadDecimal();
                    ((TextBox)sender).Text = null;
                    ((Control)sender).Focus();
                    return false;
                }

            if (mWhole.ToString(CultureInfo.InvariantCulture).Length > intLength)
            {
                Mensajes.msgValorInvalidoCantidadEnteros();
                ((TextBox)sender).Text = null;
                ((Control)sender).Focus();
                return false;
            }

            return true;
        }

        static public Boolean ValidaSingle(object sender, EventArgs e)
        {
            float x;
            if (!String.IsNullOrEmpty(((TextBox)sender).Text) && !Single.TryParse(((TextBox)sender).Text, out x))
            {
                Mensajes.msgValorInvalidoNumero();
                ((TextBox)sender).Text = null;
                ((Control)sender).Focus();
                return false;
            }
            return true;
        }

        static public Boolean ValidaEntero16(object sender, EventArgs e)
        {
            Int16 x;
            if (sender is TextBox)
            {
                if (!String.IsNullOrEmpty(((TextBox)sender).Text) && !Int16.TryParse(((TextBox)sender).Text, out x))
                {
                    Mensajes.msgValorInvalidoEntero();
                    ((TextBox)sender).Text = null;
                    ((Control)sender).Focus();
                    return false;
                }
            }
            if (sender is MaskedTextBox)
            {
                if (!String.IsNullOrEmpty(((MaskedTextBox)sender).Text) && !Int16.TryParse(((MaskedTextBox)sender).Text, out x))
                {
                    Mensajes.msgValorInvalidoEntero();
                    ((MaskedTextBox)sender).Text = null;
                    ((Control)sender).Focus();
                    return false;
                }
            }

            return true;
        }
        static public bool ValidaFecha(object sender, EventArgs e)
        {
            DateTime x= DateTime.MinValue;
            if (sender is TextBox)
            {
                TextBox obj = (TextBox)sender;
                if (!String.IsNullOrEmpty(((TextBox)sender).Text) && !DateTime.TryParse(((TextBox)sender).Text, out x))
                {
                    Mensajes.msgValorInvalidoFecha();
                    ((TextBox)sender).Text = null;
                    ((Control)sender).Focus();
                    return false;
                }
                DateTime fechaMinima = Convert.ToDateTime("01/01/1900");
                DateTime.TryParse(((TextBox)sender).Text, out x);
                if(x < fechaMinima)
                {
                    Mensajes.msgValorInvalidoFecha();
                    return false;
                }
                return true;
            }

            if (sender is MaskedTextBox)
            {
                MaskedTextBox obj = (MaskedTextBox)sender;
                obj.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                if (!String.IsNullOrEmpty(((MaskedTextBox)sender).Text) )
                {
                    obj.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                    if (!DateTime.TryParse(((MaskedTextBox)sender).Text, out x))
                    {
                        Mensajes.msgValorInvalidoFecha();
                        ((MaskedTextBox)sender).Text = null;
                        ((Control)sender).Focus();                        
                        return false;
                    }
                    DateTime fechaMinima = Convert.ToDateTime("01/01/1900");
                    DateTime.TryParse(((MaskedTextBox)sender).Text, out x);
                    if (x < fechaMinima)
                    {
                        Mensajes.msgValorInvalidoFecha();
                        ((MaskedTextBox)sender).Text = null;
                        ((Control)sender).Focus();             
                        return false;
                    }

                }

                obj.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                return true;
            }
            throw new Exception("Contorl Invalido");
        }


        static public bool ValidaHora(object sender, EventArgs e)
        {
            DateTime x;
            if (sender is TextBox)
            {
                TextBox obj = (TextBox)sender;
                if (!String.IsNullOrEmpty(((TextBox)sender).Text) && !DateTime.TryParse(((TextBox)sender).Text, out x))
                {
                    Mensajes.msgValorInvalidoHora();
                    ((TextBox)sender).Text = null;
                    ((Control)sender).Focus();
                    return false;
                }
                return true;
            }

            if (sender is MaskedTextBox)
            {
                MaskedTextBox obj = (MaskedTextBox)sender;
                obj.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                if (!String.IsNullOrEmpty(((MaskedTextBox)sender).Text))
                {
                    obj.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                    if (!DateTime.TryParse(((MaskedTextBox)sender).Text, out x))
                    {
                        Mensajes.msgValorInvalidoHora();
                        ((MaskedTextBox)sender).Text = null;
                        ((Control)sender).Focus();
                        return false;
                    }
                }
                obj.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                return true;
            }
            throw new Exception("Contorl Invalido");
        }

    }
}
