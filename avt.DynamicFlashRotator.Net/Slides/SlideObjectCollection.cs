
namespace DnnSharp.DynamicRotator.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI;
    using System.ComponentModel;
    using System.Collections;
    using System.Web.UI.WebControls;
    using System.Web;
    using System.Security.Permissions;
    using System.Drawing.Design;


    [Editor("avt.AllinOneRotator.Net.SlideCollectionEditor, avt.AllinOneRotator.Net", typeof(UITypeEditor)), AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public sealed class SlideObjectCollection : IList, ICollection, IEnumerable
    {
        // Fields
        private ArrayList _SlideObjects = new ArrayList();

        // Methods

        public void Add(SlideObjectInfo slideObject)
        {
            this._SlideObjects.Add(slideObject);
            //if (this.marked) {
            //    item.Dirty = true;
            //}
        }

        public void AddRange(SlideObjectInfo[] items)
        {
            if (items == null) {
                throw new ArgumentNullException("items");
            }
            foreach (SlideObjectInfo item in items) {
                this.Add(item);
            }
        }

        public void Clear()
        {
            this._SlideObjects.Clear();
        }

        public bool Contains(SlideObjectInfo item)
        {
            return this._SlideObjects.Contains(item);
        }

        public void CopyTo(Array array, int index)
        {
            this._SlideObjects.CopyTo(array, index);
        }

        //public SlideObjectInfo FindByText(string text)
        //{
        //    int num = this.FindByTextInternal(text, true);
        //    if (num != -1) {
        //        return (SlideObjectInfo)this._SlideObjects[num];
        //    }
        //    return null;
        //}

        //internal int FindByTextInternal(string text, bool includeDisabled)
        //{
        //    int num = 0;
        //    foreach (SlideObjectInfo item in this._SlideObjects) {
        //        if (item.Title.Equals(text) && (includeDisabled || item.Enabled)) {
        //            return num;
        //        }
        //        num++;
        //    }
        //    return -1;
        //}

        //public SlideObjectInfo FindByValue(string value)
        //{
        //    int num = this.FindByValueInternal(value, true);
        //    if (num != -1) {
        //        return (SlideObjectInfo)this._SlideObjects[num];
        //    }
        //    return null;
        //}

        //internal int FindByValueInternal(string value, bool includeDisabled)
        //{
        //    int num = 0;
        //    foreach (SlideObjectInfo item in this._SlideObjects) {
        //        if (item.Value.Equals(value) && (includeDisabled || item.Enabled)) {
        //            return num;
        //        }
        //        num++;
        //    }
        //    return -1;
        //}

        public IEnumerator GetEnumerator()
        {
            return this._SlideObjects.GetEnumerator();
        }

        public int IndexOf(SlideObjectInfo item)
        {
            return this._SlideObjects.IndexOf(item);
        }

        //public void Insert(int index, string item)
        //{
        //    this.Insert(index, new SlideObjectInfo(item));
        //}

        public void Insert(int index, SlideObjectInfo item)
        {
            this._SlideObjects.Insert(index, item);
        }

        internal void LoadViewState(object state)
        {
            //if (state != null) {
            //    if (state is Pair) {
            //        Pair pair = (Pair)state;
            //        ArrayList first = (ArrayList)pair.First;
            //        ArrayList second = (ArrayList)pair.Second;
            //        for (int i = 0; i < first.Count; i++) {
            //            int num2 = (int)first[i];
            //            if (num2 < this.Count) {
            //                this[num2].LoadViewState(second[i]);
            //            } else {
            //                SlideObjectInfo item = new SlideObjectInfo();
            //                item.LoadViewState(second[i]);
            //                this.Add(item);
            //            }
            //        }
            //    } else {
            //        Triplet triplet = (Triplet)state;
            //        this._SlideObjects = new ArrayList();
            //        this.saveAll = true;
            //        string[] strArray = (string[])triplet.First;
            //        string[] strArray2 = (string[])triplet.Second;
            //        bool[] third = (bool[])triplet.Third;
            //        for (int j = 0; j < strArray.Length; j++) {
            //            this.Add(new SlideObjectInfo(strArray[j], strArray2[j], third[j]));
            //        }
            //    }
            //}
        }

        //public void Remove(string item)
        //{
        //    int index = this.IndexOf(new SlideObjectInfo(item));
        //    if (index >= 0) {
        //        this.RemoveAt(index);
        //    }
        //}

        public void Remove(SlideObjectInfo item)
        {
            int index = this.IndexOf(item);
            if (index >= 0) {
                this.RemoveAt(index);
            }
        }

        public void RemoveAt(int index)
        {
            this._SlideObjects.RemoveAt(index);
        }

        internal object SaveViewState()
        {
            //if (this.saveAll) {
            //    int count = this.Count;
            //    object[] objArray = new string[count];
            //    object[] objArray2 = new string[count];
            //    bool[] z = new bool[count];
            //    for (int j = 0; j < count; j++) {
            //        objArray[j] = this[j].Text;
            //        objArray2[j] = this[j].Value;
            //        z[j] = this[j].Enabled;
            //    }
            //    return new Triplet(objArray, objArray2, z);
            //}
            //ArrayList x = new ArrayList(4);
            //ArrayList y = new ArrayList(4);
            //for (int i = 0; i < this.Count; i++) {
            //    object obj2 = this[i].SaveViewState();
            //    if (obj2 != null) {
            //        x.Add(i);
            //        y.Add(obj2);
            //    }
            //}
            //if (x.Count > 0) {
            //    return new Pair(x, y);
            //}
            return null;
        }

        int IList.Add(object item)
        {
            SlideObjectInfo item2 = (SlideObjectInfo)item;
            int num = this._SlideObjects.Add(item2);
            //if (this.marked) {
            //    item2.Dirty = true;
            //}
            return num;
        }

        bool IList.Contains(object item)
        {
            return this.Contains((SlideObjectInfo)item);
        }

        int IList.IndexOf(object item)
        {
            return this.IndexOf((SlideObjectInfo)item);
        }

        void IList.Insert(int index, object item)
        {
            this.Insert(index, (SlideObjectInfo)item);
        }

        void IList.Remove(object item)
        {
            this.Remove((SlideObjectInfo)item);
        }

        //void IStateManager.LoadViewState(object state)
        //{
        //    this.LoadViewState(state);
        //}

        //object IStateManager.SaveViewState()
        //{
        //    return this.SaveViewState();
        //}

        //void IStateManager.TrackViewState()
        //{
        //    this.TrackViewState();
        //}

        //internal void TrackViewState()
        //{
        //    this.marked = true;
        //    //for (int i = 0; i < this.Count; i++) {
        //    //    this[i].TrackViewState();
        //    //}
        //}

        // Properties
        public int Capacity
        {
            get
            {
                return this._SlideObjects.Capacity;
            }
            set
            {
                this._SlideObjects.Capacity = value;
            }
        }

        public int Count
        {
            get
            {
                return this._SlideObjects.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return this._SlideObjects.IsReadOnly;
            }
        }

        public bool IsSynchronized
        {
            get
            {
                return this._SlideObjects.IsSynchronized;
            }
        }

        public SlideObjectInfo this[int index]
        {
            get
            {
                return (SlideObjectInfo)this._SlideObjects[index];
            }
        }

        public object SyncRoot
        {
            get
            {
                return this;
            }
        }

        bool IList.IsFixedSize
        {
            get
            {
                return false;
            }
        }

        object IList.this[int index]
        {
            get
            {
                return this._SlideObjects[index];
            }
            set
            {
                this._SlideObjects[index] = (SlideObjectInfo)value;
            }
        }

        //bool IStateManager.IsTrackingViewState
        //{
        //    get
        //    {
        //        return this.marked;
        //    }
        //}
    }
}
