using EvaluacionGUI.Views;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EvaluacionGUI
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CargarGrilla();
            WindowStartupLocation = WindowStartupLocation.CenterScreen; // Posicionamiento al centro de la pantalla
        }

        private void optAgregarEquipo_Click(object sender, RoutedEventArgs e)
        {
            Views.AgregarEquipo agregarEquipo = new Views.AgregarEquipo();
            agregarEquipo.Owner = this; // La instancia hereda de la ventana principal
            agregarEquipo.ShowDialog();
        }

        private void optListarEquipos_Click(object sender, RoutedEventArgs e)
        {
            Views.ListarEquipos listarEquipos = new Views.ListarEquipos();
            listarEquipos.Owner = this; // La instancia hereda de la ventana principal
            listarEquipos.ShowDialog();
        }

        private void optReportes_Click(object sender, RoutedEventArgs e)
        {
            CargarReportes();
        }

        private void optAcercaDe_Click(object sender, RoutedEventArgs e)
        {
            Views.AcercaDe acercaDe = new Views.AcercaDe();
            acercaDe.Owner = this; // La instancia hereda de la ventana principal
            acercaDe.ShowDialog();
        }

        private void CargarGrilla()
        {
            EvaluacionNegocio.EquipoCollection equiposCollection = new EvaluacionNegocio.EquipoCollection();
            dgListadoEquipos.ItemsSource = equiposCollection.ReadAll();
        }

        private void CargarReportes()
        {
            EvaluacionNegocio.EquipoReportes equipoReportes = new EvaluacionNegocio.EquipoReportes();

            int cantMasculinos = equipoReportes.CantidadEquiposMasculinos();
            int cantFemeninos = equipoReportes.CantidadEquiposFemeninos();

            string message = string.Format("Cantidad de equipos femeninos: {0} \n" +
                                             "Cantidad de equipos masculinos: {1}",
                                             cantFemeninos, cantMasculinos);
            MessageBox.Show(message);
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void dgListadoEquipos_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Error" || e.Column.Header.ToString() == "ErrorCollection")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
    }
}
