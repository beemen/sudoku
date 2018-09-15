using NUnit.Framework;
using System;
using System.Linq;
using System.Reflection;

namespace Sudoku
{
    [TestFixture]
    public class SolverTests
    {
        [Test]
        public void Solve_Empty_Solved(
            [Values(1,2,3)] int orderRoot, 
            [Values(typeof(BackTrackingSolver))]Type solverType)
        {
            var config = new Config() { OrderRoot = orderRoot };
            var game = new Game(config);
            var solver = solverType.InvokeMember(null, BindingFlags.CreateInstance, null, null, null) as Solver;

            var ret = solver.Solve(game);

            Assert.True(ret);
            Assert.AreEqual(1, game.Cells.First().Value);
        }

        [Test]
        public void Invalid_False(
            [Values(typeof(BackTrackingSolver))]Type solverType)
        {
            var config = new Config() { OrderRoot = 2 };
            var game = new Game(config);
            game.FillFrom(new int?[] { 2, 2 });
            var solver = solverType.InvokeMember(null, BindingFlags.CreateInstance, null, null, null) as Solver;

            var ret = solver.Solve(game);

            Assert.False(ret);
        }
    }
}
