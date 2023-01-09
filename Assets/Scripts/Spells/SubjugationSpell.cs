using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubjugationSpell : BaseSpell
{
    [SerializeField] private RaycastUnit _raycastUnit;
    private void Start()
    {
        StartCoroutine(WaitForNext());
    }
    private IEnumerator WaitForNext()
    {
        MessageText.sendMessage?.Invoke("Click on position to subdue unit");
        while (!Input.GetKeyDown(KeyCode.Mouse0))
        {
            yield return null;
        }
        Vector2 vect = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(vect);
        List<Unit> list = _raycastUnit.GetOverlapUnitAll(new Vector2(vect.x - 0.2f, vect.y+20), new Vector2(vect.x + 0.2f, vect.y - 40), 1);
        if (list.Count != 0)
        {
            Debug.Log("used");
            list[0].team = 1;
            list[0].isLeft = false;
            list[0].status = UnitStatus.Move;
            Vector3 v = list[0].GetParent().transform.localScale;
            list[0].GetParent().transform.localScale = new Vector3(-v.x, v.y, v.z);
            int x = list[0].GetOppositeLayer();
            list[0].GetParent().layer = x;
            list[0].GetParent().GetComponent<RaycastUnit>().UpdateParams(list[0].GetLayersToIgnore(x), x);
        }
    }


}
