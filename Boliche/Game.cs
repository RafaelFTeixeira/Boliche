using System.Collections.Generic;
using System.Linq;

namespace Boliche
{
    public class Game
    {
        public static int PontuacaoMaximaDoFrame = 10;
        private readonly List<Frame> _pontuacoesDosFrames;
        private Pontuacao _pontuacao;

        public Game()
        {
            _pontuacoesDosFrames = new List<Frame>();
            _pontuacao = new Pontuacao();
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
            return CalcularPontuacao();
        }

        private int CalcularPontuacao()
        {
            Frame frame, frameAnterior;
            _pontuacao.PontuacoesDosFrames = _pontuacoesDosFrames;
            for (var indice = 0; indice < _pontuacoesDosFrames.Count; indice++)
            {
                frame = _pontuacoesDosFrames[indice];
                frameAnterior = indice > 0 ? _pontuacoesDosFrames[indice - 1] : _pontuacoesDosFrames[indice];

                if (FrameTemTodasJogadasCompletasENaoEhUmStrikeOuSpare(frame))
                {
                    frame.Pontuar();
                }

                if (FrameAnteriorForUmSpare(indice))
                {
                    frameAnterior.PontuarSpare(frame);
                }

                if (EhStrike(frameAnterior))
                {
                    frameAnterior.PontuarStrike(frame);
                }

                if (UltimoFrame(frame, indice))
                {
                    frame.Pontuar();
                }
            }
            return _pontuacoesDosFrames.Sum(f => f.Pontuacao);
        }

        private bool FrameAnteriorForUmSpare(int indice)
        {
            if (0 >= indice) return false;

            var frameAnterior = _pontuacoesDosFrames[indice - 1];
            var somaDasJogadasDoFrameAnterior = frameAnterior.Jogadas.Sum();
            if (somaDasJogadasDoFrameAnterior == PontuacaoMaximaDoFrame && !EhStrike(frameAnterior))
            {
                return true;
            }
            return false;
        }

        private static bool UltimoFrame(Frame frame, int indice)
        {
            return 9 == indice && frame.Jogadas.Count() > 1;
        }

        private bool FrameTemTodasJogadasCompletasENaoEhUmStrikeOuSpare(Frame frame)
        {
            return 1 < frame.Jogadas.Count() && frame.Jogadas.Sum() < PontuacaoMaximaDoFrame;
        }

        private static bool EhStrike(Frame frame)
        {
            return frame.Jogadas.Length == 1 && frame.Jogadas.Sum() == PontuacaoMaximaDoFrame;
        }
    }
}