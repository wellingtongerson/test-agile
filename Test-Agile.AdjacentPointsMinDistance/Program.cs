using System;
using System.Collections.Generic;
using System.Linq;

namespace Test_Agile.AdjacentPointsMinDistance
{
    class Program
    {
        private const int LimitMinimumDistance = 100000000;

        public static int Solution(int[] array)
        {
            if (array == null || array.Length < 1 || array.Length > 40000)
                return -2;

            var itens = new List<Item>();

            for (var i = 0; i < array.Length - 1; i++)
            {
                var arrayCompare = new int[array.Length - (i + 1)];
                Array.Copy(array, i + 1, arrayCompare, 0, array.Length - (i + 1));

                for (var count = 0; count < arrayCompare.Length; count++)
                {
                    if ((array[i] < arrayCompare[count] && !array.Intersect(Enumerable.Range(array[i], arrayCompare[count]).Where(x => x > array[i] && x < arrayCompare[count])).Any()) ||
                        (array[i] >= arrayCompare[count] && !array.Intersect(Enumerable.Range(arrayCompare[count], array[i]).Where(x => x < array[i] && x > arrayCompare[count])).Any()))
                    {
                        itens.Add(new Item(i, count + i + 1));
                    }
                }
            }

            if (itens.Count == 0)
                return -2;

            int? minDistance = null;
            foreach (Item item in itens)
            {
                var value = Math.Abs(array[item.Item1] - array[item.Item2]);

                if (!minDistance.HasValue || minDistance.Value > value)
                    minDistance = value;
            }

            if (!minDistance.HasValue)
                return -2;

            if (minDistance.Value > LimitMinimumDistance)
                return -1;

            return minDistance.Value;
        }

        public class Item
        {
            public Item(int item1, int item2)
            {
                Item1 = item1;
                Item2 = item2;
            }

            public int Item1 { get; set; }

            public int Item2 { get; set; }
        }

        static void Main(string[] args)
        {
            Console.Write("Informe os valores, separados por virgula >>> ");

            try
            {
                var array = Console.ReadLine()?.Split(',').Select(int.Parse).ToArray();

                Console.WriteLine($"Retorno: {Solution(array)}");
            }
            catch (Exception exception)
            {
                Console.WriteLine("Ocorreu um erro ao obter a distância mínima.");
                Console.WriteLine($"Erro: {exception.Message}");
            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}
