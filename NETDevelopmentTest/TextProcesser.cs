using Booster.CodingTest.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETDevelopmentTest
{
    public class TextProcesser
        : InterfaceTextProcesser
    {
        public TextProcesser(int bufferLength = 1000)
        {
            if (bufferLength < 1)
            {
                bufferLength = 1000;
            }
            BufferLength = bufferLength;
        }

        private readonly int BufferLength;

        private readonly Dictionary<string, int> WordOccurrences = new Dictionary<string, int>();

        private readonly List<char> CurrentWord = new List<char>();

        /// <summary>
        /// ProcessStream
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public async Task ProcessStream(Stream stream)
        {
            using var reader = new StreamReader(stream);
            char[] buffer = new char[BufferLength];
            int read;
            while ((read = reader.Read(buffer, 0, buffer.Length)) > 0)
            {
                await ProcessBuffer(buffer, read);
            }

            if (CurrentWord.Count > 0)
            {
                var word = new string(CurrentWord.ToArray()).ToLowerInvariant();
                if (!WordOccurrences.ContainsKey(word))
                {
                    WordOccurrences[word] = 0;
                }
                WordOccurrences[word]++;
            }
        }


        public int GetOccurrences(string? word = null)
        {
            if (string.IsNullOrEmpty(word))
            {
                return WordOccurrences.Sum(kvp => kvp.Value);
            }
            else if(WordOccurrences.ContainsKey(word))
            {
                return WordOccurrences[word];
            }
            return 0;
        }


        /// <summary>
        /// DisplayResults
        /// </summary>
        public void DisplayResults()
        {
            var wordOccurrences = new Dictionary<string, int>(WordOccurrences);

            Console.WriteLine($"Total characters: {wordOccurrences.Sum(kvp => kvp.Key.Length * kvp.Value)}");
            Console.WriteLine($"Total words: {wordOccurrences.Sum(kvp => kvp.Value)}");

            var largestWords = wordOccurrences.Keys.OrderByDescending(w => w.Length).Take(5);
            var smallestWords = wordOccurrences.Keys.OrderBy(w => w.Length).Take(5);

            Console.WriteLine("5 Largest Words:");
            foreach (var word in largestWords)
            {
                Console.WriteLine(word);
            }

            Console.WriteLine("5 Smallest Words:");
            foreach (var word in smallestWords)
            {
                Console.WriteLine(word);
            }

            var mostFrequentWords = wordOccurrences.OrderByDescending(kvp => kvp.Value).Take(10);
            Console.WriteLine("10 Most Frequent Words:");
            foreach (var group in mostFrequentWords)
            {
                Console.WriteLine($"{group.Key}: {group.Value}");
            }

            var sortedCharFrequency = wordOccurrences.OrderByDescending(kvp => kvp.Value);
            Console.WriteLine("Words by Frequency:");
            foreach (var kvp in sortedCharFrequency)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }

        }

        /// <summary>
        /// ProcessBuffer
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="read"></param>
        /// <returns></returns>
        private async Task ProcessBuffer(char[] buffer, int read)
        {
            for (int i = 0; i < read; i++)
            {
                var character = buffer[i];

                if (char.IsLetter(character))
                {
                    CurrentWord.Add(character);
                }
                else if (CurrentWord.Count > 0)
                {
                    var word = new string(CurrentWord.ToArray()).ToLowerInvariant();
                    if (!WordOccurrences.ContainsKey(word))
                    {
                        WordOccurrences[word] = 0;
                    }
                    WordOccurrences[word]++;

                    CurrentWord.Clear();
                }
            }
        }
    }
}
