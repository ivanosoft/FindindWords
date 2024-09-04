using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FidingWords.Business
{
    public class WordFinder
    {
        private List<string> wordMatrix;
        private const int RESULT_SIZE = 10;

        public WordFinder(IEnumerable<string> matrix)
        {
            wordMatrix = CreateMatrix(matrix);
        }

        #region Construction
        private List<string> CreateMatrix(IEnumerable<string> matrix)
        {
            List<string> horizontalWords = GetHorizontalWords(matrix);
            List<string> verticalWords = GetVerticalWords(horizontalWords);

            horizontalWords.AddRange(verticalWords);

            return horizontalWords;
        }

        private static List<string> GetHorizontalWords(IEnumerable<string> matrix)
        {
            var result = new List<string>();

            foreach (var matrixItem in matrix)
            {
                result.Add(matrixItem);
            }

            return result;
        }

        private List<string> GetVerticalWords(List<string> rows)
        {
            var result = new List<string>();
            var word = new StringBuilder(rows.Count);

            for (int i = 0; i < rows[0].Length; i++)
            {
                word.Clear();
                for (int j = 0; j < rows.Count; j++)
                {
                    word.Append(rows[j][i].ToString());
                }
                result.Add(word.ToString());
            }

            return result;
        }
        #endregion

        public IEnumerable<string> Find(IEnumerable<string> wordStream)
        {
            Dictionary<string, int> wordCount = CountWords(wordStream);

            return wordCount.Where(word => word.Value > 0)
                .OrderByDescending(word => word.Value)
                .ThenBy(word => word.Key)
                .Take(RESULT_SIZE)
                .Select(word => word.Key);
        }

        #region Helpers
        private Dictionary<string, int> CountWords(IEnumerable<string> wordStream)
        {
            var result = new Dictionary<string, int>();
            wordStream = wordStream.Distinct().ToList();

            foreach (var word in wordStream)
            {
                result[word] = Count(word);
            }

            return result;
        }

        private int Count(string word)
        {
            int result = 0;

            foreach (var row in wordMatrix)
            {
                result += Count(word, row);
            }

            return result;
        }

        private int Count(string word, string row)
        {
            int counter = 0;
            int idx = 0;

            while (idx + word.Length <= row.Length)
            {
                idx = row.IndexOf(word, idx);
                if (idx == -1) break;

                counter++;
                idx = idx + 1;
            }

            return counter;
        }
        #endregion
    }
}
