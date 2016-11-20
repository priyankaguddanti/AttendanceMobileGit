using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace Attendance
{
    [Activity(Label = "Attendance")]
    public class WelcomeActivity : Activity
    {
        private TextView welcomeTextView;
        private TextView welcomeTextView1;
        private Button coursesButton;
        private Button classViewButton;
        private Button settingsButton;
        private Button logOutButton;
        private string userName;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Welcome);

            userName = Intent.Extras.GetString("userName");

            FindViews();
            HandleEvents();
        }

        private void FindViews()
        {
            welcomeTextView1 = FindViewById<TextView>(Resource.Id.textView1);
            welcomeTextView = FindViewById<TextView>(Resource.Id.welcomeText);
            coursesButton = FindViewById<Button>(Resource.Id.coursesButton);
            classViewButton = FindViewById<Button>(Resource.Id.classViewButton);
            settingsButton = FindViewById<Button>(Resource.Id.settingsButton);
            logOutButton = FindViewById<Button>(Resource.Id.logoutButton);
        }

        private void HandleEvents()
        {
            welcomeTextView.Text = $"Welcome {userName}!";
            coursesButton.Click += CoursesButton_Click;
            classViewButton.Click += ClassViewButton_Click;
            settingsButton.Click += SettingsButton_Click;
            logOutButton.Click += LogOutButton_Click;
        }

        private void LogOutButton_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent();
            intent.SetClass(this, typeof(MainActivity));
            StartActivity(intent);
        }

        private void SettingsButton_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent();
            intent.SetClass(this, typeof(ProfileActivity));
            intent.PutExtra("userName", userName);
            StartActivity(intent);
        }

        private void ClassViewButton_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent();
            intent.SetClass(this, typeof(ClassViewActivity));
            intent.PutExtra("userName", userName);
            StartActivity(intent);
        }

        private void CoursesButton_Click(object sender, System.EventArgs e)
        {
           var intent = new Intent();
            intent.SetClass(this, typeof(CoursesActivity));
            intent.PutExtra("userName", userName);
            StartActivity(intent);
        }
    }
}