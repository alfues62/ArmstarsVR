using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBeaten : MonoBehaviour
{
    public Renderer renderer;
    public Material materialDespierto;
    public Material materialDerrotado;


    public RagdollToggle ragdollscrip;

    public void BeatEnemy() {
        //Debug.Log("Me han derrotado!");
        ChangeFace(false);
        ragdollscrip.DoRagdoll();

    }
    
    private void ChangeFace(bool state) { //True = Despierto, False = Derrotado
        if (state)
        {
            renderer.material = materialDespierto;
        }
        else
        {
            renderer.material = materialDerrotado;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            BeatEnemy();
        }
    }
}
