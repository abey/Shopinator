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

namespace Shopinator
{
    [Activity(Label = "MainMenuActivity")]
    public class MainMenuActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MainMenu);

            Button scan = FindViewById<Button>(Resource.Id.scanBtn);
            Button product_list = FindViewById<Button>(Resource.Id.productListBtn);

            scan.Click += scan_Click;
            product_list.Click += product_list_Click;
        }

        private void scan_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Loading Barcode Scanner...", ToastLength.Short).Show();
            StartActivity(typeof(ScanActivity));
        }

        private void product_list_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Loading Product List...", ToastLength.Short).Show();
            StartActivity(typeof(ProductListActivity));
        }

    }
}