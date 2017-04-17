using UnityEngine;
using System.Collections.Generic;

public class SliceGrid : MonoBehaviour
{
    static public SliceGrid instance;
    const int size = 16;

    public class Slice
    {
        public Vector2i pos;

        bool active = false;
        HashSet<SliceItem> items = new HashSet<SliceItem>();

        public void Add(SliceItem item)
        {
            item.gameObject.SetActive(active);
            items.Add(item);
        }

        public void Remove(SliceItem item)
        {
            items.Remove(item);
        }

        public void SetActive(bool active)
        {
            this.active = active;
            foreach(var item in items)
            {
                item.gameObject.SetActive(active);
            }
        }

        public void DrawDebugGizmos()
        {
            var p = Iso.MapToWorld(new Vector3(pos.x, pos.y) * size * 5);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(p, p + Iso.MapTileToWorld(new Vector3(size, 0)));
            Gizmos.DrawLine(p + Iso.MapTileToWorld(new Vector3(size, 0)), p + Iso.MapTileToWorld(new Vector3(size, size)));
            Gizmos.DrawLine(p + Iso.MapTileToWorld(new Vector3(size, size)), p + Iso.MapTileToWorld(new Vector3(0, size)));
            Gizmos.DrawLine(p, p + Iso.MapTileToWorld(new Vector3(0, size)));
        }
    };

    private Slice[] slices;
    private int width = 256;
    private int height = 256;
    private int activeCount = 1;
    private List<Slice> activeSlices = new List<Slice>();
    private List<Slice> newlyActivatedSlices = new List<Slice>();
    private Slice centralSlice;

    void OnEnable()
    {
        instance = this;
        slices = new Slice[width * height];
    }

    Slice GetOrCreateSlice(Vector3 pos)
    {
        var slicePos = Iso.Snap(Iso.MapToIso(pos)) / 5 / size;
        return GetOrCreateSlice(slicePos);
    }

    Slice GetOrCreateSlice(Vector2i pos)
    {
        int index = pos.y * width + pos.x;
        var slice = slices[index];
        if (slice == null)
        {
            slice = new Slice();
            slice.pos = pos;
            slices[index] = slice;
        }
        return slice;
    }

    public void UpdateItem(SliceItem item)
    {
        var newSlice = GetOrCreateSlice(item.transform.position);
        if (item.slice != newSlice)
        {
            if (item.slice != null)
            {
                item.slice.Remove(item);
            }
            item.slice = newSlice;
            newSlice.Add(item);
        }
    }

    void LateUpdate()
    {
        var newSlice = GetOrCreateSlice(Camera.main.transform.position);
        if (newSlice == centralSlice)
            return;

        centralSlice = newSlice;

        newlyActivatedSlices.Clear();

        int x1 = centralSlice.pos.x - activeCount;
        int y1 = centralSlice.pos.y - activeCount;
        int x2 = centralSlice.pos.x + activeCount + 1;
        int y2 = centralSlice.pos.y + activeCount + 1;
        int index = y1 * width;
        for (int y = y1; y < y2; ++y)
        {
            for (int x = x1; x < x2; ++x)
            {
                if (index < 0 || index >= slices.Length)
                    continue;
                var slice = slices[index + x];
                if (slice == null)
                    continue;
                newlyActivatedSlices.Add(slice);
            }
            index += width;
        }

        foreach(var slice in newlyActivatedSlices)
        {
            if (!activeSlices.Contains(slice))
            {
                slice.SetActive(true);
            }
        }

        foreach(var slice in activeSlices)
        {
            if (!newlyActivatedSlices.Contains(slice))
            {
                slice.SetActive(false);
            }
        }
        
        Tools.Swap(ref activeSlices, ref newlyActivatedSlices);
    }

    void OnDrawGizmos()
    {
        foreach (var slice in activeSlices)
        {
            slice.DrawDebugGizmos();
        }
    }
}
