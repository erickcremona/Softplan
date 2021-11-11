namespace Softplan.CalculoPrecoVenda.Domain.Entidades
{
    public class Categoria: Entity
    {
        public string Nome { get; set; }
        public float Lucro { get; set; }

        public float VerificarTipoCategoria(string nome)
        {
            return nome switch
            {
                "Brinquedos" => 25f,
                "Bebidas" => 30f,
                "Informática" => 10f,
                "Softplan" => 5f,
                _ => 15f,
            };
        }
    }
}
