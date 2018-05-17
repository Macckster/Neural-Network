//Created by Marcus Jansson on 2018-02-21
//GPLV3 License

using System;
using System.Collections.Generic;
using System.IO;

namespace Neural_Network
{
    class Matrix
    {
        private Random prng = new Random();

        public int rows, cols;
        public float[,] matrix;


        //Constructor
        public Matrix(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;

            matrix = new float[rows, cols];
        }

        //Add a scalar value to all elements in the matrix
        public void add(float n)
        {
            //Scalar add
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] += n;
                }
            }
        }

        public void add(Matrix mat)
        {
            //Scalar add
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] += mat.matrix[i,j];
                }
            }
        }

        public float[] toArray()
        {
            //Temporary solution, is ugly and slow and bad kappa
            List<float> temp = new List<float>();
            float[] output = new float[cols * rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    temp.Add(matrix[i, j]);
                }
            }
            output = temp.ToArray();
            return output;
        }
        //Multiply each index with a scalar value
        public void multiply(float n)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] *= n;
                }
            }
        }

        //Multiply this matrix with another;
        public void multiply(Matrix mat)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] *= mat.matrix[i, j];
                }
            }
        }

        //Generate a random value betwen -1 and 1 (inclusive) for each element in the array. These values are floats
        public void randomize()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] += (float)prng.NextDouble() * 2 - 1;
                }
            }
        }

        //Apply a function to all elements in the matrix
        public void map(Func<float, float> function)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = function(matrix[i, j]);
                }
            }
        }

        public void save(string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path);
            }

            string[] s = new string[cols * rows];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    s[cols * i + j] += "" + matrix[i, j];
                }
            }

            File.WriteAllLines(path,s);
        }


        //DEBUG: PRINT ALL VALUES
        public void print()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(matrix[i, j]);
                }
                Console.WriteLine();
            }
        }

        //Matrix class specific funtions not related to an individual object

        //Convert from array to Matrix
        internal static Matrix fromArray(float[] input)
        {

            Matrix m = new Matrix(input.Length, 1);

            for (int i = 0; i < input.Length; i++)
            {
                m.matrix[i, 0] = input[i];
            }
            return m;
        }


        //Multiply two matrices togheter
        internal static Matrix multiply(Matrix a, Matrix b)
        {
            if (a.cols != b.rows)
            {
                return new Matrix(69, 420);
            }

            Matrix result = new Matrix(a.rows, b.cols);

            for (int i = 0; i < result.rows; i++)
            {
                for (int j = 0; j < result.cols; j++)
                {
                    //Dot prodcut
                    float sum = 0;
                    for (int k = 0; k < a.cols; k++)
                    {
                        sum += a.matrix[i, k] * b.matrix[k, j];
                    }
                    result.matrix[i, j] = sum;
                }
            }

            return result;
        }

        //"Flip" the matrix 90 degrees and with each element in the same order
        internal static Matrix transpose(Matrix matrix)
        {
            Matrix result = new Matrix(matrix.cols, matrix.rows);
            for (int i = 0; i < matrix.rows; i++)
            {
                for (int j = 0; j < matrix.cols; j++)
                {
                    result.matrix[j, i] = matrix.matrix[i, j];
                }
            }

            return result;
        }

        //Apply a function to all elements in the matrix
        internal static Matrix map(Matrix mat,Func<float, float> function)
        {
            Matrix outP = new Matrix(mat.rows, mat.cols);

            for (int i = 0; i < mat.rows; i++)
            {
                for (int j = 0; j < mat.cols; j++)
                {
                    outP.matrix[i, j] = function(mat.matrix[i, j]);
                }
            }

            return outP;
        }

            //Subtract two matrices from eachother
            internal static Matrix subtract(Matrix a, Matrix b)
        {
            Matrix result = new Matrix(a.rows, a.cols);

            for (int i = 0; i < result.rows; i++)
            {
                for (int j = 0; j < result.cols; j++)
                {
                    result.matrix[i, j] = a.matrix[i, j] - b.matrix[i, j];
                }
            }
            return result;
        }

        //Add two matrices togheter
        internal static Matrix add(Matrix a, Matrix b)
        {
            Matrix result = new Matrix(a.rows, a.cols);

            for (int i = 0; i < a.rows; i++)
            {
                for (int j = 0; j < a.cols; j++)
                {
                    result.matrix[i, j] = a.matrix[i, j] + b.matrix[i, j];
                }
            }

            return result;
        }
    }
}
