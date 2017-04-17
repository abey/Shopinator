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
using System.IO;
using SQLite;

namespace Shopinator
{
    [Activity(Label = "RegisterationActivity")]
    public class RegisterationActivity : Activity
    {
        EditText unameSrc;
        EditText pswdSrc;
        EditText pswdConfirmSrc;
        Button createBtn;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RegistrationLayout);
            // Create your application here  
            unameSrc = FindViewById<EditText>(Resource.Id.userName);
            pswdSrc = FindViewById<EditText>(Resource.Id.password);
            pswdConfirmSrc = FindViewById<EditText>(Resource.Id.passwordConfirm);
            createBtn = FindViewById<Button>(Resource.Id.create_user);
            createBtn.Click += createBtn_Click;
        }
        private void createBtn_Click(object sender, EventArgs e)
        {
            if(pswdSrc.Text != pswdConfirmSrc.Text)
            {
                Toast.MakeText(this, "Please enter matching passwords!", ToastLength.Short).Show();
            }
            else
            {
                try
                {
                    string databaseSource = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "userinfo.db3");
                    var database = new SQLiteConnection(databaseSource);
                    database.CreateTable<LoginTable>();
                    LoginTable loginTable = new LoginTable();
                    loginTable.username = unameSrc.Text;
                    loginTable.password = pswdSrc.Text;
                    database.Insert(loginTable);
                    Toast.MakeText(this, "User added Successfully!", ToastLength.Short).Show();
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
                }
            }


            
        }
    }
}