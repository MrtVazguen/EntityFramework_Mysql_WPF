using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Exercici4._1
{
    static class UIUtils
    {
        public static Object ContingutGraella(DataGrid  dgv, int columna)
        {
            return dgv.Items.GetItemAt(columna);
        }
    }
}
