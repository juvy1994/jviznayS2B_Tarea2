using System.Globalization;

namespace jviznayS2B_Tarea2.Views;

public partial class FrmPrincipal : ContentPage
{
	public FrmPrincipal()
	{
		InitializeComponent();
	}

    private async void btnCalcular_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (pkEstudiantes.SelectedIndex == -1 || pkEstudiantes.SelectedItem == null)
            {
                await DisplayAlert("Error", "Seleccione un estudiante.", "OK");
                return;
            }

            if (!validarVacios(txtNotaUno.Text, txtExamenDos.Text, txtNotaDos.Text, txtExamenDos.Text))
            {
                return;
            }

            string? nombre = pkEstudiantes.SelectedItem.ToString();
            double notaUno = Convert.ToDouble(txtNotaUno.Text, CultureInfo.InvariantCulture);
            double examenUno = Convert.ToDouble(txtExamenUno.Text, CultureInfo.InvariantCulture);
            double notaDos = Convert.ToDouble(txtNotaDos.Text, CultureInfo.InvariantCulture);
            double examenDos = Convert.ToDouble(txtExamenDos.Text, CultureInfo.InvariantCulture);
            string estado;

            if (!validarRangoNumeros(notaUno, examenUno, notaDos, examenDos))
            {
                return;
            }


            // Cálculo de parciales y final
            double parcialUno = (notaUno * 0.3) + (examenUno * 0.2);
            double parcialDos = (notaDos * 0.3) + (examenDos * 0.2);
            double notaFinal = parcialDos + parcialDos;


            if (notaFinal >= 7 && notaFinal <=10)
            {
                estado = "Aprobado";
            }
            else if (notaFinal >= 5 && notaFinal <= 6.9)
            {
                estado = "Complementario";
            }
            else
            {
                estado = "Reprobado";
            }


            string mensaje = $"Nombre: {nombre}\n" +
                             $"Fecha: {txtFecha.Date:dd/MM/yyyy}\n\n" +
                             $"Nota Parcial 1: {notaUno:F1}\n" +
                             $"Nota Parcial 2: {parcialDos:F1}\n" +
                             $"Nota Final: {notaFinal:F2}\n" +
                             $"Estado: {estado}";

            await DisplayAlert("Resultado", mensaje, "OK");
            limpiarCasilla();
        }
        catch (FormatException ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    //VALIDAR ENTRY VACIOS

    private bool validarVacios(string notaUno, string examenUno, string notaDos, string examenDos)
    {
        if (string.IsNullOrEmpty(notaUno) || string.IsNullOrEmpty(examenUno) ||
              string.IsNullOrEmpty(notaDos) || string.IsNullOrEmpty(examenDos))
        {
            DisplayAlert("Error", "Por favor, complete todos los campos.", "OK");
            return false;
        }
        return true;
    }

    //VALIDAR EL RANDO DE LOS NÚMEROS
    private bool validarRangoNumeros(double notaUno, double examenUno, double notaDos, double examenDos) {
        if (notaUno < 0 || notaUno > 10) {
            DisplayAlert("Error", "Por favor, cambie el rango de la nota 1: De 1 a 10.", "OK");
            return false;
        }

        if (examenUno < 0 || examenUno > 10)
        {
            DisplayAlert("Error", "Por favor, cambie el rango de la examen 1: De 1 a 10.", "OK");
            return false;
        }

        if (notaDos < 0 || notaDos > 10)
        {
            DisplayAlert("Error", "Por favor, cambie el rango de la nota 2: De 1 a 10.", "OK");
            return false;
        }

        if (examenDos < 0 || examenDos > 10)
        {
            DisplayAlert("Error", "Por favor, cambie el rango de la examen 2: De 1 a 10.", "OK");
            return false;
        }

        return true;
    }

    //LIMPIA LOS ENTRY Y EL PICKER
    private void limpiarCasilla()
    {

        txtNotaUno.Text = string.Empty;
        txtExamenUno.Text = string.Empty;
        txtNotaDos.Text = string.Empty;
        txtExamenDos.Text = string.Empty;
        pkEstudiantes.SelectedIndex = -1;
    }

    // FUNCION PARA PERMITIR SOLO NUMERO Y UN PUNTO
    public string soloNumerosYpunto (string textoActual) {

        bool puntoYaExiste = false;
        string nuevoTexto = "";

        foreach (char c in textoActual)
        {
            if (char.IsDigit(c))
            {
                nuevoTexto += c;
            }
            else if (c == '.' && !puntoYaExiste)
            {
                nuevoTexto += c;
                puntoYaExiste = true;
            }
        }
        return nuevoTexto;
    }

    //FUNCION QUE PERMITE ACTUALIZAR EL ENTRY
    public void actualizarTexto(string textoActual, string nuevoTexto, object sender)
    {
        var entry = (Entry)sender;
        if (textoActual != nuevoTexto)
        {
            entry.Text = nuevoTexto;
            entry.CursorPosition = nuevoTexto.Length;
        }
    }


    // ENTRY PERMITE SOLO NUMERO Y PUNTO CON LA PROPIEDAD CHANGED
    private void txtExamenUno_TextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = (Entry)sender;
        string textoActual = entry.Text;
        string nuevoTexto= soloNumerosYpunto (textoActual);
        actualizarTexto(textoActual, nuevoTexto,sender);
    }


    private void txtNotaUno_TextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = (Entry)sender;
        string textoActual = entry.Text;
        string nuevoTexto = soloNumerosYpunto(textoActual);
        actualizarTexto(textoActual, nuevoTexto, sender);
    }

    private void txtNotaDos_TextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = (Entry)sender;
        string textoActual = entry.Text;
        string nuevoTexto = soloNumerosYpunto(textoActual);
        actualizarTexto(textoActual, nuevoTexto, sender);
    }

    private void txtExamenDos_TextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = (Entry)sender;
        string textoActual = entry.Text;
        string nuevoTexto = soloNumerosYpunto(textoActual);
        actualizarTexto(textoActual, nuevoTexto, sender);
    }
}