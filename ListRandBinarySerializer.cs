using System.Collections.Generic;
using System.IO;

namespace DoublyLinkedListSerialization
{
    public class ListRandBinarySerializer : ICustomSerializer<ListRand>
    {
        public void Serialize(ListRand obj, Stream stream)
        {
            var indexByNode = new Dictionary<ListNode, int>();
            var currentNode = obj.Head;

            for (int index = 0; index < obj.Count; index++)
            {
                indexByNode.Add(currentNode, index);
                currentNode = currentNode.Next;
            }

            using (var binaryWriter = new BinaryWriter(stream))
            {
                foreach (var nodeIndexPair in indexByNode)
                {
                    var node = nodeIndexPair.Key;
                    var randIndex = node.Rand == null ? -1 : indexByNode[node.Rand];
                    binaryWriter.Write(node.Data);
                    binaryWriter.Write(randIndex);
                }
            }
        }

        public ListRand Deserialize(Stream stream)
        {
            var nodeByIndex = new Dictionary<int, (ListNode Node, int RandIndex)>();

            using (var binaryReader = new BinaryReader(stream))
            {
                var index = 0;

                while (binaryReader.PeekChar() != -1)
                {
                    var data = binaryReader.ReadString();
                    var randomIndex = binaryReader.ReadInt32();

                    var node = new ListNode { Data = data };

                    nodeByIndex.Add(index, (node, randomIndex));

                    index++;
                }
            }

            var result = new ListRand
            {
                Count = nodeByIndex.Count,
                Head = nodeByIndex[0].Node,
                Tail = nodeByIndex[nodeByIndex.Count - 1].Node
            };

            for (int index = 0; index < nodeByIndex.Count; index++)
            {
                var (node, randIndex) = nodeByIndex[index];

                if (index > 0)
                    node.Prev = nodeByIndex[index - 1].Node;
                if (index < nodeByIndex.Count - 1)
                    node.Next = nodeByIndex[index + 1].Node;
                if (randIndex != -1)
                    node.Rand = nodeByIndex[randIndex].Node;
            }

            return result;
        }
    }
}
