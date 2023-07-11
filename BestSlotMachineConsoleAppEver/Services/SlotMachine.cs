using BestSlotMachineConsoleAppEver.Entities;
using Microsoft.Extensions.Configuration;

namespace BestSlotMachineConsoleAppEver.Services
{
    public class SlotMachine : ISlotMachine
    {
        public SlotMachine(IConfiguration config)
        {
            _configuration = config;
            _minimumDeposit = int.Parse(_configuration["MinimumDeposit"] ?? "1");
            _minimumStake = int.Parse(_configuration["MinimumStake"] ?? "1");
        }


        public void StartSlotMachine()
        {
            CreateSymbolsList();

            Console.WriteLine("Hello and welcome");

            RequestDeposit();

            StartGame();

            Console.ReadLine(); // Keep the console window open
        }


        void StartGame()
        {
            _gameNumber++;

            Console.WriteLine();
            Console.WriteLine($"---|Game {_gameNumber}|---");

            RequestStake();

            Spin();            

            SpinOutcomeMessage();

            GameOver();
        }

        SpinResult Spin()
        {
            SpinResult spinResult = new SpinResult(_symbols, _stake);

            foreach (RowResult rowresult in spinResult.RowResults)
            {
                Console.WriteLine($"-|{rowresult.symbolCell1.symbol}|{rowresult.symbolCell2.symbol}|{rowresult.symbolCell3.symbol}|- {IsWinner(rowresult)}");
            }

            _gameWinAmount = spinResult.RowResults.Sum(obj => obj.winAmount);
            _balance = _balance + _gameWinAmount;

            return spinResult;
        }

        void SpinOutcomeMessage()
        {
            Console.WriteLine();
            if (_gameWinAmount > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"CONGRATULATION! You won: {_gameWinAmount}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"LOSER!");
                Console.ResetColor();
            }
            Console.WriteLine($"Your current balance is: {_balance}");

            decimal profit = _balance - _desposit;
            if(profit > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine($"Overall profit: {_balance - _desposit}");
            Console.ResetColor();
        }

        string? IsWinner(RowResult rowresult)
        {
            return rowresult.winAmount > 0 ? $"WINNER! ({rowresult.winAmount})" : null;
        }

        void GameOver()
        {
            if (_balance < 1) // < 1 as balance can be 0.6, app doesn't allow decimal stakes. 
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Your remaining balance is {_balance}, GAME OVER");
                Console.ResetColor();
                Console.WriteLine($"Thanks for playing");
            }
            else
            {
                StartGame();
            }
        }

        void RequestDeposit() {
            Console.Write("Please deposit a starting amount: ");
            Console.WriteLine();

            ValidateDeposit(Console.ReadLine()??"0");
        }

        void ValidateDeposit(string depositString)
        {
            if (int.TryParse(depositString, out int deposit) && deposit >= _minimumDeposit)
            {
                _desposit = deposit;
                _balance = deposit;
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"Please enter a valid integer of {_minimumDeposit} or greater.");
                RequestDeposit();
            }
        }

        void RequestStake()
        {
            Console.Write("Enter stake amount: ");
            Console.WriteLine();

            ValidateStake(Console.ReadLine() ?? "0");
        }

        void ValidateStake(string stakeString)
        {
            if (int.TryParse(stakeString, out int stake) && stake >= _minimumStake)
            {
                if (_balance - stake < 0)
                {
                    Console.WriteLine($"Invalid stake. Stack must be equal to or less than your current Balance of {_balance}.");
                    RequestStake();
                }
                else
                {
                    _stake = stake;
                    _balance = _balance - stake;
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine($"Please enter a valid integer of {_minimumStake} or greater.");
                RequestStake();
            }
        }

        void CreateSymbolsList()
        {
            _symbols = new List<Symbol>
            {
                new Symbol { symbol = "A", coefficient = 0.4m, probability = 45 },
                new Symbol { symbol = "B", coefficient = 0.6m, probability = 35 },
                new Symbol { symbol = "P", coefficient = 0.8m, probability = 15 },
                new Symbol { symbol = "*", coefficient = 0m, probability = 5, isWildCard = true }
            };
        }

        readonly IConfiguration _configuration;
        decimal _balance;
        int _minimumDeposit;
        int _desposit;
        int _minimumStake;
        int _stake;
        int _gameNumber;
        decimal _gameWinAmount;

        List<Symbol> _symbols = new();
    }
}
