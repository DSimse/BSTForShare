using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BinarySearchTree
{
    //add, remove and find values
    class Program
    {
        public class Node
        {
            public int Data;
            public Node LeftChild;
            public Node RightChild;

            //To find the number/node
            public Node Find(int value)
            {
                //this node is the starting current node
                Node currentNode = this;

                //loop through this node and all of the children of this node
                while (currentNode != null)
                {
                    //if the current nodes data is equal to the value passed in return it
                    if (value == currentNode.Data)
                    {
                        Console.WriteLine("Your number is found in the tree");
                        return currentNode;
                    }
                    else if (value > currentNode.Data)//if the value passed in is greater than the current data then go to the right child
                    {
                        currentNode = currentNode.RightChild;
                        Console.WriteLine("Searching the tree");
                    }
                    else//otherwise if the value is less than the current nodes data the go to the left child node 
                    {
                        currentNode = currentNode.LeftChild;
                        Console.WriteLine("Searching the tree");
                    }
                }
                //Node not found
                Console.WriteLine("Your number is not found in the tree");
                return null;
            }
        }
        
        //This is the basic tree :)
        public class BinarySearchTree
        {
            public Node root;

            public BinarySearchTree()
            {
                root = null;
            }

            public Node ReturnRoot()
            {
                return root;
            }

            //Used for removeValue function to find node when the parent has two children
            private Node GetSuccessor(Node node)
            {
                Node parentOfSuccessor = node;
                Node successor = node;
                Node current = node.RightChild;

                //starting at the right child we go down every left child node
                while (current != null)
                {
                    parentOfSuccessor = successor;
                    successor = current;
                    current = current.LeftChild;// go to next left node
                }
                //if the succesor is not just the right node then
                if (successor != node.RightChild)
                {
                    //set the Left node on the parent node of the succesor node to the right child node of the successor in case it has one
                    parentOfSuccessor.LeftChild = successor.RightChild;
                    //attach the right child node of the node being deleted to the successors right node
                    successor.RightChild = node.RightChild;
                }
                //attach the left child node of the node being deleted to the successors leftnode node
                successor.LeftChild = node.LeftChild;

                return successor;
            }


            //function to add a value to the tree :D
            //needs a check if the number already is in the tree
            public void addValue(int data)
            {
                Node newNode = new Node();
                newNode.Data = data;
                if (root == null)
                {
                    root = newNode;
                }
                else
                {
                    Node current = root;
                    Node parent;

                    while (true)
                    {
                        parent = current;
                        //Hvis det nye tal er mindre end current data så skal det nye tal placeres til venstre
                        if(data < current.Data)
                        {
                            current = current.LeftChild;
                            if (current == null)
                            {
                                parent.LeftChild = newNode;
                                break;
                            }
                        }
                        //ellers er det til højre
                        else
                        {
                            current = current.RightChild;
                            if(current == null)
                            {
                                parent.RightChild = newNode;
                                break;
                            }
                        }
                    }
                }
            }
            
            //function to remove a value from the tree 
            public void removeValue(int data)
            {
                //Set the current and parent node to root, so when we remove we can remove using the parents reference
                Node current = root;
                Node parent = root;
                bool isLeftChild = false;//keeps track of which child of parent should be removed

                //empty tree
                if (current == null)
                {//nothing to be removed, end method
                    Console.WriteLine("The tree is empty");
                    return;
                }

                //Find the Node
                //loop through until node is not found or if we found the node with matching data
                while (current != null && current.Data != data)
                {
                    //set current node to be new parent reference, then we look at its children
                    parent = current;

                    //if the data we are looking for is less than the current node then we look at its left child
                    if (data < current.Data)
                    {
                        current = current.LeftChild;
                        isLeftChild = true;//Set the variable to determin which child we are looking at
                    }
                    else
                    {//Otherwise we look at its right child
                        current = current.RightChild;
                        isLeftChild = false;//Set the variable to determin which child we are looking at
                    }
                }

                //if the node is not found there is nothing to delete just return
                if (current == null)
                {
                    Console.WriteLine("The number is not in the tree");
                    return;
                }

                //We found a Leaf node aka no children
                if (current.RightChild == null && current.LeftChild == null)
                {
                    //The root doesn't have parent to check what child it is,so just set to null
                    if (current == root)
                    {
                        root = null;
                    }
                    else
                    {
                        //When not the root node
                        //see which child of the parent should be deleted
                        if (isLeftChild)
                        {
                            //remove reference to left child node
                            parent.LeftChild = null;
                        }
                        else
                        {   //remove reference to right child node
                            parent.RightChild = null;
                        }
                    }
                }
                else if (current.RightChild == null) //current only has left child, so we set the parents node child to be this nodes left child
                {
                    //If the current node is the root then we just set root to Left child node
                    if (current == root)
                    {
                        root = current.LeftChild;
                    }
                    else
                    {
                        //see which child of the parent should be deleted
                        if (isLeftChild)//is this the right child or left child
                        {
                            //current is left child so we set the left node of the parent to the current nodes left child
                            parent.LeftChild = current.LeftChild;
                        }
                        else
                        {   //current is right child so we set the right node of the parent to the current nodes left child
                            parent.RightChild = current.LeftChild;
                        }
                    }
                }
                else if (current.LeftChild == null) //current only has right child, so we set the parents node child to be this nodes right child
                {
                    //If the current node is the root then we just set root to Right child node
                    if (current == root)
                    {
                        root = current.RightChild;
                    }
                    else
                    {
                        //see which child of the parent should be deleted
                        if (isLeftChild)
                        {   //current is left child so we set the left node of the parent to the current nodes right child
                            parent.LeftChild = current.RightChild;
                        }
                        else
                        {   //current is right child so we set the right node of the parent to the current nodes right child
                            parent.RightChild = current.RightChild;
                        }
                    }
                }
                else//Current Node has both a left and a right child
                {
                    //When both child nodes exist we can go to the right node and then find the leaf node of the left child as this will be the least number
                    //that is greater than the current node. It may have right child, so the right child would become..left child of the parent of this leaf aka successer node

                    //Find the successor node aka least greater node
                    Node successor = GetSuccessor(current);
                    //if the current node is the root node then the new root is the successor node
                    if (current == root)
                    {
                        root = successor;
                    }
                    else if (isLeftChild)
                    {//if this is the left child set the parents left child node as the successor node
                        parent.LeftChild = successor;
                    }
                    else
                    {//if this is the right child set the parents right child node as the successor node
                        parent.RightChild = successor;
                    }
                }
                Console.WriteLine("The number has been removed from the tree");
            }

            //To find the a specific/given node
            public Node findVal(int data)
            {
                //if the root is not null then we call the find method on the root
                if (root != null)
                {
                    // call node method Find
                    return root.Find(data);
                }
                else
                {//the root is null so we return null, nothing to find
                    return null;
                }
            }

            //to triverse/write out the values in the tree in the three ways: inorder, preorder and postorder
            public void Inorder(Node Root)
            {
                if (Root != null)
                {
                    Inorder(Root.LeftChild);
                    Console.Write(Root.Data + " ");
                    Inorder(Root.RightChild);
                }
            }
            public void Preorder(Node Root)
            {
                if (Root != null)
                {
                    Console.Write(Root.Data + " ");
                    Preorder(Root.LeftChild);
                    Preorder(Root.RightChild);
                }
            }
            public void Postorder(Node Root)
            {
                if (Root != null)
                {
                    Postorder(Root.LeftChild);
                    Postorder(Root.RightChild);
                    Console.Write(Root.Data + " ");
                }
            }

        }
        
        //the program
        static void Main(string[] args)
        {
            BinarySearchTree tree1 = new BinarySearchTree();
            //add the given values from the picture of the tree
            tree1.addValue(8);
            tree1.addValue(3);
            tree1.addValue(10);
            tree1.addValue(1);
            tree1.addValue(6);
            tree1.addValue(14);
            tree1.addValue(4);
            tree1.addValue(7);
            tree1.addValue(13);

            //Introduce the user to the tree
            Console.WriteLine("Welcome to the binary search tree");
            Console.WriteLine();
            Console.WriteLine("You can add a number to the tree with the 'add' command");
            Console.WriteLine("You can remove a number to the tree with the 'remove' command");
            Console.WriteLine("You can get all numbers in the tree with the 'triverse' command");
            Console.WriteLine("Or you can check if a number is in the tree with the 'find' command");
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("Please enter a command");
                //read what the userinput is
                string input1 = Console.ReadLine();
                switch (input1)
                {
                    case "add":
                        //code to add a new value to the tree
                        Console.WriteLine("Please enter the number you want to add to the binary tree");
                        string addedValue = Console.ReadLine();
                        //parse the string value to a int
                        int addedValueInt = Int32.Parse(addedValue);
                        //add the value to the tree                 I assume the user is not a duce and tries to type in something that is not a number
                        tree1.addValue(addedValueInt);
                        Console.WriteLine("Your number " + addedValueInt + " has been added to the binary tree");
                        Console.WriteLine();
                        break;
                    case "triverse":
                        //code to write out every number in the tree
                        Console.WriteLine("Inorder traversal: ");
                        tree1.Inorder(tree1.ReturnRoot());
                        Console.WriteLine();
                        Console.WriteLine("Preorder traversal: ");
                        tree1.Preorder(tree1.ReturnRoot());
                        Console.WriteLine();
                        Console.WriteLine("Postorder traversal: ");
                        tree1.Postorder(tree1.ReturnRoot());
                        Console.WriteLine();
                        Console.WriteLine();
                        break;
                    case "remove":
                        Console.WriteLine("What number do you want to remove?");
                        string removedValue = Console.ReadLine();
                        //parse the string number into an int
                        int removedValueInt = Int32.Parse(removedValue);
                        //Code to remove from tree
                        tree1.removeValue(removedValueInt);
                        Console.WriteLine();
                        break;
                    case "find":
                        Console.WriteLine("Which number do you want to find?");
                        string findValue = Console.ReadLine();
                        //parse the string into an int
                        int findValueInt = Int32.Parse(findValue);
                        //Code to see if it is in the tree
                        tree1.findVal(findValueInt);
                        Console.WriteLine();
                        break;
                    default:
                        Console.WriteLine("Invalid comment");
                        Console.WriteLine();
                        break;
                }
            }
        }
    }
}
