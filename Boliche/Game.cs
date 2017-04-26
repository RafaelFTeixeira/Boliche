using System.Collections.Generic;
using System.Linq;

namespace Boliche
{
    public class Game
    {
        public static int PontuacaoMaximaDoFrame = 10;
        private readonly List<Frame> _pontuacoesDosFrames;

        public Game()
        {
            _pontuacoesDosFrames = new List<Frame>();
        }
        public void Adicionar(Frame pinosQueForamDerrubados)
        {
            _pontuacoesDosFrames.Add(pinosQueForamDerrubados);
        }

        public void Adicionar(IEnumerable<Frame> pinosQueForamDerrubados)
        {
            _pontuacoesDosFrames.AddRange(pinosQueForamDerrubados);
        }

        public int ObterPontuacao()
        {
            foreach (var frame in _pontuacoesDosFrames)
            {
                var somaDasJogadasDoFrame = frame.Jogadas.Sum();
                if (somaDasJogadasDoFrame < PontuacaoMaximaDoFrame)
                {
                    frame.PontuarFrame();
                }
            }
            return _pontuacoesDosFrames.Sum(frame => frame.Pontuacao);
        }
    }
}