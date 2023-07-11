namespace BestSlotMachineConsoleAppEver.Entities
{
    public class SpinResult
    {
        private List<Symbol> _symbols;

        public List<RowResult> RowResults { get; set; } = new List<RowResult>();

        public SpinResult(List<Symbol> symbols, int stake)
        {
            _symbols = symbols;

            // Generate four random rows
            for (int i = 0; i < 4; i++)
            {
                RowResults.Add(new RowResult(_symbols, stake));
            }
        }
    }
}
