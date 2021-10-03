using GuestBookFront.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GuestBookFront.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IList<GuestBookEntry> GuestBookEnties => _guestBookList;
        private List<GuestBookEntry> _guestBookList = new List<GuestBookEntry>();

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            await GetEntriesListAsync();
        }

        public async Task OnPost()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://20.93.189.154:80/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            string PostUrl = "guestbook/entries";

            GuestBookEntry guestBookEntry = new GuestBookEntry();
            guestBookEntry.Title = Request.Form["title"];
            guestBookEntry.Comment = Request.Form["comment"];
            guestBookEntry.Name = Request.Form["name"];
            guestBookEntry.Time = DateTime.UtcNow;

            HttpResponseMessage response = await client.PostAsJsonAsync(
                PostUrl, guestBookEntry);
            response.EnsureSuccessStatusCode();

            await OnGetAsync();
        }

        public async Task GetEntriesListAsync()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://20.93.189.154:80/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            List<GuestBookEntry> guestBookEntries = null;
            HttpResponseMessage response = await client.GetAsync("guestbook/entries");
            if (response.IsSuccessStatusCode)
            {
                string rawResult = await response.Content.ReadAsStringAsync();
                guestBookEntries = ((JArray)(JObject.Parse(rawResult)["entries"])).ToObject<List<GuestBookEntry>>();
            }
            _guestBookList = guestBookEntries.OrderByDescending(x => x.Time).ToList<GuestBookEntry>();
        }
    }
}
