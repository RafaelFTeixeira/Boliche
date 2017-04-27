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
            return CalcularPontuacao();
        }

        private int CalcularPontuacao()
        {
            Frame frame;
            for (var indice = 0; indice < _pontuacoesDosFrames.Count; indice++)
            {
                frame = _pontuacoesDosFrames[indice];
                if (PontueFrameQuandoTiverTodasJogadasCompletasENaoForUmStrikeOuSpare(frame))
                {
                    frame.Pontuar();
                }

                PontueFrameAnteriorSeForUmSpareOuStrike(indice);

                if (UltimoFrame(frame, indice))
                {
                    frame.Pontuar();
                }
            }
            return _pontuacoesDosFrames.Sum(f => f.Pontuacao);
        }

        private static bool UltimoFrame(Frame frame, int indice)
        {
            return 9 == indice && frame.Jogadas.Count() > 1;
        }

        private bool PontueFrameQuandoTiverTodasJogadasCompletasENaoForUmStrikeOuSpare(Frame frame)
        {
            return 1 < frame.Jogadas.Count() && frame.Jogadas.Sum() < PontuacaoMaximaDoFrame;
        }

        private void PontueFrameAnteriorSeForUmSpareOuStrike(int indice)
        {
            if (0 >= indice) return;

            var frameAnterior = _pontuacoesDosFrames[indice - 1];
            var somaDasJogadasDoFrameAnterior = frameAnterior.Jogadas.Sum();
            if (somaDasJogadasDoFrameAnterior == PontuacaoMaximaDoFrame)
            {
                var frameAtual = _pontuacoesDosFrames[indice];
                if (EhStrike(frameAnterior) && frameAtual.Jogadas.Count() > 1)
                {
                    int primeiraJogada = frameAtual.Jogadas[0];
                    int segundaJogada = frameAtual.Jogadas[1];
                    frameAnterior.Pontuar(primeiraJogada + segundaJogada);
                }
                else if (!EhStrike(frameAnterior))
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
    }
}