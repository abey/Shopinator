using Android.App;
using Android.Widget;
using Android.OS;
using System;
using SQLite;
using System.IO;

namespace Shopinator
{
    [Activity(Label = "Shopinator", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        EditText unameSrc;
        EditText pswdSrc;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            unameSrc = FindViewById<EditText>(Resource.Id.userName);
            pswdSrc = FindViewById<EditText>(Resource.Id.password);

            Button login = FindViewById<Button>(Resource.Id.login);
            Button register = FindViewById<Button>(Resource.Id.register);

            login.Click += LoginBtn_Click;
            register.Click += RegisterBtn_Click;
            CreateDB();
        }

        private void LoginBtn_Click (object sender, EventArgs e) {
            try
            {
                string databaseSource = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "userinfo.db3"); //Call Database  
                var database = new SQLiteConnection(databaseSource);
                var data = database.Table<LoginTable>(); 
                var data1 = data.Where(x => x.username == unameSrc.Text && x.password == pswdSrc.Text).FirstOrDefault(); //Linq Query  
                if (data1 != null)
                {
                    Toast.MakeText(this, "Login Successful", ToastLength.Short).Show();
                    StartActivity(typeof(MainMenuActivity));
                }
                else
                {
                    Toast.MakeText(this, "Username or Password is invalid. Please try again!", ToastLength.Short).Show();
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(RegisterationActivity));
        }

        public string CreateDB()
        {
            var output = "DB Setup";
            string databaseSource = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "userinfo.db3");
            var database = new SQLiteConnection(databaseSource);
            return output;
        }
    }
}

