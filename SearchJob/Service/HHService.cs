using Newtonsoft.Json;
using SearchJob.Dtos.Job;
using SearchJob.Interfaces;
using SearchJob.Models;
using SearchJob.Mappers;
using Microsoft.AspNetCore.Mvc;
namespace SearchJob.Service
{
    public class HHService : IHHService
    {
        private readonly HttpClient _httpClien;

        
        public HHService(HttpClient httpClien) 
        { 
            _httpClien = httpClien; 
        }


        public async Task<List<Job>> FindJobInHHAsync(int page)
        {
            
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.hh.ru/vacancies?page={page}&per_page=20");
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.63 Safari/537.36");
            
            using (var result = await _httpClien.SendAsync(request))
            {
                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    
                    var tasks = JsonConvert.DeserializeObject<HHJob.Root>(content);

                    
                    if (tasks != null)
                    {   
                        var jobs = tasks.items.Select(item => JobMapper.ToJobFromHH(item)).ToList();
                        return jobs;
                    }
                    return null;
                }
                else
                {
                    
                    var errorContent = await result.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {errorContent}");
                }

            }
            return null;
        }
    }
}
