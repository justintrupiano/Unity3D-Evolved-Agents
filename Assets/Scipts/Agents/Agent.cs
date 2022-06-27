using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NeuralNetwork;

public class Agent : MonoBehaviour
{
    public NeuralNet net;
    public NeuralNet previousNet;
    public int inputSize;
    public int hiddenSize;
    public int numHiddenLayers;
    public int outputSize;

    public double[] outputs;
    public double fitness = 0.0;

    public int agentNum;

    public void NewAgent()
    {
        MultipleAgents multipleAgents = GetComponentInParent<MultipleAgents>();
        if (multipleAgents == null){
            Debug.LogError("Agent script must be attached to a gameobject with a MultipleAgents script");
        }

        inputSize = multipleAgents.inputSize;
        hiddenSize = multipleAgents.hiddenSize;
        numHiddenLayers = multipleAgents.numHiddenLayers;
        outputSize = multipleAgents.outputSize;
        net = new NeuralNet(inputSize, hiddenSize, numHiddenLayers, outputSize);

    }

    public void UpdatePreviousNet(){
      previousNet = net;
    }

    public double[] calculateOutputs(double[] inputs){
      double[] outputData = net.Compute(inputs);
      outputs = outputData;
      return outputData;
    }
}
