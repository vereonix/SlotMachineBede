using BestSlotMachineConsoleAppEver.Services;
using Microsoft.Extensions.Configuration;

class Program
{
    static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        ISlotMachine slotMachine = new SlotMachine(configuration);

        slotMachine.StartSlotMachine();        
    }
}
