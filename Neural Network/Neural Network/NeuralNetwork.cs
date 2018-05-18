//Created by Marcus Jansson on 2018-02-26
//Neural Network Library
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

        double lr;

        Matrix weights_InHid;
        Matrix weights_HidOut;

        Matrix bias_Hidden;
        Matrix bias_Out;

        FunctionHandler activationFunction;

        public NeuralNetwork(int inputNodes, int hiddenNodes, int outPutnodes, double lr, FunctionHandler activationFunction)
        {
            //Asign the amount of nodes specified
            this.inputNodes = inputNodes;
            this.hiddenNodes = hiddenNodes;
            this.outPutnodes = outPutnodes;

            this.lr = lr;
            this.activationFunction = activationFunction;

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

        /// <summary>
        /// Serialize the Neural Net to a folder
        /// </summary>
        /// <param name="path">
        /// The path to the folder where the network will be saved
        /// </param>
        public void serialize(string path)
        {
            //Serialize the weights
            weights_InHid.serialize(path + @"\inHid.txt");
            weights_HidOut.serialize(path + @"\hidOut.txt");

            //Serialize the biases
            bias_Hidden.serialize(path + @"\biasHidden.txt");
            bias_Out.serialize(path + @"\biasOut.txt");
        }

        public void deSerialize(string path)
        {
            //Deserialize the weights
            weights_InHid.deSerialize(path + @"\inHid.txt");
            weights_HidOut.deSerialize(path + @"\hidOut.txt");

            //Deserialize the biases
            bias_Hidden.deSerialize(path + @"\biasHidden.txt");
            bias_Out.deSerialize(path + @"\biasOut.txt");
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

        public void setLR(double lr)
        {
            this.lr = lr;
        }
        public void setFunc(FunctionHandler func)
        {
            activationFunction = func;
        }

        public double[] feedForward(double[] input)
        {
            // Generating the Hidden Outputs
            Matrix inputs = Matrix.fromArray(input);
;
            Matrix hidden = Matrix.multiply(weights_InHid, inputs);
            hidden.add(bias_Hidden);

            // activation function!
            hidden.map(activationFunction.activeFunc);

            // Generating the output
            Matrix output = Matrix.multiply(weights_HidOut, hidden);
            output.add(bias_Out);
            output.map(activationFunction.activeFunc);

            //Return
            return output.toArray();
        }

        public void train(double[] input, double[] answer)
        {
            //Generating the Hidden Outputs
            Matrix inputs = Matrix.fromArray(input);
            Matrix hidden = Matrix.multiply(weights_InHid, inputs);
            hidden.add(bias_Hidden);

            //activation function
            hidden.map(activationFunction.activeFunc);

            //Generating the output's output!
            Matrix outputs = Matrix.multiply(weights_HidOut, hidden);
            outputs.add(bias_Out);
            outputs.map(activationFunction.activeFunc);

            //Convert array to matrix object
            Matrix targets = Matrix.fromArray(answer);

            //Calculate the error
            Matrix output_errors = Matrix.subtract(targets, outputs);

            //Calculate gradient
            Matrix gradients = Matrix.map(outputs, activationFunction.deriveFunc);
            gradients.multiply(output_errors);
            gradients.multiply(lr);

            //Calculate deltas
            Matrix hidden_T = Matrix.transpose(hidden);
            Matrix weight_ho_deltas = Matrix.multiply(gradients, hidden_T);

            //Adjust the weights by deltas
            weights_HidOut.add(weight_ho_deltas);

            //Adjust the bias by its deltas (which is just the gradients)
            bias_Out.add(gradients);

            //Calculate the hidden layer errors
            Matrix who_t = Matrix.transpose(weights_HidOut);
            Matrix hidden_errors = Matrix.multiply(who_t, output_errors);

            //Calculate hidden gradient
            Matrix hidden_gradient = Matrix.map(hidden, activationFunction.deriveFunc);
            hidden_gradient.multiply(hidden_errors);
            hidden_gradient.multiply(lr);

            // Calcuate the input to hidden deltas
            Matrix inputs_T = Matrix.transpose(inputs);
            Matrix weight_ih_deltas = Matrix.multiply(hidden_gradient, inputs_T);

            //Add the deltas
            weights_InHid.add(weight_ih_deltas);

            //Adjust the bias by its deltas (which is just the gradients)
            bias_Hidden.add(hidden_gradient);
        }
    }
}

//Class to handle lots of activation functions
public class FunctionHandler
{
    public Func<double, double> activeFunc;
    public Func<double, double> deriveFunc;

    public FunctionHandler(Func<double, double> activeFunc, Func<double, double> deriveFunc)
    {
        this.activeFunc = activeFunc;
        this.deriveFunc = deriveFunc;
    }
}