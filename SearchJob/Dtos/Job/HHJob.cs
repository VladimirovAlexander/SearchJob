using static System.Net.Mime.MediaTypeNames;
using System.Net;
using Newtonsoft.Json;

namespace SearchJob.Dtos.Job
{
    public class HHJob
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Address
        {
            public string city { get; set; }
            public string street { get; set; }
            public string building { get; set; }
            public double lat { get; set; }
            public double lng { get; set; }
            public object description { get; set; }
            public string raw { get; set; }
            public Metro metro { get; set; }
            public List<MetroStation> metro_stations { get; set; }
            public string id { get; set; }
        }

        public class Area
        {
            public string id { get; set; }
            public string name { get; set; }
            public string url { get; set; }
        }

        public class Employer
        {
            public string id { get; set; }
            public string name { get; set; }
            public string url { get; set; }
            public string alternate_url { get; set; }
            public object logo_urls { get; set; }
            public string vacancies_url { get; set; }
            public bool accredited_it_employer { get; set; }
            public bool trusted { get; set; }
        }

        public class Employment
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Experience
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Item
        {
            public int id { get; set; }
            public bool premium { get; set; }
            public string name { get; set; }
            public object department { get; set; }
            public bool has_test { get; set; }
            public bool response_letter_required { get; set; }
            public Area area { get; set; }
            public Salary salary { get; set; }
            public Type type { get; set; }
            public Address address { get; set; }
            public object response_url { get; set; }
            public object sort_point_distance { get; set; }
            public DateTime published_at { get; set; }
            public DateTime created_at { get; set; }
            public bool archived { get; set; }
            public string apply_alternate_url { get; set; }
            public object insider_interview { get; set; }
            public string url { get; set; }
            public string alternate_url { get; set; }
            public List<object> relations { get; set; }
            public Employer employer { get; set; }
            public Snippet snippet { get; set; }
            public object contacts { get; set; }
            public Schedule schedule { get; set; }
            public List<object> working_days { get; set; }
            public List<object> working_time_intervals { get; set; }
            public List<object> working_time_modes { get; set; }
            public bool accept_temporary { get; set; }
            public List<ProfessionalRole> professional_roles { get; set; }
            public bool accept_incomplete_resumes { get; set; }
            public Experience experience { get; set; }
            public Employment employment { get; set; }
            public object adv_response_url { get; set; }
            public bool is_adv_vacancy { get; set; }
            public object adv_context { get; set; }
        }

        public class Metro
        {
            public string station_name { get; set; }
            public string line_name { get; set; }
            public string station_id { get; set; }
            public string line_id { get; set; }
            public double lat { get; set; }
            public double lng { get; set; }
        }

        public class MetroStation
        {
            public string station_name { get; set; }
            public string line_name { get; set; }
            public string station_id { get; set; }
            public string line_id { get; set; }
            public double lat { get; set; }
            public double lng { get; set; }
        }

        public class ProfessionalRole
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Root
        {

            public List<Item> items { get; set; }
            public int found { get; set; }
            public int pages { get; set; }
            public int page { get; set; }
            public int per_page { get; set; }
            public object clusters { get; set; }
            public object arguments { get; set; }
            public object fixes { get; set; }
            public object suggests { get; set; }
            public string alternate_url { get; set; }
        }

        public class Salary
        {
            public int? from { get; set; }
            public object? to { get; set; }
            public string currency { get; set; }
            public bool gross { get; set; }
        }

        public class Schedule
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Snippet
        {
            public string requirement { get; set; }
            public string responsibility { get; set; }
        }

        public class Type
        {
            public string id { get; set; }
            public string name { get; set; }
        }


    }
}
