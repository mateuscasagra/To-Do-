using Microsoft.AspNetCore.Mvc;
using Nova_pasta.Models;
using System.Collections.Generic; // <-- Importante!

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
                tarefas.Add(new TarefaModel.Tarefas { Tarefa = tarefa, Concluida = false });
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
    }
}
