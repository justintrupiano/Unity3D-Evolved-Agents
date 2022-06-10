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


  // Range from 0.0 to 0.5
  [Range(0.0f, 0.5f)]
  public float globalMutationRate = 0.1f;

  // // XOR
  public static double[,] inputData = new double[4,2] { {0, 0}, {0, 1}, {1, 0}, {1, 1} };
  public static double[] answerData = new double[4] {0, 1, 1, 0};

  // // AND
  // public static double[,] inputData = new double[4,2] { {0, 0}, {0, 1}, {1, 0}, {1, 1} };
  // public static double[] answerData = new double[4] {1, 0, 0, 1};

  // public static double[,] inputData = new double[2,2] { {0, 0},{1, 1} };
  // public static double[] answerData = new double[2] {0, 1};

  public double[] currentTestData = new double[2];



  public int numCorrect;
  public int generationLength;
  public int generationCount;
  private int generationCountDown;

  // public double averageFitness = 0.0;
  // public double maxFitness = 0.0;

  // public float PercentCorrect = 0f;

  // public float startEnergy;
  // public float energyLossRate;
  public int GenePoolSize;
  // private IEnumerator copyCoroutine;

  void Start(){
    // This counts down frames
    generationCountDown = generationLength;

    // This stores the index of the agents who enter the genepool
    genePool = new List<int>();

    // Fill the agents array with all new agents
    allNewAgents();
    // Debug.Log(map(0.999f, 0, 1, -1,1));
    // copyCoroutine = CopyANew(2);
    // StartCoroutine(copyCoroutine);
  }

  void Update(){

    RunTests();

    generationCountDown--;
    if (generationCountDown <= 0) {
      // Debug.Log("Generation " + generationCount.ToString());
      generationCount++;
      generationCountDown = generationLength;
      // UpdateFitnessAndGenePool_XOR();
      UpdateGenePool();
      NewGeneration(2);

    }
    // RunTest();
  }



  private void NewGeneration(int numParents){
    // Debug.Log("Copy A New");
    foreach(GameObject updateAgent in agents){
     

      List<NeuralNetwork.NeuralNet> nets = new List<NeuralNetwork.NeuralNet>();

      for(int p = 0; p < numParents; p++)
      { // Get random agent from gene pool
        int randomAgentIndex = Random.Range(0, genePool.Count);
        nets.Add(agents[genePool[randomAgentIndex]].GetComponent<XOR_AgentScript>().previousNet);
      }
      
      NeuralNetwork.NeuralNet outputNet = NNCopy.copyMultiple( nets, 0.1f );
      NeuralNetwork.NNCopy.MutateWeightsAndBias(outputNet, globalMutationRate, 0.25f);

      // // // //
      updateAgent.GetComponent<XOR_AgentScript>().net = outputNet;

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
      XOR_AgentScript agentScript = agent.GetComponent<XOR_AgentScript>();

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

  double XORTest(XOR_AgentScript agentScript){
    double[] agentGuess = agentScript.calculateGuess(currentTestData);
      // This should be sigmoid
      return (agentGuess[0] > agentGuess[1]) ? 0 : 1;
      
  }




  void UpdateGenePool(){

    genePool.Clear();
    // For each agent in agents make a list of their fitnesses
    Dictionary<int, float> fitnesses = new Dictionary<int, float>();
    foreach (GameObject agent in agents){
      XOR_AgentScript agentScript = agent.GetComponent<XOR_AgentScript>();
      fitnesses.Add(agentScript.agentNum, agentScript.NumCorrect);
      agentScript.NumCorrect = 0;
    }

    // Sort that dictionary based on higest fitness
    fitnesses = fitnesses.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

    // Add the top half of the gene pool to the gene pool
    for (int i = 0; i < GenePoolSize; i++){
      float NormalRandomNum = Mathf.Clamp(Mathf.Abs(ExtendedMath.generateNormalRandom(0, 0.1f)), 0, 1);
      int chosenAgent = fitnesses.ElementAt((int)Mathf.Floor(NormalRandomNum*(fitnesses.Count-1))).Key;
      genePool.Add(chosenAgent);
      agents[chosenAgent].GetComponent<XOR_AgentScript>().UpdatePreviousNet();
      // Debug.Log(chosenAgent);
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
    // for (int x = -(int)sqRtAgents/2; x <= (int)sqRtAgents/2; x++)
    for (int x = 0; x < sqRtAgents; x++)
    {
      // for (int y = -(int)sqRtAgents/2; y <= (int)sqRtAgents/2; y++)
      for (int y = 0; y < sqRtAgents; y++)
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
