using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapLoader : MonoBehaviour
{
    [SerializeField] private int index = 1;
    private GameObject currentMap;

    [System.Serializable]
    public struct Maps
    {
        public GameObject map;
        public Vector2Int mapSpawnPoint;
        public float rota;
    }

    [SerializeField] private List<Maps> maps = new List<Maps>();
    [SerializeField] private GameObject player;

    private void Start()
    {
        ActiveMap();
        SetCharacterPosition();
    }

    public void ChangeMap()
    {
        index += 1;
        if (index < maps.Count)
        {
            currentMap.SetActive(false);
            SetCharacterPosition();
            ActiveMap();
        }
        else SceneManager.LoadScene("Test_EndLore");
    }

    private void ActiveMap()
    {
        currentMap = maps[index].map;
        currentMap.SetActive(true);
        
    }

    private void SetCharacterPosition()
    {
        player.GetComponent<PlayerMovement>().Spawn(maps[index].mapSpawnPoint, maps[index].rota);
    }

}
