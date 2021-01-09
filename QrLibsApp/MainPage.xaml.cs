using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;
using QrLibs;



namespace QrLibsApp
{
    
    public partial class MainPage : ContentPage
    {
        QrLibsCl myQrLibs;
        string result;

        public MainPage()
        {
            InitializeComponent();
            
        }

        async void OnButtonClicked(object sender, EventArgs args)
        {
            labelRes.Text = result;

            Button button = (Button)sender;
            await DisplayAlert("Clicked!",
                "The button labeled '" + button.Text + "' has been clicked",
                "OK");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            myQrLibs = new QrLibsCl();
            TestMathFuncs();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            myQrLibs.Dispose();
        }

        private void TestMathFuncs()
        {
            var numberA = 1;
            var numberB = 2;

            // Test Add function
            var addResult = myQrLibs.Add(numberA, numberB);

            // Test Subtract function
            var subtractResult = myQrLibs.Subtract(numberA, numberB);

            // Test Multiply function
            var multiplyResult = myQrLibs.Multiply(numberA, numberB);

            // Test Divide function
            var divideResult = myQrLibs.Divide(numberA, numberB);

            // Output results
            Debug.WriteLine($"{numberA} + {numberB} = {addResult}");
            Debug.WriteLine($"{numberA} - {numberB} = {subtractResult}");
            Debug.WriteLine($"{numberA} * {numberB} = {multiplyResult}");
            Debug.WriteLine($"{numberA} / {numberB} = {divideResult}");


            // TEST QR DECODE
            string str = "F22:1:3:0.00%M:0.00%M:9.00%M:9.00%M:9.00%M:9.00%M+3Y31";

            int len = myQrLibs.QRDecode(str, 12);
            result = myQrLibs.QRGetResult(len);

            Debug.WriteLine(result);
            Debug.WriteLine("QR test end.");


            
        }


    }
}
