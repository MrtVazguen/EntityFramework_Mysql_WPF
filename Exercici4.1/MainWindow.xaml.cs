using Exercici4._1.MODEL;
using System;
using System.Collections;
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

namespace Exercici4._1
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private string currentProductLine = string.Empty;
        private bool[] filtrarDades = { false, false };
        
        public enum TipusCrud
        {
            DonarALTA,
            MODIFICAR,
            ELIMINAR
        }
        MODEL.classicmodelsContext context ;
        DAOManager manager;

        public MainWindow()
        {
            InitializeComponent();
            try { 
            context = new MODEL.classicmodelsContext();
            manager = new DAOManager(context);
            GeneratProductesList();

            //filtrar producte(estat)
            CheckboxFiltreEstat(false); 
            ckbFiltrar.IsEnabled = false;
            btnFiltrar.IsEnabled = false;
            //estat pasiu botons CRUD
            CanviEstatBotonsCrud(false);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void GeneratProductesList()
        {
            dtProductesLines.AutoGenerateColumns = false;

            dtProductesLines.ItemsSource = manager.GetProducteLines();
        }
         
        private void dtProductesLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //int index = dtProductesLines.SelectedIndex;
            // Object o = dtProductesLines.Items.GetItemAt(index);
            MODEL.Productlines product   = (MODEL.Productlines)dtProductesLines.SelectedItem;
            this.Title = product.ProductLine;
            dtProductes.Visibility = Visibility.Visible;
            currentProductLine = product.ProductLine;
            dtProductes.ItemsSource = manager.GetProducts(currentProductLine);
            

            ///activem el filtre
            ckbFiltrar.IsEnabled = true;
            btnFiltrar.IsEnabled = true;

            //estat actiu botons de crud
            CanviEstatBotonsCrud(true);
        }

        #region FILTRAR CONTINGUT
       
        /// <summary>
        /// Fins que no selecionem cap producte de product line no es pot filtrar productes
        /// </summary>
        /// <param name="isEnable"> estat de contorls de filtrar per num stock i per nom</param>
        public void CheckboxFiltreEstat( bool isEnable)
        {
            integerUpDouwnFiltrarStock.IsEnabled = isEnable;
            tbxFiltrarPerNom.IsEnabled = isEnable;
            cbxFiltarNom.IsEnabled = isEnable;
            cbxFiltarDescripcion.IsEnabled = isEnable; 
           
            rbOver.IsEnabled = isEnable;
            rbUnder.IsEnabled = isEnable;
        }

        private void ckbFiltrar_Checked(object sender, RoutedEventArgs e)
        {
            CheckboxFiltreEstat(true);
        }

        private void ckbFiltrar_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckboxFiltreEstat(false);
            dtProductes.ItemsSource = manager.GetProducts(currentProductLine);
        } 

        private void btnFiltrar_Click(object sender, RoutedEventArgs e)
        {
            MODEL.Productlines dataFiltrar = (MODEL.Productlines)dtProductesLines.SelectedItem;
             if (integerUpDouwnFiltrarStock.Value == 0 & tbxFiltrarPerNom.Text == "")
            {
                MessageBox.Show("No he selecionat el tipo de filtre");
                MODEL.Productlines product = (MODEL.Productlines)dtProductesLines.SelectedItem;
                dtProductes.ItemsSource = manager.GetProducts(product.ProductLine);

            }
            else// (tbxFiltrarPerNom.Text != "")
            {
                if (cbxFiltarDescripcion.IsChecked == true & cbxFiltarNom.IsChecked == true)
                {
                    dataFiltrar.Products= manager.GetProductsFiltratsPerNomODescripcio(tbxFiltrarPerNom.Text, dataFiltrar.ProductLine);
                    dtProductes.ItemsSource = dataFiltrar.Products;
                }
                else if ((cbxFiltarNom.IsChecked == false & cbxFiltarDescripcion.IsChecked == false ))
                {
                    dataFiltrar.Products = manager.GetProductsFiltratsPerNomOrDescripcio(tbxFiltrarPerNom.Text, dataFiltrar.ProductLine);
                    dtProductes.ItemsSource = dataFiltrar.Products;

                } 
                else
                {
                    if (cbxFiltarDescripcion.IsChecked == true)
                    {
                        dataFiltrar.Products = manager.GetProductsFiltratsPerDescripcio(tbxFiltrarPerNom.Text, dataFiltrar.ProductLine);
                        dtProductes.ItemsSource = dataFiltrar.Products;
                    }
                    if (cbxFiltarNom.IsChecked == true)
                    {
                        dataFiltrar.Products = manager.GetProductsFiltratsPerNom(tbxFiltrarPerNom.Text, dataFiltrar.ProductLine);
                        dtProductes.ItemsSource = dataFiltrar.Products;
                    }
                }
            }

            if (integerUpDouwnFiltrarStock.Value > 0)
            {
                bool checkOver;
                string[] txtFiltrarCanitdad = { "x>=", "x<=" ," == ", " Ɵ " };
               
                if (rbOver.IsChecked == true)
                {
                    checkOver = true;
                    dtProductes.ItemsSource = manager.GetProductsFiltratsCantidatInStock(short.Parse( integerUpDouwnFiltrarStock.Value.ToString()), dataFiltrar, checkOver);
                  }
                else if (rbUnder.IsChecked == true)
                {
                    checkOver = false;
                    dtProductes.ItemsSource = manager.GetProductsFiltratsCantidatInStock(short.Parse(integerUpDouwnFiltrarStock.Value.ToString()), dataFiltrar, checkOver);
                }
                else 
                {
                    dtProductes.ItemsSource = manager.GetProductsFiltratsCantidatInStock(short.Parse(integerUpDouwnFiltrarStock.Value.ToString()), dataFiltrar);//igual que la cantidad marcada
                }
               
            }
           // else //nofiltramos por numero
            
           
        }
        #endregion
        //var cellInfo = dtProductesLines.SelectedCells[0];
        //var content = cellInfo.Column.GetCellContent(cellInfo.Item);
        #region CRUD dades:DB
        private void btnDonarAlta_Click(object sender, RoutedEventArgs e)
        {
            
            wndCrudDB wndCrud = new wndCrudDB(TipusCrud.DonarALTA,this.Title);

            wndCrud.ShowDialog();
            dtProductes.ItemsSource = manager.GetProducts(currentProductLine);
        } 
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            
            MODEL.Products dataElimnar = (MODEL.Products)dtProductes.SelectedItem;
          
                if (dataElimnar != null)
                {
                    int nComands = manager.GetNumComandes(dataElimnar.ProductCode);

                    if (nComands > 0) 
                        MessageBox.Show($"No eliminat !!! Numero de comands: {nComands}");
                    else
                    {
                         MessageBoxResult result = MessageBox.Show("Important !!!", "Vols eliminar ???", MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                            {
                                manager.Delete(dataElimnar);
                                //actualizem la tabla
                                dtProductes.ItemsSource = manager.GetProducts(currentProductLine);
                                MessageBox.Show($"Hem eliminat amb exit .");
                        }
                    }
                }
                else MessageBox.Show("Falta selecionar registro de Products !!!");
            
           
        } 

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            MODEL.Products dataModificar = (MODEL.Products)dtProductes.SelectedItem;
            if (dtProductes.SelectedItems.Count > 0) 
            {
                wndCrudDB wndCrud = new wndCrudDB(TipusCrud.MODIFICAR, 
                   dataModificar);

                 wndCrud.ShowDialog();
                //actualizem la taula
                dtProductes.ItemsSource = manager.GetProducts(currentProductLine);
            }
            else
            {
                MessageBox.Show("No heu selectionat cap registre, per modificar");
            }
        }

        public void CanviEstatBotonsCrud(Boolean isEnable)
        {
            btnDonarAlta.IsEnabled = isEnable;
            btnEliminar.IsEnabled = isEnable;
            btnModificar.IsEnabled = isEnable;
        }
        #endregion


        #region Propietats per pasarAltreWindows
        
        public static String CrudUtilitzat { get; set; }
        public static String ProducteLineText { get; set; }

        #endregion

        private void rbOver_Checked(object sender, RoutedEventArgs e)
        {
            filtrarDades[0] = true;

            rbUnder.IsEnabled = false;
            rbOver.Background = Brushes.Red;

            MostrarTipoFiltroCantidad(filtrarDades);
        }

        private void rbUnder_Checked(object sender, RoutedEventArgs e)
        {
            filtrarDades[1] = true;

            rbOver.IsEnabled = false;
            rbUnder.Background = Brushes.Red;

            MostrarTipoFiltroCantidad(filtrarDades);
        }

        private void rbOver_Unchecked(object sender, RoutedEventArgs e)
        {
            filtrarDades[0] = false;

            rbUnder.IsEnabled = true;
            rbOver.Background = Brushes.White;

            MostrarTipoFiltroCantidad(filtrarDades);
        }
      
        private void rbUnder_Unchecked(object sender, RoutedEventArgs e)
        {
            filtrarDades[1] = false;

            rbOver.IsEnabled = true;
            rbUnder.Background = Brushes.White;

            MostrarTipoFiltroCantidad(filtrarDades);
        }
        private void integerUpDouwnFiltrarStock_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
           

            if (integerUpDouwnFiltrarStock != null && integerUpDouwnFiltrarStock.Value > 0)
                MostrarTipoFiltroCantidad(filtrarDades);
           
           else if (integerUpDouwnFiltrarStock != null&& tbTextFiltrarCantidad!=null && integerUpDouwnFiltrarStock.Value == 0) { tbTextFiltrarCantidad.Text = "Ø"; }
        }

        public void MostrarTipoFiltroCantidad(bool[] filtro)
        {
            string[] txtFiltrarCanitdad = { " ↑↑↑ ", " ↓↓↓ ",  "  ==  ", " Ɵ " };
            string simboloCantidadStock = "";

            if (filtro[0] == true & integerUpDouwnFiltrarStock.Value>0) //(rbOver.IsChecked == true)
            {
                tbTextFiltrarCantidad.Text = txtFiltrarCanitdad[0] + simboloCantidadStock;
            }
            else if (filtro[1] == true & integerUpDouwnFiltrarStock.Value > 0) // (rbUnder.IsChecked == true)
            {
                tbTextFiltrarCantidad.Text = txtFiltrarCanitdad[1] + simboloCantidadStock;
                // tbTextFiltrarCantidad.Text = txtFiltrarCanitdad[1] + integerUpDouwnFiltrarStock.Value.Value.ToString();
            }
            else  if(filtro[0]==false & filtro[1]==false&& integerUpDouwnFiltrarStock.Value.Value>0)
            {

                // tbTextFiltrarCantidad.Text = txtFiltrarCanitdad[3]+ integerUpDouwnFiltrarStock.Value.Value.ToString();
                tbTextFiltrarCantidad.Text = txtFiltrarCanitdad[2] + simboloCantidadStock;
            }
            else
            {
                tbTextFiltrarCantidad.Text = txtFiltrarCanitdad[3];
            }
        }
        public static void mostrarMsgError(string msgError)
        {
            MessageBox.Show("Error !!!","Error "+ msgError);
        }
      
    }

}
