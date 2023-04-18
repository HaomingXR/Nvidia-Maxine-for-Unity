namespace MaxineAR
{
    public class ArDefs
    {
        public struct NvAR_Rect
        {
            public float x;
            public float y;
            public float width;
            public float height;
        }

        public unsafe struct NvAR_BBoxes
        {
            public NvAR_Rect* boxes;
            public byte num_boxes;
            public byte max_boxes;
        }
    }
}