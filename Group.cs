using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public abstract class Group
    {
        public List<Cell> Cells { get; } = new List<Cell>();
        public abstract bool Contains(Cell cell);
        public bool HasRepetitions
        {
            get
            {
                var values = Cells.Where(c => c.Value.HasValue).Select(c => c.Value.Value).ToArray();
                return values.Count() > values.Distinct().Count();
            }
        }
    }

    public class Square : Group
    {
        public Game Game { get; set; }
        public int StartRowId { get; set; }
        public int StartColumnId { get; set; }

        public int EndRowId
        {
            get
            {
                return StartRowId + Game.Config.OrderRoot;
            }
        }

        public int EndColumnId
        {
            get
            {
                return StartColumnId + Game.Config.OrderRoot;
            }
        }

        public override bool Contains(Cell cell)
        {
            return
                cell.RowId >= this.StartRowId && cell.RowId < this.EndRowId &&
                cell.ColumnId >= this.StartColumnId && cell.ColumnId < this.EndColumnId;
        }
    }

    public class Row : Group
    {
        public int Id { get; set; }
        public override bool Contains(Cell cell)
        {
            return cell.RowId == this.Id;
        }
    }

    public class Column : Group
    {
        public int Id { get; set; }
        public override bool Contains(Cell cell)
        {
            return cell.ColumnId == this.Id;
        }
    }
}
