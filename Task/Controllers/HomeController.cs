using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbFirstModel _context;

        public HomeController()
        {
            _context = new DbFirstModel();
        }

        public IActionResult Index()
        {
            var query = from b in _context.Sentences
                        select b;
            return View(query.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> Index(IList<IFormFile> files, string keyword)
        {
            foreach (var file in files)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                if (fileName.EndsWith(".txt"))
                {
                    var result = string.Empty;
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        result = reader.ReadToEnd();
                        string[] input = result.Split('.');
                        for(int i = 0; i < input.Length; i++)
                        {
                            if (input[i].ToLower().Contains(keyword.ToLower()))
                            {
                                int numberOfOccurances = 0;
                                string[] input1 = input[i].Split(' ');
                                for(int j = 0; j < input1.Length; j++)
                                {
                                    if(input1[j].ToLower() == keyword.ToLower())
                                    {
                                        numberOfOccurances++;
                                    }
                                }
                                _context.Add(new InputSentences {Sentence = input[i], NumberOfOccurrences = numberOfOccurances});
                                _context.SaveChanges();
                            }
                        }
                    }
                }
             }

            return RedirectToAction("Index");
       }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
