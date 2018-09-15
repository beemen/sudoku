using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class Cell
    {
        public Game Game { get; set; }

        public int Id { get; set; }
        public List<Group> Groups { get; } = new List<Group>();

        public int ColumnId
        {
            get
            {
                return Id % Game.Config.Order;
            }
        }

        public int RowId
        {
            get
            {
                return Id / Game.Config.Order;
            }
        }

        public int? Value { get; set; }
        public int[] PossibleValues
        {
            get
            {
                return Game.Config.PossibleValues
                    .Where(v => this.Groups.TrueForAll(g => g.Cells.Any(c => c.Value == v) == false))
                    .ToArray();
            }
        }

        public Row Row
        {
            get { return this.Groups.OfType<Row>().SingleOrDefault(); }
        }
        public Column Column
        {
            get { return this.Groups.OfType<Column>().SingleOrDefault(); }
        }
        public Square Square
        {
            get { return this.Groups.OfType<Square>().SingleOrDefault(); }
        }
    }
}
