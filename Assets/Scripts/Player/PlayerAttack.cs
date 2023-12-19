using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] private PlayerMovement playerMovement;
    GameGrid grid;
    [SerializeField] AudioSource wiffVFX;

    public static PlayerAttack Instance;

    public HitImpact impact;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletStartPos;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        grid = GameGrid.instance;
    }

    public void Attack (float amount)
    {
        float angle = transform.eulerAngles.y;

        int x, z;

        if(angle >= 315f || angle < 45f)
        {
            x = 0; z = 1;
        }
        else if (angle >= 45f && angle < 135f)
        {
            x = 1; z = 0;
        }
        else if (angle >= 135f && angle < 225f)
        {
            x = 0; z = -1;
        }
        else
        {
            x = -1; z = 0;
        }

        Cell targetCell = grid.GetCell(playerMovement.cellOn.gridPos.Item1 + x, playerMovement.cellOn.gridPos.Item2 + z);

        if (targetCell.HasEntity())
        {
            targetCell.entity.transform.GetComponent<EnemyMovement>().TakeDamage(amount);
            return;
        }
        else
        {
            wiffVFX.Play();
        }

    }

    public void DistantAttack(float amount, int distance)
    {
        float angle = transform.eulerAngles.y;

        int x, z;

        if (angle >= 315f || angle < 45f)
        {
            x = 0; z = 1;
        }
        else if (angle >= 45f && angle < 135f)
        {
            x = 1; z = 0;
        }
        else if (angle >= 135f && angle < 225f)
        {
            x = 0; z = -1;
        }
        else
        {
            x = -1; z = 0;
        }

        GameObject newBullet = Instantiate(bullet);
        newBullet.GetComponent<BulletBehaviour>().Init(bulletStartPos.position, new Vector3(x, 0, z), distance * grid.cellSpacement);

        for (int i = 1; i < distance+1; i++)
        {
           Cell targetCell = grid.GetCell(playerMovement.cellOn.gridPos.Item1 + i*x, playerMovement.cellOn.gridPos.Item2 + i*z);
           if (targetCell == null) 
            {
                return;
            }
           if (targetCell.HasEntity())
           {
                impact.PlayImpact(distance, targetCell.entity);
                targetCell.entity.transform.GetComponent<EnemyMovement>().TakeDamage(amount);
                    return;
           }
        }
    }

    public void Heal(CharactersControler group,float ammount)
    {
        group.HealGroup(ammount);
    }
}
