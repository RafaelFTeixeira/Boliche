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
            Frame frame;
            var somaDasJogadasDoFrame = 0;
            for (var i = 0; i < _pontuacoesDosFrames.Count; i++)
            {
                frame = _pontuacoesDosFrames[i];
                somaDasJogadasDoFrame = frame.Jogadas.Sum();
                if (somaDasJogadasDoFrame < PontuacaoMaximaDoFrame)
                {
                    frame.PontuarFrame();
                }
                if (0 < i)
                {
                    var frameAnterior = _pontuacoesDosFrames[i - 1];
                    var somaDasJogadasDoFrameAnterior = frameAnterior.Jogadas.Sum();
                    if (somaDasJogadasDoFrameAnterior == PontuacaoMaximaDoFrame)
                    {
                        frameAnterior.PontuarFrame(frame.Jogadas.FirstOrDefault());
                    }
                }
            }
            return _pontuacoesDosFrames.Sum(f => f.Pontuacao);
        }
    }
}