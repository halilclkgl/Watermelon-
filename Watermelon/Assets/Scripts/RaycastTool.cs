using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTool : MonoBehaviour
{
    public LayerMask layerMask;
    [SerializeField] private GameArea _gameArea;

    private bool IsWorking;
    RaycastHit2D[] hits;
    private void Update()
    {
        var left = _gameArea.GetBorderPositionHorizontal().x;
        var up = _gameArea.GetBorderPositionVertical().y;
        Vector2 startingPoint = new Vector2(left, up);

        var direction = Vector2.right;

        hits = Physics2D.RaycastAll(startingPoint, direction, 10f, layerMask);

        if (hits.Length == 2)
        {
            return;
        }
        if (IsWorking)
        {
            return;
        }

        IsWorking = true;
        StartCoroutine(CheckList());

    }

    private IEnumerator CheckList()
    {

        yield return new WaitForSeconds(2);
        if (hits.Length > 0)
        {
            GameManager.Instance.GameOver();
        }
        IsWorking = false;
    }
}
