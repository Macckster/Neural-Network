//Created by Marcus Jansson on 2018-02-21
//Part of neural network code
//GPLV3 License
//This document may be reproduced and reused for any purpose


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neural_Network
{
    class Matrix
    {
        private Random prng = new Random();

        public int rows, cols;
        public float[,] matrix;


        //Constructor
        public Matrix(int rows,int cols)
        {
            this.rows = rows;
            this.cols = cols;

            matrix = new float[rows,cols];
        }

        //Add two matrices togheter ir add a scalar value to alla elements in the matrix
        public void add(object n)
        {
            Type t = n.GetType();

            if(t.Equals(typeof(float)) ||t.Equals(typeof(int)))
            {
                //Scalar add
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        matrix[i, j] += float.Parse(n.ToString());
                    }
                }
            }
            else
            {
                //Elementwise add

                float[,] tempArray = (float[,])n;
                
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        matrix[i, j] += tempArray[i, j];
                    }
                }
            }
        }


        //Multiply two matrices togheter or multiply a scalar value to each index
        public Matrix multiply(object n)
        {
            Type t = n.GetType();
            if (t.Equals(typeof(float)) || t.Equals(typeof(int)))
            {
                //Scalar multiply

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        matrix[i, j] *= float.Parse(n.ToString());
                    }
                }

                return new Matrix(0, 0);
            }
            else
            {
                Matrix mat = (Matrix)n;
                //Matrix Product

                if (cols != mat.rows)
                {
                    return new Matrix(69,0);
                }

                Matrix result = new Matrix(rows, mat.cols);

                for (int i = 0; i < result.rows; i++)
                {
                    for (int j = 0; j < result.cols; j++)
                    {
                        //Dot prodcut
                        float sum = 0;
                        for (int k = 0; k < cols; k++)
                        {
                            sum += matrix[i, k] * mat.matrix[k, j];
                        }
                        result.matrix[i, j] = sum;
                    }
                }

                return result;
            }
        }

        //"Flip" the matrix 90 degrees and with each element in the same order
        public Matrix transpose()
        {
            Matrix result = new Matrix(cols, rows);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result.matrix[j, i] = matrix[i, j];
                }
            }

            return result;
        }

        //Generate a random value betwen 0 and 10 (inclusive) for each element in the array. These values are floats
        public void randomize()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] += (float)prng.NextDouble() * 10;
                }
            }
        }
    }
}
