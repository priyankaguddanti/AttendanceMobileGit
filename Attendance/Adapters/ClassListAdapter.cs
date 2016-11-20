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
using Attendance.Core.ClassView;
using Attendance.Core.Course;
using Attendance.Core.Models;
using Attendance.Core.Service;

namespace Attendance.Adapters
{
    public class ClassListAdapter : BaseAdapter<ClassView>
    {
        List<ClassView> classViewItems;
        Activity context;
        private AttendanceService _attendanceService = new AttendanceService();

        public ClassListAdapter(Activity context, List<ClassView> classViewItems) : base()
        {
            this.classViewItems = classViewItems;
            this.context = context;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override ClassView this[int position]
        {
            get { return classViewItems[position]; }
        }

        public override int Count
        {
            get { return classViewItems.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = classViewItems[position];

            if (convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(Resource.Layout.ClassViewRow, null);
            }

            convertView.FindViewById<TextView>(Resource.Id.courseTitleTextView).Text = item.CourseTitle;
            convertView.FindViewById<TextView>(Resource.Id.courseDateTextView).Text = item.CourseAttendedDate;
            convertView.FindViewById<TextView>(Resource.Id.courseStatusTextView).Text = item.Status;

            var disputeButton = convertView.FindViewById<Button>(Resource.Id.disputeButton);
            disputeButton.Visibility = item.CanBeDisputed ? ViewStates.Visible : ViewStates.Invisible;
            if (item.CanBeDisputed)
            {
                disputeButton.Click += (sender, args) =>
                {
                    var currentItem = classViewItems[position];

                    var alert = new AlertDialog.Builder(context);
                    alert.SetTitle("Dispute Reason");
                    alert.SetView(Resource.Layout.DisputeReason);
                    alert.SetPositiveButton("Ok", async (senderAlert, args1) =>
                    {
                        var disputeDialog = (AlertDialog) senderAlert;
                        var disputeReason = disputeDialog.FindViewById<EditText>(Resource.Id.disputeReasonEditText);
                        var disputedResult =
                            await _attendanceService.DisputeCourse(currentItem.CourseAttendanceId, disputeReason.Text);

                        var disputeResultMessage = disputedResult.HasError
                            ? "Dispute submission failed"
                            : "Dispute submitted";
                        Toast.MakeText(this.context, disputeResultMessage, ToastLength.Long).Show();

                        if (!disputedResult.HasError)
                        {
                            currentItem.CanBeDisputed = false;
                            currentItem.Status = AttendanceStatus.Pending.ToString();
                            this.NotifyDataSetInvalidated();
                        }
                    });
                    Dialog dialog = alert.Create();
                    dialog.Show();
                };
            }
            return convertView;
        }
    }
}