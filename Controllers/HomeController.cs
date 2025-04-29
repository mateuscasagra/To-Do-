using Microsoft.AspNetCore.Mvc;
using Nova_pasta.Models;
using System.Text.Json;
using System.Collections.Generic;

namespace Nova_pasta.Controllers
{
    public class HomeController : Controller
    {

        private static List<TarefaModel.Tarefas> tarefas = new List<TarefaModel.Tarefas>();

        public IActionResult Index()
        {
            return View(tarefas);
        }

        [HttpPost]
        public IActionResult AdicionarTarefa(string tarefa)
        {
            if (!string.IsNullOrEmpty(tarefa))
            {
                tarefas.Add(new TarefaModel.Tarefas { Tarefa = tarefa });
                string jsonTarefas = JsonSerializer.Serialize(tarefas);
                Response.Cookies.Append("tarefas", jsonTarefas, new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(7)
                });

            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ConcluirTarefa(int indice)
        {
            if (indice >= 0 && indice < tarefas.Count)
            {
                tarefas.RemoveAt(indice);
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Pesquisa(string pesquisa)
        {
            if (!string.IsNullOrEmpty(pesquisa))
            {
                var resultado = tarefas.Where(t => t.Tarefa.Contains(pesquisa, StringComparison.OrdinalIgnoreCase)).ToList();
                return View("Index", resultado);
            }

            return RedirectToAction("Index");
        }
    }
}
