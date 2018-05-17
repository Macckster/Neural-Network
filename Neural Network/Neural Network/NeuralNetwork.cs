//Created by Marcus Jansson on 2018-02-26
//GPLV3 License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neural_Network
{
    class NeuralNetwork
    {
        int inputNodes;
        int hiddenNodes;
        int outPutnodes;

        Matrix weights_InHid;
        Matrix weights_HidOut;

        Matrix bias_Hidden;
        Matrix bias_Out;

        public NeuralNetwork(int inputNodes, int hiddenNodes, int outPutnodes)
        {
            //Asign the amount of nodes specified
            this.inputNodes = inputNodes;
            this.hiddenNodes = hiddenNodes;
            this.outPutnodes = outPutnodes;

            //Create weight matrices
            weights_InHid = new Matrix(this.hiddenNodes, this.inputNodes);
            weights_HidOut = new Matrix(this.outPutnodes, this.hiddenNodes);

            //Randomize starting values in weight matrices
            weights_InHid.randomize();
            weights_HidOut.randomize();

            //Create new matrices for the biases for each connection
            bias_Hidden = new Matrix(hiddenNodes, 1);
            bias_Out = new Matrix(outPutnodes, 1);

            //Randomize the bias matrices
            bias_Hidden.randomize();
            bias_Out.randomize();
        }

        //clamp x between -1 and 1
        float sigmoid(float x)
        {
            return (float)(1 / (1 + Math.Exp(-x)));
        }

        public float[] feedForward(object inputArray)
        {
            //Allows passing argumnents of a simple float array or a already formatted matrix
            Type t = inputArray.GetType();

            Matrix inputs;

            if (!t.Equals(typeof(Matrix)))
            {
                //Convert from an array if that is what was supplied
                 inputs = Matrix.fromArray((float[])inputArray);
            }
            else
            {
                //If a matrix was passed as the argument
                inputs = (Matrix)inputArray;
            }
            
            //Generate hidden outputs
            Matrix hidden = weights_InHid.multiply(inputs);
            hidden.add(bias_Hidden);

            //Activation function
            hidden.map(sigmoid);

            //Generate the outputs
            Matrix output = weights_HidOut.multiply(hidden);
            output.add(bias_Out);

            //Activation function
            output.map(sigmoid);

            //Return output
            return output.toArray();
        }

        public void train(float[] input, float[] answer)
        {
            Matrix outputs = Matrix.fromArray(feedForward(input));
            Matrix targets = Matrix.fromArray(answer);

            //Calculate output error
            Matrix outErrors = Matrix.subtract(targets, outputs);

            //Calculate hidden errors
            Matrix who_t = Matrix.transpose(weights_HidOut);
            Matrix errors_hidden = Matrix.multiplyTwo(who_t, outErrors);

            targets.print();
            outputs.print();
            outErrors.print();
        }
    }
}