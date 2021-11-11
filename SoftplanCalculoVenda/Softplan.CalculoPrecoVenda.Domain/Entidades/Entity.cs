using System;

namespace Softplan.CalculoPrecoVenda.Domain.Entidades
{
    public class Entity
    {
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
