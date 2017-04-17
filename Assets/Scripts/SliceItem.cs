using UnityEngine;

public class SliceItem : MonoBehaviour
{
    public SliceGrid.Slice slice;

    void Update()
    {
        SliceGrid.instance.UpdateItem(this);
    }
}
