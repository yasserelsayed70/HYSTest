using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    #region VARS
    [SerializeField] int blockSize;
    #endregion
    #region PUBLIC METHODS
    public int BlockSize { get { return blockSize; } }
    #endregion
}
