using NUnit.Framework;
using Microsoft.Extensions.Caching.Memory;
using NewsAPI.Controllers;
using System.Threading.Tasks;
using System;
using NewsAPI.Models;
using System.Collections.Generic;

namespace NewsTests
{
    public class Tests
    {
        private readonly StoriesController ts = new(new MemoryCache(new MemoryCacheOptions()));

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task TestStory()
        {
            var rand = new Random();

            for (int x = 0; x < 500; x++)
            {
                int randId = rand.Next(1, 25265033);
                var randStory = await ts.Get(randId);
                Assert.IsInstanceOf<Story>(randStory);
            }
        }
        [Test]
        public async Task TestTopStories()
        {
            var topStories = await ts.GetAll();
            Assert.IsInstanceOf<IEnumerable<Story>>(topStories);
            foreach(Story story in topStories)
            {
                Assert.AreNotEqual(story, null);
            }
        }
    }
}