using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CadastroProdutos.Domain.Interfaces.Service;
using CadastroProdutos.Domain.ValueObjects;
using Newtonsoft.Json;

namespace CadastroProdutos.Infra.Data.Repository
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<double> ObterPrecoVenda(double precoCusto, string categoria)
        {
            var response = await _httpClient.GetAsync(string.Format("https://localhost:44389/api/v2/Categoria/calcular-preco?categoria={0}&preco={1:n2}", categoria, precoCusto));
            var json = await response.Content.ReadAsStringAsync();
            ResponseCalculoVenda cat = JsonConvert.DeserializeObject<ResponseCalculoVenda>(json);

            if(cat.success == true)
            return Convert.ToDouble(cat.data);
            else
            return 0;
        }
    }
}
