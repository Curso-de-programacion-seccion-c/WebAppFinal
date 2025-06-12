using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebAppFinal.Controllers
{
    public class CategoriaController : Controller
    {

        private readonly string _connectionApi = ConfigurationManager.AppSettings["ConnectionApi"];
        private readonly HttpClient _httpClient = new HttpClient();

        public CategoriaController()
        {
            _httpClient.BaseAddress = new Uri(_connectionApi);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: Categoria
        public async Task<ActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Categoria");
                if (response.IsSuccessStatusCode)
                {
                    var model = await response.Content.ReadAsStringAsync();
                    var categorias = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.CategoriaModel>>(model);

                    return View(categorias);
                }
                else
                {
                    ModelState.AddModelError("", "Error al obtener las categorías.");
                    return View(new List<WebAppFinal.Models.CategoriaModel>());
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Excepción: {ex.Message}");
                return View(new List<WebAppFinal.Models.CategoriaModel>());
            }
        }
    }
}