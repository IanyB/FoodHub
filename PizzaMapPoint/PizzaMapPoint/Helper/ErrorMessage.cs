using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMapPoint.Helper
{
    public class ErrorMessage
    {
        public static async void MessageBox(string msg)
        {
            var msgDlg = new Windows.UI.Popups.MessageDialog(msg);
            msgDlg.DefaultCommandIndex = 1;
            await msgDlg.ShowAsync();
        }
    }
}
