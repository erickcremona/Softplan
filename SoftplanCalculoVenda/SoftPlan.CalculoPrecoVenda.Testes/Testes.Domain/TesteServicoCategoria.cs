using Moq;
using Softplan.CalculoPrecoVenda.Domain.Contratos;
using Softplan.CalculoPrecoVenda.Domain.Contratos.Repositorio;
using Softplan.CalculoPrecoVenda.Domain.Entidades;
using Softplan.CalculoPrecoVenda.Domain.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Softplan.CalculoPrecoVenda.Testes.TestesDomain
{
    public class TesteServicoCategoria
    {
        Mock<IRepositorioCategoria> repositorioCategoria = new Mock<IRepositorioCategoria>();
        Mock<INotificador> notificador = new Mock<INotificador>();

        List<Categoria> categorias;

        public TesteServicoCategoria()
        {
            CarregarCategorias();
        }

        private void CarregarCategorias()
        {
            categorias = new List<Categoria>
            {
                new Categoria
                {
                    Id = Guid.NewGuid(),
                    DataCriacao = DateTime.Now.Date,
                    Nome = "Brinquedos",
                    Lucro = 25f
                },
                new Categoria
                {
                    Id = Guid.NewGuid(),
                    DataCriacao = DateTime.Now.Date,
                    Nome = "Bebidas",
                    Lucro = 30f
                },
                new Categoria
                {
                    Id = Guid.NewGuid(),
                    DataCriacao = DateTime.Now.Date,
                    Nome = "Informática",
                    Lucro = 10f
                },
                new Categoria
                {
                    Id = Guid.NewGuid(),
                    DataCriacao = DateTime.Now.Date,
                    Nome = "Softplan",
                    Lucro = 5f
                }
            };
        }


        [Theory]
        [InlineData(29.9)]
        [InlineData(26.9)]
        [InlineData(23.9)]
        [InlineData(10.9)]
        [InlineData(9.9)]
        public void True_CalcularPrecoVenda(double precoCusto)
        {
            //Arrange          
            var categoria = new Categoria
            {
                Id = Guid.NewGuid(),
                DataCriacao = DateTime.Now.Date,
                Nome = "Brinquedos",
                Lucro = 25f
            };

            var servicoCategoria = new ServicoCategoria(repositorioCategoria.Object, notificador.Object);
            
            //Act
            var precoVenda = servicoCategoria.CalcularPrecoVenda(categoria, precoCusto);
            double valorEsperado = precoCusto + (precoCusto * categoria.Lucro) / 100;

            //Assert
            Assert.True(precoVenda == valorEsperado);
        }

        [Theory]       
        [InlineData("Brinquedos")]
        [InlineData("Bebidas")]
        [InlineData("Informática")]
        [InlineData("Softplan")]
        public void Equal_VerificarTipoCategoria(string nome)
        {
            //Arrange          
            var categoria = new Categoria();
                       
            categoria = categorias.FirstOrDefault(p => p.Nome == nome);

            //Act
            var retorno = categoria.VerificarTipoCategoria(nome);

            //Assert
            Assert.Equal(retorno, categoria.Lucro);
        }
    }
}
