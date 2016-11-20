using System;
using Android.App;
using Android.OS;
using Android.Widget;
using Attendance.Core.Models;
using Attendance.Core.Service;

namespace Attendance
{
    [Activity(Label = "Attendance")]
    public class ProfileActivity : Activity
    {
        private string userName;
        private EditText firstNameEditText;
        private EditText lastNameEditText;
        private EditText emailAddressEditText;
        private Button saveButton;
        private Activity context;
        private AttendanceService attendanceService = new AttendanceService();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Profile);

            userName = Intent.Extras.GetString("userName");

            FindViews();

            HandleEvents();
        }

        private void FindViews()
        {
            context = this;
            firstNameEditText = FindViewById<EditText>(Resource.Id.firstNameEditText);
            lastNameEditText = FindViewById<EditText>(Resource.Id.lastNameEditText);
            emailAddressEditText = FindViewById<EditText>(Resource.Id.emailAddressEditText);
            saveButton = FindViewById<Button>(Resource.Id.btnProfileSave);
        }

        private async void HandleEvents()
        {
            saveButton.Click += SaveButton_Click;
            var user = await attendanceService.GetUser(userName);

            firstNameEditText.Text = user.FirstName;
            lastNameEditText.Text = user.LastName;
            emailAddressEditText.Text = user.EmailAddress;
        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            var saveResult = await attendanceService.SaveProfile(new User
            {
                UserName = userName,
                EmailAddress = emailAddressEditText.Text,
                FirstName = firstNameEditText.Text,
                LastName = lastNameEditText.Text
            });

            var message = saveResult ? "Saved profile changes" : "Unable to save profile changes";

            Toast.MakeText(context, message, ToastLength.Long).Show();
        }
    }
}