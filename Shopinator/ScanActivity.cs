using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ZXing.Mobile;

namespace Shopinator
{
    [Activity(Label = "ScanActivity")]
    public class ScanActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ScanLayout);
            MobileBarcodeScanner.Initialize(Application);

            Button scanCodeSrc = FindViewById<Button>(Resource.Id.scanCode);
            Button goBackSrc = FindViewById<Button>(Resource.Id.goBackBtn);

            scanCodeSrc.Click += scanCodeSrc_Click;
            goBackSrc.Click += goBackSrc_Click;
        }

        private async void scanCodeSrc_Click(object sender, EventArgs e)
        {
            var scanner = new ZXing.Mobile.MobileBarcodeScanner();
            var result = await scanner.Scan();

            if (result != null)
            {
                Console.WriteLine("Scanned Barcode: " + result.Text);
                Toast.MakeText(this, result.Text, ToastLength.Short).Show();
            }
        }

        private void goBackSrc_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainMenuActivity));
        }
    }
}