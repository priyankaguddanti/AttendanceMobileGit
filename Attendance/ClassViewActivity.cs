using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Attendance.Adapters;
using Attendance.Core.ClassView;
using Attendance.Core.Models;
using Attendance.Core.Service;

namespace Attendance
{
    [Activity(Label = "Attendance")]
    public class ClassViewActivity : Activity
    {
        private string userName;
        private ListView classListView;
        private AttendanceService _attendanceService = new AttendanceService();
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ClassView);

            userName = Intent.Extras.GetString("userName");

            Spinner spinner = FindViewById<Spinner>(Resource.Id.classViewSpinner);

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.classview_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            var classViewList = await _attendanceService.GetClassViewDetails(userName, ClassViewRange.ThreeDays);
            
            classListView = FindViewById<ListView>(Resource.Id.classListView);
            classListView.Adapter = new ClassListAdapter(this, classViewList);
        }

        protected async void spinner_ItemSelected(object s, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner) s;

            var classRangeSelected = spinner.GetItemAtPosition(e.Position).ToString();
            var classRange = GetClassViewRange(classRangeSelected);
            var classViewList = await _attendanceService.GetClassViewDetails(userName, classRange);
            classListView.Adapter = new ClassListAdapter(this, classViewList);
        }

        private ClassViewRange GetClassViewRange(string range)
        {
            switch (range)
            {
                case "Past month":
                    return ClassViewRange.OneMonth;
                case "Past 2 weeks":
                    return ClassViewRange.TwoWeeks;
                case "Past 7 days":
                    return ClassViewRange.SevenDays;
                default:
                    return ClassViewRange.ThreeDays;
            }
        }
    }
}