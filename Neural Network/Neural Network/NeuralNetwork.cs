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

        float lr;

        Matrix weights_InHid;
        Matrix weights_HidOut;

        Matrix bias_Hidden;
        Matrix bias_Out;

        public NeuralNetwork(int inputNodes, int hiddenNodes, int outPutnodes, float lr)
        {
            //Asign the amount of nodes specified
            this.inputNodes = inputNodes;
            this.hiddenNodes = hiddenNodes;
            this.outPutnodes = outPutnodes;

            this.lr = lr;

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

        //clamp x between 0 and 1
        public float sigmoid(float x)
        {
            return (float)(1 / (1 + Math.Exp(-x)));
        }
        
        //Derivative of sigmoid (asuming x has already been passed through sigmoid once
        float dSigmoid(float x)
        {
            return x * (1 - x);
        }

        //Print the weights in the network
        public void printWeights()
        {
            Console.WriteLine("Input to hidden");
            weights_InHid.print();
            Console.WriteLine();
            Console.WriteLine("Hidden to output");
            weights_HidOut.print();
        }

        //Print the bias in the network
        public void printBias()
        {
            Console.WriteLine("Bias input to hidden");
            bias_Hidden.print();
            Console.WriteLine();
            Console.WriteLine("Bias Hidden to output");
            bias_Out.print();
        }
        public float[] feedForward(float[] input)
        {
            // Generating the Hidden Outputs
            Matrix inputs = Matrix.fromArray(input);
;
            Matrix hidden = Matrix.multiply(weights_InHid, inputs);
            hidden.add(bias_Hidden);

            // activation function!
            hidden.map(sigmoid);

            // Generating the output
            Matrix output = Matrix.multiply(weights_HidOut, hidden);
            output.add(bias_Out);
            output.map(sigmoid);

            //Return
            return output.toArray();
        }

        public void train(float[] input, float[] answer)
        {
            // Generating the Hidden Outputs
            Matrix inputs = Matrix.fromArray(input);
            Matrix hidden = Matrix.multiply(weights_InHid, inputs);
            hidden.add(bias_Hidden);
            // activation function!
            hidden.map(sigmoid);

            // Generating the output's output!
            Matrix outputs = Matrix.multiply(weights_HidOut, hidden);
            outputs.add(bias_Out);
            outputs.map(sigmoid);

            // Convert array to matrix object
            Matrix targets = Matrix.fromArray(answer);

            // Calculate the error
            // ERROR = TARGETS - OUTPUTS
            Matrix output_errors = Matrix.subtract(targets, outputs);

            // Matrix gradient = outputs * (1 - outputs);
            // Calculate gradient
            Matrix gradients = Matrix.map(outputs, dSigmoid);
            gradients.multiply(output_errors);
            gradients.multiply(lr);

            // Calculate deltas
            Matrix hidden_T = Matrix.transpose(hidden);
            Matrix weight_ho_deltas = Matrix.multiply(gradients, hidden_T);

            // Adjust the weights by deltas
            weights_HidOut.add(weight_ho_deltas);
            // Adjust the bias by its deltas (which is just the gradients)
            bias_Out.add(gradients);

            // Calculate the hidden layer errors
            Matrix who_t = Matrix.transpose(weights_HidOut);
            Matrix hidden_errors = Matrix.multiply(who_t, output_errors);

            // Calculate hidden gradient
            Matrix hidden_gradient = Matrix.map(hidden, dSigmoid);
            hidden_gradient.multiply(hidden_errors);
            hidden_gradient.multiply(lr);

            // Calcuate input->hidden deltas
            Matrix inputs_T = Matrix.transpose(inputs);
            Matrix weight_ih_deltas = Matrix.multiply(hidden_gradient, inputs_T);

            weights_InHid.add(weight_ih_deltas);
            // Adjust the bias by its deltas (which is just the gradients)
            bias_Hidden.add(hidden_gradient);
        }
    }
}