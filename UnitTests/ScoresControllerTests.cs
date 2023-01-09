using System;
using Xunit;

namespace UnitTests
{
    public class ScoresControllerTests
    {
        var _database = mongoClient.GetDatabase(DatabaseName);
        [Fact]
        public void GetItemAsync_NotExistingItem_ReturnNotFound()
        {
            // arrange
            
            // act
            
            // assert
        }
    }
}