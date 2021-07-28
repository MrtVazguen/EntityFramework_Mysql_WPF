using Microsoft.Extensions.Primitives;
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
using static Exercici4._1.DAOManager;

namespace Exercici4._1
{
    /// <summary>
    /// Lógica de interacción para wndCrudDB.xaml
    /// </summary>
    public partial class wndCrudDB : Window 
    {
        MODEL.classicmodelsContext context;
        DAOManager manager;
        MODEL.Products p;
        private String productLine; 
        private MainWindow.TipusCrud tipusCRUD;
        MainWindow mainWindow;
        public wndCrudDB()
        {
            InitializeComponent();
            context = new MODEL.classicmodelsContext();
            manager = new DAOManager(context);
        }

        public wndCrudDB(MainWindow.TipusCrud tipusCRUD, String producteLineName) : this()
        {
            ProductLine = producteLineName;
            TipusCRUD = tipusCRUD;
        }
        public wndCrudDB(MainWindow.TipusCrud tipusCRUD, MODEL.Products _p) : this()
        {
            p = _p;
           TipusCRUD = tipusCRUD;
        }
        private void stackPanelDades_Loaded(object sender, RoutedEventArgs e)
        { 
            
            switch (TipusCRUD)
            {
                case MainWindow.TipusCrud.DonarALTA:
                    tbTipusCRUD.Text = "Donar alta";
                    btnFerCRUD.Content = "Fer ALTA";
                    //valors per defecte (registre)
                    tbProductLine.Text = productLine;
                    tbTipusCRUD.Background = Brushes.Green;
                    break; 
                case MainWindow.TipusCrud.MODIFICAR:

                    productLine = p.ProductLine;

                    tbTipusCRUD.Text = "Modificar";
                    btnFerCRUD.Content = "Modificar";
                    //valors per defecte
                    tbProductLine.Text = p.ProductLine;
                    tbProductCode.Text = p.ProductCode; tbProductCode.IsReadOnly = true;
                    tbBuyPrice.Text = Convert.ToString( p.BuyPrice);
                    tbDescripcio.Text = p.ProductDescription;
                    tbMSRP.Text = Convert.ToString(p.Msrp);
                    tbProductName.Text = p.ProductName;
                    tbQuantituStock.Text = Convert.ToString(p.QuantityInStock);
                    tbproductScale.Text = p.ProductScale;
                    tbProductVendor.Text = p.ProductVendor;
                    tbTipusCRUD.Background = Brushes.YellowGreen;
                    break;
            }
            tbTipusCRUD.Text = TipusCRUD.ToString();
        } 

        public string ProductLine { get => productLine; set => productLine = value; }

        public MainWindow.TipusCrud TipusCRUD { get => tipusCRUD; set => tipusCRUD = value; }

        private void btnFerCRUD_Click(object sender, RoutedEventArgs e)
        {   

            ValidarDatosCRUD validarDatos = new ValidarDatosCRUD();

            String msgError = string.Empty;
            bool[] dadesValidades ;
            string[] msgErrorArr;

            _ = !validarDatos.validarProductCode(tbProductCode.Text)? tbProductCode.Background = Brushes.Red : tbProductCode.Background = Brushes.White;
            _ = !validarDatos.validarProductName(tbProductName.Text)? tbProductName.Background = Brushes.Red : tbProductName.Background = Brushes.White;
            _ = !validarDatos.validarProductScale (tbproductScale.Text)  ? tbproductScale.Background = Brushes.Red : tbproductScale.Background = Brushes.White;
            _ = !validarDatos.validarProductVendor(tbProductVendor.Text ) ? tbProductVendor.Background = Brushes.Red : tbProductVendor.Background = Brushes.White;
            _ = !validarDatos.validarProductDescription(tbDescripcio.Text) ? tbDescripcio.Background = Brushes.Red : tbDescripcio.Background = Brushes.White;
            _ = !validarDatos.validarQuantityInStock(tbQuantituStock.Text) ? tbQuantituStock.Background = Brushes.Red : tbQuantituStock.Background = Brushes.White;
            _ = !validarDatos.validarBuyPrice(tbBuyPrice.Text) ? tbBuyPrice.Background = Brushes.Red : tbBuyPrice.Background = Brushes.White;
            _ = !validarDatos.validarMsrp(tbMSRP.Text)? tbMSRP.Background = Brushes.Red : tbMSRP.Background = Brushes.White;


            dadesValidades = validarDatos.GetValidadDades();
            msgErrorArr = validarDatos.GetErrorMesage();

            for (int i = 0; i < validarDatos.GetErrorMesage().Length; i++)
            {
                if (!dadesValidades[i])
                {
                    msgError += msgErrorArr[i];
                    _ = i < dadesValidades.Length-1 ? msgError += "," : msgError += "";
                }
            }

            if (msgError.Length > 0)
            { 
                MessageBox.Show($" [ {msgError} ] " ,"Dades no son valides  !!!",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
            
            else {

                p = validarDatos.GetProducts();
                p.ProductLine = productLine;
                
                switch (TipusCRUD)
                {
                    case MainWindow.TipusCrud.DonarALTA:
                        try { 
                        if (manager.Insert(p)) { MessageBox.Show("ALTA REALITZADA AMB ÈXIT");this.Close(); }
                        else
                        {
                            MessageBox.Show("No hem pogut donar de alta.");
                        }
                        }catch(Exception ex)
                        {
                            MainWindow.mostrarMsgError(ex.Message);
                        }
                        break;
                    case MainWindow.TipusCrud.MODIFICAR:
                        try { 
                        if (manager.Update(p)) { MessageBox.Show("Modificacio REALITZADA AMB ÈXIT"); this.Close(); }
                        else
                        {
                            MessageBox.Show("No hem pogut fer la modificacio");
                        }
                        }catch(Exception ex)
                        {
                            MainWindow.mostrarMsgError(ex.Message);
                        }
                        break;
                }
            }
        }

      
    }
}
