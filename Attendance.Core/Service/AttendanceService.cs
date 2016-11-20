using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Attendance.Core.Models;
using Newtonsoft.Json;

namespace Attendance.Core.Service
{
    public class AttendanceService
    {
        private const string AttendancePortalUrl = "http://attendanceportal.azurewebsites.net";

        public async Task<User> GetUser(string userName)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync($"{AttendancePortalUrl}/api/attendance/user?userName={userName}");
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    throw new InvalidOperationException("unable to get user");
                }

                var jsonString = result.Content.ReadAsStringAsync();
                jsonString.Wait();

                return JsonConvert.DeserializeObject<User>(jsonString.Result);
            }
        }
        public async Task<bool> SaveProfile(User user)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                var result = await client.PostAsync($"{AttendancePortalUrl}/api/attendance/user", content);
                return result.StatusCode == HttpStatusCode.OK;
            }
        }

        public async Task<Result> Login(LoginModel model)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                var result = await client.PostAsync($"{AttendancePortalUrl}/api/attendance/login", content);

                var jsonString = result.Content.ReadAsStringAsync();
                jsonString.Wait();

                return JsonConvert.DeserializeObject<Result>(jsonString.Result);
            }
        }

        public async Task<List<Course.Course>> GetCourses(string userName)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync($"{AttendancePortalUrl}/api/attendance/courses?userName={userName}");
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    throw new InvalidOperationException("unable to get courses");
                }

                var jsonString = result.Content.ReadAsStringAsync();
                jsonString.Wait();

                return JsonConvert.DeserializeObject<List<Course.Course>>(jsonString.Result);
            }
        }

        public async Task<Result> CheckInCourse(CheckInCourseModel model)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                var result = await client.PostAsync($"{AttendancePortalUrl}/api/attendance/CheckInCourse", content);

                var jsonString = result.Content.ReadAsStringAsync();
                jsonString.Wait();

                return JsonConvert.DeserializeObject<Result>(jsonString.Result);
            }
        }

        public async Task<List<ClassView.ClassView>> GetClassViewDetails(string userName, ClassViewRange range)
        {
            var model = new ClassViewDetailsRequest
            {
                UserName = userName,
                Range = range
            };

            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var result = await client.PostAsync($"{AttendancePortalUrl}/api/attendance/GetClassViewDetails", content);
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    throw new InvalidOperationException("unable to get class view details");
                }

                var jsonString = result.Content.ReadAsStringAsync();
                jsonString.Wait();

                return JsonConvert.DeserializeObject<List<ClassView.ClassView>>(jsonString.Result);
            }
        }

        public async Task<Result> DisputeCourse(int courseAttendanceId, string reason)
        {
            using (var client = new HttpClient())
            {
                var model = new DisputeCourseRequest
                {
                    DisputeReason = reason,
                    CourseAttendanceId = courseAttendanceId
                };

                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                var result = await client.PostAsync($"{AttendancePortalUrl}/api/attendance/DisputeCourse", content);

                var jsonString = result.Content.ReadAsStringAsync();
                jsonString.Wait();

                return JsonConvert.DeserializeObject<Result>(jsonString.Result);
            }
        }

    }
}
