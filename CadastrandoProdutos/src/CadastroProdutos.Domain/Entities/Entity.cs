using System;

namespace CadastroProdutos.Domain.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public DateTime DataCadastro { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
