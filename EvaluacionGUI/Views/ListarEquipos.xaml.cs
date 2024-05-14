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
using System.Windows.Shapes;

namespace EvaluacionGUI.Views
{
    /// <summary>
    /// Lógica de interacción para ListarEquipos.xaml
    /// </summary>
    public partial class ListarEquipos : Window
    {
        public ListarEquipos()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterOwner; // Posición en base a la ventana principal
            CargarGrilla();
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            var filaSeleccionada = (EvaluacionNegocio.Equipo)dgListadoEquipos.SelectedItem;
            int EquipoId = filaSeleccionada.EquipoId;
            ActualizarEquipo actualizarEquipo = new ActualizarEquipo(EquipoId);
            actualizarEquipo.Owner = this; // La instancia hereda las propiedades
            actualizarEquipo.ShowDialog();
        }
        
        private void btnEliminar_Click(Object sender, RoutedEventArgs e)
        {
            EliminarRegistro();
        }

        private void CargarGrilla()
        {
            EvaluacionNegocio.EquipoCollection personaCollection = new EvaluacionNegocio.EquipoCollection();
            dgListadoEquipos.ItemsSource = personaCollection.ReadAll();
        }

        private void EliminarRegistro()
        {
            var filaSeleccionada = (EvaluacionNegocio.Equipo)dgListadoEquipos.SelectedItem;
            int equipoId = filaSeleccionada.EquipoId;

            string header = string.Format("Eliminar registro {0}", equipoId);
            string message = string.Format("¿Deseas eliminar el registro {0}?", equipoId);

            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show(header, message, buttons);

            if (result == MessageBoxResult.Yes)
            {
                // Se muestra caja de mensaje si el resultado del request delete es true o false 
                var response = filaSeleccionada.Delete(equipoId) ?
                    MessageBox.Show("Registro eliminado") :
                    MessageBox.Show("No se pudo eliminar el registro");
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            CargarGrilla();
        }
    }
}
