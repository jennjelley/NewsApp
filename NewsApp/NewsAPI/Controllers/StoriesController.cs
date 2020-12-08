using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NewsAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NewsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoriesController : Controller
    {
        private readonly string baseURL = "https://hacker-news.firebaseio.com/v0";
        private readonly HttpClient client = new();
        private readonly IMemoryCache _cache;

        public StoriesController(IMemoryCache cache)
        {
            _cache = cache;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IEnumerable<Story>> GetAll()
        {
            var response = await client.GetAsync(baseURL + "/newstories.json");
            IEnumerable<int> topStoriesIds = Array.Empty<int>();
            List<Story> topStories = new();

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                topStoriesIds = JsonConvert.DeserializeObject<IEnumerable<int>>(responseString);
            }

            foreach (int x in topStoriesIds)
            {
                if (!_cache.TryGetValue(x, out Story story))
                {
                    _cache.Set(x, await Get(x));
                    if (story != null) {
                        topStories.Add(story);
                    }
                }
                if(_cache.Get(x) != null){
                    topStories.Add(_cache.Get(x) as Story);
                }
                
            }
            return topStories;
        }

        [HttpGet("page")]
        public async Task<IEnumerable<Story>> GetPaginated([FromQuery] int page)
        {
            var topStories = await GetAll();
            var pageSize = 50;
            return topStories.Skip((page - 1) * pageSize).Take(pageSize);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<Story> Get(int id)
        {
            var response = await client.GetAsync(baseURL + "/item/" + id + ".json");
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Story>(responseString);
            }
            return null;
        }

        [HttpGet("search")]
        public async Task<IEnumerable<Story>> Get(string searchTerm)
        {
            var topStories = await GetAll();
            IEnumerable<Story> found = Array.Empty<Story>();

            foreach(Story story in topStories)
            {
                if (story.Title.ToLower().Contains(searchTerm.ToLower()))
                {
                    found = found.Append(story);
                }
            }

            return found;
        }
    }
}
