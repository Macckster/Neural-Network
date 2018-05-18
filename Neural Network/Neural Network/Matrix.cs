//Created by Marcus Jansson on 2018-02-21
//Neural Network Library
//GPLV3 License

using System;
using System.IO;

namespace Neural_Network
{
    class Matrix
    {
        private Random prng = new Random(); //Random object used to randomize the matrix

        public int rows, cols; //Amount of rows and colums in the matrix
        public double[,] matrix; //The object that holds all the data

        //Constructor
        public Matrix(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;

            matrix = new double[rows, cols];
        }

        //Add a scalar value to all elements in the matrix
        public void add(double n)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] += n;
                }
            }
        }

        //Add each of the values in one matrix to another given that they are the same size
        public void add(Matrix mat)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] += mat.matrix[i,j];
                }
            }
        }

        //Convert the matrix to a 1D array
        public double[] toArray()
        {
            double[] output = new double[cols * rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    output[i * cols + j] = matrix[i, j];
                }
            }
            return output;
        }

        //Multiply each index with a scalar value
        public void multiply(double n)
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

        //Generate a random value betwen -1 and 1 (inclusive) for each element in the array. These values are doubles
        public void randomize()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] += (double)prng.NextDouble() * 2 - 1;
                }
            }
        }

        //Apply a function to all elements in the matrix
        public void map(Func<double, double> function)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = function(matrix[i, j]);
                }
            }
        }

        //Dump the contents of the matrix in to a file so that you can save the values for later use
        public void serialize(string path)
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
        
        //Read the contents of a file and asign all the numbers in it to their corresponding position in the matrix
        public void deSerialize(string path)
        {
            if (!File.Exists(path))
            {
                return;
            }

            string[] s = File.ReadAllLines(path);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = double.Parse(s[cols * i + j]);
                }
            }
        }

        //Print the values of the matrix
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
        public static Matrix fromArray(double[] input)
        {
            Matrix m = new Matrix(input.Length, 1);

            for (int i = 0; i < input.Length; i++)
            {
                m.matrix[i, 0] = input[i];
            }
            return m;
        }


        //Multiply two matrices togheter
        public static Matrix multiply(Matrix a, Matrix b)
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
                    double sum = 0;
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
        public static Matrix transpose(Matrix mat)
        {
            Matrix result = new Matrix(mat.cols, mat.rows);
            for (int i = 0; i < mat.rows; i++)
            {
                for (int j = 0; j < mat.cols; j++)
                {
                    result.matrix[j, i] = mat.matrix[i, j];
                }
            }
            return result;
        }

        //Apply a function to all elements in the matrix
        public static Matrix map(Matrix mat,Func<double, double> function)
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
        public static Matrix subtract(Matrix a, Matrix b)
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
        public static Matrix add(Matrix a, Matrix b)
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