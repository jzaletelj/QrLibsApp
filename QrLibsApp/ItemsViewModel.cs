using Newtonsoft.Json.Linq;
using QrLibs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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
            string[] terms = termsList.ToArray();
            var mainNode = new Items() { Name = "QR Decoded Results" };
            Items[] x = new Items[str.Length];

            mainNode.SubItems = new ObservableCollection<Items>();

            for (int j = 0; j < str.Length; j++)
            {
                dynamic data = JObject.Parse(terms[j]);

                var child = new Items() { Name = "Measurement id " + data.DDid };
                                
                mainNode.SubItems.Add(child);

                child.SubItems = new ObservableCollection<Items>();

                for (int i = 0; i < data.content.Count; i++)
                {
                    child.SubItems.Add(new Items() { Name = data.content[i].type + " -> " + data.content[i].value });
                }

                x[j] = child;

            }

            this.ItemsInfo = new ObservableCollection<Items>();
            ItemsInfo.Add(mainNode);

        }

        private void QrDecode()
        {
            int len;
            for (int i = 0; i < str.Length; i++)
            {
                len = myQrLibs.QRDecode(str[i], 12);
                result = myQrLibs.QRGetResult(len);
 
            }

            for (int runs = 0; runs < str.Length; runs++)
            {
                len = myQrLibs.QRDecode(str[runs], 12);
                termsList.Add(myQrLibs.QRGetResult(len));
            }
        }
    }
}
