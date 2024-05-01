using AdfGL_Cad2Dlib.Objects;
using System.Collections;

namespace AdfGL_Cad2Dlib
{
    public class Scene2D : IList<Object2Dbase>
    {
        readonly List<Object2Dbase> objects;

        public Scene2D()
        {
            objects = new List<Object2Dbase>();
        }

        public int IndexOf(Object2Dbase item)
        {
            return objects.IndexOf(item);
        }

        public void Insert(int index, Object2Dbase item)
        {
            objects.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            objects.RemoveAt(index);
        }

        public Object2Dbase this[int index]
        {
            get { return objects[index]; }
            set { objects[index] = value; }
        }

        public void Add(Object2Dbase item)
        {
            objects.Add(item);
        }

        public void Clear()
        {
            objects.Clear();
        }

        public bool Contains(Object2Dbase item)
        {
            return objects.Contains(item);
        }

        public void CopyTo(Object2Dbase[] array, int arrayIndex)
        {
            objects.CopyTo(array, arrayIndex);
        }

        public bool Remove(Object2Dbase item)
        {
            return objects.Remove(item);
        }

        public int Count
        {
            get { return objects.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((ICollection<Object2Dbase>)objects).IsReadOnly; }
        }

        public IEnumerator<Object2Dbase> GetEnumerator()
        {
            return objects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return objects.GetEnumerator();
        }
    }
}
