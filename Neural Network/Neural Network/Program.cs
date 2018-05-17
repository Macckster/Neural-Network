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
            public float[] inputs;
            public float[] targets;

            public data(float[] inputs, float[] targets)
            {
                this.inputs = inputs;
                this.targets = targets;
            }
        }

        static void Main(string[] args)
        {
            NeuralNetwork nn = new NeuralNetwork(2, 2, 1, 0.1f);
       
            data[] datas = new data[4];

            datas[0] = new data(new float[] { 1, 1 }, new float[] { 0 });
            datas[1] = new data(new float[] { 0, 0 }, new float[] { 0 });
            datas[2] = new data(new float[] { 1, 0 }, new float[] { 0 });
            datas[3] = new data(new float[] { 0, 1 }, new float[] { 1 });


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

                float[] inp = {value, value2};

                float[] outPut = nn.feedForward(inp);

                print(outPut);
            }
        }

        static void test()
        {
            Matrix a = new Matrix(1, 1);
            Matrix b = new Matrix(1, 1);
            a.matrix[0, 0] = 20;
            b.matrix[0, 0] = 10;

            a = Matrix.multiply(a, b);

            a.print();

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
