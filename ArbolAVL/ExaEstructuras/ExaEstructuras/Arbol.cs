using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaEstructuras
{
    internal class Arbol
    {
        public Nodo Root { get; set; }
        private int Height(Nodo node)
        {
            if (node == null)
            {
                return 0;
            }

            return node.Height;
        }

        private Nodo MinValue(Nodo nodo)
        {
            if (nodo == null || nodo.Left == null)
            {
                return nodo;
            }
            return MinValue(nodo.Left);

        }

        public Nodo Search(int capacidad)
        {
            Nodo x = Root;

            while (x != null && x.Data != capacidad)
            {
                if (capacidad < x.Data)
                {
                    x = x.Left;
                }
                else
                {
                    x = x.Right;
                }
            }

            return x;
        }

        public Nodo Delete(Nodo root, int cantidad)
        {
            if (root == null)
            {
                return root;
            }
            else if (cantidad < root.Data)
            {
                root.Left = Delete(root.Left, cantidad);
            }
            else if (cantidad > root.Data)
            {
                root.Right = Delete(root.Right, cantidad);
            }
            else
            {
                Nodo temp;
                if (root.Left == null)
                {
                    temp = root.Right;
                    root = null;
                    return temp;
                }
                else if (root.Right == null)
                {
                    temp = root.Left;
                    root = null;
                    return temp;
                }

                temp = MinValue(root.Right);
                // here we have to copy all the temp node data and pass to the root node
                root.Data = temp.Data;
                root.Right = Delete(root.Right, temp.Data);
            }
            root.Height = 1 + Math.Max(Height(root.Left), Height(root.Right));


            int balancef = BalanceFactor(root);


            if (balancef > 1 && BalanceFactor(root.Left) >= 0)
            {
                return RotateRight(root);
            }

            if (balancef < -1 && BalanceFactor(root.Right) <= 0)
            {
                return RotateLeft(root);
            }

            if (balancef > 1 && BalanceFactor(root.Left) < 0)
            {
                root.Left = RotateLeft(root.Left);
                return RotateRight(root);
            }

            if (balancef < -1 && BalanceFactor(root.Right) > 0)
            {
                root.Right = RotateRight(root.Right);
                return RotateLeft(root);
            }
            return root;

        }

        private int BalanceFactor(Nodo node)
        {
            if (node == null)
            {
                return 0;
            }

            return Height(node.Left) - Height(node.Right);
        }

        public Nodo Insert(Nodo root, Nodo nodo)
        {
            if (root == null)
            {
                Console.WriteLine($"Insertando nodo con dato {nodo.Data}");
                return nodo;
            }
            else if (root.Data > nodo.Data)
            {
                Console.WriteLine($"Root data {root.Data} es mayor que nodo data {nodo.Data}");
                root.Left = Insert(root.Left, nodo);
                Console.WriteLine($"Nodo {root.Left.Data} inseertado a la izquierda de {root.Data}");
            }
            else
            {
                Console.WriteLine($"Root data {root.Data} es menor que nodo data {nodo.Data}");
                root.Right = Insert(root.Right, nodo);
                Console.WriteLine($"Nodo {root.Right.Data} inseertado a la iderecha de {root.Data}");

            }


            root.Height = 1 + Math.Max(Height(root.Left), Height(root.Right));


            int balancef = BalanceFactor(root);
            Console.WriteLine($"For node {root.Data} the H is {root.Height} with a balance factor of {balancef}");

            if (balancef > 1 && nodo.Data < root.Left.Data)
            {
                Console.WriteLine($"Right rotate with node {root.Data} as pivot");
                return RotateRight(root);
            }



            if (balancef < -1 && nodo.Data > root.Right.Data)
            {
                Console.WriteLine($"Left rotate with node {root.Data} as pivot");
                return RotateLeft(root);
            }

            if (balancef > 1 && nodo.Data > root.Left.Data)
            {
                Console.WriteLine($"Left and then Right rotate with node {root.Left.Data} and  {root.Data} as pivot");
                root.Left = RotateLeft(root.Left);
                return RotateRight(root);
            }

            if (balancef < -1 && nodo.Data < root.Right.Data)
            {
                Console.WriteLine($"Right and left rotate with node {root.Right.Data} and  {root.Data} as pivot");
                root.Right = RotateRight(root.Right);
                return RotateLeft(root);
            }
            return root;

        }
        public Nodo RotateLeft(Nodo pivote)
        {
            Nodo B = pivote.Right;
            Nodo Y = B.Left;

            Console.WriteLine($" right node of pivot is {B.Data} and Y is {Y} (if blank, null)");


            B.Left = pivote;
            pivote.Right = Y;
            Console.WriteLine($"From new pibot {B.Data} left node is {B.Left.Data} and right is {B.Right.Data}");
            pivote.Height = 1 + Math.Max(Height(pivote.Left), Height(pivote.Right));
            B.Height = 1 + Math.Max(Height(B.Left), Height(B.Right));

            return B;
        }

        public Nodo RotateRight(Nodo pivote)
        {
            Nodo A = pivote.Left;
            Nodo Y = A.Right;

            A.Right = pivote;
            pivote.Left = Y;

            pivote.Height = 1 + Math.Max(Height(pivote.Left), Height(pivote.Right));
            A.Height = 1 + Math.Max(Height(A.Left), Height(A.Right));

            return A;
        }

        public void PrintTree(Nodo root)
        {
            Queue<Nodo> queueNodos = new Queue<Nodo>();

            queueNodos.Enqueue(root);
            Nodo temp = null;

            while (queueNodos.Count > 0)
            {
                temp = queueNodos.Dequeue();

                if (temp.Left != null)
                {
                    queueNodos.Enqueue(temp.Left);
                }

                if (temp.Right != null)
                {
                    queueNodos.Enqueue(temp.Right);
                }

                Console.WriteLine("Nodo " + temp.Data);
            }
        }
        public int encontrarValor(Nodo raiz, int capacidad)
        {
            // Inicializar el valor cercano como un valor grande para asegurar la actualización correcta
            int valorCercano = int.MaxValue;

            // Inicializar el nodo cercano como nulo
            Nodo nodoCercano = null;

            // Traversal del árbol
            while (raiz != null)
            {
                // primero revisa si la raiz es igual al valor 
                if (raiz.Data == capacidad)
                {
                    return raiz.Data;
                }
                // Actualizar el valor cercano y el nodo cercano si encontramos un valor más cercano
                if (Math.Abs(raiz.Data - capacidad) < Math.Abs(valorCercano - capacidad))
                {
                    valorCercano = raiz.Data;
                    nodoCercano = raiz;
                }

                else if (raiz.Data > capacidad)
                {
                    raiz = raiz.Left;
                }
                else
                {
                    // si no es alguno de los dos casos debe ser menor a la capacidad entonces buscamos a la derecha
                    raiz = raiz.Right;
                }
            }

            // Devolver el valor más cercano encontrado
            return nodoCercano != null ? nodoCercano.Data : -1;
        }

        public List<Nodo> getNodos(Nodo root)
        {
            Queue<Nodo> nodos = new Queue<Nodo>();
            List<Nodo> nodosOut = new List<Nodo>();

            nodos.Enqueue(root);

            while (nodos.Count > 0)
            { 
                Nodo temp = nodos.Dequeue(); 

                if (temp.Left != null)
                {
                    nodos.Enqueue(temp.Left);
                }
                if (temp.Right != null)
                {
                    nodos.Enqueue (temp.Right);
                }
                nodosOut.Add(temp);
                
            }

            return nodosOut;
        }


    }
}
