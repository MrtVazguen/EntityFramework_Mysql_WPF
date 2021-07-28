using Exercici4._1.MODEL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercici4._1
{
    class DAOManager
    {
        private MODEL.classicmodelsContext context;
        
        public DAOManager(MODEL.classicmodelsContext context) 
        {
            this.context = context;
        }
        
        public MODEL.Productlines GetProductlinesById(ushort id)
        {
            MODEL.Productlines a = context.Productlines.Find(id);
            return a;
        }

        public List<MODEL.Productlines> GetProducteLines()
        {  
            var lstProducteLines = (from pLine in context.Productlines
                                    select new
                                    {
                                        producteLine = pLine.ProductLine,
                                        Prodectes = pLine.Products,
                                        txtDescripcio = pLine.TextDescription,
                                        Imatge = pLine.Image
                                    });
            var obj = lstProducteLines.OrderBy(p => p.producteLine).ToList();
             
            List<MODEL.Productlines> producteLine = new List<MODEL.Productlines>();
             
            foreach (var c in obj)//selectedItem convertir product line
            {
                producteLine.Add(new MODEL.Productlines { ProductLine = c.producteLine, /*Prodectes = c.Prodectes.GetType().ToString(),*/ TextDescription = c.txtDescripcio}); ; ;
            }
             
            return producteLine;

        }

        public List<MODEL.Products> GetProducts(string productLine)
        { 
            var lstProtas = (from fa in context.Products
                             where fa.ProductLine == productLine 
                             join a in context.Products on fa.ProductCode equals a.ProductCode


                             select new
                             {
                                 productCode = a.ProductCode,
                                 productName = a.ProductName,
                                 //Falta ProductScale
                                 ProductScale = a.ProductScale,
                                 productLine = a.ProductLine,
                                 buyPrice=  a.BuyPrice,
                                 orderdetails=  a.Orderdetails,
                                 quantityInStock = a.QuantityInStock,
                                 productVendor =  a.ProductVendor,
                                 msrp = a.Msrp,
                                 productDescription = a.ProductDescription

                             }
                            ) ;
             var obj= lstProtas.OrderBy(a => a.productName).ThenBy(a => a.productName).ToList();


            List<MODEL.Products> products = new List<MODEL.Products>();


            foreach(var c in obj)
            {
                products.Add(new MODEL.Products { ProductCode = c.productCode, ProductName = c.productName == null ? "null" : c.productName, QuantityInStock = c.quantityInStock, ProductLine = c.productLine, ProductVendor = c.productVendor!=null?c.productVendor:"Null", BuyPrice = c.buyPrice, Msrp = c.msrp, ProductDescription = c.productDescription,ProductScale = c.ProductScale });

            }

            return products;

        }

         public List<MODEL.Products> GetProductsFiltratsPerNom(string productPerFiltrar ,String productLineName)
        {
            #region codiTMP
            /*
            List<MODEL.Products> products = new List<MODEL.Products>();
            var lstProtas = (from fa in context.Products 
                             where fa.ProductName == productLineName
                             select new
                             {
                                 productCode = fa.ProductCode,
                                 productName = fa.ProductName.ToLower(),
                                 productDescription = fa.ProductDescription.ToLower(),
                                 productLine = fa.ProductLine,
                                 buyPrice = fa.BuyPrice, 
                                 quantityInStock = fa.QuantityInStock 

                             }
                           );
            
           
            var obj = lstProtas.OrderBy(a => a.productName.Contains(productPerFiltrar)).ThenBy(a => a.productName).ToList();
            
            foreach (var c in obj)
            {
              //  if( c.productName.Contains(productFiltrar.ToLower()) || c.productDescription.Contains(productFiltrar.ToLower()))
                     products.Add(new MODEL.Products { ProductCode = c.productCode,QuantityInStock = c.quantityInStock,  ProductName = c.productName, ProductDescription = c.productDescription, ProductLine = c.productLine, BuyPrice = c.buyPrice  });;
            }
           
             
            return products;

            */
            #endregion
            List<MODEL.Products> products = new List<MODEL.Products>();
            var filtered = context.Products.Where(x => x.ProductLine == productLineName && (x.ProductName.Contains(productPerFiltrar))).AsEnumerable();
            products = filtered.OrderBy(a => a.ProductName).ThenBy(a => a.ProductName).ToList();
            return products;
        }
        public List<MODEL.Products> GetProductsFiltratsPerNomODescripcio(string productPerFiltrar, String productLineName)
        {
             
            List<MODEL.Products> products = new List<MODEL.Products>();
            var filtered = context.Products.Where(x => x.ProductLine == productLineName && (x.ProductDescription.Contains(productPerFiltrar) || x.ProductName.Contains(productPerFiltrar))).AsEnumerable();
            products = filtered.OrderBy(a => a.ProductName).ThenBy(a => a.ProductName).ToList();
            return products;
        }
 

        public List<MODEL.Products> GetProductsFiltratsPerNomOrDescripcio(string productPerFiltrar, String productLineName)
        {

            List<MODEL.Products> products = new List<MODEL.Products>();
            var filtered = context.Products.Where(x => x.ProductLine == productLineName && (x.ProductDescription.Contains(productPerFiltrar) | x.ProductName.Contains(productPerFiltrar))).AsEnumerable();
            products = filtered.OrderBy(a => a.ProductName).ThenBy(a => a.ProductName).ToList();
            return products;
        }
        public List<MODEL.Products> GetProductsFiltratsPerDescripcio(string productPerFiltrar, String productLineName)
        {

            List<MODEL.Products> products = new List<MODEL.Products>();
            var filtered = context.Products.Where(x => x.ProductLine == productLineName && (x.ProductDescription.Contains(productPerFiltrar))).AsEnumerable();
            products = filtered.OrderBy(a => a.ProductName).ThenBy(a => a.ProductName).ToList();
            return products;
        }
        #region GetProductsFiltratsCantidatInStock Antic
        // public List<MODEL.Products> GetProductsFiltratsCantidatInStock(int cantidatInStock, string productLine , bool checkOver)
        //{
        //    List<MODEL.Products> products = new List<MODEL.Products>();
        //    #region codiTMP
        //    /*
        //    var lstProtas = (from fa in context.Products 
        //                     where fa.ProductName == productLineName && fa.QuantityInStock>=cantidatInStock

        //                     select new
        //                     {
        //                         productCode = fa.ProductCode,
        //                         productName = fa.ProductName.ToLower(),
        //                         productDescription = fa.ProductDescription.ToLower(),
        //                         productLine = fa.ProductLine,
        //                         buyPrice = fa.BuyPrice,
        //                         quantityInStock = fa.QuantityInStock 
        //                     }
        //                   );

        //    var obj = lstProtas.OrderBy(a => a.productName).ThenBy(a => a.productName).ToList();

        //    foreach (var c in obj)
        //    {
        //        if (c.quantityInStock >= cantidatInStock)
        //            products.Add(new MODEL.Products { ProductCode = c.productCode, ProductName = c.productName,QuantityInStock = c.quantityInStock,   ProductDescription = c.productDescription, ProductLine = c.productLine, BuyPrice = c.buyPrice });
        //    }*/
        //    #endregion\

        //    if (checkOver)
        //    {
        //        var filtered = context.Products.Where(x => x.QuantityInStock >= cantidatInStock && x.ProductLine == productLine).AsEnumerable();
        //        products = filtered.OrderBy(a => a.ProductName).ThenBy(a => a.ProductName).ToList();
        //    }
        //    else
        //    {
        //        var filtered = context.Products.Where(x => x.QuantityInStock <= cantidatInStock && x.ProductLine == productLine).AsEnumerable();
        //        products = filtered.OrderBy(a => a.ProductName).ThenBy(a => a.ProductName).ToList();
        //    }

        //    return products;
        //}
        #endregion
        public List<MODEL.Products> GetProductsFiltratsCantidatInStock(short cantidatInStock, Productlines product, bool checkOver)
        {
            List<MODEL.Products> products = new List<MODEL.Products>();
        
            if (checkOver)
            {
                var filtered = product.Products.Where(x => x.QuantityInStock >= cantidatInStock  ).AsEnumerable();
                products = filtered.OrderBy(a => a.ProductName).ThenBy(a => a.ProductName).ToList();
            }
            else
            {
                var filtered = product.Products.Where(x => x.QuantityInStock <= cantidatInStock).AsEnumerable();
                products = filtered.OrderBy(a => a.ProductName).ThenBy(a => a.ProductName).ToList();
            }

            return products;
          }
        /// <summary>
        /// Cuando es igual que la cantidad marcada
        /// </summary>
        /// <param name="cantidatInStock"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        public List<MODEL.Products> GetProductsFiltratsCantidatInStock(short cantidatInStock, Productlines product)
        {
            List<MODEL.Products> products = new List<MODEL.Products>();

                var filtered = product.Products.Where(x => x.QuantityInStock == cantidatInStock).AsEnumerable();
                products = filtered.OrderBy(a => a.ProductName).ThenBy(a => a.ProductName).ToList();
          
            return products;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        public int GetNumComandes(string productCode)
        {

            #region codiTMP
            // var query = context.Orderdetails.Where(x => productCode.Contains(x.ProductCode)).Select(x => x.ProductCode);
            /* (from s1 in context.Orderdetails
                                           where s1.ProductCode == productCode
                                                select s1).SingleOrDefault();*/
            #endregion
            int nComandes = -1;
            if ((from item in context.Orderdetails where item.ProductCode == productCode select item).Count() > 0)
            {
                MODEL.Orderdetails orders = context.Orderdetails.First(x => x.ProductCode == (productCode));
                if (orders != null && orders.QuantityOrdered > 0)
                    nComandes= orders.QuantityOrdered;
            }

            return nComandes; 
        }

        #region CRUD 

        public MODEL.Products GetProductById(ushort id)
        {
            MODEL.Products a = context.Products.Find(id);
            return a;
        }
     
        
        public bool Insert(MODEL.Products a)
        {
            context.Products.Add(a);

            return context.SaveChanges() > 0;
        }

        public bool Update(MODEL.Products a)
        {
            context.Products.Update(a);
            return context.SaveChanges() > 0;
        }
        public bool Delete(MODEL.Products a)
        {
            // var itemToRemove = context.Products.SingleOrDefault(x => x.ProductCode == a.ProductCode);
            var itemToRemove = context.Products.First(x => x.ProductCode == a.ProductCode);
            context.Products.Remove(itemToRemove); 
            return context.SaveChanges() > 0;

        }

        #endregion

        #region CodiTMP
        /*
        #region Utilitat:Clases 
        public class ProducteLine
        {
            private string producteLine;
            private string descripcion;
            public string Descripcion { get => descripcion; set => descripcion = value; }
            public string ProducteLines { get => producteLine; set => producteLine = value; }

            public string Details
            {
                get
                {
                    return String.Format("Descripcio: {0}", this.descripcion.ToString());
                }

            }
        } 
        public  class Product
        {
            private string productCode;
            private string productName;
            private string productLine;
            private string productScale;
            private string prouctVendor;
            private string productDescription;
            private short quantityInStock;
            private decimal buyPrice;
            private decimal MSRP; 

            public string ProductCode { get => productCode; set => productCode = value; }
            public string ProductName { get => productName; set => productName = value; }
            public string ProductLine { get => productLine; set => productLine = value; }
            public string ProductDescription { get => productDescription; set => productDescription = value; }
            public string ProductScale { get => productScale; set => productScale = value; }
            public string ProuctVendor { get => prouctVendor; set => prouctVendor = value; }          
            public short QuantityInStock { get => quantityInStock; set => quantityInStock = value; }
            public decimal BuyPrice { get => buyPrice; set => buyPrice = value; }
            public decimal MSRP1 { get => MSRP; set => MSRP = value; }
        }
        #endregion
        */
        #endregion
    }
}
