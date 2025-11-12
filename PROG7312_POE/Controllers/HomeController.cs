using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PROG7312_POE.Models;
using System.Diagnostics;
using static PROG7312_POE.Models.DataStructures;

namespace PROG7312_POE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // partone feedbac
        private static CustomBuiltLinkedList _reports = new CustomBuiltLinkedList();
        //patr two
        private static SortedDictionary<DateTime, List<Event>> events = new SortedDictionary<DateTime, List<Event>>();
        private static HashSet<string> eventCategories = new HashSet<string>();
        private static Stack<string> recentSearches = new Stack<string>();
        //part three
        private static DataStructures.BST bst = new DataStructures.BST();
        private static DataStructures.AVLTree avl = new DataStructures.AVLTree();
        private static DataStructures.MinHeap heap = new DataStructures.MinHeap();
        private static DataStructures.Graph graph = new DataStructures.Graph();
        private static int _nextReportId = 1;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        static HomeController()
        {
            var MunicipalityEvents = new List<Event>
    {
                //USed gpt to fill out the rest of the entries for each cat
                // Entertainment
        new Event { Title = "Music Festival", Category = "Entertainment", Date = DateTime.Today.AddDays(1), Description = "Local band performances" },
        new Event { Title = "Comedy Night", Category = "Entertainment", Date = DateTime.Today.AddDays(2), Description = "Stand-up comedy show" },
        new Event { Title = "Movie Marathon", Category = "Entertainment", Date = DateTime.Today.AddDays(3), Description = "Outdoor cinema under the stars" },
        new Event { Title = "Art Expo", Category = "Entertainment", Date = DateTime.Today.AddDays(4), Description = "Local artists showcase" },
        new Event { Title = "Jazz Night", Category = "Entertainment", Date = DateTime.Today.AddDays(5), Description = "Smooth jazz evening" },
        new Event { Title = "Dance Performance", Category = "Entertainment", Date = DateTime.Today.AddDays(6), Description = "Ballet and modern dance" },
        new Event { Title = "Theater Play", Category = "Entertainment", Date = DateTime.Today.AddDays(7), Description = "Community theater performance" },
        new Event { Title = "Magic Show", Category = "Entertainment", Date = DateTime.Today.AddDays(8), Description = "Illusions and tricks" },
        new Event { Title = "Karaoke Night", Category = "Entertainment", Date = DateTime.Today.AddDays(9), Description = "Sing your favorite songs" },
        new Event { Title = "Food & Music Festival", Category = "Entertainment", Date = DateTime.Today.AddDays(10), Description = "Live music and food trucks" },

                // Market
        new Event { Title = "Farmers Market", Category = "Market", Date = DateTime.Today.AddDays(1), Description = "Fresh produce from local farmers" },
        new Event { Title = "Craft Fair", Category = "Market", Date = DateTime.Today.AddDays(2), Description = "Handmade items by artisans" },
        new Event { Title = "Book Sale", Category = "Market", Date = DateTime.Today.AddDays(3), Description = "Second-hand books and rare finds" },
        new Event { Title = "Flea Market", Category = "Market", Date = DateTime.Today.AddDays(4), Description = "Unique vintage items" },
        new Event { Title = "Flower Market", Category = "Market", Date = DateTime.Today.AddDays(5), Description = "Fresh flowers and plants" },
        new Event { Title = "Holiday Market", Category = "Market", Date = DateTime.Today.AddDays(6), Description = "Seasonal gifts and crafts" },
        new Event { Title = "Farm-to-Table Market", Category = "Market", Date = DateTime.Today.AddDays(7), Description = "Organic local produce" },
        new Event { Title = "Artisan Food Fair", Category = "Market", Date = DateTime.Today.AddDays(8), Description = "Cheese, bread, and jams" },
        new Event { Title = "Vintage Market", Category = "Market", Date = DateTime.Today.AddDays(9), Description = "Clothing and accessories" },
        new Event { Title = "Street Food Market", Category = "Market", Date = DateTime.Today.AddDays(10), Description = "International cuisine stalls" },

                // Community
        new Event { Title = "Town Hall Meeting", Category = "Community", Date = DateTime.Today.AddDays(1), Description = "Discussion on local issues" },
        new Event { Title = "Neighborhood Cleanup", Category = "Community", Date = DateTime.Today.AddDays(2), Description = "Volunteer to clean local streets" },
        new Event { Title = "Charity Drive", Category = "Community", Date = DateTime.Today.AddDays(3), Description = "Donate clothes and essentials" },
        new Event { Title = "Community BBQ", Category = "Community", Date = DateTime.Today.AddDays(4), Description = "Meet your neighbors" },
        new Event { Title = "Pet Adoption Fair", Category = "Community", Date = DateTime.Today.AddDays(5), Description = "Find a new furry friend" },
        new Event { Title = "Cultural Festival", Category = "Community", Date = DateTime.Today.AddDays(6), Description = "Celebrate diverse cultures" },
        new Event { Title = "Volunteer Fair", Category = "Community", Date = DateTime.Today.AddDays(7), Description = "Sign up for local charities" },
        new Event { Title = "Community Sports Day", Category = "Community", Date = DateTime.Today.AddDays(8), Description = "Fun games for all ages" },
        new Event { Title = "Senior Social", Category = "Community", Date = DateTime.Today.AddDays(9), Description = "Activities for senior citizens" },
        new Event { Title = "Local History Tour", Category = "Community", Date = DateTime.Today.AddDays(10), Description = "Discover local landmarks" },

                // Sports
        new Event { Title = "Local Soccer Match", Category = "Sports", Date = DateTime.Today.AddDays(1), Description = "Community soccer tournament" },
        new Event { Title = "Basketball Championship", Category = "Sports", Date = DateTime.Today.AddDays(2), Description = "High school teams compete" },
        new Event { Title = "Marathon", Category = "Sports", Date = DateTime.Today.AddDays(3), Description = "5k and 10k runs" },
        new Event { Title = "Tennis Open", Category = "Sports", Date = DateTime.Today.AddDays(4), Description = "Singles and doubles matches" },
        new Event { Title = "Swimming Gala", Category = "Sports", Date = DateTime.Today.AddDays(5), Description = "Competitive swimming event" },
        new Event { Title = "Cycling Race", Category = "Sports", Date = DateTime.Today.AddDays(6), Description = "Local cycling competition" },
        new Event { Title = "Yoga Workshop", Category = "Sports", Date = DateTime.Today.AddDays(7), Description = "Outdoor yoga session" },
        new Event { Title = "Football Training Camp", Category = "Sports", Date = DateTime.Today.AddDays(8), Description = "Skills and drills" },
        new Event { Title = "Golf Tournament", Category = "Sports", Date = DateTime.Today.AddDays(9), Description = "Amateur golf competition" },
        new Event { Title = "Karate Championship", Category = "Sports", Date = DateTime.Today.AddDays(10), Description = "Martial arts matches" },

                // Education
        new Event { Title = "Coding Workshop", Category = "Education", Date = DateTime.Today.AddDays(1), Description = "Learn basics of C#" },
        new Event { Title = "Math Seminar", Category = "Education", Date = DateTime.Today.AddDays(2), Description = "Advanced problem-solving techniques" },
        new Event { Title = "Science Fair", Category = "Education", Date = DateTime.Today.AddDays(3), Description = "Students present projects" },
        new Event { Title = "History Lecture", Category = "Education", Date = DateTime.Today.AddDays(4), Description = "Local historical events" },
        new Event { Title = "Language Workshop", Category = "Education", Date = DateTime.Today.AddDays(5), Description = "Improve communication skills" },
        new Event { Title = "Art Class", Category = "Education", Date = DateTime.Today.AddDays(6), Description = "Painting and drawing session" },
        new Event { Title = "Robotics Workshop", Category = "Education", Date = DateTime.Today.AddDays(7), Description = "Learn basic robotics" },
        new Event { Title = "Photography Class", Category = "Education", Date = DateTime.Today.AddDays(8), Description = "Capture amazing photos" },
        new Event { Title = "Environmental Talk", Category = "Education", Date = DateTime.Today.AddDays(9), Description = "Sustainable living tips" },
        new Event { Title = "Book Club Meeting", Category = "Education", Date = DateTime.Today.AddDays(10), Description = "Discuss popular books" },

                // Health
        new Event { Title = "Free Health Checkup", Category = "Health", Date = DateTime.Today.AddDays(1), Description = "Blood pressure and sugar testing" },
        new Event { Title = "Yoga for Beginners", Category = "Health", Date = DateTime.Today.AddDays(2), Description = "Gentle yoga session" },
        new Event { Title = "Mental Health Talk", Category = "Health", Date = DateTime.Today.AddDays(3), Description = "Stress management tips" },
        new Event { Title = "Nutrition Workshop", Category = "Health", Date = DateTime.Today.AddDays(4), Description = "Healthy eating habits" },
        new Event { Title = "Zumba Class", Category = "Health", Date = DateTime.Today.AddDays(5), Description = "Fun dance workout" },
        new Event { Title = "Meditation Session", Category = "Health", Date = DateTime.Today.AddDays(6), Description = "Relax and focus" },
        new Event { Title = "First Aid Training", Category = "Health", Date = DateTime.Today.AddDays(7), Description = "Learn essential skills" },
        new Event { Title = "Community Walk", Category = "Health", Date = DateTime.Today.AddDays(8), Description = "Walk for fitness" },
        new Event { Title = "Blood Donation Camp", Category = "Health", Date = DateTime.Today.AddDays(9), Description = "Donate blood and save lives" },
        new Event { Title = "Cycling Club Meetup", Category = "Health", Date = DateTime.Today.AddDays(10), Description = "Group cycling activity" }
    };

            foreach (var muniEvent in MunicipalityEvents)
            {
                if (!events.ContainsKey(muniEvent.Date))
                    events[muniEvent.Date] = new List<Event>();
                events[muniEvent.Date].Add(muniEvent);
                eventCategories.Add(muniEvent.Category);
            }
        }


        public IActionResult LocalEvents()
        {
            ViewData["Title"] = "Local Events and Announcements";

            var allEvents = events.SelectMany(kvp => kvp.Value).OrderBy(e => e.Date).ToList();

            ViewData["AllEvents"] = allEvents;
            ViewData["Categories"] = eventCategories;
            ViewData["RecentSearches"] = recentSearches.Reverse().Take(3).ToList();
            ViewData["Events"] = events;


            return View("LocalEventsandAnnouncements");
        }

        [HttpPost]
        public IActionResult SearchEvents(string category, DateTime? date)
        {
            var results = events.SelectMany(kvp => kvp.Value)
                                .Where(e => (string.IsNullOrEmpty(category) || e.Category == category) && (!date.HasValue || e.Date == date.Value))
                                .OrderBy(e => e.Date)
                                .ToList();

            if (!string.IsNullOrEmpty(category))
                recentSearches.Push(category);

            ViewData["SearchResults"] = results;
            ViewData["Categories"] = eventCategories;
            ViewData["RecentSearches"] = recentSearches.Reverse().Take(3).ToList();
            ViewData["Events"] = events;

            return View("LocalEventsandAnnouncements");
        }

        public IActionResult ServiceRequests()
        {
            ViewData["Title"] = "Service Requests Status";

            var allReports = _reports.ToList(); 

            ViewData["Reports"] = allReports;
            ViewData["SearchId"] = 0;           
            ViewData["SearchResult"] = null;    

            return View("ServiceRequestsStatus");
        }

        [HttpPost]
        public IActionResult ServiceRequests(string searchId)
        {
            int.TryParse(searchId, out int id);

            var allReports = _reports.ToList();

            Report searchResult = null;

            if (id > 0)
            {
                searchResult = allReports.FirstOrDefault(r => r.ReportId == id);
            }
            else
            {
                searchResult = null; 
            }

            ViewData["Reports"] = allReports;
            ViewData["SearchId"] = searchId;
            ViewData["SearchResult"] = searchResult;

            return View("ServiceRequestsStatus");
        }

        [HttpPost]
        public IActionResult ViewStructure(string structure)
        {
            var allReports = _reports.ToList();
            List<Report> sortedReports = new List<Report>();

            switch (structure)
            {
                case "BST":
                    // sortes by street number
                    bst.InOrderTraversal(bst.Root, sortedReports);
                    break;

                case "AVL":
                    // sorts by category, in alphabetucal order
                    avl.InOrderTraversal(avl.Root, sortedReports);
                    sortedReports = sortedReports.OrderBy(r => r.ReportCategory).ToList();
                    break;

                case "Heap":
                    // shortest address first
                    sortedReports = heap.ToList();
                    sortedReports.Sort((a, b) => a.StreetAddress.Length.CompareTo(b.StreetAddress.Length));
                    break;

                case "Graph":
                    // FILO, displays all issues reported from most recent to oldest
                    sortedReports = allReports
                        .OrderByDescending(r => r.ReportId)
                        .ToList();
                    break;
            }

            ViewData["Reports"] = sortedReports;
            ViewData["StructureView"] = structure; 
            ViewData["SearchId"] = "0";
            ViewData["SearchResult"] = null;

            return View("ServiceRequestsStatus");
        }

        [HttpPost]
        public IActionResult ReportIssues(Report report, IFormFile? Attachment)
        {
            if (ModelState.IsValid)
            {
                // Handle file attachment
                if (Attachment != null && Attachment.Length > 0)
                {
                    var fileName = Path.GetFileName(Attachment.FileName);
                    var filePath = Path.Combine("wwwroot/uploads", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        Attachment.CopyTo(stream);
                    }

                    report.ReportDocument = "/uploads/" + fileName;
                }

                report.ReportId = _nextReportId++;
                _reports.Add(report);
                
                // part three
                bst.Insert(report);        // bst
                avl.Insert(report);        // avl
                heap.Insert(report);       // heap
                graph.AddVertex(report.ReportId); // Graph 

                if (_nextReportId > 2) 
                {
                    int prevReportId = _nextReportId - 2;
                    graph.AddEdge(prevReportId, report.ReportId);
                }

                int reportCount = HttpContext.Session.GetInt32("ReportCount") ?? 0;
                reportCount++;
                HttpContext.Session.SetInt32("ReportCount", reportCount);

                string rank;
                if (reportCount >= 10)
                    rank = "Legendary Leopard";
                else if (reportCount >= 7)
                    rank = "Bold Buffalo";
                else if (reportCount >= 4)
                    rank = "Loyal Lion";
                else if (reportCount >= 2)
                    rank = "Eager Elephant";
                else
                    rank = "Rusty Rhino";

                TempData["UserRank"] = rank;

                return RedirectToAction("ReportSuccess");
            }

            return View(report);
        }

        public IActionResult ReportIssues()
        {
            ViewData["Title"] = "Report an Issue";
            return View();
        }

        public IActionResult ReportSuccess()
        {
            ViewData["Title"] = "Report Submitted";
            return View();
        }

        //seemed redundant
        //public IActionResult ViewReports()
        //{
        //    ViewData["Title"] = "All Submitted Reports";
        //    return View(_reports.ToList());
        //}

        public IActionResult Index()
        {
            int reportCount = HttpContext.Session.GetInt32("ReportCount") ?? 0;
            string userRank;

            if (reportCount >= 10)
                userRank = "Legendary Leopard";
            else if (reportCount >= 7)
                userRank = "Bold Buffalo";
            else if (reportCount >= 4)
                userRank = "Loyal Lion";
            else if (reportCount >= 2)
                userRank = "Eager Elephant";
            else
                userRank = "Rusty Rhino";

            ViewData["UserRank"] = userRank;
            ViewData["ReportCount"] = reportCount;

            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}