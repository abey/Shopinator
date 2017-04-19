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

            //string json = "{\"products\": [{\"id\": \"1\",\"name\": \"Staples FSC-Certified Copy Paper\"},{\"id\": \"2\",\"name\": \"Polypropylene Strap Kit with Metal Buckles\"},{\"id\": \"3\",\"name\": \"aafaf\"}]}";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var json = new WebClient().DownloadString(API);
            SetContentView(Resource.Layout.ProductList);
            RootObject r = JsonConvert.DeserializeObject<RootObject>(json);
            productListView = FindViewById<ListView>(Resource.Id.productListView);
            productList = new List<string>();

            for (int i=0; i<6;i++)
            {
                productList.Add("Name: " + r.products[i].name + "\n\nID: " + r.products[i].id + "\n\nDescription: " + r.products[i].description + "\n\n\n");
            }
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, productList);
            productListView.Adapter = adapter;
            productListView.ItemClick += Listnames_ItemClick;
        }

        private void Listnames_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(this, e.Position.ToString(), ToastLength.Long).Show();
        }

        
    }
}