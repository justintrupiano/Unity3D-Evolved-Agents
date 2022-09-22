using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover_Agent : Agent
{
    public bool Is2D;
    public float energy;
    public GameObject target;
    void Start()
    {
        NewAgent();
        energy = 1;
        target = GetComponentInParent<Multiple_Mover_Agents>().Target;
    }

    void Update()
    {
        UpdateEnergy();
        CalculateNewPosition(Is2D);
    }


    void CalculateNewPosition(bool is2D)
    {
        // Vector3 newPos = Random.insideUnitSphere * 0.01f;
        // Get agent's current position
        Vector3 currentPos = transform.position;
        // Send to the neural network
        double[] inputs = new double[4]{
                                        currentPos.x,
                                        currentPos.y,
                                        (is2D) ? 0 : currentPos.z,
                                        energy
                                        };
        // Calculate outputs
        double[] outputs = calculateOutputs(inputs);

        Vector3 newPos = new Vector3(   
                                        ExtendedMath.Map((float)outputs[0], 0, 1, -1, 1),
                                        ExtendedMath.Map((float)outputs[1], 0, 1, -1, 1),
                                        (is2D) ? 0 : ExtendedMath.Map((float)outputs[2], 0, 1, -1, 1)
                                    );
        newPos = newPos;

        // Get the output of the neural network
        // Move the agent according to the output of the neural network
        transform.position += newPos;
    }

    void UpdateEnergy()
    {
        energy = (energy < 0) ? 0 : energy - 0.01f;

        float distToTarget = Vector3.Distance(target.transform.position, transform.position);    
        distToTarget = ExtendedMath.Map(distToTarget, 0, 1000, 1, 0);
        // Debug.Log(distToTarget);
        if (distToTarget > 0){
            energy += distToTarget;
        }

    }

}
