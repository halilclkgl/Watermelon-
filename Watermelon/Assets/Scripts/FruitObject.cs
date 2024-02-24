using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitObject : MonoBehaviour
{
    public int type { get; set; }
    [SerializeField] private SpriteRenderer spriteRenderer;
    public bool SendedMergeSignal { get; private set; }

    public void Prepare(Sprite _sprite, int index, float scale)
    {
        spriteRenderer.sprite = _sprite;
        type = index;
        transform.localScale = Vector3.one * scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var fruitObject = collision.transform.GetComponent<FruitObject>();
        if (!fruitObject)
            return;

        if (fruitObject.type != type)
            return;

        if (fruitObject.SendedMergeSignal)
            return;


        SendedMergeSignal = true;
        GameManager.Instance.Merge(this, fruitObject);

    }

}
