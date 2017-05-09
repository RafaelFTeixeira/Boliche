using System;
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

        public void Pontuar(int adicionalDePontuacao = 0)
        {
            Pontuacao = Jogadas.Sum() + adicionalDePontuacao;
        }

        public Func<int> PontuarSpare(Frame frameAtual)
        {
            var pinosderrubadosDaPrimeiraJogadaDoFrameAtual = frameAtual.Jogadas.FirstOrDefault();
            var adicionalDePontuacao = 0 != pinosderrubadosDaPrimeiraJogadaDoFrameAtual
                ? pinosderrubadosDaPrimeiraJogadaDoFrameAtual
                : frameAtual.Jogadas.LastOrDefault();
            this.Pontuar(adicionalDePontuacao);

            return () => Pontuacao;
        }

        public void PontuarStrike(Frame frame)
        {
            var primeiraJogada = frame.Jogadas[0];
            if (frame.Jogadas.Count() > 1)
            {
                var segundaJogada = frame.Jogadas[1];
                Pontuar(primeiraJogada + segundaJogada);
            }
            
        }
    }
}
