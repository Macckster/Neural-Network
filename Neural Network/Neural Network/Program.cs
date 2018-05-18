using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neural_Network
{
    class Program
    {
        static Random prng = new Random();

        public struct data
        {
            public double[] inputs;
            public double[] targets;

            public data(double[] inputs, double[] targets)
            {
                this.inputs = inputs;
                this.targets = targets;
            }
        }

        //clamp x between 0 and 1
        public static double sigmoid(double x)
        {
            return (double)(1 / (1 + Math.Exp(-x)));
        }

        //Derivative of sigmoid (asuming x has already been passed through sigmoid once
        public static double dSigmoid(double x)
        {
            return x * (1 - x);
        }

        static void Main(string[] args)
        {
            FunctionHandler f = new FunctionHandler(sigmoid, dSigmoid);

            NeuralNetwork nn = new NeuralNetwork(2, 2, 1, 0.1f, f);

            test(nn);
        }

        static void test(NeuralNetwork nn)
        {
            data[] datas = new data[4];

            datas[0] = new data(new double[] { 1, 1 }, new double[] { 0 });
            datas[1] = new data(new double[] { 0, 0 }, new double[] { 0 });
            datas[2] = new data(new double[] { 1, 0 }, new double[] { 1 });
            datas[3] = new data(new double[] { 0, 1 }, new double[] { 1 });


            for (int i = 0; i < 50000; i++)
            {
                int index = prng.Next(datas.Length);

                data data = datas[index];
                nn.train(data.inputs, data.targets);
            }


            while (true)
            {
                string[] ord = Console.ReadLine().Split();

                int value = int.Parse(ord[0]);
                int value2 = int.Parse(ord[1]);

                double[] inp = { value, value2 };

                double[] outPut = nn.guess(inp);

                print(outPut);
            }
        }

        static void print(double[] outP)
        {
            for (int i = 0; i < outP.Length; i++)
            {
                Console.WriteLine(outP[i]);
            }
        }
    }
}
