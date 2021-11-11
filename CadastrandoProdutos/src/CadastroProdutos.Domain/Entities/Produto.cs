

namespace CadastroProdutos.Domain.Entities
{
    public class Produto : Entity
    {
        public string Descricao { get; set; }
        public double PrecoCusto { get; set; }
        public string Categoria { get; set; }
        public double PrecoVenda { get; set; }

        public bool VerificarCategoriaSoftplan(string descricao){
            return descricao.Contains("Softplayer");
        }
    }
}
