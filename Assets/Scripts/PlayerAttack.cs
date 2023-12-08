using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack Instance;
    [SerializeField] private PlayerMovement playerMovement;
    private void Awake()
    {
        Instance = this;
    }

    public void Attack (float amount)
    {
        Vector3 targetCell = new Vector3(playerMovement.cellOn.gridPos.Item1 + 1, 0, playerMovement.cellOn.gridPos.Item2) ;
    }
}
