using System.IO;

namespace DoublyLinkedListSerialization
{
    public class ListRand
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;

        public void Serialize(FileStream s)
        {
            var serializer = new ListRandBinarySerializer();
            serializer.Serialize(this, s);
        }

        public void Deserialize(FileStream s)
        {
            var serializer = new ListRandBinarySerializer();
            var desirialized = serializer.Deserialize(s);

            Head = desirialized.Head;
            Tail = desirialized.Tail;
            Count = desirialized.Count;
        }
    }
}
