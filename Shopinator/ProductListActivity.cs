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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;

namespace Shopinator
{
    [Activity(Label = "ProductListActivity")]
    public class ProductListActivity : Activity
    {
        private ListView productListView;
        private List<string> productList;
        public const string API = "https://abinodh.github.io/Shopinator/jsonfile.json";
        public const string COUNT_API = "https://abinodh.github.io/Shopinator/count.txt";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            var json = new WebClient().DownloadString(API);
            string characterCountString = new WebClient().DownloadString(COUNT_API);
            int count = Int32.Parse(characterCountString);
            SetContentView(Resource.Layout.ProductList);
            RootObject r = JsonConvert.DeserializeObject<RootObject>(json);
            productListView = FindViewById<ListView>(Resource.Id.productListView);
            productList = new List<string>();

            for (int i = 0; i < count; i++)
            {
                productList.Add("Name: " + r.products[i].name + "\n\nID: " + r.products[i].id + "\n\nDescription: " + r.products[i].description);
            }
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, productList);
            productListView.Adapter = adapter;
            productListView.ItemClick += Listnames_ItemClick;
        }

        private void Listnames_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            string dataLink = "https://abinodh.github.io/Shopinator/" + (e.Position + 1) + ".png";
            Toast.MakeText(this, "Opening " + dataLink, ToastLength.Long).Show();
            var uri = Android.Net.Uri.Parse(dataLink);
            var intent = new Intent(Intent.ActionView, uri);
            StartActivity(intent);
        }

        
    }
}