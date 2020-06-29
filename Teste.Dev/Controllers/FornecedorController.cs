using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teste.Dev.Application;
using Teste.Dev.Domain.Constante;
using Teste.Dev.Domain.Dto;
using Teste.Dev.Domain.Entidade;
using Teste.Dev.Domain.Enum;
using Teste.Dev.Domain.IApplication;
using Teste.Dev.Domain.Model;

namespace Teste.Dev.Controllers
{
    public class FornecedorController : Controller
    {
        public IFornecedorApplication _fornecedorApp { get; set; }
        public IEmpresaApplication _empresaApp { get; set; }

        public FornecedorController()
        {
            _fornecedorApp = new FornecedorApplication();
            _empresaApp = new EmpresaApplication();
        }

        public ActionResult Index(string Mensagem)
        {
            ViewBag.Mensagem = Mensagem;

            try
            {
                var modelo = new FornecedorLista();
                var retornoBusca = _fornecedorApp.BuscarTodos().Result.Objeto;

                if (retornoBusca.Count > 0)
                {
                    modelo.FornecedorList = retornoBusca;
                    modelo.FornecedorList = modelo.FornecedorList.OrderByDescending(x => x.Codigo).ToList();
                }
                else
                {
                    modelo.FornecedorList = new List<Fornecedor>();
                }

                return View("ConsultaFornecedorView", modelo);
            }
            catch (Exception ex)
            {
                return View("ErrorHandler", new ErrorHandler() { Erro = ex.Message, DetalhamentoErro = ex.StackTrace });
            }
        }

        public ActionResult FiltroFornecedor(FornecedorFiltroListagemDto filtro)
        {
            try
            {
                var modelo = new FornecedorLista();

                if (filtro.DataCadastro == null && filtro.Documento == null && filtro.NomeFantasia == null)
                {
                    modelo.FornecedorList = _fornecedorApp.BuscarTodos().Result.Objeto;
                }
                else
                {
                    var retornoBusca = _fornecedorApp.BuscarFiltroListagem(filtro).Result.Objeto;

                    if (retornoBusca.Count > 0)
                    {
                        modelo.FornecedorList = retornoBusca;
                        modelo.FornecedorList = modelo.FornecedorList.OrderByDescending(x => x.Codigo).ToList();
                    }
                    else
                    {
                        modelo.FornecedorList = _fornecedorApp.BuscarTodos().Result.Objeto;
                    }
                }

                return View("ConsultaFornecedorView", modelo);
            }
            catch (Exception ex)
            {
                return View("ErrorHandler", new ErrorHandler() { Erro = ex.Message, DetalhamentoErro = ex.StackTrace });
            }
        }

        public ActionResult CadastroFornecedor(string id)
        {
            try
            {
                var model = new Fornecedor();
                if (id != null)
                {
                    model = _fornecedorApp.Buscar(id).Result.Objeto;
                }
                else
                {
                    model.Codigo = _fornecedorApp.BuscarProximoCodigo().Result.Objeto;
                }

                return View("CadastroFornecedorView", model);
            }
            catch (Exception ex)
            {
                return View("ErrorHandler", new ErrorHandler() { Erro = ex.Message, DetalhamentoErro = ex.StackTrace });
            }
        }

        public ActionResult InfoFornecedor(string id, int control)
        {
            try
            {
                var model = new Fornecedor();

                if (id != null)
                {
                    model = _fornecedorApp.Buscar(id).Result.Objeto;
                }

                model.Control = control;

                return View("CadastroFornecedorView", model);
            }
            catch (Exception ex)
            {
                return View("ErrorHandler", new ErrorHandler() { Erro = ex.Message, DetalhamentoErro = ex.StackTrace });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Salvar(Fornecedor fornecedor)
        {
            var empresa = _empresaApp.BuscarPorCodigo(fornecedor.CodigoEmpresa.ToString()).Result.Objeto;

            if (empresa == null || fornecedor.CodigoEmpresa == 0)
            {
                ModelState.AddModelError("Geral", "Empresa não cadastrada!");
                return View("CadastroFornecedorView", fornecedor);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (fornecedor.Control == 0)
                    {
                        var retorno = await _fornecedorApp.Cadastrar(fornecedor);
                    }
                    else
                    {
                        var retorno = await _fornecedorApp.Atualizar(fornecedor);
                    }                    
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    return View("ErrorHandler", new ErrorHandler() { Erro = ex.Message, DetalhamentoErro = ex.StackTrace });
                }

                return RedirectToAction(nameof(Index), new { Mensagem = "Cadastrado com sucesso!" });
            }

            //Caso a empresa seja do Paraná, não permitir cadastrar um fornecedor pessoa física menor de idade;
            if (empresa.UF == "PR" && fornecedor.Pessoa == TipoPessoa.Fisica)
            {
                if (fornecedor.DataNascimento.Date.AddYears(18) > DateTime.Now.Date)
                {
                    ModelState.AddModelError("Geral", "Não é permitido cadastrar fornecedor menor de idade e pessoa física para o estado do Paraná!");
                }
            }

            //Caso o fornecedor seja pessoa física, também é necessário cadastrar o RG e a data de nascimento;
            if (fornecedor.Pessoa == TipoPessoa.Fisica && fornecedor.RG == null && fornecedor.DataNascimento.ToString() == "01/01/0001 00:00:00")
            {
                ModelState.AddModelError("Geral", "Necessário cadastrar RG e data de nascimento para fornecedores do tipo pessoa física!");
            }

            if (fornecedor.NomeFantasia == null)
            {
                ModelState.AddModelError("Geral", "Nome inválido!");
            }

            if (fornecedor.DataNascimento.ToString() == "01/01/0001 00:00:00")
            {
                ModelState.AddModelError("Geral", "Informe a data de nascimento!");
            }

            if (fornecedor.Documento == null && fornecedor.RG == null)
            {
                ModelState.AddModelError("Geral", "Preencha um RG ou CNPJ!");
            }

            if (fornecedor.Telefone[0] is null)
            {
                ModelState.AddModelError("Geral", "Preencha ao menos um número de telefone!");
            }

            return View("CadastroFornecedorView", fornecedor);
        }

        public ActionResult Deletar(string id)
        {
            try
            {
                var retorno = _fornecedorApp.Deletar(id);

                var modelo = new FornecedorLista();
                var retornoBusca = _fornecedorApp.BuscarTodos().Result.Objeto;

                if (retornoBusca.Count > 0)
                {
                    modelo.FornecedorList = retornoBusca;
                    modelo.FornecedorList = modelo.FornecedorList.OrderByDescending(x => x.Codigo).ToList();
                }
                else
                {
                    modelo.FornecedorList = new List<Fornecedor>();
                }

                return View("ConsultaFornecedorView", modelo);
            }
            catch (Exception ex)
            {
                return View("ErrorHandler", new ErrorHandler() { Erro = ex.Message, DetalhamentoErro = ex.StackTrace });
            }
        }

    }
}
