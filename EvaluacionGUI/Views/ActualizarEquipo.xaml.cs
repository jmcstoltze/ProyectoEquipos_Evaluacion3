using EvaluacionGUI.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Lógica de interacción para ActualizarEquipo.xaml
    /// </summary>
    public partial class ActualizarEquipo : Window
    {
        EvaluacionNegocio.Equipo equipo;
        public ActualizarEquipo(int EquipoId)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterOwner; // Posición en base a la ventana principal
            equipo = new EvaluacionNegocio.Equipo();
            this.DataContext = equipo;
            CargarFormulario(EquipoId);            
        }

        private void btnActualizarEquipo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EvaluacionNegocio.Equipo equipo = new EvaluacionNegocio.Equipo();
                equipo.EquipoId = Convert.ToInt32(txtEquipoId.Text);
                equipo.NombreEquipo = txtNombreEquipo.Text;
                equipo.CantidadJugadores = Convert.ToInt32(txtCantidadJugadores.Text);
                equipo.NombreDT = txtNombreDT.Text;
                equipo.TipoEquipo = cmbTipoEquipo.Text;
                equipo.CapitanEquipo = txtCapitanEquipo.Text;
                equipo.TieneSub21 = (chkTieneSub21.IsChecked.Value) ? true : false;

                int equipoId = equipo.EquipoId; 
                bool response = equipo.Update(equipoId);
                
                if (response)
                {
                    MessageBox.Show("Registro actualizado exitosamente");
                    Close();
                }
                else
                {
                    MessageBox.Show("El registro no pudo ser actualizado");
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CheckTextInput(object sender, TextCompositionEventArgs e)
        {
            CommonValidations.CheckTextInput(sender, e);
        }      

        private void CargarFormulario(int EquipoId)
        {
            EvaluacionNegocio.Equipo equipo = new EvaluacionNegocio.Equipo();
            bool response = equipo.Read(EquipoId);

            if (response)
            {
                txtEquipoId.Text = equipo.EquipoId.ToString();
                txtNombreEquipo.Text = equipo.NombreEquipo;
                txtCantidadJugadores.Text = equipo.CantidadJugadores.ToString();
                txtNombreDT.Text = equipo.NombreDT;
                cmbTipoEquipo.SelectedItem = equipo.TipoEquipo;
                txtCapitanEquipo.Text = equipo.CapitanEquipo;
                chkTieneSub21.IsChecked = (equipo.TieneSub21) ? true : false;

                cmbTipoEquipo.Items[0] = equipo.TipoEquipo;
            }
            else
            {
                MessageBox.Show("No se pudo encontrar el equipo en el listado");
            }
        }
    }
}
