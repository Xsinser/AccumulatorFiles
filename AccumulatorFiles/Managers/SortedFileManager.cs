using AccumulatorFiles.Models;
using System.Collections.Generic;
using System.Linq;
using System;
namespace AccumulatorFiles.Managers
{
    public class SortedFileManager
    {
        public void Order(FormatModel format, ref SortedFileModelByColumns file)
        {
            if (format.ColumnSortingSequence.Length > 0)
            {
                int[] colNumbers = GetColumnsNumbers(format, ref file);
                if (format.Descending)
                {
                    OrderFileDesc(colNumbers, ref file);
                }
                else
                {
                    OrderFile(colNumbers, ref file);
                }
            }
        }

        private void OrderFile(int[] cols, ref SortedFileModelByColumns file)
        {
            var bufList = file.Columns.OrderBy(ob => ob[cols[0]]);
            for (int i = 1; i < cols.Length; i++)
            {
                bufList = bufList.ThenBy(ob => ob[cols[i]]);
            }
            file.Columns = bufList.ToArray();
        }
        private void OrderFileDesc(int[] cols, ref SortedFileModelByColumns file)
        {
            var bufList = file.Columns.OrderByDescending(ob => ob[cols[0]]);
            for (int i = 1; i < cols.Length; i++)
            {
                int tmpI = i; 
                bufList = bufList.ThenByDescending(ob => ob[cols[tmpI]]);
            }
            var tt = bufList.ToArray();
            file.Columns = bufList.ToArray();
        }
        private int[] GetColumnsNumbers(FormatModel format, ref SortedFileModelByColumns file)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < format.ColumnSortingSequence.Length; i++)
            {
                int value = Array.IndexOf(file.Th, format.ColumnSortingSequence[i]);
                if (value != -1)
                {
                    result.Add(value);
                }
            }
            return result.ToArray();

        }
    }
}
