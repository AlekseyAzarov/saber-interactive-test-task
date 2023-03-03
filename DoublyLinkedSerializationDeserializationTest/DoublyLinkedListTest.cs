using DoublyLinkedListSerialization;

namespace DoublyLinkedSerializationDeserializationTest
{
    [TestClass]
    public class DoublyLinkedListTest
    {
        [TestMethod]
        public void TestDoublyLinkedList()
        {
            try
            {
                var directoryPath = $"{Environment.CurrentDirectory}\\TestFiles\\";
                Directory.CreateDirectory(directoryPath);
                var filePath = $"{directoryPath}\\SerializedList.file";

                var referenceList = CreateReferenceList();
                var testList = new ListRand();

                using (var fileStream = File.Open(filePath, FileMode.OpenOrCreate))
                {
                    referenceList.Serialize(fileStream);
                }
                using (var fileStream = File.Open(filePath, FileMode.Open))
                {
                    testList.Deserialize(fileStream);
                }

                Assert.AreEqual(testList.Count, referenceList.Count);
                Assert.AreEqual(testList.Head.Data, referenceList.Head.Data);
                Assert.AreEqual(testList.Tail.Data, referenceList.Tail.Data);

                var nodeById = GetNodeByIdList(referenceList);

                var testNode = testList.Head;

                for (int index = 0; index < testList.Count; index++) 
                {
                    var referenceNode = nodeById[index];

                    Assert.AreEqual(testNode.Data, referenceNode.Data);
                    Assert.AreEqual(testNode.Rand?.Data, referenceNode.Rand?.Data);

                    testNode = testNode.Next;
                }
            } 
            catch (Exception exception) 
            {
                Assert.Fail(exception.Message);
            }
        }

        private Dictionary<int, ListNode> GetNodeByIdList(ListRand list)
        {
            var nodeById = new Dictionary<int, ListNode>();
            var currentNode = list.Head;

            for (int index = 0; index < list.Count; index++)
            {
                nodeById.Add(index, currentNode);
                currentNode = currentNode.Next;
            }

            return nodeById;
        }

        private ListRand CreateReferenceList()
        {
            var result = new ListRand();

            var firstNode = new ListNode { Data = "firstNode" };
            var secondNode = new ListNode { Data = "secondNode" };
            var thirdNode = new ListNode { Data = "thirdNode" };
            var fourthNode = new ListNode { Data = "fourthNode" };

            firstNode.Next = secondNode;
            secondNode.Next = thirdNode;
            thirdNode.Next = fourthNode;

            secondNode.Prev = firstNode;
            thirdNode.Prev = secondNode;
            fourthNode.Prev = thirdNode;

            secondNode.Rand = secondNode;
            thirdNode.Rand = firstNode;
            firstNode.Rand = fourthNode;

            result.Head = firstNode;
            result.Tail = fourthNode;
            result.Count = 4;

            return result;
        }
    }
}