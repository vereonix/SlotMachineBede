namespace BestSlotMachineConsoleAppEver.Entities.Tests
{
    [TestFixture]
    public class RowResultTests
    {
        [Test]
        public void CalculateWinAmount_WithWinningCombination_ReturnsCorrectWinAmount()
        {
            // Arrange
            var symbols = new List<Symbol>
            {
                new Symbol { coefficient = 2, isWildCard = false, probability = 1 },
                new Symbol { coefficient = 3, isWildCard = false, probability = 1 },
                new Symbol { coefficient = 4, isWildCard = false, probability = 1 }
            };

            var rowResult = new RowResult(symbols, 1)
            {
                symbolCell1 = symbols[0],
                symbolCell2 = symbols[0],
                symbolCell3 = symbols[0]
            };

            // Act
            decimal winAmount = rowResult.CalculateWinAmount(1);

            // Assert
            Assert.AreEqual(6, winAmount);
        }

        [Test]
        public void CalculateWinAmount_WithLosingCombination_ReturnsZeroWinAmount()
        {
            // Arrange
            var symbols = new List<Symbol>
            {
                new Symbol { coefficient = 2, isWildCard = false, probability = 1 },
                new Symbol { coefficient = 3, isWildCard = false, probability = 1 },
                new Symbol { coefficient = 4, isWildCard = false, probability = 1 }
            };

            var rowResult = new RowResult(symbols, 1)
            {
                symbolCell1 = symbols[0],
                symbolCell2 = symbols[1],
                symbolCell3 = symbols[2]
            };

            // Act
            decimal winAmount = rowResult.CalculateWinAmount(1);

            // Assert
            Assert.AreEqual(0, winAmount);
        }

        [Test]
        public void SelectRandomSymbol_ReturnsARandomSymbol()
        {
            // Arrange
            var symbols = new List<Symbol>
            {
                new Symbol { coefficient = 2, isWildCard = false, probability = 1 },
                new Symbol { coefficient = 3, isWildCard = false, probability = 1 },
                new Symbol { coefficient = 4, isWildCard = false, probability = 1 }
            };

            var rowResult = new RowResult(symbols, 1);

            // Act
            Symbol randomSymbol = rowResult.SelectRandomSymbol();

            // Assert
            Assert.Contains(randomSymbol, symbols);
        }

        [Test]
        public void CheckIfWin_WithWinningCombination_ReturnsTrue()
        {
            // Arrange
            var symbols = new List<Symbol>
            {
                new Symbol { coefficient = 2, isWildCard = false, probability = 1 },
                new Symbol { coefficient = 2, isWildCard = false, probability = 1 },
                new Symbol { coefficient = 2, isWildCard = false, probability = 1 }
            };

            var rowResult = new RowResult(symbols, 1)
            {
                symbolCell1 = symbols[0],
                symbolCell2 = symbols[0],
                symbolCell3 = symbols[0]
            };

            // Act
            bool isWin = rowResult.CheckIfWin();

            // Assert
            Assert.IsTrue(isWin);
        }

        [Test]
        public void CheckIfWin_WithLosingCombination_ReturnsFalse()
        {
            // Arrange
            var symbols = new List<Symbol>
            {
                new Symbol { coefficient = 2, isWildCard = false, probability = 1 },
                new Symbol { coefficient = 3, isWildCard = false, probability = 1 },
                new Symbol { coefficient = 4, isWildCard = false, probability = 1 }
            };

            var rowResult = new RowResult(symbols, 1)
            {
                symbolCell1 = symbols[0],
                symbolCell2 = symbols[1],
                symbolCell3 = symbols[2]
            };

            // Act
            bool isWin = rowResult.CheckIfWin();

            // Assert
            Assert.IsFalse(isWin);
        }
    }
}
