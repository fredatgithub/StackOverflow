using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace temp
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<TweetDto> 
            {
                new TweetDto
                {
                    screen_name = "nolan_dorris",
                    text = "Some text"
                },
                new TweetDto
                {
                    screen_name = "nolan_dorris",
                    text = "Some other text"
                },
                new TweetDto
                {
                    screen_name = "imogene_kovacek",
                    text = "Some text"
                }
            };

            var mapped = list
                .GroupBy(dto => dto.screen_name)
                .Select(group => new Dictionary<string, List<TweetDto>> 
                {
                    { group.Key, group.ToList() }
                });

            var json = JsonConvert.SerializeObject(mapped, Formatting.Indented);

            Console.WriteLine(json);
        }
    }

    public class TweetDto
    {
        public string screen_name { get; set; }
        public string text { get; set; }
    }
}