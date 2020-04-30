using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Ясень
{
    class Tree<T>
    {
        public Node<T> root;//ссылка на корень
        public List<Node<T>> ListOFNode = new List<Node<T>>();
        int count;
       
        
        

        public void Add(T value, int key)//проверка пустой корень или нет, если нет . то добавляем корень
        {
            if (root == null)
            { root = new Node<T>(value, key);
            ListOFNode.Add(new Node<T>(value, key));
            count++;
            }
            else
            { recAdd(new Node<T>(value,key),root);}
            count++;
        }
        void recAdd(Node<T> newNode, Node<T> Node)//Если не корень
        {
            if (newNode.key == Node.key)
            {
                Node.value = newNode.value;
               return; 
            }
            else
            {
            if (newNode.key < Node.key)
            {
                if (Node.left == null)
                {
                    Node.left = newNode;
                    newNode.daddy = Node;
                    ListOFNode.Add(newNode);
                    return;
                }
                else
                { recAdd(newNode, Node.left); }
            }
            else
            {
                if (Node.right == null)
                {
                    Node.right = newNode;
                    newNode.daddy = Node;
                    ListOFNode.Add(newNode);
                    return;
                }
                else
                {
                    recAdd(newNode, Node.right);
                }
            }
            }
        }

        public string lnr()// центрированный (симметричный) обход-----порядки обхода
        {
            return rec_lnr(root);
        }
        string rec_lnr(Node<T> node)
        {
            if (node == null) return string.Empty;
            return rec_lnr(node.left) + node.key.ToString() + " " +
                  rec_lnr(node.right);
        }
        public string nlr()//прямой обход
        {
            return rec_nlr(root);
        }
        string rec_nlr(Node<T> node)
        {
            if (node == null) return string.Empty;
            return node.key.ToString() + " " +
                  rec_nlr(node.left) + rec_nlr(node.right);
        }
        public string lrn()//обратный
        {
            return rec_lrn(root);
        }
        string rec_lrn(Node<T> node)// обратный обход
        {
            if (node == null) return string.Empty;
            return rec_lrn(node.left) + rec_lrn(node.right) + node.key.ToString() + " ";
        }



        public void Delete(int key, Node<T> Node, string s)//daddy ссылка на родителя
        { 
            
            if (Node != null)   
            {count--;
            if (key != root.key)
            {
                if (key > Node.key)
                {
                    Delete(key, Node.right, "right");
                }
                if (key < Node.key)
                {
                    Delete(key, Node.left, "left");
                }
                if (key == Node.key)
                {
                    if (Node.IsLeaf() == true)
                    {
                        if (s == "left")
                        {
                            Node.daddy.left = null;
                        }
                        else
                        {
                            Node.daddy.right = null;
                        }
                    }
                    else
                    {
                        if (Node.IsFull() == true)
                        {
                            Node<T> TMP;
                            TMP = NodeIsnotLeaf(Node.right);
                            TMP.left = Node.left;
                            Node.left.daddy = TMP;
                            if (s == "left")
                            { Node.daddy.left = Node.right; }
                            else
                            { Node.daddy.right = Node.right; }
                            Node.right.daddy = Node.daddy;
                        }
                        else
                        {
                            if (Node.left != null)
                            {
                                if (s == "left")
                                { Node.daddy.left = Node.left; }
                                else
                                { Node.daddy.right = Node.left; }
                                Node.left.daddy = Node.daddy;
                            }
                            else
                            {
                                if (s == "left")
                                { Node.daddy.left = Node.right; }
                                else
                                { Node.daddy.right = Node.right; }
                                Node.right.daddy = Node.daddy;
                            }
                        }
                    }
                }
            }
            else
            {
                if (root.IsFull())
                {
                
                }
                else
                {
                    if (root.IsLeaf())
                    { root = null; }
                    else
                    {

                        if (Node.left == null)
                        {
                            Node.right.daddy = null;
                            root = Node.right;

                        }
                        else
                        {
                            Node.left.daddy = null;
                            root = Node.left;
                        }
                    }
                }

            
            }
            }
        }
      
        Node<T> NodeIsnotLeaf(Node<T> Node) // (Удаление) для случая если удаляемый эл-нт имеет оба поддерева (возвращает самый левый правого поддереваузел R)
        {
           if (Node.left == null)
           { return Node; }
           return NodeIsnotLeaf(Node.left);
        }

        public int Deep()//глубина
        {
            return DeepTree(root);
        }

        int DeepTree(Node<T> r)
        {

            if (r == null)
            {
                return -1;
            }
            else
            {
                return 1 + Math.Max(DeepTree(r.left), DeepTree(r.right));
            }
        }  //Глyбина





        public void Draw(Graphics g, int PanelWidth, int PanelHeight, float NodeSize)//Чтобы вызывать (панель, ширина панели(на которой график определён), высота(-"-"-), размер листа)
        {
            g.Clear(Color.White);//Очещение понели g-graphics панели
            float deltaH = PanelHeight / (this.Deep() + 2); //вычисление расстояние между узлами по вертикали +2- чтобы с отступой и внизу с отступом
            DrawNode(g, ref root, PanelWidth / 2, deltaH, NodeSize, PanelWidth / 2, deltaH);//Готовит почву для реккурсивного метода.( графикс, корень, флот х(координата Х, середина), дельта по У, размер, дельта вейт, дельта)
        }


        private void DrawNode(Graphics gr, ref Node<T> root, float X, float Y, float NodeSize, float width, float deltaH)//Руккурсивный и сам работает, почему раздельно вычисляем, для того, чтобы не пришлось int PanelWidth, int PanelHeight, float NodeSize
        {

            if (root != null)//Левый, правый, отрисовка.
            {
                if (root.left != null)
                {
                    float x1, y1;//Координаты левого шарика
                    x1 = X - width / 2;//(в каждом новыом вызове свой)
                    y1 = Y + deltaH;
                    gr.DrawLine(new Pen(Color.Black), X, Y, x1, y1);//отрисовка линни, вычисления координат
                    DrawNode(gr, ref root.left, x1, y1, NodeSize, width / 2, deltaH);//Руккурсия 
                }
                if (root.right != null)
                {
                    float x1, y1;
                    x1 = X + width / 2;
                    y1 = Y + deltaH;
                    gr.DrawLine(new Pen(Color.Black), X, Y, x1, y1);
                    DrawNode(gr, ref root.right, x1, y1, NodeSize, width / 2, deltaH);
                }

                gr.FillEllipse(new SolidBrush(Color.White), X - NodeSize / 2, Y - NodeSize / 2, NodeSize, NodeSize);//закрашенный шарик
                gr.DrawEllipse(new Pen(Color.Black), X - NodeSize / 2, Y - NodeSize / 2, NodeSize, NodeSize);//Окружность
                gr.DrawString(root.value.ToString(), new Font(new FontFamily("times"), NodeSize / 3), new SolidBrush(Color.Black), X - NodeSize / 4, Y - NodeSize / 4);
                gr.DrawString(root.key.ToString(), new Font(new FontFamily("times"), NodeSize / 4), new SolidBrush(Color.Red), (X - NodeSize / 4) + NodeSize / 5, (Y - NodeSize / 4) + NodeSize / 3);
            }
            else return;
        }

        public int List()//сколько листов
        {
            int a = 0;
            List(ref a, root);
            return a;
        }
        private void List(ref int i, Node<T> r)//реккурсиуная
        {
            if (r == null) return;
            if ((r.left == null) && (r.right == null))
            {
                i++;
                return;
            }
            else
            {
                List(ref i, r.right);
                List(ref i, r.left);
            }
        }

        public string Search(int key)
        {
            if (root == null)
            {
                return "Дерево пyсто";
            }
            else
            {

               return recSearch(root,key).ToString();
            }
        }
            string recSearch(Node<T> Node,int key)
        {
            if (Node != null)
            {
                if (key == Node.key)
                { return Node.ToString(); }
                else
                {
                    if (key < Node.key)
                    {
                       return recSearch(Node.left, key);
                    }
                    else
                    {
                        return recSearch(Node.right, key);
                    }




                }
            }
            else
            { return "Не найдено"; }
            }

        
    }
}
