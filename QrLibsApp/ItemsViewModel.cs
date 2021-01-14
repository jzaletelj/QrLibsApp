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
        public string result;

        public ItemsViewModel()
        {
            myQrLibs = new QrLibsCl();
            QrDecode();
            GenerateItemInfo();
            
        }

        public ObservableCollection<Items> ItemsInfo { get; set; }

        public void GenerateItemInfo()
        {

            dynamic data = JObject.Parse(result);

            var mainNode = new Items() { Name = "QR Decoded Results" };
            var child1 = new Items() { Name = "Measurement id " + data.DDid };


            this.ItemsInfo = new ObservableCollection<Items>();
            ItemsInfo.Add(mainNode);

            mainNode.SubItems = new ObservableCollection<Items>();
            mainNode.SubItems.Add(child1);

            //var child2 = new Items() { Name = "Measurement id 118" };

            /*
            for (int i = 0; i < data.content.Count; i++)
            {
                 items[i] = (data.content[i].type + " -> " + data.content[i].value);
            }
            */

            child1.SubItems = new ObservableCollection<Items>();

            for (int i = 0; i < data.content.Count; i++)
            {
                child1.SubItems.Add(new Items() { Name = data.content[i].type + " -> " + data.content[i].value });
            }

            /*
           
            //mainNode.SubItems.Add(child2);

           
            
           child1.SubItems.Add(item1);
           child1.SubItems.Add(item2);
           child1.SubItems.Add(item3);
           child2.SubItems = new ObservableCollection<Items>();
           child2.SubItems.Add(item4);
           child2.SubItems.Add(item5);
           child2.SubItems.Add(item6);

           */






        }

        private void QrDecode()
        {
            // TEST QR DECODE
            string str = "F22:1:3:0.00%M:0.00%M:9.00%M:9.00%M:9.00%M:9.00%M+3Y31";

            int len = myQrLibs.QRDecode(str, 12);
            result = myQrLibs.QRGetResult(len);

            //Console.WriteLine("Začetek testa: ");
            //Console.WriteLine(result);

        }
  
            
    }
}
