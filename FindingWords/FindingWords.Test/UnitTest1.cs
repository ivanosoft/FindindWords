using FidingWords.Business;

namespace FindingWords.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test_Challenge_Example()
        {
            var list = new List<string>
            { "abcgc",
              "fgwio",
              "chill",
              "pqnsd",
              "uvdxy"};
            var sut = new WordFinder(list);

            var expected = new List<string> { "chill", "cold", "wind" };
            Assert.Equal(expected, sut.Find(expected));
        }

        [Fact]
        public void Count_Just_Found_Words()
        {
            var list = new List<string>
            { "abcgc",
              "fgwio",
              "chill",
              "pqnsd",
              "uvdxy"};
            var sut = new WordFinder(list);

            var argument = new List<string> { "chill", "cold", "windi" };
            var expected = new List<string> { "chill", "cold" };
            Assert.Equal(expected, sut.Find(argument));
        }

        [Fact]
        public void Result_Is_Sorted_Descending()
        {
            var list = new List<string>
            { "helloahello",
              "fgwiouecold",
              "chillulaaaa",
              "pqnsdulcold",
              "uvdxyuocccc"};
            var sut = new WordFinder(list);

            var argument = new List<string> { "chill", "cold", "wind", "hello" };
            var expected = new List<string> { "hello", "cold", "chill", "wind" };
            Assert.Equal(expected, sut.Find(argument));
        }

        [Fact]
        public void Several_Repeated()
        {
            var list = new List<string>
            { "helloahello",
              "fgwiouecold",
              "chillulaaaa",
              "pqnsdulcold",
              "uvdxyuocccc"};
            var sut = new WordFinder(list);

            var argument = new List<string> { "chill", "cold", "wind", "hello", "bye" };
            var expected = new List<string> { "hello", "cold", "chill", "wind" };
            Assert.Equal(expected, sut.Find(argument));
        }

        [Fact]
        public void Duplicated_Words()
        {
            var list = new List<string>
            { "helloahello",
              "fgwiouecold",
              "chillulaaaa",
              "pqnsdulcold",
              "uvdxyuocccc"};
            var sut = new WordFinder(list);

            var argument = new List<string> { "chill", "cold", "wind", "hello", "bye","cold","bye","chill" };
            var expected = new List<string> { "hello", "cold", "chill", "wind" };
            Assert.Equal(expected, sut.Find(argument));
        }
    }
}