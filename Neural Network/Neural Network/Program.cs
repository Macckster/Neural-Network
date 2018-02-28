using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neural_Network
{
    class Program
    {
        static void Main(string[] args)
        {
            NeuralNetwork nn = new NeuralNetwork(2, 2, 25);

            float[] input = { 1, 0 };
            float[] output = nn.feedForward(input);

            for (int i = 0; i < output.Length; i++)
            {
                Console.WriteLine(output[i]);
            }
            
            Console.ReadLine();
        } 
    }
}
