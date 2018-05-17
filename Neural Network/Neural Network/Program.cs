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
            float[] outP = nn.feedForward(input);
            //print(outP);

            nn.train(input, output);
            
            Matrix ut = Matrix.fromArray(outP);

            ut.save("xd.txt");

            Console.ReadLine();
        }

        static void print(float[] outP)
        {
            for (int i = 0; i < outP.Length; i++)
            {
                Console.WriteLine(outP[i]);
            }
        }
    }
}
