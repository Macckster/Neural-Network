using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neural_Network
{
    class Program
    {
        static Matrix matrix = new Matrix(2, 3);
        static Matrix mat2 = new Matrix(3, 2);
        static void Main(string[] args)
        {
            matrix.randomize();
            mat2.randomize();

            Matrix mat3 = matrix.transpose();

            for (int i = 0; i < matrix.rows; i++)
            {
                for (int j = 0; j < matrix.cols; j++)
                {
                    Console.WriteLine(matrix.matrix[i, j]);
                }
            }

            for (int i = 0; i < mat3.rows; i++)
            {
                for (int j = 0; j < mat3.cols; j++)
                {
                    Console.WriteLine(mat3.matrix[i, j]);
                }
            }
            Console.ReadKey();
        } 
    }
}
