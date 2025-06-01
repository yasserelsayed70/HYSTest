using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using UnityEngine;

public class StackHandler : MonoBehaviour
{
    #region VARS
    [SerializeField] Stack<Stickman> stickmenStack = new Stack<Stickman>();
    [SerializeField] Stickman firstStickman;
    #endregion
    #region ENGINE
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Stickman>())
        {
            AddNewStickmanToStack(other.GetComponent<Stickman>());
        }
        if (other.GetComponent<NpcStackHolder>())
        {
            AddNewStack(other.GetComponent<NpcStackHolder>().NPCBlock);
        }
        if (other.GetComponent<Block>())
        {
            PopFromStack(other.GetComponent<Block>().BlockSize);
            
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SetupFirstStickman();   
    }
    #endregion
    #region MEMBER METHODS
    void SetupFirstStickman()
    {
        if (firstStickman)
        {
            firstStickman.GetComponent<Animator>().SetBool("IsRunning", true);
            stickmenStack.Push(firstStickman);
        }
    }
    void AddNewStickmanToStack(Stickman newStickman)
    {
        //Disable collider
        newStickman.GetComponent<CapsuleCollider>().enabled = false;
        //Setparent to playerstack
        newStickman.transform.SetParent(transform);
        //set newplayer to the socket
        stickmenStack.ElementAt(0).transform.SetParent(newStickman.Get_StickmanNextPlayerSocket().transform);
        stickmenStack.ElementAt(0).SetSettingAnimaiton();
        newStickman.GetComponent<Stickman>().SetRunningAnimatoin();
        stickmenStack.Push(newStickman);
        stickmenStack.ElementAt(1).transform.localPosition = Vector3.zero;
    }
    void AddNewStack(Stack<Stickman> newStack)
    {
        stickmenStack.ElementAt(0).SetSettingAnimaiton();
        Stickman[] arrStickmen = new Stickman[newStack.Count];
        newStack.CopyTo(arrStickmen, 0);
        for (int i = arrStickmen.Length - 1; i >= 0; i--)
        {
            arrStickmen[i].transform.SetParent(transform);
            stickmenStack.ElementAt(0).transform.SetParent(arrStickmen[i].Get_StickmanNextPlayerSocket().transform);
            stickmenStack.Push(arrStickmen[i]);
            stickmenStack.ElementAt(1).transform.localPosition = Vector3.zero;
            if (i==0)
            {
                stickmenStack.ElementAt(0).GetComponent<Stickman>().SetRunningAnimatoin();
                stickmenStack.ElementAt(0).transform.localPosition = Vector3.zero;
            }
        }
    }
    void PopFromStack(int blockSize)
    {
        if (blockSize < stickmenStack.Count)
        {
            stickmenStack.ElementAt(blockSize).GetComponent<Stickman>().GetComponent<Animator>().SetBool("IsSetting", false);
            stickmenStack.ElementAt(blockSize).GetComponent<Stickman>().GetComponent<Animator>().SetBool("IsRunning", true);

            stickmenStack.ElementAt(blockSize).transform.SetParent(transform);
            StartCoroutine(ResetStackPosition(stickmenStack.ElementAt(blockSize)));
            for (int i = 0; i < blockSize; i++)
            {
                stickmenStack.ElementAt(0).gameObject.transform.SetParent(null);
                stickmenStack.Pop();
            }
        }
        else
        {
            stickmenStack.ElementAt(0).transform.SetParent(null);
            for (int i = 0; i < stickmenStack.Count; i++)
            {
                stickmenStack.Pop();
            }
        }

    }
    IEnumerator ResetStackPosition(Stickman firstStickman)
    {
        yield return new WaitForSeconds(0.3f);
        Vector3 currentPosition = firstStickman.transform.localPosition;
        float startTime = Time.time;
        while (Time.time < startTime + 0.3f)
        {
            firstStickman.transform.localPosition = Vector3.Lerp(currentPosition, Vector3.zero, (Time.time - startTime) / 0.3f);
            yield return null;
        }
        firstStickman.transform.localPosition = Vector3.zero;
    }
    #endregion

}
