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


  public void allNewAgents(){
    // agents = new GameObject[numAgents];
    agents = new List<GameObject>();

  }

}
