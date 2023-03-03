using System.IO;

namespace DoublyLinkedListSerialization
{
    public interface ICustomSerializer<T>
    {
        void Serialize(T obj, Stream stream);
        T Deserialize(Stream stream);
    }
}
