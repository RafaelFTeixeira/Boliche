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
            for (var i = 0; i < _pontuacoesDosFrames.Count; i++)
            {
                frame = _pontuacoesDosFrames[i];
                if (TemMaisQueUmaJogada(frame) && !EhUmSpareOuStrike(frame))
                {
                    frame.Pontuar();
                }
                if (PrimeiraJogadaDoFrameTemPinosDerrubados(frame) && 0 < i
                    || !PrimeiraJogadaDoFrameTemPinosDerrubados(frame) && SegundaJogadaDoFrameTemPinosDerrubados(frame) && 0 < i)
                {
                    PontueFrameAnteriorSeForUmSpareOuStrike(i);
                }
            }
            return _pontuacoesDosFrames.Sum(f => f.Pontuacao);
        }

        private bool PrimeiraJogadaDoFrameTemPinosDerrubados(Frame frame)
        {
            return 0 < frame.Jogadas.FirstOrDefault();
        }

        private bool SegundaJogadaDoFrameTemPinosDerrubados(Frame frame)
        {
            return 0 < frame.Jogadas.LastOrDefault();
        }
        private void PontueFrameAnteriorSeForUmSpareOuStrike(int indice)
        {
            var frameAnterior = _pontuacoesDosFrames[indice - 1];
            var somaDasJogadasDoFrameAnterior = frameAnterior.Jogadas.Sum();
            if (somaDasJogadasDoFrameAnterior == PontuacaoMaximaDoFrame)
            {
                var frameAtual = _pontuacoesDosFrames[indice];
                if (EhStrike(frameAnterior) && frameAtual.Jogadas.Count() > 1)
                {
                    frameAnterior.Pontuar(frameAtual.Jogadas.Sum());
                }
                else if(!EhStrike(frameAnterior))
                {
                    var pinosderrubadosDaPrimeiraJogadaDoFrameAtual = frameAtual.Jogadas.FirstOrDefault();
                    var adicionalDePontuacao = 0 != pinosderrubadosDaPrimeiraJogadaDoFrameAtual
                        ? pinosderrubadosDaPrimeiraJogadaDoFrameAtual
                        : frameAtual.Jogadas.LastOrDefault();
                    frameAnterior.Pontuar(adicionalDePontuacao);
                }
            }
        }

        private static bool EhStrike(Frame frameAnterior)
        {
            return frameAnterior.Jogadas.Length == 1;
        }

        private bool TemMaisQueUmaJogada(Frame frame)
        {
            return 1 < frame.Jogadas.Count();
        }

        private bool EhUmSpareOuStrike(Frame frame)
        {
            return frame.Jogadas.Sum() == PontuacaoMaximaDoFrame;
        }
    }
}