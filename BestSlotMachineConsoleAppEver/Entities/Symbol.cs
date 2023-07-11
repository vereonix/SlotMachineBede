using System;

namespace BestSlotMachineConsoleAppEver.Entities
{
    public class Symbol
    {
        public string? symbol { get; set; }
        public decimal coefficient { get; set; }
        public int probability { get; set; }
        public bool isWildCard { get; set; } = false;
    }
}
