using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;

namespace EvaluacionGUI.Views
{
    /// <summary>
    /// Lógica de interacción para AgregarEquipo.xaml
    /// </summary>
    public partial class AgregarEquipo : Window
    {

        EvaluacionNegocio.Equipo equipo;

        public AgregarEquipo()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterOwner; // Posición en base a la ventana principal
            equipo = new EvaluacionNegocio.Equipo();
            this.DataContext = equipo; // Vincula la interfaz con las propiedades de la instancia
        }

        private void btnAgregarEquipo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // EvaluacionNegocio.Equipo equipo = new EvaluacionNegocio.Equipo();

                equipo.NombreEquipo = txtNombreEquipo.Text;
                equipo.CantidadJugadores = Convert.ToInt32(txtCantidadJugadores.Text);
                equipo.NombreDT = txtNombreDT.Text;
                equipo.TipoEquipo = cmbTipoEquipo.Text;
                equipo.CapitanEquipo = txtCapitanEquipo.Text;
                equipo.TieneSub21 = (chkTieneSub21.IsChecked.Value) ? true : false;

                bool response = equipo.Create();
                              
                if (response)
                {
                    MessageBox.Show("Equipo agregado al listado");
                    AgregarRegistro();
                }
                else
                {
                    MessageBox.Show("No se pudo agregar equipo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }        

        private void AgregarRegistro()
        {
            string title = "Agregar Nuevo Equipo";
            string message = "¿Desea agregar otro equipo?";

            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show(title, message, buttons);

            if (result == MessageBoxResult.Yes)
            {
                LimpiarCampos();
            }
            else
            {
                this.Close();
            }
        }

        private void LimpiarCampos()
        {
            // Se limpian los campos del formulario
            txtNombreEquipo.Text = string.Empty;
            txtCantidadJugadores.Text = string.Empty;
            txtNombreDT.Text = string.Empty;
            cmbTipoEquipo.SelectedIndex = 0;
            txtCapitanEquipo.Text= string.Empty;
            chkTieneSub21.IsChecked = false;            
        }

        private void CheckTextInput(object sender, TextCompositionEventArgs e)
        {
            // Valida que se han ingresado solo número en el formulario, campo cantidad jugadores
            Classes.CommonValidations.CheckTextInput(sender, e);
        }
    }
}
