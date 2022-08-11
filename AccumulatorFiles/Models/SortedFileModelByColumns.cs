namespace AccumulatorFiles.Models
{
    public class SortedFileModelByColumns: SortedFileModel
    {
        /// <summary>
        ///   1.  2.
        ///1. 43, 43
        ///2. 44, 65
        ///
        /// 44 = Columns[2][1]
        /// </summary>
        public string[][] Columns { get; set; }
    }

    public static class SortedFileModelByColumnsExtension
    {
        public static string GetString(this string[] strings,char separator)
        {
            string result = "";
            for(int i = strings.Length - 1; i != 0; i--)
            {
                result = separator + strings[i] +  result;
            }
            result = strings[0] + result;
            return result;
        }
    }
}
