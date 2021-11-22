using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppLib.Expressao
{
    public class Arvore
    {
        public List<Item> itens = new List<Item>();

        public Arvore() { }

        public Arvore(System.Windows.Forms.TreeView treeView1)
        {
            itens.Add(new Item(treeView1.Nodes[0]));
        }
    }

    public class Item
    {
        public String Texto { get; set; }
        public String Tipo { get; set; }
        public List<Item> itens = new List<Item>();

        public Item() { }

        public Item(System.Windows.Forms.TreeNode treeNode1)
        {
            this.Texto = treeNode1.Text;
            this.Tipo = treeNode1.ImageKey;

            for (int i = 0; i < treeNode1.Nodes.Count; i++)
            {
                this.itens.Add(new Item(treeNode1.Nodes[i]));
            }
        }
    }

}
