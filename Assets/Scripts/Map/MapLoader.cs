using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapLoader : MonoBehaviour
{
    private int index = 0;
    private GameObject currentMap;

    [System.Serializable]
    public struct Maps
    {
        public GameObject map;
        public Vector2Int mapSpawnPoint;
    }

    [SerializeField] private List<Maps> maps = new List<Maps>();
    [SerializeField] private GameObject player;
    private int x = 7; private int y = 1;

    private void Start()
    {
        ActiveMap();
    }

    public void ChangeMap()
    {
        currentMap.SetActive(false);
        index += 1;
        if (index > maps.Count)
        {
            ActiveMap();
            SetCharacterPosition();
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
        player.GetComponent<PlayerMovement>().Spawn(maps[index].mapSpawnPoint);
    }

}
