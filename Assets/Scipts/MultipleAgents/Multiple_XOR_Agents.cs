using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using NeuralNetwork;

public class Multiple_XOR_Agents : MultipleAgents
{
  // XOR
  public static double[,] inputData = new double[4,2] { {0, 0}, {0, 1}, {1, 0}, {1, 1} };
  public static double[] answerData = new double[4] {0, 1, 1, 0};

  // AND
  // public static double[,] inputData = new double[4,2] { {0, 0}, {0, 1}, {1, 0}, {1, 1} };
  // public static double[] answerData = new double[4] {1, 0, 0, 1};

  public double[] currentTestData = new double[2];

  public int numCorrect;
  public int generationLength;
  public int generationCount;
  private int generationCountDown;

  void Start(){

    AllNewAgents();
    AlignInGrid();
    // This counts down frames
    generationCountDown = generationLength;

    // This stores the index of the agents who enter the genepool
    genePool = new List<int>();

  }

  void Update(){

    RunTests();

    generationCountDown--;
    if (generationCountDown <= 0) {
      generationCount++;
      generationCountDown = generationLength;
      UpdateGenePool();
      NewGeneration(2);

    }
  }

  void AlignInGrid(){
    int sqRtAgents = (int)Mathf.Sqrt(numAgents);
    for (int x = 0; x < sqRtAgents; x++)
    {
      for (int y = 0; y < sqRtAgents; y++)
      {
        // Convert xy to a 1-dimensional index
        int index = x * sqRtAgents + y;
        agents[index].transform.position = new Vector3(x, y, 0);
        // i++;
      }
    }
  }

  private void NewGeneration(int numParents){
    // Debug.Log("Copy A New");
    if (GenePoolSize < numParents){
      Debug.Log("Error GenePoolSize must be larger than numParents");
    }
    foreach(GameObject updateAgent in agents){
     

      List<NeuralNetwork.NeuralNet> nets = new List<NeuralNetwork.NeuralNet>();

      for(int p = 0; p < numParents; p++)
      { // Get random agent from gene pool
        int randomAgentIndex = Random.Range(0, genePool.Count);
        nets.Add(agents[genePool[randomAgentIndex]].GetComponent<XOR_Agent>().previousNet);
      }
      
      NeuralNetwork.NeuralNet outputNet = NNCopy.copyMultiple( nets, 0.1f );
      NeuralNetwork.NNCopy.MutateWeightsAndBias(outputNet, globalMutationRate, 0.25f);

      // // // //
      updateAgent.GetComponent<XOR_Agent>().net = outputNet;

      // Debug.Log(updateAgent.name);
      // NeuralNetwork.NNCopy.PrintWeightsAndBias(updateAgent.GetComponent<XOR_AgentScript>().net);
    }
  }

  void RunTests(){
    // Pick a random pair
    int currentTestIndex = (int)(Random.Range(0, answerData.Length));
    // Debug.Log(currentTestIndex);

    // Update the global var
    UpdateTestData(currentTestIndex);

    numCorrect = 0;
    // Test the agent with the data

    //// TODO This should just happen once and the agentscripts could be stored.
    foreach (GameObject agent in agents){
      XOR_Agent agentScript = agent.GetComponent<XOR_Agent>();
      if (agentScript == null){
        Debug.Log("Agent Script is null");
      }

      double agentChoice = XORTest(agentScript);
      // agentScript.ChangeColor(agentChoice);
      agentScript.ChangeText(agentChoice);
      // agentScript.Flash(agentChoice);

      // Debug.Log(agentChoice);
      if (agentChoice == answerData[currentTestIndex]) {
        // genePool.Add(agentScript.agentNum);
        numCorrect++;
        // agentScript.UpdatePreviousNet();
        agentScript.NumCorrect++;
      }
    }
  }

  double XORTest(Agent agentScript){
    double[] agentGuess = agentScript.calculateOutputs(currentTestData);
      // This should be sigmoid
      return (agentGuess[0] > agentGuess[1]) ? 0 : 1;
      
  }
  


  void UpdateTestData(int testSetIndex){
    // Debug.Log(testSetIndex);
    currentTestData[ 0 ] = inputData[ testSetIndex, 0 ];
    currentTestData[ 1 ] = inputData[ testSetIndex, 1 ];
  }



}
