using Newtonsoft.Json;
using SearchJob.Dtos.Job;
using SearchJob.Interfaces;
using SearchJob.Models;
using SearchJob.Interfaces.Mappers;
namespace SearchJob.Service
{
    public class HHService : IHHService
    {
        private readonly HttpClient _httpClien;

        
        public HHService(HttpClient httpClien) 
        { 
            _httpClien = httpClien; 
        }


        public async Task<Job> FindJobByTitleAsync(string title)
        { 
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.hh.ru/vacancies?text={title}&per_page=1");
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.63 Safari/537.36");
            
            using (var result = await _httpClien.SendAsync(request))
            {
                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();

                    var tasks = JsonConvert.DeserializeObject<HHJob.Root>(content);

                    
                    if (tasks != null)
                    {
                        var firstJob = tasks.items.First();
                        return firstJob.ToJobFromHH();
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
