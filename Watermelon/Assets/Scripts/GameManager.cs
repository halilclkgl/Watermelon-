using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] FruitObjectSettingSO fruitObjectSettingSO;
    [SerializeField] private GameArea gameArea;
    [SerializeField] private Transform spawnPoint;
    public int score;
    [SerializeField] TextMeshProUGUI scoreText;
    private readonly Vector2Int fruitRange = new Vector2Int(0, 4);
    private bool IsClick => Input.GetMouseButtonDown(0);


    private void Awake()
    {
        Instance = this;
    }
    private float GetInputHorizontalPosition()
    {
        var inputPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;

        var limit = gameArea.GetBorderPositionHorizontal();
        var result = Mathf.Clamp(inputPosition, limit.x, limit.y);
        return result;
    }

    private void OnClicked()
    {
        var index = Random.Range(fruitRange.x, fruitRange.y);
        var spawnPosition = new Vector2(GetInputHorizontalPosition(), spawnPoint.position.y);
        SpawnFruit(index, spawnPosition);
    }

    void SpawnFruit(int index, Vector2 pos)
    {
        var prefab = fruitObjectSettingSO.spawnObject;

        var fruitObject = Instantiate(prefab, pos, Quaternion.identity);

        var sprite = fruitObjectSettingSO.GetSprite(index);
        var scale = fruitObjectSettingSO.GetSpriteScale(index);


        fruitObject.Prepare(sprite, index, scale);
    }
    private void Update()
    {
        scoreText.text = "Score: " + score;
        if (IsClick)
        {
            OnClicked();
        }
    }
    public void Merge(FruitObject first, FruitObject second)
    {
        var type = first.type + 1;
        score += (type * 10);
        var spawnPosition = (first.transform.position + second.transform.position) * 0.5f;
        Destroy(first.gameObject);
        Destroy(second.gameObject);

        SpawnFruit(type, spawnPosition);
    }
    public void GameOver()
    {
        Time.timeScale = 0;
        Debug.Log("GameOver!!!");
    }

}
