using Xunit;
using System.IO;
using System.Text;
using System.Linq;
using NETDevelopmentTest;
using Booster.CodingTest.Library;

namespace TestProject
{
    public class UnitTest1
    {
        [Fact]
        public async Task ProcessStreamCorrectWordCount()
        {
            var processor = new TextProcesser(8);
            var testInput = "   Hello world! Hello Thomas. A hot day and a nice boy.  ";

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(testInput));
            await processor.ProcessStream(stream);

            Assert.Equal(11, processor.GetOccurrences());
            Assert.Equal(2, processor.GetOccurrences("hello"));
            Assert.Equal(1, processor.GetOccurrences("world"));
            Assert.Equal(1, processor.GetOccurrences("thomas"));
            Assert.Equal(2, processor.GetOccurrences("a"));

        }

        [Fact]
        public async Task ProcessStreamCheckWordCount()
        {
            var processor = new TextProcesser(3);
            var testInput = "a and a, A hot day and a nice boy.  a";

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(testInput));
            await processor.ProcessStream(stream);

            Assert.Equal(11, processor.GetOccurrences());
            Assert.Equal(2, processor.GetOccurrences("and"));
            Assert.Equal(1, processor.GetOccurrences("day"));
            Assert.Equal(1, processor.GetOccurrences("hot"));
            Assert.Equal(5, processor.GetOccurrences("a"));
 
        }

        [Fact]
        public async Task ProcessStreamEmptyCheckWordCount()
        {
            var processor = new TextProcesser();
            var testInput = "";

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(testInput));
            await processor.ProcessStream(stream);

            Assert.Equal(0, processor.GetOccurrences());

        }
    }
}