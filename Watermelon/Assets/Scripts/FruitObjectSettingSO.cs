using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Creat FruitObjectSetting", fileName = "FruitObjectSetting", order = 1)]
public class FruitObjectSettingSO : ScriptableObject
{
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private List<float> spriteScale;
    [SerializeField] private FruitObject prefab;

    public FruitObject spawnObject => prefab;
    public Sprite GetSprite(int index)
    {
        if (index < 0 || index > sprites.Count)
        {
            Debug.LogError("hata");
        }
        return sprites[index];
    }
    public float GetSpriteScale(int index)
    {
        if (index < 0 || index > spriteScale.Count)
        {
            Debug.LogError("hata");
        }
        return spriteScale[index];
    }

    [ContextMenu(nameof(SetScaleData))]
    public void SetScaleData()
    {
        spriteScale.Clear();
        for (int i = 0; i < sprites.Count; i++)
        {
            spriteScale.Add((i + 1) * 0.25f);
        }
    }

}