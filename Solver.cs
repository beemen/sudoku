using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    public abstract class Solver
    {
        public abstract bool Solve(Game game);
    }

    public class BackTrackingSolver : Solver
    {
        public override bool Solve(Game game)
        {
            if (game.IsValid)
            {
                var status = new Status(game);
                if (status.IsPossible == true)
                {
                    foreach (var currentCell in status.EmptyCells)
                    {
                        if (Solve(game, currentCell))
                            return true;
                    }
                }
            }
            return false;
        }

        public bool Solve(Game game, Cell currentCell)
        {
            foreach (var value in currentCell.PossibleValues)
            {
                currentCell.Value = value;
                var status = new Status(game);
                if (status.IsPossible)
                {
                    if (status.EmptyCells.Any())
                        return Solve(game, status.EmptyCells.First());
                    else
                        return true;
                }
                else
                {
                    // Invalid
                    currentCell.Value = null;
                    continue;
                }
            }
            return currentCell.Value.HasValue;
        }

        public class Status
        {
            public List<Cell> FilledCells { get; private set; }
            public List<Cell> EmptyCells { get; private set; }

            public bool IsPossible
            {
                get
                {
                    return EmptyCells.Any(c => c.PossibleValues.Any() == false) == false;
                }
            }

            public Status(Game game)
            {
                EmptyCells = game.Cells.Where(c => c.Value == null)
                    .OrderBy(c => c.PossibleValues.Count())
                    .ToList();

                FilledCells = game.Cells.Where(c => c.Value.HasValue)
                    .ToList();
            }
        }

    }
}