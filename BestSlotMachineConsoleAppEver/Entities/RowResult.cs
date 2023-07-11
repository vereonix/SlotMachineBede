namespace BestSlotMachineConsoleAppEver.Entities
{
    public class RowResult
    {
        private List<Symbol> _symbols;

        public Symbol symbolCell1 { get; set; }
        public Symbol symbolCell2 { get; set; }
        public Symbol symbolCell3 { get; set; }

        public decimal winAmount { get; set; }

        public RowResult(List<Symbol> symbols, int stake)
        {
            _symbols = symbols;

            // Generate three random symbols
            symbolCell1 = SelectRandomSymbol();
            symbolCell2 = SelectRandomSymbol();
            symbolCell3 = SelectRandomSymbol();

            winAmount = CalculateWinAmount(stake);
        }

        public Symbol SelectRandomSymbol()
        {
            // Calculate the total probability
            int totalProbability = _symbols.Sum(s => s.probability);

            // Generate a random number between 1 and the total probability
            int randomNumber = new Random().Next(1, totalProbability + 1);

            // Iterate through the symbols and find the symbol corresponding to the random number
            int cumulativeProbability = 0;
            foreach (Symbol symbol in _symbols)
            {
                cumulativeProbability += symbol.probability;
                if (randomNumber <= cumulativeProbability)
                {
                    return symbol;
                }
            }

            // This point should never be reached, but in case it does, return the last symbol
            return _symbols.Last();
        }

        public bool CheckIfWin()
        {
            List<Symbol> symbols = new List<Symbol> { symbolCell1, symbolCell2, symbolCell3 };

            // Get count of all distict symbols from spin
            int distinct = symbols.Distinct().Count();

            // All match
            if (distinct == 1)
            {
                return true;
            }
            // 2 wildcards or 2 symbols + wildcard
            else if (distinct == 2 && symbols.Any(symbol => symbol.isWildCard))
            {
                return true;
            }

            return false; // No win
        }

        public decimal CalculateWinAmount(int stake)
        {
            if (CheckIfWin())
            {
                return (symbolCell1.coefficient + symbolCell2.coefficient + symbolCell3.coefficient) * stake;
            }

            return 0;
        }
    }
}
