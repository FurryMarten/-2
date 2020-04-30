using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ясень
{
    class Node <T>
    {
        public int key;
        public Node<T> left;
        public Node<T> right;
        public Node<T> daddy;
        public T value;
        public Node(T value, int key)
    {
        this.value = value;
        this.key = key;
    }
        public bool IsLeaf()
        {
            return (left == null && right == null);
        }
        public bool IsFull()
        { return (left != null && right != null); }
        public override string ToString()
        {
            return Convert.ToString(key);
        }

       

    }
}
