using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NeuralNetwork
{
  public class NNCopy
  {

    public static void PrintWeightsAndBias(NeuralNet NN){

      //// Input Layer
      // Debug.Log("///// INPUT LAYER /////");

      foreach (Neuron neuron in NN.InputLayer){
        // Debug.Log(neuron);
        // Debug.Log("///// Bias /////");
        Debug.Log(neuron.Bias);
        // Debug.Log("///// Input Weights /////");
        foreach(Synapse synapse in neuron.InputSynapses){
          Debug.Log(synapse.Weight);
        }
        // Debug.Log("///// Output Weights /////");
        foreach(Synapse synapse in neuron.OutputSynapses){
          Debug.Log(synapse.Weight);
        }
      }

      //// Hidden Layers
      // Debug.Log("///// HIDDEN LAYERS /////");

      foreach(List<Neuron> hiddenLayer in NN.HiddenLayers){
        foreach (Neuron neuron in hiddenLayer){
          // Debug.Log(neuron);
          // Debug.Log("///// Bias /////");
          Debug.Log(neuron.Bias);

          // Debug.Log("///// Input Weights /////");
          foreach(Synapse synapse in neuron.InputSynapses){
            Debug.Log(synapse.Weight);
          }

          // Debug.Log("///// Output Weights /////");
          foreach(Synapse synapse in neuron.OutputSynapses){
            Debug.Log(synapse.Weight);
          }
        }
      }

      //// Output Layer
      // Debug.Log("///// OUTPUT LAYER /////");

      foreach (Neuron neuron in NN.OutputLayer){
        // Debug.Log(neuron);
        // Debug.Log("///// Bias /////");
        Debug.Log(neuron.Bias);

        // Debug.Log("///// Input Weights /////");
        foreach(Synapse synapse in neuron.InputSynapses){
          Debug.Log(synapse.Weight);
        }

        // Debug.Log("///// Output Weights /////");
        foreach(Synapse synapse in neuron.OutputSynapses){
          Debug.Log(synapse.Weight);
        }
      }
    }

    //// END PrintWeightsAndBias


    //
    // public static void NewCopy(NeuralNet NN1, NeuralNet NN2){
    //   // NeuralNet:
    //   // List<Neuron> InputLayer
    //   // List<List<Neuron>> HiddenLayers
    //   // List<Neuron> OutputLayer
    //
    //   // Neuron:
    //   // List<Synapse> InputSynapses
    //   // List<Synapse> OutputSynapses
    //   // double Bias
    //
    //   // Synapse:
    //   // Neuron InputNeuron
    //   // Neuron OutputNeuron
    //   // double Weight
    //
    //   for (int i = 0; i< NN1.InputLayer.Count; i++){
    //     Debug.Log("Hello");
    //   }
    // }

    public static void MutateWeightsAndBias(NeuralNet NN, float _mutationRate, float _mutationAmount){

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
    }
    //
    // public static NeuralNet ReturnMutatedNN(NeuralNet inputNN, float _mutationRate, float _mutationAmount){
    //
    //   NeuralNet NN = new NeuralNet(inputNN.inputSize, inputNN.numHiddenLayers, inputNN.hiddenSize, inputNN.outputSize);
    //   NN = inputNN;
    //
    //   //// Input Layer
    //   foreach (Neuron neuron in NN.InputLayer){
    //     if (Random.value < _mutationRate)
    //       neuron.Bias = Random.Range(-_mutationAmount, _mutationAmount);
    //
    //     foreach(Synapse synapse in neuron.InputSynapses)
    //       if (Random.value < _mutationRate)
    //         synapse.Weight = Random.Range(-_mutationAmount, _mutationAmount);
    //
    //     foreach(Synapse synapse in neuron.OutputSynapses)
    //       if (Random.value < _mutationRate)
    //         synapse.Weight = Random.Range(-_mutationAmount, _mutationAmount);
    //
    //   }
    //
    //   //// Hidden Layers
    //   foreach(List<Neuron> hiddenLayer in NN.HiddenLayers){
    //     foreach (Neuron neuron in hiddenLayer){
    //       if (Random.value < _mutationRate)
    //         neuron.Bias = Random.Range(-_mutationAmount, _mutationAmount);
    //
    //       foreach(Synapse synapse in neuron.InputSynapses){
    //         if (Random.value < _mutationRate)
    //           synapse.Weight = Random.Range(-_mutationAmount, _mutationAmount);
    //       }
    //       foreach(Synapse synapse in neuron.OutputSynapses){
    //         if (Random.value < _mutationRate)
    //           synapse.Weight = Random.Range(-_mutationAmount, _mutationAmount);
    //       }
    //     }
    //   }
    //
    //   //// Output Layer
    //   foreach (Neuron neuron in NN.OutputLayer){
    //     if (Random.value < _mutationRate)
    //       neuron.Bias = Random.Range(-_mutationAmount, _mutationAmount);
    //     foreach(Synapse synapse in neuron.InputSynapses){
    //       if (Random.value < _mutationRate)
    //         synapse.Weight = Random.Range(-_mutationAmount, _mutationAmount);
    //     }
    //     foreach(Synapse synapse in neuron.OutputSynapses){
    //       if (Random.value < _mutationRate)
    //         synapse.Weight = Random.Range(-_mutationAmount, _mutationAmount);
    //     }
    //   }
    //
    //   return NN;
    // }




    public static void RandomizeWeightsAndBias(NeuralNet NN){
      //// Input Layer
      foreach (Neuron neuron in NN.InputLayer){
        neuron.Bias = Random.Range(-1.0f, 1.0f);

        foreach(Synapse synapse in neuron.InputSynapses)
          synapse.Weight = Random.Range(-1.0f, 1.0f);

        foreach(Synapse synapse in neuron.OutputSynapses)
          synapse.Weight = Random.Range(-1.0f, 1.0f);

      }

      //// Hidden Layers
      foreach(List<Neuron> hiddenLayer in NN.HiddenLayers){
        foreach (Neuron neuron in hiddenLayer){
          neuron.Bias = Random.Range(-1.0f, 1.0f);
          foreach(Synapse synapse in neuron.InputSynapses){
            synapse.Weight = Random.Range(-1.0f, 1.0f);
          }
          foreach(Synapse synapse in neuron.OutputSynapses){
            synapse.Weight = Random.Range(-1.0f, 1.0f);
          }
        }
      }

      //// Output Layer
      foreach (Neuron neuron in NN.OutputLayer){
        neuron.Bias = Random.Range(-1.0f, 1.0f);
        foreach(Synapse synapse in neuron.InputSynapses){
          synapse.Weight = Random.Range(-1.0f, 1.0f);
        }
        foreach(Synapse synapse in neuron.OutputSynapses){
          synapse.Weight = Random.Range(-1.0f, 1.0f);
        }
      }
    }



  public static NeuralNet copyTwo( NeuralNet NN_A, NeuralNet NN_B, float noise) ///
  {
    NeuralNet output = NN_A; // This just makes the new network the same size;
    bool copyOther = (Random.value < noise) ? true : false;
    // bool copyOther = true;

    int inputSize = NN_A.inputSize;
    int hiddenSize = NN_A.hiddenSize;
    int numHiddenLayers = NN_A.numHiddenLayers;
    int outputSize = NN_A.outputSize;

    //// INPUT LAYERS ////
    for (int y = 0; y < hiddenSize; y++) /// NUMBER OF HIDDEN NODES
    {
      for (int z = 0; z < inputSize; z++) /// NUMBER OF INPUT NODES
      {
        if (Random.value < noise)
          copyOther = !copyOther;
        if (copyOther)
        {
          output.InputLayer[z].OutputSynapses[y].Weight = NN_B.InputLayer[z].OutputSynapses[y].Weight;
          output.HiddenLayers[0][y].InputSynapses[z].Weight = NN_B.HiddenLayers[0][y].InputSynapses[z].Weight;
        }
      }
    }
    ///// Biases
    for (int x = 0; x < inputSize; x++)
    {
      if (Random.value < noise)
        copyOther = !copyOther;
      if (copyOther)
        output.InputLayer[x].Bias = NN_B.InputLayer[x].Bias;

      // Mutate
      // if (Random.value < _mutationAmount)
      //   NN_A.InputLayer[x].Bias += Random.Range(-0.01f, 0.01f);
    }


    //// HIDDEN LAYERS ////
    for (int x = 0; x < numHiddenLayers-1; x++)
    {  /// NUMBER OF HIDDEN LAYERS
      for (int y = 0; y < hiddenSize; y++)
      {       /// NUMBER OF HIDDEN NODES
        for (int z = 0; z < hiddenSize; z++){     /// NUMBER OF OUTPUT NODES
            if (Random.value < noise)
              copyOther = !copyOther;
            if (copyOther)
            {
              output.HiddenLayers[x][y].OutputSynapses[z].Weight = NN_B.HiddenLayers[x][y].OutputSynapses[z].Weight;

              // if (Random.value < _mutationAmount)
              //   NN_A.HiddenLayers[x][y].OutputSynapses[z].Weight += Random.Range(-0.01f, 0.01f);

              //// Copy output of one layer to inputs of next layer.
              output.HiddenLayers[x+1][z].InputSynapses[y].Weight = NN_A.HiddenLayers[x][y].OutputSynapses[z].Weight;
            }
          }
        }
      }
    //// HIDDEN BIASES
    for (int x = 0; x < numHiddenLayers; x++) /// NUMBER OF HIDDEN LAYERS
      {
        for (int y = 0; y < hiddenSize; y++) /// NUMBER OF HIDDEN LAYERS
          {
            if (Random.value < noise)
              copyOther = !copyOther;
            if (copyOther)
            {
              output.HiddenLayers[x][y].Bias = NN_B.HiddenLayers[x][y].Bias;

              // if (Random.value < _mutationAmount)
              //   NN_A.HiddenLayers[x][y].Bias += Random.Range(-0.01f, 0.01f);
            }
          }
        }


    //// OUTPUT LAYERS
    for (int x = 0; x < hiddenSize; x++){
      for (int y = 0; y < hiddenSize; y++){   /// NUMBER OF HIDDEN NODES
        for (int z = 0; z < outputSize; z++){ /// NUMBER OF OUTPUT NODES
          if (Random.value < noise)
            copyOther = !copyOther;
          if (copyOther)
          {
            output.HiddenLayers[numHiddenLayers-1][y].OutputSynapses[z].Weight = NN_B.HiddenLayers[numHiddenLayers-1][y].OutputSynapses[z].Weight;

            // if (Random.value < noise)
            //   NN_A.HiddenLayers[numHiddenLayers-1][y].OutputSynapses[z].Weight += Random.Range(-0.1f, 0.1f);
          }
        }
      }
    }
    //// OUTPUT BIASES
    for (int x = 0; x < outputSize; x++){ /// NUMBER OF OUTPUT NODES
      if (Random.value < noise)
        copyOther = !copyOther;
      if (copyOther)
      {
        output.OutputLayer[x].Bias = NN_B.OutputLayer[x].Bias;

        // if (Random.value < noise)
        //   NN_A.OutputLayer[x].Bias += Random.Range(-0.1f, 0.1f);
      }
    }
    return output;

  }

}
}
