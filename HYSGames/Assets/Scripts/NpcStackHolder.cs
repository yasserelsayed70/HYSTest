using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NpcStackHolder : MonoBehaviour
{
    #region VARS
    [SerializeField] GameObject StickmanPrefab;
    [SerializeField] int stickmenNumber = 1;

    Stack<Stickman> NpcStack = new Stack<Stickman>();
    #endregion
    #region ENGINE
    // Start is called before the first frame update
    void Start()
    {
        InstantiateStickmen();
    }
    #endregion
    #region MEMBER METHODS 
    void InstantiateStickmen()
    {
        if (StickmanPrefab)
        {
            for (int i = 0; i < stickmenNumber; i++)
            {
                GameObject newStickmanGO = Instantiate(StickmanPrefab, transform);
                Stickman newStickman = newStickmanGO.GetComponent<Stickman>();
                if (stickmenNumber != 1)
                {
                    if (i == 0)
                    {
                        NpcStack.Push(newStickman);

                        NpcStack.ElementAt(0).GetComponent<Animator>().SetBool("IsSetting", true);
                    }
                    else if (i == stickmenNumber - 1)
                    {
                        NpcStack.ElementAt(0).transform.SetParent(newStickman.Get_StickmanNextPlayerSocket().transform);
                        NpcStack.Push(newStickman);
                        NpcStack.ElementAt(1).transform.localPosition = Vector3.zero;
                    }
                    else
                    {
                        NpcStack.ElementAt(0).transform.SetParent(newStickman.Get_StickmanNextPlayerSocket().transform);
                        NpcStack.ElementAt(0).SetSettingAnimaiton();
                        NpcStack.Push(newStickman);
                        NpcStack.ElementAt(0).GetComponent<Animator>().SetBool("IsSetting", true);
                        NpcStack.ElementAt(1).transform.localPosition = Vector3.zero;
                    }
                }
                else
                {
                    NpcStack.Push(newStickman);
                }
            }
        }
    }
    #endregion
    #region PUBLIC METHODS
    public Stack<Stickman> NPCBlock { get { return NpcStack; } }
    #endregion

}
