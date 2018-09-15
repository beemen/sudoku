using System;
using System.Linq;

namespace Sudoku
{
    public class Game
    {
        public Cell[] Cells;
        public Row[] Rows;
        public Column[] Columns;
        public Square[] Squares;

        public Group[] Groups;

        public Config Config;

        public Game(Config config)
        {
            this.Config = config;

            this.Cells = Enumerable.Range(0, Config.Order * Config.Order)
                .Select(i => new Cell() { Id = i, Game = this })
                .ToArray();

            this.Rows = Enumerable.Range(0, Config.Order)
                .Select(i => new Row() { Id = i })
                .ToArray();

            this.Columns = Enumerable.Range(0, Config.Order)
                .Select(i => new Column() { Id = i })
                .ToArray();

            this.Squares = Enumerable.Range(0, Config.OrderRoot)
                .SelectMany(i =>
                   Enumerable.Range(0, Config.OrderRoot)
                   .Select(j => new Square() { StartRowId = i * Config.OrderRoot, StartColumnId = j * Config.OrderRoot, Game = this }))
                .ToArray();

            this.Groups = new Group[0]
                .Concat(this.Rows)
                .Concat(this.Columns)
                .Concat(this.Squares)
                .ToArray();

            foreach (var group in this.Groups)
            {
                foreach (var cell in this.Cells)
                {
                    if (group.Contains(cell))
                    {
                        group.Cells.Add(cell);
                        cell.Groups.Add(group);
                    }
                }
            }
        }

        public bool IsValid
        {
            get
            {
                return Array.TrueForAll(Groups, g => g.HasRepetitions == false);
            }
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine,
                Rows.Select(r => string.Join(", ", r.Cells.Select(c => c.Value)))
                );
        }

        public void FillFrom(int?[] values)
        {
            foreach (var o in Cells.Zip(values, (c, v) => new { c, v }))
                o.c.Value = o.v;
        }
    }
}