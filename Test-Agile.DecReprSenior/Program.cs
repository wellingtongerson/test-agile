using System;
using System.Linq;

namespace Test_Agile.DecReprSenior
{
    class Program
    {
        private const int LimitNumber = 100000000;

        public static int Solution(int number)
        {
            if (number > LimitNumber)
                return -1;

            var length = number.ToString().Length;
            var array = new int[length];

            for (int index = 0; index < length; index++)
            {
                int.TryParse(number.ToString().Substring(index, 1), out int value);

                for (int arrayIndex = 0; arrayIndex < array.Length; arrayIndex++)
                {
                    if (value > array[arrayIndex])
                    {
                        for (int i = array.Length - 1; i > arrayIndex; i--)
                        {
                            array[i] = array[i - 1];
                        }

                        array[arrayIndex] = value;
                        break;
                    }
                }
            }

            var stringArray = string.Empty;
            for (int index = 0; index < length; index++)
            {
                stringArray += array[index].ToString(); 
            }

            int.TryParse(stringArray, out int largestNumber);

            return largestNumber <= LimitNumber ? largestNumber : -1;
        }

        public static int SolutionAlternative(int number)
        {
            if (number > LimitNumber)
                return -1;

            var numbers =
                number
                    .ToString()
                    .Where(char.IsDigit)
                    .Select(digito => byte.Parse(digito.ToString()))
                    .OrderByDescending(digito => digito);

            int.TryParse(string.Join(string.Empty, numbers), out int largestNumber);

            return largestNumber <= LimitNumber ? largestNumber : -1;
        }

        static void Main(string[] args)
        {
            Console.Write("Entre com um número >>> ");

            try
            {
                if (!int.TryParse(Console.ReadLine(), out int number))
                    throw new Exception("Informe um número inteiro válido.");

                if (number < 0)
                    throw new Exception("Informe um número inteiro positivo.");

                var largestNumber = Solution(number);
                //var largestNumber = SolutionAlternative(number);

                Console.WriteLine($"O maior número irmão é: {largestNumber}");
            }
            catch (Exception exception)
            {
                Console.WriteLine("Ocorreu um erro ao obter o maior número irmão.");
                Console.WriteLine($"Erro: {exception.Message}");
            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}
