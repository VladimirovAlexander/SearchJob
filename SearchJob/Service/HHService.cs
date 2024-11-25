using Newtonsoft.Json;
using SearchJob.Dtos.Job;
using SearchJob.Interfaces;
using SearchJob.Models;
using SearchJob.Mappers;
using Microsoft.AspNetCore.Mvc;
using SearchJob.Repository;
using System.Net.Http;
using SearchJob.Dtos.Job.HHJob;
namespace SearchJob.Service
{
    public class HHService : IHHService
    {
        private readonly HttpClient _httpClient;
        private readonly IJobRepository _repository;
        private readonly IConfiguration _configuration;
        
        public HHService(HttpClient httpClient, IJobRepository repository, IConfiguration configuration ) 
        { 
            _httpClient = httpClient; 
            _repository = repository;
            _configuration = configuration;
        }


        public async Task<List<Job>> FindJobInHHAsync(int page)
        {
            var getString = _configuration["ApiHH:Get20Item"].Replace("Intpage", page.ToString());

            var request = new HttpRequestMessage(HttpMethod.Get, getString);
            request.Headers.Add("User-Agent", "Mozilla/5.0");

            using (var result = await _httpClient.SendAsync(request))
            {
                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var tasks = JsonConvert.DeserializeObject<Root>(content);

                    if (tasks != null && tasks.items != null)
                    {
                        var jobs = tasks.items.Select(item => JobMapper.ToJobFromHH(item)).ToList();

                       
                        foreach (var job in jobs)
                        {
                            var jobExists = (await _repository.GetAsync()).Any(x => x.Id == job.Id);
                            if (!jobExists)
                            {
                                await _repository.CreateAsync(job);
                            }
                        }

                        return jobs;
                    }
                }
                else
                {
                    var errorContent = await result.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {errorContent}");
                }
            }
            return new List<Job>(); // Возвращаем пустой список, если не удалось загрузить вакансии
        }
    }
}
