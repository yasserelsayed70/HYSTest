using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stickman : MonoBehaviour
{
    #region VARS
    [SerializeField] GameObject nextPlayerSocket;

    Animator animator;
    #endregion
    #region ENGINE
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        nextPlayerSocket = transform.GetChild(2).gameObject;
    }
    #endregion
    #region PUBLIC METHODS
    public GameObject Get_StickmanNextPlayerSocket()
    {
        return nextPlayerSocket;
    }
    public void SetRunningAnimatoin()
    {
        if (animator)
        {
            animator.SetBool("IsRunning", true);
        }
    }
    public void SetSettingAnimaiton()
    {
        if (animator)
        {
            animator.SetBool("IsSetting", true);
        }
    }
    #endregion
}
