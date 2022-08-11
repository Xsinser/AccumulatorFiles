using AccumulatorFiles.Managers;
using AccumulatorFiles.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccumulatorFiles.Tests
{
    [TestClass]
    public  class SortedFileManagerTest
    {
        [TestMethod]
        public void GetFileByLine()
        {
            string FilePath = "ss.csv";
            var obj = new FileManager().GetFileByLine(FilePath).Result;

            var normalObject = new SortedFileModelByColumns() { Th = new string[] { "col1", "col2" } };
            normalObject.Columns = new string[3][];
            normalObject.Columns[0] = new string[] { "1", "2" };
            normalObject.Columns[1] = new string[] { "1", "3" };
            normalObject.Columns[2] = new string[] { "3", "4" };

            var filter = new FormatModel() { Descending = false };
            filter.ColumnSortingSequence = new string[] {"col1" };

            SortedFileManager sortedFileManager = new SortedFileManager();
            sortedFileManager.Order(filter,ref obj);
            CollectionAssert.AreEqual(obj.Th, normalObject.Th);
            CollectionAssert.AreEqual(obj.Columns[0], normalObject.Columns[0]);
            CollectionAssert.AreEqual(obj.Columns[1], normalObject.Columns[1]);
            CollectionAssert.AreEqual(obj.Columns[2], normalObject.Columns[2]);
        }
    }
}
