using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teste.Dev.Application;
using Teste.Dev.Domain.Constante;
using Teste.Dev.Domain.Dto;
using Teste.Dev.Domain.Entidade;
using Teste.Dev.Domain.IApplication;
using Teste.Dev.Domain.Model;

namespace Teste.Dev.Controllers
{
    public class EmpresaController : Controller
    {
        public IFornecedorApplication _fornecedorApp { get; set; }
        public IEmpresaApplication _empresaApp { get; set; }

        public EmpresaController()
        {
            _fornecedorApp = new FornecedorApplication();
            _empresaApp = new EmpresaApplication();
        }

        public ActionResult Index()
        {
            try
            {
                var modelo = new EmpresaLista();
                var retornoBusca = _empresaApp.BuscarTodos().Result.Objeto;

                if (retornoBusca.Count > 0)
                {
                    modelo.EmpresaList = retornoBusca;
                    modelo.EmpresaList = modelo.EmpresaList.OrderByDescending(x => x.Codigo).ToList();
                }
                else
                {
                    modelo.EmpresaList = new List<Empresa>();
                }

                return View("ConsultaEmpresaView", modelo);
            }
            catch (Exception ex)
            {
                return View("ErrorHandler", new ErrorHandler() { Erro = ex.Message, DetalhamentoErro = ex.StackTrace });
            }
        }

        public ActionResult FiltroEmpresa(EmpresaFiltroListagemDto filtro)
        {
            try
            {
                var modelo = new EmpresaLista();
                if (filtro.DataCadastro == null && filtro.CNPJ == null && filtro.NomeFantasia == null)
                {
                    modelo.EmpresaList = _empresaApp.BuscarTodos().Result.Objeto;
                }
                else
                {
                    var retornoBusca = _empresaApp.BuscarFiltroListagem(filtro).Result.Objeto;

                    if (retornoBusca.Count > 0)
                    {
                        modelo.EmpresaList = retornoBusca;
                        modelo.EmpresaList = modelo.EmpresaList.OrderByDescending(x => x.Codigo).ToList();
                    }
                    else
                    {
                        modelo.EmpresaList = _empresaApp.BuscarTodos().Result.Objeto;
                    }
                }

                return View("ConsultaEmpresaView", modelo);
            }
            catch (Exception ex)
            {
                return View("ErrorHandler", new ErrorHandler() { Erro = ex.Message, DetalhamentoErro = ex.StackTrace });
            }
        }

        public ActionResult InfoEmpresa(string id, int control)
        {
            try
            {
                var model = new Empresa();
                if (id != null)
                {
                    model = _empresaApp.Buscar(id).Result.Objeto;
                }
                else
                {
                    model.Codigo = _empresaApp.BuscarProximoCodigo().Result.Objeto;
                }

                model.Control = control;

                return View("CadastroEmpresaView", model);
            }
            catch (Exception ex)
            {
                return View("ErrorHandler", new ErrorHandler() { Erro = ex.Message, DetalhamentoErro = ex.StackTrace });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Salvar(Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    empresa.UF = empresa.UF.ToUpper();

                    if (empresa.Control == 0)
                    {
                        var retorno = await _empresaApp.Cadastrar(empresa);
                    }
                    else
                    {
                        var retorno = await _empresaApp.Atualizar(empresa);
                    }

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    return View("ErrorHandler", new ErrorHandler() { Erro = ex.Message, DetalhamentoErro = ex.StackTrace });
                }

                return RedirectToAction(nameof(Index), new { Mensagem = "Cadastrado com sucesso!" });
            }

            return View("CadastroEmpresaView", empresa);
        }

        public ActionResult Deletar(string id)
        {
            try
            {
                var retorno = _empresaApp.Deletar(id);

                var modelo = new EmpresaLista();
                var retornoBusca = _empresaApp.BuscarTodos().Result.Objeto;

                if (retornoBusca.Count > 0)
                {
                    modelo.EmpresaList = retornoBusca;
                    modelo.EmpresaList = modelo.EmpresaList.OrderByDescending(x => x.Codigo).ToList();
                }
                else
                {
                    modelo.EmpresaList = _empresaApp.BuscarTodos().Result.Objeto;
                }

                return View("ConsultaEmpresaView", modelo);
            }
            catch (Exception ex)
            {
                return View("ErrorHandler", new ErrorHandler() { Erro = ex.Message, DetalhamentoErro = ex.StackTrace });
            }
        }

    }
}
