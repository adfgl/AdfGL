using AdfGL_Cad3Dlib.Objects;
using System.Collections;

namespace AdfGL_Cad3Dlib
{
    public class Scene3D : IList<Object3D>
    {
        readonly List<Object3D> objects;

        public Scene3D()
        {
            objects = new List<Object3D>();
        }

        public int IndexOf(Object3D item)
        {
            return objects.IndexOf(item);
        }

        public void Insert(int index, Object3D item)
        {
            objects.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            objects.RemoveAt(index);
        }

        public Object3D this[int index]
        {
            get { return objects[index]; }
            set { objects[index] = value; }
        }

        public void Add(Object3D item)
        {
            objects.Add(item);
        }

        public void Clear()
        {
            objects.Clear();
        }

        public bool Contains(Object3D item)
        {
            return objects.Contains(item);
        }

        public void CopyTo(Object3D[] array, int arrayIndex)
        {
            objects.CopyTo(array, arrayIndex);
        }

        public bool Remove(Object3D item)
        {
            return objects.Remove(item);
        }

        public int Count
        {
            get { return objects.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((ICollection<Object3D>)objects).IsReadOnly; }
        }

        public IEnumerator<Object3D> GetEnumerator()
        {
            return objects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return objects.GetEnumerator();
        }
    }
}
