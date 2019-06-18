using System;
using Xunit;
using SistemaVendas.Models;
using System.Collections.Generic;

namespace ProjetoTestes
{
    public class UnitTestModels
    {
        [Fact]
        public void TestLogin()
        {
            LoginModel objLogin = new LoginModel();
            objLogin.Email = "emnetojf@gmail.com";
            objLogin.Senha = "123456";
            bool resultado = objLogin.ValidarLogin();
            Assert.True(resultado);
        }

        [Fact]
        public void TestLoginFail()
        {
            LoginModel objLogin = new LoginModel();
            objLogin.Email = "emnetojf@gmail.com";
            objLogin.Senha = "1234567";
            bool resultado = objLogin.ValidarLogin();
            Assert.False(resultado);
        }


        [Fact]
        public void CheckTypeListProdutos()
        {
            ProdutoModel produtoModel = new ProdutoModel();
            var lista = produtoModel.ListaProdutos();
            Assert.IsType<List<ProdutoModel>>(lista);
        }

    }
}
