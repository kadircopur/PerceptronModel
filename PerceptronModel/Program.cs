using System;
using System.Collections.Generic;

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
            double[,] dataSet = { { 0.6, 0.5, 1 }, { 0.2, 0.4, 1 }, { -0.3, -0.5, -1 }, { -0.1, -0.1, -1 }, { 0.1, 0.1, 1 }, { -0.2, 0.7, 1 }, { -0.4, -0.2, -1 }, { -0.6, 0.3, -1 } };
            Neuron neuron = new Neuron(dataSet);

            neuron.learn(10);
            Console.WriteLine("Accuracy Rate After 10 epoch Supervised Learning: %{0}", neuron.calculateAccRate() * 100);
            neuron.learn(90);
            Console.WriteLine("Accuracy Rate After 100 epoch Supervised Learning: %{0}", neuron.calculateAccRate() * 100);

            // Test Datas
            double[,] testDataSet_1 = { { 0.6, 0.4, 1 }, { -0.7, 0.3, -1 }, { 0.9, 0.2, 1 }, { 0.4, -0.6, -1 }, { 0.5, 0.1, 1 }, { -0.8, -0.3, -1 }, { -0.2, 0.7, 1 }, { 0.8, 0.3, 1 } };
            double[,] testDataSet_2 = { { 0.2, -0.1, 1 }, { -0.5, 0.6, 1 }, { 0.6, -0.1, 1 }, { 0.2, -0.8, -1 }, { 0.1, -0.3, -1 }, { -0.4, 0.1, -1 }, { 0.6, -0.3, 1 }, { -0.5, 0.1, -1 } };
            double[,] testDataSet_3 = { { 0.9, -0.3, 1 }, { 0.4, -0.6, -1 }, { 0.7, 0.3, 1 }, { 0.8, -0.2, 1 }, { -0.5, 0.4, -1 }, { 0.1, 0.3, 1 }, { 0.5, -0.4, 1 }, { 0.7, -0.6, 1 } };
            double[,] testDataSet_4 = { { -0.1, 0.2, 1 }, { 0.7, -0.9, -1 }, { -0.2, -0.6, -1 }, { 0.8, 0.6, 1 }, { 0.4, 0.8, 1 }, { -0.4, -0.5, -1 }, { -0.3, -0.2, -1 }, { -0.2, 0.6, 1 } };
            double[,] testDataSet_5 = { { 0.3, 0.7, 1 }, { 0.1, -0.8, -1 }, { 0.8, -0.2, 1 }, { -0.7, -0.4, -1 }, { -0.6, -0.4, -1 }, { 0.7, 0.2, 1 }, { -0.7, -0.1, -1 }, { 0.6, 0.8, 1 } };

            List<double[,]> testDatas = new List<double[,]>();
            testDatas.Add(testDataSet_1);
            testDatas.Add(testDataSet_2);
            testDatas.Add(testDataSet_3);
            testDatas.Add(testDataSet_4);
            testDatas.Add(testDataSet_5);

            Console.WriteLine("\nAccuracy Rates For Test Datas");
            Console.WriteLine("-----------------------------");

            for (int i = 0; i < testDatas.Count; i++)
            {
                neuron.dataSet = testDatas[i];
                Console.WriteLine("Accuracy rate for testData-{0}: %{1}", i + 1, neuron.calculateAccRate() * 100);
            }
        }
    }

    class Neuron
    {
        const double lambda = 0.05; // learning coefficient
        static Random r = new Random();

        public double[,] dataSet;
        public double[] weightSet;

        public Neuron(double[,] dataSet)
        {
            this.dataSet = dataSet;
            weightSet = generateRandomWeights();
        }

        // Generates random 2 weights' values for inputs
        private double[] generateRandomWeights()
        {
            double[] randomWeights = new double[dataSet.GetLength(1) - 1];

            for (int i = 0; i < randomWeights.GetLength(0); i++)
            {
                randomWeights[i] = r.NextDouble(-1, 1);
            }
            return randomWeights;
        }

        // Calculates the output value of inputs
        private int calculateOutput(double[] data)
        {
            if ((data[0] * weightSet[0]) + (data[1] * weightSet[1]) < 0)
            {
                return -1;
            }
            return 1;
        }

        // Changes the weights' values if target and output are not equal
        private void editWeights(double[] data)
        {
            if (data[2] < 0 && calculateOutput(data) == 1)
            {
                weightSet[0] += lambda * -2 * data[0];
                weightSet[1] += lambda * -2 * data[1];
            }
            else if (data[2] > 0 && calculateOutput(data) == -1)
            {
                weightSet[0] += lambda * 2 * data[0];
                weightSet[1] += lambda * 2 * data[1];
            }
        }

        // Edits weights according to input values and it does that until epoch number is reached
        public void learn(int epokNumber)
        {
            for (int j = 0; j < epokNumber; j++)
            {
                for (int i = 0; i < dataSet.GetLength(0); i++)
                {
                    double[] data = new double[dataSet.GetLength(1)];
                    data[0] = dataSet[i, 0]; data[1] = dataSet[i, 1]; data[2] = dataSet[i, 2];

                    editWeights(data);
                }
            }
        }

        // Finds the accuracy rate 
        public double calculateAccRate()
        {
            double dataLength = dataSet.GetLength(0);
            double accNum = 0;

            for (int i = 0; i < dataSet.GetLength(0); i++)
            {
                double[] data = new double[dataSet.GetLength(1)];
                data[0] = dataSet[i, 0]; data[1] = dataSet[i, 1]; data[2] = dataSet[i, 2];

                if (((dataSet[i, 2] < 0) && calculateOutput(data) == -1) || ((dataSet[i, 2] > 0) && calculateOutput(data) == 1))
                {
                    accNum++;
                }
            }
            return (accNum / dataLength);
        }
    }
}


