using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public class ColorBagNode
    {
        private string bagColor;

        private List<Tuple<int, ColorBagNode>> parents;
        private List<Tuple<int, ColorBagNode>> childs;

        public List<Tuple<int, ColorBagNode>> Parents { get => parents; }
        public List<Tuple<int, ColorBagNode>> Childs { get => childs; }
        public string BagColor { get => bagColor; }

        public ColorBagNode(string bagColor)
        {
            this.bagColor = bagColor;

            parents = new List<Tuple<int, ColorBagNode>>();
            childs = new List<Tuple<int, ColorBagNode>>();
        }

        public void AddParent(int amount, ColorBagNode parent)
        {
            parents.Add(new Tuple<int, ColorBagNode>(amount, parent));
        }

        public void AddChild(int amount, ColorBagNode child)
        {
            childs.Add(new Tuple<int, ColorBagNode>(amount, child));
        }
    }
}