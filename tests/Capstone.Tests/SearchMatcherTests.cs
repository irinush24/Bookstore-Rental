using Xunit;
using System;
using SharpQuest.Contracts;

namespace Capstone.Tests
{
    public class SearchMatcherTests
    {
        [Theory]
        [InlineData("Pikachu", "")]
        [InlineData("Pikachu", "   ")]
        public void Matches_EmptyOrBlankQuery_ReturnsTrue(string text, string query)
        {
            var matcher = SearchMatcher.Create();
            Assert.True(matcher.Matches(text, query));
        }

        [Theory]
        [InlineData("Pikachu", "PIKA")]
        [InlineData("Café", "cafe")]
        [InlineData("șosea", "Sosea")]
        public void Matches_IgnoresCaseAndDiacritics(string text, string query)
        {
            var matcher = SearchMatcher.Create();
            Assert.True(matcher.Matches(text, query));
        }

        [Theory]
        [InlineData("fire lizard", "lizard fire", true)]
        [InlineData("Pikachu", "chu", true)]
        [InlineData("Pikachu", "chu pika", true)]
        [InlineData("Pikachu", "chu char", false)]
        public void Matches_ValidatesMultipleSubstringsAndOrder(string text, string query, bool expectedMatch)
        {
            var matcher = SearchMatcher.Create();
            Assert.Equal(expectedMatch, matcher.Matches(text, query));
        }

        [Fact]
        public void Matches_NullInputs_FollowContractRules()
        {
            var matcher = SearchMatcher.Create();

            Assert.False(matcher.Matches(null!, "valid query"));

            Assert.Throws<ArgumentNullException>(() => matcher.Matches("valid text", null!));
        }
    }
}