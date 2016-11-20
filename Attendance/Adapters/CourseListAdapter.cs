using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using Attendance.Core.Course;
using Attendance.Core.Models;
using Attendance.Core.Service;

namespace Attendance.Adapters
{
    public class CourseListAdapter : BaseAdapter<Course>
    {
        List<Course> courses;
        Activity context;
        private AttendanceService _service = new AttendanceService();

        public CourseListAdapter(Activity context, List<Course> courses) : base()
        {
            this.courses = courses;
            this.context = context;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Course this[int position]
        {
            get { return courses[position]; }
        }

        public override int Count
        {
            get { return courses.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = courses[position];

            if (convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(Resource.Layout.CourseRowView, null);
            }

            var checkInButton = convertView.FindViewById<Button>(Resource.Id.checkInButton);
            convertView.FindViewById<TextView>(Resource.Id.checkInTimeTextView).Text = item.CheckInTime;
            convertView.FindViewById<TextView>(Resource.Id.courseTextView).Text = item.CourseTitle;
            checkInButton.Enabled = item.IsEligibleForCheckIn;

            if (item.AlreadyCheckedIn)
            {
                checkInButton.Text = "Checked";
            }

            checkInButton.Click += async (sender, args) =>
            {
                var currentItem = courses[position];
                var result = await _service.CheckInCourse(new CheckInCourseModel
                {
                    UserName = currentItem.UserName,
                    CourseId = currentItem.CourseId
                });

                if (!result.HasError)
                {

                    checkInButton.Text = "Checked";
                    checkInButton.Enabled = false;
                }

                var message = result.HasError ? "Unable to check in, please try again" : "Course check in is succesfull";

                var alert = new AlertDialog.Builder(context);
                alert.SetTitle("Check In");
                alert.SetMessage(message);
                alert.SetPositiveButton("Ok", (senderAlert, args1) =>
                {
                });
                Dialog dialog = alert.Create();
                dialog.Show();
            };
            return convertView;
        }
    }
}