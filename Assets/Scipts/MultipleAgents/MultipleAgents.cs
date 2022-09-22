using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using NeuralNetwork;

public class MultipleAgents : MonoBehaviour
{
  // public List<int> genePool;
  public int inputSize;
  public int hiddenSize;
  public int numHiddenLayers;
  public int outputSize;

  public List<GameObject> agents;
  public int numAgents;
  public GameObject agentPrefab;
  
  // Range from 0.0 to 0.5
  [Range(0f, 1f)]
  public float globalMutationRate = 0.1f;

  public List<int> genePool;
  public int GenePoolSize;


  public void AllNewAgents(){

    agents = new List<GameObject>();

    for (int i = 0; i < numAgents; i++){
      GameObject agent = Instantiate(agentPrefab);
      agents.Add(agent);
      agents[i].transform.parent = gameObject.transform;
      agents[i].name = "agent_" + i.ToString("D6");
      agents[i].GetComponent<Agent>().agentNum = i;
    }


  }


  public void UpdateGenePool(){
    genePool.Clear();
    // For each agent in agents make a list of their fitnesses
    Dictionary<int, float> fitnesses = new Dictionary<int, float>();
    foreach (GameObject agent in agents){
      XOR_Agent agentScript = agent.GetComponent<XOR_Agent>();
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
      agents[chosenAgent].GetComponent<XOR_Agent>().UpdatePreviousNet();
      // Debug.Log(chosenAgent);
    }
  }

}
