using Exercici4._1.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercici4._1
{
    class ValidarDatosCRUD
    {
     
        Products products = new Products();
        private const int nombreDades = 8;
        private bool[] validadDades = new bool[nombreDades];
        private string[] errorMesage = new string[nombreDades];

        public bool validarProductCode(string productCode)
        {
            validadDades[0] = false;
           
            if (productCode.Length>4&&(  productCode.Length <= 14&productCode[0] == 'S'& productCode.Contains( '_')))
            {
                validadDades[0] = true;
                errorMesage[0] = "";
                products.ProductCode = productCode;
            }
            else
            {
                errorMesage[0] = "product code";
            }

            return validadDades[0];
        }
        public bool validarProductName(String productName) {
            if (productName.Length > 3)
            {
                products.ProductName = productName;
                validadDades[1] =true;
            }
            else {
                validadDades[1] = false;
                errorMesage[1] = "nom de producte";
            }
            return validadDades[1];
        }
        public bool validarProductScale(String productScala)
        {
            if (productScala.Length >= 2 && productScala.Contains(':'))
            {
                products.ProductScale = productScala;
                validadDades[2] = true;
              
            }
            else
            {
                validadDades[2] = false;
                errorMesage[2] = "product scala";
            }

            return validadDades[2];
        }
        public bool validarProductVendor(String productVendor)////////
        {
            if (productVendor.Length > 3)
            {
                products.ProductVendor = productVendor;
                validadDades[3] = true;
                
            }
            else { validadDades[3] = false; errorMesage[3] = "vendedor"; }

            return validadDades[3];

        }
        public bool validarProductDescription(String description)
        {
            if (description.Length > 3)
            {
                products.ProductDescription = description;
                validadDades[4] = true;
              
            }
            else { validadDades[4] = false; errorMesage[4] = "descripcio"; }

            return validadDades[4];
        }
        public bool validarQuantityInStock(String quantityInStock)
        {
            if (short.TryParse(quantityInStock, out _))
            {
                products.QuantityInStock = short.Parse(quantityInStock);
                validadDades[5] = true;

            }
            else { validadDades[5] = false; errorMesage[5] = "cantidad en stock"; }

            return validadDades[5];
        }
        public bool validarBuyPrice(String buyPrice) 
        {
            if (decimal.TryParse(buyPrice, out _))
            {
                products.BuyPrice = decimal.Parse(buyPrice);
                validadDades[6] = true;

            }
            else { validadDades[6] = false; errorMesage[6] = "preu de compra"; }

            return validadDades[6];
        }
        public bool validarMsrp(String msrp) {

            if (decimal.TryParse(msrp, out _))
            {
                products.Msrp = decimal.Parse(msrp);
                validadDades[7] = true;

            }
            else { validadDades[7] = false; errorMesage[7] = "msrp"; }

            return validadDades[7];
        }

        public Products GetProducts()
        {
            return this.products;
        }
        public bool[] GetValidadDades() { return validadDades; }
        public string[] GetErrorMesage() { return errorMesage; }
    }
}
