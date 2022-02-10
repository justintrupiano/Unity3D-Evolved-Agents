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

    public float energy;
    public bool alive;
    public int NumCorrect;


    UnityEngine.Renderer renderer;
    public TMP_Text displayText;
    public ParticleSystem particleSystem;

    public float percentageCorrect;

    void Start()
    {
      MultipleAgentsScript multipleAgents = GetComponentInParent<MultipleAgentsScript>();
      inputSize = multipleAgents.inputSize;
      hiddenSize = multipleAgents.hiddenSize;
      numHiddenLayers = multipleAgents.numHiddenLayers;
      outputSize = multipleAgents.outputSize;
      // energy = multipleAgents.startEnergy;
      // alive = true;
      net = new NeuralNet(inputSize, hiddenSize, numHiddenLayers, outputSize);

      // transform.position = new Vector3(0, agentNum*1.25f,0);

      renderer = GetComponent<Renderer>();

      // Debug.Log(agentNum);
      // NNCopy.PrintWeightsAndBias(net);


      // inputData = new double[2, 2];
    }

    // void Update(){
    //   // energy += GetEnergy();
    // }

    public void UpdatePreviousNet(){
      previousNet = net;
    }

    // public void calculateMovement(){
    //   double[] inputs = new double[inputSize];
    //
    //   inputs[0] = energy;
    //   inputs[1] = transform.position.x;
    //   inputs[2] = transform.position.y;
    //   inputs[3] = transform.position.z;
    //
    //   double[] outputData = net.Compute(inputs);
    //   outputs = outputData;
    //   move(outputData);
    // }

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
        particleSystem.Play();
        // Debug.Log("Play");
      }
    }

    public void move(double[] pos){
      transform.position += new Vector3(  MultipleAgentsScript.Map((float)pos[0], 0f, 1f, -1f, 1f),
                                          MultipleAgentsScript.Map((float)pos[1], 0f, 1f, -1f, 1f),
                                          MultipleAgentsScript.Map((float)pos[2], 0f, 1f, -1f, 1f));
      transform.position += Random.insideUnitSphere;
    }

    private float GetEnergy(){
      float returnEnergy;
      float distance = Vector3.Distance(Vector3.zero, transform.position);
      // print(distance);
      returnEnergy = MultipleAgentsScript.Map(distance, 0, 100, 0.1f, 0);
      return returnEnergy;
    }

    public double[] calculateGuess(double[] inputs){
      double[] outputData = net.Compute(inputs);
      outputs = outputData;
      return outputData;
    }

    // public void ClearDiffCounter(){
    //   totalDifferenceCounter = 0.0;
    // }



    // double[] getOutputs(double[] inputs){
    //   double[] outputData = net.Compute(inputs);
    //   return outputData;
    // }



}
