using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeaten : MonoBehaviour
{
    public Renderer renderer;
    public Material materialDespierto;
    public Material materialDerrotado;

    public GameObject estrellasInconsciente;

    public RagdollToggle ragdollscrip;

    public void BeatEnemy()
    {
        //Debug.Log("Me han derrotado!");
        ChangeFace(false);
        ragdollscrip.DoRagdoll();
        estrellasInconsciente.SetActive(true);
    }

    public void UnBeatEnemy()
    {
        ChangeFace(true);
        ragdollscrip.UnDoRagdoll();
        estrellasInconsciente.SetActive(false);
    }

    private void ChangeFace(bool state)
    { //True = Despierto, False = Derrotado
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
        if (Input.GetKeyDown(KeyCode.V))
        {
            UnBeatEnemy();
        }
    }
}