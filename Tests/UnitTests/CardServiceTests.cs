using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaycastForWindows.Services;
using RaycastForWindows.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RaycastForWindows.Tests.UnitTests
{
    [TestClass]
    public class CardServiceTests
    {
        private string GetTestDataPath(string fileName = null)
        {
            // Assumes Tests/TestData directory is relative to the execution path of tests.
            // This attempts to find the solution root and then navigate to Tests/TestData
            string currentDir = Directory.GetCurrentDirectory(); // Or AppDomain.CurrentDomain.BaseDirectory
            string solutionRoot = currentDir;

            // Heuristic: go up until we find a directory containing "RaycastForWindows.sln" or a known root marker
            // For this environment, let's assume the execution starts somewhere within /app
            // and the .sln file is at /app/RaycastForWindows.sln or similar.
            // We are creating files relative to /app. So Tests/TestData is /app/Tests/TestData.
            // If currentDir is /app/Tests/UnitTests/bin/Debug/netX.X
            // We want to get to /app.

            // Simplified approach for this environment: Assume /app is the root for tests.
            // And the files were created like `Tests/TestData/file.json` which means `/app/Tests/TestData/file.json`
            solutionRoot = "/app"; // Specific to this sandbox environment structure.

            var testDataDir = Path.Combine(solutionRoot, "Tests", "TestData");

            if (!Directory.Exists(testDataDir))
            {
                // Fallback for local dev if /app isn't the root.
                // This part is tricky without knowing actual execution root.
                // Trying a common structure: solution/src, solution/tests
                // If current dir is .../MyProject.Tests/bin/Debug, go up 3 levels to MyProject.Tests, then one more to solution root.
                currentDir = Directory.GetCurrentDirectory();
                solutionRoot = Path.GetFullPath(Path.Combine(currentDir, "..", "..", "..", ".."));
                testDataDir = Path.Combine(solutionRoot, "Tests", "TestData");
                if (!Directory.Exists(testDataDir))
                {
                     // If still not found, try relative to current dir directly (e.g. if tests run from solution root)
                    testDataDir = Path.Combine(currentDir, "Tests", "TestData");
                     if (!Directory.Exists(testDataDir) && currentDir.EndsWith("UnitTests")) // Common for test project folder
                     {
                        // currentDir = /app/Tests/UnitTests
                        testDataDir = Path.Combine(currentDir, "..", "TestData"); //  /app/Tests/TestData
                     }
                }
            }


            if (fileName == null)
                return testDataDir;
            return Path.Combine(testDataDir, fileName);
        }

        [TestMethod]
        public void Test_LoadAllCategories_ReturnsCorrectCategoryNames()
        {
            // Arrange
            // CardService constructor needs a base path, but LoadAllCategories doesn't use it.
            // Provide a dummy path for consistency.
            var cardService = new CardService("dummy_path");
            var expectedCategories = new List<string> { "Tarot", "Jokers", "Enhancements", "Boosters", "Planets", "Seals" };

            // Act
            var actualCategories = cardService.LoadAllCategories();

            // Assert
            CollectionAssert.AreEqual(expectedCategories, actualCategories);
        }

        [TestMethod]
        public void Test_LoadCardsForCategory_ValidCategory_LoadsCards()
        {
            // Arrange
            var testDataRootPath = GetTestDataPath();
            var cardService = new CardService(testDataRootPath);

            // Act
            // This will look for "tarot.json" in the testDataRootPath
            var cards = cardService.LoadCardsForCategory("Tarot_test");

            // Assert
            Assert.IsNotNull(cards, "Cards list should not be null.");
            Assert.AreEqual(2, cards.Count, "Should load 2 cards from tarot_test.json.");

            var card1 = cards.FirstOrDefault(c => c.Name == "Test Tarot Card 1");
            Assert.IsNotNull(card1, "Test Tarot Card 1 not found.");
            Assert.AreEqual("test_tarot1.png", card1.Appearance);
            Assert.AreEqual("Test Effect for Tarot 1", card1.Effect);
        }

        [TestMethod]
        public void Test_LoadCardsForCategory_InvalidCategory_ReturnsEmptyList()
        {
            // Arrange
            var testDataRootPath = GetTestDataPath();
            var cardService = new CardService(testDataRootPath);

            // Act
            var cards = cardService.LoadCardsForCategory("NonExistentCategory");

            // Assert
            Assert.IsNotNull(cards, "Cards list should not be null.");
            Assert.AreEqual(0, cards.Count, "Should return an empty list for a non-existent category file.");
        }

        [TestMethod]
        public void Test_LoadCardsForCategory_MalformedJson_ReturnsEmptyList()
        {
            // Arrange
            var testDataRootPath = GetTestDataPath();
            var cardService = new CardService(testDataRootPath);

            // Act
            // This will look for "malformed_test.json"
            var cards = cardService.LoadCardsForCategory("Malformed_test");

            // Assert
            Assert.IsNotNull(cards, "Cards list should not be null for malformed JSON.");
            Assert.AreEqual(0, cards.Count, "Should return an empty list if JSON is malformed.");
        }

        [TestMethod]
        public void Test_LoadCards_AbsolutePathProvidedToCardService()
        {
            // Arrange
            var testDataJsonFile = GetTestDataPath("tarot_test.json");
            // CardService expects a directory, not a file. So, pass the directory.
            var cardService = new CardService(GetTestDataPath());

            // Act
            // LoadCards itself takes the filename, not the category name.
            // The LoadCardsForCategory method translates "Tarot_test" to "tarot_test.json".
            var cards = cardService.LoadCards("tarot_test.json");

            // Assert
            Assert.IsNotNull(cards);
            Assert.AreEqual(2, cards.Count);
            Assert.AreEqual("Test Tarot Card 1", cards[0].Name);
        }
    }
}
