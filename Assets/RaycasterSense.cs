using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycasterSense : MonoBehaviour
{
    public float[] Senses;
    public float SenseDistance;
    public int SenseAngles;


    void Update(){

        Senses = GetSenses();

    }


    public float[] GetSenses(){
        float _senses;
        // Rotate this object to face multiple directions in 3d
        for (int i = 0; i <= 10; i += 1){
            for(int j = 0; j <= 10; j += 1){
                for(int k = 0; k <= 10; k += 1){
                    Vector3 direction = new Vector3(i, j, k);
                    RaycastHit hit;
                    // rotate to face the direction
                    // transform.rotation = Quaternion.RotateTowards(direction);
                    transform.LookAt(direction);
                    if (Physics.Raycast(transform.position, transform.forward*SenseDistance, out hit)){
                        Debug.DrawRay(transform.position, transform.forward*SenseDistance, Color.red);
                    }
                    else{
                        Debug.DrawRay(transform.position, transform.forward*SenseDistance, Color.green);
                    }


                    // if (Physics.Raycast(transform.position, transform.rotation * direction, out hit)){
                    //     Debug.DrawRay(transform.position, transform.rotation * direction, Color.red);
                    // }
                    // else{
                    //     Debug.DrawRay(transform.position, transform.rotation * direction, Color.green);
                    // }
                }
            }
        }

        return Senses;
    }


}
