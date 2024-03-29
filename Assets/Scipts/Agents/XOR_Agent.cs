﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using NeuralNetwork;

public class XOR_Agent : Agent
{
    public double totalDifferenceCounter = 0.0;

    public int NumCorrect;
    public float percentageCorrect;

    UnityEngine.Renderer renderer;
    public TMP_Text displayText;


    void Start()
    {
      NewAgent();
    }



    public void ChangeText(double output){
      displayText.text = output.ToString();
    }

    // public void Flash(double output){
    //   if (output == 1){
    //     GetComponent<ParticleSystem>().Play();
    //   }
    // }

    // public void ChangeColor(double output){
    //   if (output == 0){
    //     renderer.material.SetColor("_Color", Color.black);
    //   }
    //   else if (output == 1){
    //     renderer.material.SetColor("_Color", Color.white);
    //   }
    // }

    // private float GetEnergy(){
    //   float returnEnergy;
    //   float distance = Vector3.Distance(Vector3.zero, transform.position);
    //   returnEnergy = ExtendedMath.Map(distance, 0, 100, 0.1f, 0);
    //   return returnEnergy;
    // }

    // public double[] calculateOutputs(double[] inputs){
    //   double[] outputData = net.Compute(inputs);
    //   outputs = outputData;
    //   return outputData;
    // }

}
