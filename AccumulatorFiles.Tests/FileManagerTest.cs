using AccumulatorFiles.Managers;
using AccumulatorFiles.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AccumulatorFiles.Tests
{
    [TestClass]
    public class FileManagerTest
    {
        [TestMethod]
        public void GetFileByLine()
        {
            string FilePath = "ss.csv";
            var obj = new FileManager().GetFileByLine(FilePath).Result;
            var normalObject = new SortedFileModelByColumns() { Th = new string[] { "col1", "col2" } };
            normalObject.Columns = new string[3][];
            normalObject.Columns[0] = new string[] { "1", "2" };
            normalObject.Columns[1] = new string[] { "3", "4" };
            normalObject.Columns[2] = new string[] { "1", "3" };
            CollectionAssert.AreEqual(obj.Th, normalObject.Th);
            CollectionAssert.AreEqual(obj.Columns[0], normalObject.Columns[0]);
            CollectionAssert.AreEqual(obj.Columns[1], normalObject.Columns[1]);
            CollectionAssert.AreEqual(obj.Columns[2], normalObject.Columns[2]);
        }

        [TestMethod]
        public void GetFileByLineByFilter()
        {
            string FilePath = "ss.csv";
            var tt = File.Exists(FilePath);
            var filter = new FormatModel() { Descending = true };
            filter.Filters = new System.Collections.Generic.Dictionary<int, System.Text.RegularExpressions.Regex>();
            filter.Filters.Add(0, new Regex(@"1(\@*)"));
            var obj = new FileManager().GetFileByLineByFilter(FilePath, filter).Result;
            var normalObject = new SortedFileModelByColumns() { Th = new string[] { "col1", "col2" } };
            normalObject.Columns = new string[3][];
            normalObject.Columns[0] = new string[] { "1", "2" };
            normalObject.Columns[1] = new string[] { "1", "3" };
            CollectionAssert.AreEqual(obj.Th, normalObject.Th);
            CollectionAssert.AreEqual(obj.Columns[0], normalObject.Columns[0]);
            CollectionAssert.AreEqual(obj.Columns[1], normalObject.Columns[1]);
        }

    }
}
