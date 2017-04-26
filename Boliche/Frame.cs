using System.Linq;

namespace Boliche
{
    public class Frame
    {
        public int[] Jogadas { get; private set; }
        public int Pontuacao { get; private set; }

        public Frame(int[] jogadas)
        {
            Jogadas = jogadas;
        }

        public void PontuarFrame()
        {
            Pontuacao = Jogadas.Sum();
        }
    }
}
