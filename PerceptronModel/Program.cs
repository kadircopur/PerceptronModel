using System;

namespace PerceptronModel
{
    public static class RandomExtensions
    {
        public static double NextDouble(
            this Random random,
            double minValue,
            double maxValue)
        {
            return random.NextDouble() * (maxValue - minValue) + minValue;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();
            
            double[] x1Inputs = {0.6, 0.2, -0.3, -0.1, 0.1, -0.2, -0.4, -0.6};
            double[] x2Inputs = { 0.5, 0.4, -0.5, -0.1, 0.1, 0.7, -0.2, -0.3 };
            double[] weights = new double[x1Inputs.Length];
           
            for (int i = 0; i < x1Inputs.Length; i++)
            {
                weights[i] = r.NextDouble(-1, 1);
            }

        }
    }

    class Neuron
    {
        public double[] x1Inputs;
        public double[] x2Inputs;
        public double[] weights;

        
        
        public Neuron(double[] x1Inputs, double[] x2Inputs, double[] weights)
        {
            this.x1Inputs = x1Inputs;
            this.x2Inputs = x2Inputs;
            this.weights = weights;
        }
        
        public int[] calculateOutput()
        {
            int[] outputs = new int[weights.Length]; 
            for (int i = 0; i < x1Inputs.Length; i++)
            {
                if ((x1Inputs[i] * weights[i]) + (x2Inputs[i] * weights[i]) < 0)
                {
                    outputs[i] = -1;
                }
                else
                {
                    outputs[i] = 1;
                }
            }

            return outputs;
        }

        public void editWeights()
        {
            int[] outputs = calculateOutput();
            for (int i = 0; i < x1Inputs.Length; i++)
            {
                if (((x1Inputs[i]  + (x2Inputs[i]) < 0) && outputs[i] == 1))
                {
                    
                }

                if (((x1Inputs[i] + (x2Inputs[i]) > 0) && outputs[i] == -1))
                {

                }
            }
        }
    }
}

