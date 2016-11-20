using Android.App;
using Android.OS;
using Android.Widget;
using Attendance.Adapters;
using Attendance.Core.Service;

namespace Attendance
{
    [Activity(Label = "Attendance")]
    public class CoursesActivity : Activity
    {
        private ListView courseListView;
        private string userName;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Courses);

            courseListView = FindViewById<ListView>(Resource.Id.coursesListView);

            userName = Intent.Extras.GetString("userName");

            var attendanceService = new AttendanceService();
            var coursesListResult = await attendanceService.GetCourses(userName);
            coursesListResult.ForEach(c=> c.UserName = userName);
            courseListView.Adapter = new CourseListAdapter(this, coursesListResult);

            courseListView.FindViewById<Button>(Resource.Id.checkInButton);
        }
    }
}