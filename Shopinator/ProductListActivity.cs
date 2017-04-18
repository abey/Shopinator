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
    [Activity(Label = "ProductListActivity")]
    public class ProductListActivity : Activity
    {
        private ListView productListView;
        private List<string> productList;

        // JSON STUFF START
        public class Product
        {
            public string id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public string image { get; set; }
        }

        public class RootObject
        {
            public List<Product> products { get; set; }
        }
        // JSON STUFF END

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ProductList);

            productListView = FindViewById<ListView>(Resource.Id.productListView);
            productList = new List<string>();
            productList.Add("Item 0 \n somestuff");
            productList.Add("Item \n somestuff1");
            productList.Add("Item \n somestuff2");
            productList.Add("Item \n somestuff3");
            productList.Add("Item \n somestuff4");

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