using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ProjetoSara.Pages
{
    public class UsuariosModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public UsuariosModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public List<UsuarioDTO> Usuarios { get; set; } = new();

        public async Task OnGetAsync()
        {
            // URL da sua API local (ajuste conforme necessário)
            var url = "https://localhost:5203/api/usuarios";

            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                // Como seu endpoint retorna paginação, pegamos o conteúdo direto para simplicidade
                var pageResponse = await response.Content.ReadFromJsonAsync<PagedResult<UsuarioDTO>>();
                Usuarios = pageResponse?.Items ?? new List<UsuarioDTO>();
            }
        }
    }

    // DTO deve bater com o que seu backend retorna
    public class UsuarioDTO
    {
        public long Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
    }

    // Classe para "desembrulhar" a resposta paginada do backend
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalPages { get; set; }
        public int TotalElements { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}