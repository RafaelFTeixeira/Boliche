using System.Collections.Generic;
using Boliche;
using NUnit.Framework;

namespace Test.Boliche
{
    [TestFixture]
    public class GameTest
    {
        private Game _game;

        [SetUp]
        public void Init()
        {
            _game = new Game();
        }

        [Test]
        public void Deve_lancar_a_primeira_jogada_do_frame()
        {
            var frame = new Frame(new [] {1});

            _game.Adicionar(frame);

            Assert.AreEqual(1, _game.ObterPontuacao());
        }

        [Test]
        public void Deve_lancar_as_duas_jogadas_do_frame()
        {
            var frame = new Frame(new [] { 1, 4});

            _game.Adicionar(frame);

            Assert.AreEqual(5, _game.ObterPontuacao());
        }

        [Test]
        public void Deve_lancar_a_segunda_jogada_do_frame()
        {
            var frames = new List<Frame>();
            frames.Add(new Frame(new[] { 1, 4}));
            frames.Add(new Frame(new[] { 4, 5}));

            _game.Adicionar(frames);

            Assert.AreEqual(14, _game.ObterPontuacao());
        }

        [Test]
        public void Nao_deve_pontuar_quando_houver_spare_sem_jogar_proximo_frame()
        {
            var frames = new List<Frame>();
            frames.Add(new Frame(new[] { 1, 4 }));
            frames.Add(new Frame(new[] { 4, 5 }));
            frames.Add(new Frame(new[] { 6, 4 }));

            _game.Adicionar(frames);

            Assert.AreEqual(14, _game.ObterPontuacao());
        }

        [Test]
        public void Deve_pontuar_quando_houver_spare_e_proximo_frame_tiver_jogada()
        {
            var frames = new List<Frame>();
            frames.Add(new Frame(new[] { 1, 4 }));
            frames.Add(new Frame(new[] { 4, 5 }));
            frames.Add(new Frame(new[] { 6, 4 }));
            frames.Add(new Frame(new[] { 5 }));

            _game.Adicionar(frames);

            Assert.AreEqual(29, _game.ObterPontuacao());
        }
    }
}
