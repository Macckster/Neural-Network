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
            NeuralNetwork nn = new NeuralNetwork(2, 2, 1);

            float[] input = { 1, 0 };
            float[] output = { 1 };
            //float[] output = nn.feedForward(input);

            nn.train(input, output);
            
            Console.ReadLine();
        } 
    }
}
