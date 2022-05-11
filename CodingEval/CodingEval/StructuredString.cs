using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingEval
{
    public class StructuredString
    {
        /// <summary>
        /// Join a set of strings into one structured string.
        /// </summary>
        public string Join(IEnumerable<string> blocks)
        {
            StringBuilder res = new StringBuilder();
            foreach (var str in blocks)
            {
                res.Append($"[{str}]");
            }

            return res.ToString();
        }

        /// <summary>
        /// Split a structured string back into blocks.
        /// </summary>
        public string[] Split(string str)
        {
            List<string> listOfStr = new List<string>();
            return Split(str, listOfStr);
        }

        /// <summary>
        /// This helper method finds the last structured string (ie: "[[foo][bar][baz]][zoop]" will find [zoop]
        /// then removes the bracket and added to the list of strings.
        /// After adding "zoop" to list, remove "[zoop]" from original string and used recursion to do the same thing.
        /// </summary>
        private string[] Split(string str, List<string> words)
        {
            if (str.Length == 0)
            {
                return words.ToArray();
            }

            int lastIndex = str.Length - 1;
            int count = 0;
            while (str[lastIndex].Equals(']'))
            {
                lastIndex--;
                count++;
            }

            StringBuilder left = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                left.Append('[');
            }

            int lastIndexOfLeft = str.LastIndexOf(left.ToString());
            if (lastIndexOfLeft == 1)
            {
                lastIndexOfLeft = 0;
            }
            int len = str.Length - lastIndexOfLeft;
            var strWithBrackets = str.Substring(lastIndexOfLeft, len);

            var s = strWithBrackets.Substring(1, strWithBrackets.Length - 2);

            words.Add(s);

            str = str.Replace(strWithBrackets, "");

            Split(str, words);

            words.Reverse();
            return words.ToArray();
        }

        /// <summary>
        /// Find out if the input is a valid structured string.
        /// simply check to see if brackets are correctly placed.
        /// </summary>
        public bool IsStructuredString(string str)
        {
            if (!str[0].Equals('[') || !str[str.Length-1].Equals(']'))
            {
                return false;
            }

            Stack<char> brackets = new Stack<char>();

            var bracketsOnly = RemoveLetters(str);

            foreach (var b in bracketsOnly)
            {
                if (b.Equals('['))
                {
                    brackets.Push(b);
                }
                else if (b.Equals(']') && brackets.Count != 0 && brackets.Peek().Equals('['))
                {
                    brackets.Pop();
                }
                else
                {
                    return false;
                }
            }

            if (brackets.Count != 0)
            {
                return false;
            }

            return true;
        }

        private string RemoveLetters(string str)
        {
            return new string(str.Where(c => !Char.IsLetter(c)).ToArray());
        }
    }
}
