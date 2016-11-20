using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Attendance.Core.Models;
using Attendance.Core.Service;

namespace Attendance
{
    [Activity(Label = "Attendance", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Button _loginButton;
        private EditText _userName;
        private EditText _password;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            FindViews();
            HandleEvents();
        }

        private void FindViews()
        {
            _loginButton = FindViewById<Button>(Resource.Id.button1);
            _userName = FindViewById<EditText>(Resource.Id.editText1);
            _password = FindViewById<EditText>(Resource.Id.editText2);
        }

        private void HandleEvents()
        {
            _loginButton.Click += _loginButton_Click;
        }

        private async void _loginButton_Click(object sender, System.EventArgs e)
        {
            var userName = _userName.Text;
            var password = _password.Text;

            var attendanceService = new AttendanceService();
            var result = await attendanceService.Login(new LoginModel
            {
                Password = password,
                UserName = userName
            });

            if (result.HasError)
            {
                var alert = new AlertDialog.Builder(this);
                alert.SetTitle("Login Error");
                alert.SetMessage(result.Message);
                alert.SetPositiveButton("Ok", (senderAlert, args) =>
                {
                });

                Dialog dialog = alert.Create();
                dialog.Show();
            }
            else
            {
                var intent = new Intent();
                intent.SetClass(this, typeof(WelcomeActivity));
                intent.PutExtra("userName", userName);
                StartActivity(intent);   
            }
        }
    }
}

