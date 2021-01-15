using Newtonsoft.Json.Linq;
using QrLibs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace QrLibsApp
{
    public class ItemsViewModel
    {
        QrLibsCl myQrLibs;
        string result;
        string[] str = { "F22:1:3:0.00%M:0.00%M:9.00%M:9.00%M:9.00%M:9.00%M+3Y31", "F20:1:1:$3E199.9M:525 + 246Z / F23:1:0.001 % M:0::3.50 % M:+A1 / I3:1 + CSA" };
        List<string> termsList = new List<string>();

        public ItemsViewModel()
        {
            myQrLibs = new QrLibsCl();
            QrDecode();
            GenerateItemInfo();
        }

        public ObservableCollection<Items> ItemsInfo { get; set; }

        public void GenerateItemInfo()
        {
            Assembly assembly = typeof(MainPage).GetTypeInfo().Assembly;
            string[] terms = termsList.ToArray();
            var mainNode = new Items() { Name = "QR Decoded Results", ImageIcon = ImageSource.FromResource("QrLibsApp.Icons.apps.png", assembly) };
            Items[] x = new Items[str.Length];
  
            mainNode.SubItems = new ObservableCollection<Items>();

            this.ItemsInfo = new ObservableCollection<Items>();
            ItemsInfo.Add(mainNode);

            for (int j = 0; j < str.Length; j++)
            {
                dynamic data = JObject.Parse(terms[j]);

                var child = new Items() { Name = "Measurement id " + data.DDid, ImageIcon = ImageSource.FromResource("QrLibsApp.Icons.class.png", assembly) };
                                
                mainNode.SubItems.Add(child);

                child.SubItems = new ObservableCollection<Items>();

                for (int i = 0; i < data.content.Count; i++)
                {
                    child.SubItems.Add(new Items() { Name = data.content[i].type + " -> " + data.content[i].value, ImageIcon = ImageSource.FromResource("QrLibsApp.Icons.check.png", assembly) });
                    
                }
                x[j] = child; 
            }
        }

        private void QrDecode()
        {
            int len;
            for (int runs = 0; runs < str.Length; runs++)
            {
                len = myQrLibs.QRDecode(str[runs], 12);
                termsList.Add(myQrLibs.QRGetResult(len));
            }
        }
    }
}
