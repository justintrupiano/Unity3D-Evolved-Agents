using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NeuralNetwork
{
  public class NNCopy
  {

    public static void PrintWeightsAndBias(NeuralNet NN){

      //// Input Layer

      foreach (Neuron neuron in NN.InputLayer){
        Debug.Log(neuron.Bias);
        foreach(Synapse synapse in neuron.InputSynapses){
          Debug.Log(synapse.Weight);
        }
        foreach(Synapse synapse in neuron.OutputSynapses){
          Debug.Log(synapse.Weight);
        }
      }

      //// Hidden Layers
      foreach(List<Neuron> hiddenLayer in NN.HiddenLayers){
        foreach (Neuron neuron in hiddenLayer){
          Debug.Log(neuron.Bias);
          foreach(Synapse synapse in neuron.InputSynapses){
            Debug.Log(synapse.Weight);
          }
          foreach(Synapse synapse in neuron.OutputSynapses){
            Debug.Log(synapse.Weight);
          }
        }
      }

      //// Output Layer
      foreach (Neuron neuron in NN.OutputLayer){
        Debug.Log(neuron.Bias);
        foreach(Synapse synapse in neuron.InputSynapses){
          Debug.Log(synapse.Weight);
        }
        foreach(Synapse synapse in neuron.OutputSynapses){
          Debug.Log(synapse.Weight);
        }
      }
    }

    //// END PrintWeightsAndBias


    public static NeuralNet MutateWeightsAndBias(NeuralNet NN, float _mutationRate, float _mutationAmount){
      //// Input Layer
      foreach (Neuron neuron in NN.InputLayer){
        if (Random.value < _mutationRate)
          neuron.Bias += Random.Range(-_mutationAmount, _mutationAmount);

        foreach(Synapse synapse in neuron.InputSynapses)
          if (Random.value < _mutationRate)
            synapse.Weight += Random.Range(-_mutationAmount, _mutationAmount);

        foreach(Synapse synapse in neuron.OutputSynapses)
          if (Random.value < _mutationRate)
            synapse.Weight += Random.Range(-_mutationAmount, _mutationAmount);

      }

      //// Hidden Layers
      foreach(List<Neuron> hiddenLayer in NN.HiddenLayers){
        foreach (Neuron neuron in hiddenLayer){
          if (Random.value < _mutationRate)
            neuron.Bias += Random.Range(-_mutationAmount, _mutationAmount);

          foreach(Synapse synapse in neuron.InputSynapses){
            if (Random.value < _mutationRate)
              synapse.Weight += Random.Range(-_mutationAmount, _mutationAmount);
          }
          foreach(Synapse synapse in neuron.OutputSynapses){
            if (Random.value < _mutationRate)
              synapse.Weight += Random.Range(-_mutationAmount, _mutationAmount);
          }
        }
      }

      //// Output Layer
      foreach (Neuron neuron in NN.OutputLayer){
        if (Random.value < _mutationRate)
          neuron.Bias += Random.Range(-_mutationAmount, _mutationAmount);
        foreach(Synapse synapse in neuron.InputSynapses){
          if (Random.value < _mutationRate)
            synapse.Weight += Random.Range(-_mutationAmount, _mutationAmount);
        }
        foreach(Synapse synapse in neuron.OutputSynapses){
          if (Random.value < _mutationRate)
            synapse.Weight += Random.Range(-_mutationAmount, _mutationAmount);
        }
      }

      return NN;
    }
   


  public static NeuralNet copyMultiple( List<NeuralNet> Nets, float noise) ///
  {
    int currentIndex = Random.Range(0, Nets.Count);

    int inputSize = Nets[currentIndex].inputSize;
    int hiddenSize = Nets[currentIndex].hiddenSize;
    int numHiddenLayers = Nets[currentIndex].numHiddenLayers;
    int outputSize = Nets[currentIndex].outputSize;

    NeuralNet output = new NeuralNet(inputSize, numHiddenLayers, hiddenSize, outputSize);
    
    //// INPUT LAYERS ////
    for (int y = 0; y < hiddenSize; y++) /// NUMBER OF HIDDEN NODES
    {
      for (int z = 0; z < inputSize; z++) /// NUMBER OF INPUT NODES
      {
        if (Random.value < noise) currentIndex = Random.Range(0, Nets.Count);
        output.InputLayer[z].OutputSynapses[y].Weight = Nets[currentIndex].InputLayer[z].OutputSynapses[y].Weight;
        output.HiddenLayers[0][y].InputSynapses[z].Weight = Nets[currentIndex].HiddenLayers[0][y].InputSynapses[z].Weight;
      }
    }
    ///// Biases
    for (int x = 0; x < inputSize; x++)
    {
      if (Random.value < noise) currentIndex = Random.Range(0, Nets.Count);
      output.InputLayer[x].Bias = Nets[currentIndex].InputLayer[x].Bias;
    }

    //// HIDDEN LAYERS ////
    for (int x = 0; x < numHiddenLayers-1; x++)
    {  /// NUMBER OF HIDDEN LAYERS
      for (int y = 0; y < hiddenSize; y++)
        { /// NUMBER OF HIDDEN NODES
          for (int z = 0; z < hiddenSize; z++){     /// NUMBER OF OUTPUT NODES
           if (Random.value < noise) currentIndex = Random.Range(0, Nets.Count);

          output.HiddenLayers[x][y].OutputSynapses[z].Weight = Nets[currentIndex].HiddenLayers[x][y].OutputSynapses[z].Weight;
          output.HiddenLayers[x+1][z].InputSynapses[y].Weight = Nets[currentIndex].HiddenLayers[x][y].OutputSynapses[z].Weight;
        }
      }
    }
    //// HIDDEN BIASES
    for (int x = 0; x < numHiddenLayers; x++) /// NUMBER OF HIDDEN LAYERS
      {
        for (int y = 0; y < hiddenSize; y++) /// NUMBER OF HIDDEN LAYERS
          {
            if (Random.value < noise) currentIndex = Random.Range(0, Nets.Count);

            output.HiddenLayers[x][y].Bias = Nets[currentIndex].HiddenLayers[x][y].Bias;
            
          }
      }
    

    //// OUTPUT LAYERS
    for (int x = 0; x < hiddenSize; x++){
      for (int y = 0; y < hiddenSize; y++){   /// NUMBER OF HIDDEN NODES
        for (int z = 0; z < outputSize; z++){ /// NUMBER OF OUTPUT NODES
          if (Random.value < noise) currentIndex = Random.Range(0, Nets.Count);

          output.HiddenLayers[numHiddenLayers-1][y].OutputSynapses[z].Weight = Nets[currentIndex].HiddenLayers[numHiddenLayers-1][y].OutputSynapses[z].Weight;
        }
      }
    }
    //// OUTPUT BIASES
    for (int x = 0; x < outputSize; x++)
    { /// NUMBER OF OUTPUT NODES

        if (Random.value < noise) currentIndex = Random.Range(0, Nets.Count);
        output.OutputLayer[x].Bias = Nets[currentIndex].OutputLayer[x].Bias;
    }
    return output;

  }

}
}
