using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearJudge : MonoBehaviour
{
   [SerializeField] bool isClearJudge = false;

    public bool IsClearJudge { get { return isClearJudge; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == other.gameObject.tag)
        {
            isClearJudge = true;

            GameObject.Find("SlidePuzzleManager").GetComponent<SlidePuzzleSceneDirector>().JudgeClear();
        }
        else
        {
            isClearJudge = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isClearJudge = false;
    }
}
