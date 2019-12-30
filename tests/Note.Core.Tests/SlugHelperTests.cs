using Note.Core.Helpers;
using Xunit;

namespace Note.Core.Tests
{
    public class SlugHelperTests
    {
        [Theory]
        [InlineData("slug")]
        [InlineData("slug-123")]
        [InlineData("slug-slug-123")]
        public void CheckValidSlug(string slug)
        {
            Assert.True(SlugHelper.Validate(slug));
        }

        [Theory]
        [InlineData("slug-")]
        [InlineData("slug?")]
        [InlineData("slug@")]
        [InlineData("")]
        [InlineData("-")]
        [InlineData("slug--slug")]
        public void CheckInvalidSlug(string slug)
        {
            Assert.False(SlugHelper.Validate(slug));
        }

        [Theory]
        [InlineData("slug", "slug", new string[] { })]
        [InlineData("slug", "slug", new string[] { "slug-test", "slug-2" })]
        [InlineData("slug", "slug-2", new string[] { "slug" })]
        [InlineData("slug", "slug-3", new string[] { "slug", "slug-2" })]
        public void CheckGetUniqueSlug(string requestedSlug, string slugResult, string[] existingSlugs)
        {
            Assert.Equal(SlugHelper.GetUniqueSlug(requestedSlug, existingSlugs), slugResult);
        }
    }
}
