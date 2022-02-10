using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using NeuralNetwork;

public class MultipleAgentsScript : MonoBehaviour
{
  public List<int> genePool;
  public int inputSize;
  public int hiddenSize;
  public int numHiddenLayers;
  public int outputSize;

  public List<GameObject> agents;
  public int numAgents;
  public GameObject agentPrefab;


  // // XOR
  public static double[,] inputData = new double[4,2] { {0, 0}, {0, 1}, {1, 0}, {1, 1} };
  public static double[] answerData = new double[4] {0, 1, 1, 0};

  // // AND
  // public static double[,] inputData = new double[4,2] { {0, 0}, {0, 1}, {1, 0}, {1, 1} };
  // public static double[] answerData = new double[4] {1, 0, 0, 1};

  // public static double[,] inputData = new double[2,2] { {0, 0},{1, 1} };
  // public static double[] answerData = new double[2] {0, 1};

  public double[] currentTestData = new double[2];

  public int generationLength;
  private int generationCount;
  private int generationCountDown;

  // public double averageFitness = 0.0;
  public double maxFitness = 0.0;

  public int numCorrect;
  // public float PercentCorrect = 0f;

  // public float startEnergy;
  // public float energyLossRate;

  void Start(){
    // This counts down frames
    generationCountDown = generationLength;

    // This stores the index of the agents who enter the genepool
    genePool = new List<int>();

    // Fill the agents array with all new agents
    allNewAgents();
    // Debug.Log(map(0.999f, 0, 1, -1,1));
  }

  void Update(){

    // if (Input.GetKeyDown(KeyCode.C))
    // {
    //   NeuralNet net0 = agents[0].GetComponent<XOR_AgentScript>().net;
    //   NNCopy.MutateWeightsAndBias(net0, 0.5f, 0.1f);
    //   // NeuralNet net1 = agents[1].GetComponent<XOR_AgentScript>().net;
    //   // NNCopy.copyTwo(net0, net1, 0.5f);
    //   //
    //   Debug.Log("net0");
    //   NNCopy.PrintWeightsAndBias(net0);
    //   //
    //   // Debug.Log("net1");
    //   // NNCopy.PrintWeightsAndBias(net1);
    // }

    RunTests();

    generationCountDown--;
    if (generationCountDown <= 0) {
      print("Generation " + generationCount.ToString());
      generationCount++;
      generationCountDown = generationLength;
      // UpdateFitnessAndGenePool_XOR();
      UpdateGenePool();
      for(int i = 0; i < numAgents; i++){
        CopyANew();
      }
      genePool.Clear();

    }
    // RunTest();
  }

  public static float Map(float num, float fromMin, float fromMax, float toMin, float toMax){
    float newNum;
    newNum = (num - fromMin) * ((toMax-toMin)/(fromMax-fromMin)) + toMin;
    return newNum;
  }

  // public static double Map(double num, double fromMin, double fromMax, double toMin, double toMax){
  //   float newNum;
  //   newNum = (num - fromMin) * ((toMax-toMin)/(fromMax-fromMin)) + toMin;
  //   return newNum;
  // }





  void CopyANew(){
    // Debug.Log("Copy A New");
    if (genePool.Count > 0){
      // Debug.Log("if GenePool > 0");
      // int updateAgentIndex = (int)(Random.value * numAgents);
      int updateAgentIndex = (int)(Random.Range(0, numAgents));
      // Debug.Log(updateAgentIndex);
      // NeuralNetwork.NeuralNet output = agents[updateAgentIndex].GetComponent<FireflyEvolvedScript>().net;

      // COPY TWO //
      NeuralNetwork.NeuralNet NNParent1 = agents[genePool[(int)Random.value * genePool.Count]].GetComponent<XOR_AgentScript>().previousNet;
      NeuralNetwork.NeuralNet NNParent2 = agents[genePool[(int)Random.value * genePool.Count]].GetComponent<XOR_AgentScript>().previousNet;

      // NeuralNetwork.NeuralNet NNParent1 = agents[genePool[(int)Random.Range(0, genePool.Count)]].GetComponent<XOR_AgentScript>().previousNet;
      // NeuralNetwork.NeuralNet NNParent2 = agents[genePool[(int)Random.Range(0, genePool.Count)]].GetComponent<XOR_AgentScript>().previousNet;

    // MutateWeightsAndBias(NeuralNet NN, float _mutationRate, float _mutationAmount){

      // NeuralNetwork.NeuralNet NNParent1 = agents[genePool[0]].GetComponent<XOR_AgentScript>().previousNet;
      // NeuralNetwork.NeuralNet NNParent2 = agents[genePool[0]].GetComponent<XOR_AgentScript>().previousNet;
      NeuralNetwork.NeuralNet outputNet = NeuralNetwork.NNCopy.copyTwo( NNParent1, NNParent2, 0.01f );
      NeuralNetwork.NNCopy.MutateWeightsAndBias(outputNet, 0.01f, 0.1f);

      // // // //

      // NeuralNetwork.NeuralNet NNParent2 = agents[(int)Random.value * FlashedThisFrame.Count].GetComponent<FireflyEvolvedScript>().previousNet;

      // // COPY ONE //
      // NeuralNetwork.NeuralNet NNParent = agents[genePool[(int)Random.value * genePool.Count]].GetComponent<XOR_AgentScript>().previousNet;
      // NeuralNetwork.NeuralNet outputNet = NNParent;
      // NeuralNetwork.NNCopy.MutateWeightsAndBias(outputNet, 0.5f, 0.01f);
      // // // // // //

      // NeuralNetwork.NNCopy.ReturnMutatedNN(output, 0.01f, 0.01f);


      // NeuralNetwork.NNCopy.copyTwo(output, NNParent1, NNParent2, 0.01f);
      // Debug.Log(agents.Count);
      agents[updateAgentIndex].GetComponent<XOR_AgentScript>().net = outputNet;

      // Debug.Log(updateAgentIndex);
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
      XOR_AgentScript agentScript = agent.GetComponent<XOR_AgentScript>();

      double agentChoice = XORTest(agentScript, currentTestIndex);
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

  double XORTest(XOR_AgentScript agentScript, int currentTestIndex){
    double[] agentGuess = agentScript.calculateGuess(currentTestData);
      // This should be sigmoid
      return Mathf.RoundToInt(Sigmoid((float)agentGuess[0]));
      
  }


  public static float Sigmoid(float value) {
      return (1.0f / (1.0f + (float) Mathf.Exp(-value)));
  }
  void UpdateGenePool(){
    // int maxCorrect = 0;
    foreach (GameObject agent in agents){
      XOR_AgentScript agentScript = agent.GetComponent<XOR_AgentScript>();
      // if (agentScript.NumCorrect > maxCorrect) {
      //     genePool.Clear();
      //     maxCorrect = agentScript.NumCorrect;
      //
      //     genePool.Add(agentScript.agentNum);
      //     agentScript.UpdatePreviousNet();
      // }
      // if (maxCorrect != 0 && agentScript.NumCorrect == maxCorrect) {
      //   genePool.Add(agentScript.agentNum);
      //   agentScript.UpdatePreviousNet();
      // }

      // Debug.Log(agentScript.NumCorrect);
      agentScript.percentageCorrect = agentScript.NumCorrect/(float)generationLength;
      // float reproductionChance = 1/(1+Mathf.Pow(x/(1-x), -3));

      // if (Random.value < reproductionChance){
      //   genePool.Add(agentScript.agentNum);
      //   agentScript.UpdatePreviousNet();
      // }
      agentScript.NumCorrect = 0;


        if (agentScript.percentageCorrect > 0.55f){
        genePool.Add(agentScript.agentNum);
        agentScript.UpdatePreviousNet();
      }
    }

    agents.Sort();

  }

  

  void UpdateFitnessAndGenePool_XOR(){
    // genePool.Clear();

    foreach (GameObject agent in agents){
      XOR_AgentScript agentScript = agent.GetComponent<XOR_AgentScript>();
      // agentScript.fitness = agentScript.totalDifferenceCounter / generationLength;
      // agentScript.ClearDiffCounter();



      // else if(agentScript.fitness == maxFitness){
      //   genePool.Add(agentScript.agentNum);
      //   agentScript.UpdatePreviousNet();
      // }
      // else if (Random.value < (float)agentScript.fitness){
      //   genePool.Add(agentScript.agentNum);
      //   agentScript.UpdatePreviousNet();
      // }


      // Uncomment for xor testing //
      if (agentScript.fitness > maxFitness){
        genePool.Clear();
        genePool.Add(agentScript.agentNum);
        agentScript.UpdatePreviousNet();
        // maxFitness = agentScript.fitness;
      }
      if (agentScript.fitness > 0.5 && Random.value < (float)agentScript.fitness){
          genePool.Add(agentScript.agentNum);
          agentScript.UpdatePreviousNet();
      }
      // // // // // // // // // // //
    }

  }

  void UpdateTestData(int testSetIndex){
    // Debug.Log(testSetIndex);
    currentTestData[ 0 ] = inputData[ testSetIndex, 0 ];
    currentTestData[ 1 ] = inputData[ testSetIndex, 1 ];
  }

  public void allNewAgents(){
    // agents = new GameObject[numAgents];
    agents = new List<GameObject>();


    int i = 0;
    double sqRtAgents = Mathf.Sqrt(numAgents);
    for (int x = -(int)sqRtAgents/2; x <= (int)sqRtAgents/2; x++)
    {
      for (int y = -(int)sqRtAgents/2; y <= (int)sqRtAgents/2; y++)
      {
        GameObject agent = Instantiate(agentPrefab);
        agents.Add(agent);
        // int currentAgent = agents.Count - 1;

        agents[i].transform.parent = gameObject.transform;
        agents[i].name = "agent_" + i.ToString("D6");
        agents[i].GetComponent<XOR_AgentScript>().agentNum = i;
        agents[i].transform.position = new Vector3(x, y, 0);
        // Debug.Log(x);
        // Debug.Log(y);

        i++;
      }
    }

  }





}
