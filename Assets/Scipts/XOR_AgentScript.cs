using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using NeuralNetwork;

public class XOR_AgentScript : MonoBehaviour
{
    public NeuralNet net;
    public NeuralNet previousNet;
    public int inputSize;
    public int hiddenSize;
    public int numHiddenLayers;
    public int outputSize;
    public int agentNum;

    // public double[,] inputData;
    public double[] outputs;
    public double fitness = 0.0;
    public double totalDifferenceCounter = 0.0;

    public int NumCorrect;
    public float percentageCorrect;

    UnityEngine.Renderer renderer;
    public TMP_Text displayText;


    void Start()
    {
      MultipleAgentsScript multipleAgents = GetComponentInParent<MultipleAgentsScript>();
      inputSize = multipleAgents.inputSize;
      hiddenSize = multipleAgents.hiddenSize;
      numHiddenLayers = multipleAgents.numHiddenLayers;
      outputSize = multipleAgents.outputSize;
      net = new NeuralNet(inputSize, hiddenSize, numHiddenLayers, outputSize);

      renderer = GetComponent<Renderer>();
    }

    public void UpdatePreviousNet(){
      previousNet = net;
    }


    public void ChangeColor(double output){
      if (output == 0){
        renderer.material.SetColor("_Color", Color.black);
      }
      else if (output == 1){
        renderer.material.SetColor("_Color", Color.white);
      }
    }

    public void ChangeText(double output){
      displayText.text = output.ToString();
    }

    public void Flash(double output){
      if (output == 1){
        GetComponent<ParticleSystem>().Play();
      }
    }

    public void move(double[] pos){
      transform.position += new Vector3(  ExtendedMath.Map((float)pos[0], 0f, 1f, -1f, 1f),
                                          ExtendedMath.Map((float)pos[1], 0f, 1f, -1f, 1f),
                                          ExtendedMath.Map((float)pos[2], 0f, 1f, -1f, 1f));
      transform.position += Random.insideUnitSphere;
    }

    private float GetEnergy(){
      float returnEnergy;
      float distance = Vector3.Distance(Vector3.zero, transform.position);
      returnEnergy = ExtendedMath.Map(distance, 0, 100, 0.1f, 0);
      return returnEnergy;
    }

    public double[] calculateGuess(double[] inputs){
      double[] outputData = net.Compute(inputs);
      outputs = outputData;
      return outputData;
    }

}
