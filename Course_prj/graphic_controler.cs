using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml;

namespace Course_prj
{
    public class graphic_controler
    {
        private System.Windows.Forms.PictureBox target;
        private data_objects draws;
        public List<Node> nodes;
        private bool moving;
        private int movingNode;
        public List<Line> lines;
        public List<Connection> relations;
        private bool line;
        private int drawingLine;
        public event EventHandler Update;

        /// <param name="target">Канва, на которой будет отображаться картинка</param>
        public graphic_controler(System.Windows.Forms.PictureBox target, data_objects draws)
        {
            this.target = target;
            this.draws = draws;
            nodes = new List<Node>();
            draws.target = target.CreateGraphics();
            lines = new List<Line>();
            relations = new List<Connection>();
        }

        public void AddNode(float x, float y, ConnectType type)
        {
            Node node = new Node();
            node.x = x;
            node.y = y;
            node.station_el = false;
            node.active_el = false;
            node.path_el = false;
            node.side = draws.nodeSide;
            nodes.Add(node);
            draws.Node(node, nodes.Count - 1, "node");
            if (Update != null)
                Update(this, EventArgs.Empty);
        }

        public void AddStation(float x, float y, ConnectType type)
        {
            Node node = new Node();
            node.x = x;
            node.y = y;
            node.station_el = true;
            node.active_el = false;
            node.path_el = false;
            node.side = draws.nodeSide;
            nodes.Add(node);
            draws.Station(node, nodes.Count - 1, "station");
            if (Update != null)
                Update(this, EventArgs.Empty);
        }

        public bool isNode(float x, float y)
        {
            if (nodes.Count > 0)
            {
                for (int i = 0; i < nodes.Count; i++)
                {
                    if (draws.inNode(nodes[i], x, y))
                        return true;
                }
            }
            return false;
        }

        public int getNode(float x, float y)
        {
            if (nodes.Count > 0)
            {
                for (int i = 0; i < nodes.Count; i++)
                {
                    if (draws.inNode(nodes[i], x, y))
                        return i;
                }
            }
            return -1;
        }

        public bool startMove(float x, float y)
        {
            if (nodes.Count > 0)
            {
                for (int i = 0; i < nodes.Count; i++)
                {
                    if (draws.inNode(nodes[i], x, y))
                    {
                        moving = true;
                        movingNode = i;
                        return true;
                    }
                }
            }
            movingNode = -1;
            moving = false;
            return false;
        }

        public void Move(float x, float y)
        {
            if (moving && !line)
            {
                if (movingNode < 0)
                    return;
                Node tmp = nodes[movingNode];
                tmp.x = x;
                tmp.y = y;
                for (int j = 0; j < relations.Count; j++)
                {
                    if (relations[j].to == movingNode)
                    {
                        Line t = lines[j];
                        t.toX = x + draws.nodeSide / 2;
                        t.toY = y + draws.nodeSide / 2;
                        lines[j] = t;
                    }
                    if (relations[j].from == movingNode)
                    {
                        Line t = lines[j];
                        t.fromX = x + draws.nodeSide / 2;
                        t.fromY = y + draws.nodeSide / 2;
                        lines[j] = t;
                    }
                    //&& ((relations[j].from == i) && (relations[j].to == con.from)))
                }
                nodes[movingNode] = tmp;
                Redraw();
            }
            if (line)
            {
                Line tmp = lines[drawingLine];
                tmp.toX = x;
                tmp.toY = y;
                lines[drawingLine] = tmp;
                Redraw();
            }

        }

        public void endMove()
        {
            if (moving)
                for (int j = 0; j < relations.Count; j++)
                {
                    if (relations[j].to == movingNode)
                    {
                        Line t = lines[j];
                        t.toX = nodes[movingNode].x + draws.nodeSide / 2;
                        t.toY = nodes[movingNode].y + draws.nodeSide / 2;
                        lines[j] = t;
                    }
                    if (relations[j].from == movingNode)
                    {
                        Line t = lines[j];
                        t.fromX = nodes[movingNode].x + draws.nodeSide / 2;
                        t.fromY = nodes[movingNode].y + draws.nodeSide / 2;
                        lines[j] = t;
                    }
                    //&& ((relations[j].from == i) && (relations[j].to == con.from)))
                }
            Redraw();
            moving = false;
        }

        public void Redraw()
        {
            Bitmap b = new Bitmap(target.Width, target.Height);
            Graphics g = Graphics.FromImage(b);
            draws.target = g;
            g.Clear(target.BackColor);
            if (lines.Count > 0)
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].path_el)
                    {
                        draws.Line(lines[i], relations[i].weight, relations[i].type, "normal");
                    }
                    else
                    {
                        draws.Line(lines[i], relations[i].weight, relations[i].type, "short");
                    }
                }
            if (nodes.Count > 0)
                for (int i = 0; i < nodes.Count; i++) 
                {
                    if (nodes[i].path_el)
                    {
                        if (nodes[i].station_el)
                        {
                            draws.Station(nodes[i], i, "short");
                        }
                        else
                        {
                            draws.Node(nodes[i], i, "short");
                        }
                        continue;
                    }
                    if (nodes[i].station_el)
                    {
                        if(nodes[i].active_el)
                            draws.Station(nodes[i], i, "active");
                        else
                            draws.Station(nodes[i], i, "station");
                    }
                    else
                    {
                        if(nodes[i].active_el)
                            draws.Node(nodes[i], i, "active");
                        else
                            draws.Node(nodes[i], i, "node");
                    }

                }
            draws.target = target.CreateGraphics();
            target.Image = b;
            GC.Collect();
        }

        public bool startLine(float x, float y, int weight, ConnectType type)
        {
            if (nodes.Count > 0)
            {
                for (int i = 0; i < nodes.Count; i++)
                {
                    if (draws.inNode(nodes[i], x, y))
                    {
                        line = true;
                        Line tmp = new Line();
                        tmp.fromX = nodes[i].x + draws.nodeSide / 2;
                        tmp.fromY = nodes[i].y + draws.nodeSide / 2;
                        tmp.toX = x;
                        tmp.toY = y;
                        tmp.path_el = false;
                        lines.Add(tmp);
                        Connection con = new Connection();
                        con.from = i;
                        con.to = i;
                        con.weight = weight;
                        con.type = type;
                        relations.Add(con);
                        Redraw();
                        drawingLine = lines.Count - 1;
                        return true;
                    }
                }
            }
            drawingLine = -1;
            line = false;
            if (Update != null)
                Update(this, EventArgs.Empty);
            return false;
        }

        public void highlightPath(int num, bool normal)
        {
           
            Node node = new Node();
            node.x = nodes[num].x;
            node.y = nodes[num].y;
            node.station_el = nodes[num].station_el;
            node.side = nodes[num].side;
            if(normal)
            {
                node.active_el = false;
                node.path_el = false;
            }
            else
            {
                node.active_el = nodes[num].active_el;
                node.path_el = true;
            }
            
            nodes[num] = node;
            Redraw();

        }
        public void highlightConnection(int from, int to, bool norm)
        {
            for (int j = 0; j < relations.Count; j++)
            {
                if (norm)
                {
                    Line tmp = new Line();
                    tmp.fromX = lines[j].fromX;
                    tmp.fromY = lines[j].fromY;
                    tmp.toX = lines[j].toX;
                    tmp.toY = lines[j].toY;
                    tmp.path_el = false;
                    lines[j] = tmp;
                    Redraw();
                }
                else if (((relations[j].from == from) && (relations[j].to == to)) || ((relations[j].from == to) && (relations[j].to == from)))
                {
                    Line tmp = new Line();
                    tmp.fromX = lines[j].fromX;
                    tmp.fromY = lines[j].fromY;
                    tmp.toX = lines[j].toX;
                    tmp.toY = lines[j].toY;
                    tmp.path_el = true;
                    lines[j] = tmp;
                    Redraw();
                }
            }

       
    }
    public void highlightNode(float x, float y)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                if (draws.inNode(nodes[i], x, y))
                {
                    Node node = new Node();
                    node.x = nodes[i].x;
                    node.y = nodes[i].y;
                    node.station_el = nodes[i].station_el;
                    node.side = nodes[i].side;
                    node.active_el = true;
                    node.path_el = nodes[i].path_el;
                    nodes[i] = node;
                    /*if (nodes[i].station_el)
                        draws.activeStation(nodes[i], i);
                    else
                        draws.activeNode(nodes[i], i);
                    */
                    Redraw();
                }
                else
                {
                    Node node = new Node();
                    node.x = nodes[i].x;
                    node.y = nodes[i].y;
                    node.station_el = nodes[i].station_el;
                    node.side = nodes[i].side;
                    node.active_el = false;
                    node.path_el = nodes[i].path_el;
                    nodes[i] = node;
                    Redraw();
                }
            }

            if (Update != null)
                Update(this, EventArgs.Empty);
        }
        public bool endLine(float x, float y)
        {
            if (!line)
                return false;
            line = false;
            if (nodes.Count > 0)
            {
                for (int i = 0; i < nodes.Count; i++)
                {
                    if (draws.inNode(nodes[i], x, y))
                    {
                        Line tmp = lines[drawingLine];
                        tmp.toX = nodes[i].x + draws.nodeSide / 2;
                        tmp.toY = nodes[i].y + draws.nodeSide / 2;
                        tmp.path_el = false;
                        lines[drawingLine] = tmp;
                        Connection con = relations[drawingLine];
                        bool f = false;
                        for (int j = 0; j < relations.Count; j++)
                            if (((relations[j].from == con.from) && (relations[j].to == i)) || ((relations[j].from == i) && (relations[j].to == con.from)))
                            {
                                f = true;
                                break;
                            }
                        if (!f)
                        {
                            con.to = i;
                            relations[drawingLine] = con;
                            drawingLine = -1;
                            Redraw();
                            if (Update != null)
                                Update(this, EventArgs.Empty);
                            return true;
                        }
                        else
                            break;

                    }
                }
            }
            lines.RemoveAt(drawingLine);
            relations.RemoveAt(drawingLine);
            Redraw();
            if (Update != null)
                Update(this, EventArgs.Empty);
            return false;
        }

        public void deleteLine(int from, int to)
        {
            for (int j = 0; j < relations.Count; j++)
                if (((relations[j].from == from) && (relations[j].to == to)) || ((relations[j].from == to) && (relations[j].to == from)))
                {
                    relations.RemoveAt(j);
                    lines.RemoveAt(j);
                    Redraw();
                }
            if (Update != null)
                Update(this, EventArgs.Empty);
        }

        public void deleteNode(float x, float y)
        {
            int i = getNode(x, y);
            if (i < 0)
                return;
            for (int j = 0; j < relations.Count; j++)
            {
                if ((relations[j].to == i) || (relations[j].from == i))
                {
                    relations.RemoveAt(j);
                    lines.RemoveAt(j);
                    j--;
                    continue;
                }
                if (relations[j].to > i)
                {
                    Connection con = relations[j];
                    con.to--;
                    relations[j] = con;
                }
                if (relations[j].from > i)
                {
                    Connection con = relations[j];
                    con.from--;
                    relations[j] = con;
                }
            }
            nodes.RemoveAt(i);
            Redraw();
            if (Update != null)
                Update(this, EventArgs.Empty);
        }

        public void Save(string filename)
        {
            XmlTextWriter doc = new XmlTextWriter(filename, null);
            doc.WriteStartDocument();
            doc.WriteStartElement("Nodes");
            for (int i = 0; i < nodes.Count; i++)
            {
                doc.WriteStartElement("Node");
                doc.WriteStartAttribute("index");
                doc.WriteValue(i);
                doc.WriteEndAttribute();
                doc.WriteStartAttribute("side");
                doc.WriteValue(nodes[i].side);
                doc.WriteEndAttribute();
                doc.WriteStartAttribute("x");
                doc.WriteValue(nodes[i].x);
                doc.WriteEndAttribute();
                doc.WriteStartAttribute("y");
                doc.WriteValue(nodes[i].y);
                doc.WriteEndAttribute();
                doc.WriteStartAttribute("station_el");
                doc.WriteValue(nodes[i].station_el);
                doc.WriteEndAttribute();
                doc.WriteStartAttribute("active_el");
                doc.WriteValue(nodes[i].active_el);
                doc.WriteEndAttribute();
                doc.WriteStartAttribute("path_el");
                doc.WriteValue(nodes[i].path_el);
                doc.WriteEndAttribute();
                doc.WriteEndElement();
            }
            //doc.WriteEndElement(); 
            doc.WriteStartElement("Connections");
            for (int i = 0; i < relations.Count; i++)
            {
                doc.WriteStartElement("Connection");
                doc.WriteStartAttribute("index");
                doc.WriteValue(i);
                doc.WriteEndAttribute();
                doc.WriteStartAttribute("from");
                doc.WriteValue(relations[i].from);
                doc.WriteEndAttribute();
                doc.WriteStartAttribute("to");
                doc.WriteValue(relations[i].to);
                doc.WriteEndAttribute();
                doc.WriteStartAttribute("weight");
                doc.WriteValue(relations[i].weight);
                doc.WriteEndAttribute();
                doc.WriteStartAttribute("type");
                doc.WriteValue((int)relations[i].type);
                doc.WriteEndAttribute();
                doc.WriteEndElement();
            }
            doc.WriteEndElement();
            doc.WriteStartElement("Lines");
            for (int i = 0; i < lines.Count; i++)
            {
                doc.WriteStartElement("Line");
                doc.WriteStartAttribute("index");
                doc.WriteValue(i);
                doc.WriteEndAttribute();
                doc.WriteStartAttribute("fromX");
                doc.WriteValue((double)lines[i].fromX);
                doc.WriteEndAttribute();
                doc.WriteStartAttribute("fromY");
                doc.WriteValue(lines[i].fromY);
                doc.WriteEndAttribute();
                doc.WriteStartAttribute("toX");
                doc.WriteValue(lines[i].toX);
                doc.WriteEndAttribute();
                doc.WriteStartAttribute("toY");
                doc.WriteValue(lines[i].toY);
                doc.WriteEndAttribute();
                doc.WriteEndElement();
            }
            doc.WriteEndElement();
            doc.WriteEndDocument();
            doc.Close();

        }

        public void Load(string filename)
        {
            if (!System.IO.File.Exists(filename))
                return;
            XmlTextReader doc = new XmlTextReader(filename);
            nodes.Clear();
            relations.Clear();
            lines.Clear();
            System.Globalization.NumberFormatInfo nfi = new System.Globalization.CultureInfo("en-US").NumberFormat;
            while (doc.Read())
            {
                if (doc.Name == "Node")
                {
                    Node tmp = new Node();
                    tmp.side = float.Parse(doc.GetAttribute("side"));
                    tmp.x = float.Parse(doc.GetAttribute("x"), nfi);
                    tmp.y = float.Parse(doc.GetAttribute("y"), nfi);
                    tmp.station_el = bool.Parse(doc.GetAttribute("station_el"));
                    tmp.active_el = bool.Parse(doc.GetAttribute("active_el"));
                    tmp.path_el = bool.Parse(doc.GetAttribute("path_el"));
                    nodes.Add(tmp);
                }
                if (doc.Name == "Connection")
                {
                    Connection tmp = new Connection();
                    tmp.from = int.Parse(doc.GetAttribute("from"));
                    tmp.to = int.Parse(doc.GetAttribute("to"));
                    tmp.weight = float.Parse(doc.GetAttribute("weight"), nfi);
                    int x = int.Parse(doc.GetAttribute("type"));
                    tmp.type = (x == 1 ? ConnectType.duplex : ConnectType.half_duplex);
                    relations.Add(tmp);
                }
                if (doc.Name == "Line")
                {
                    Line tmp = new Line();
                    tmp.fromX = float.Parse(doc.GetAttribute("fromX"), nfi);
                    tmp.fromY = float.Parse(doc.GetAttribute("fromY"), nfi);
                    tmp.toX = float.Parse(doc.GetAttribute("toX"), nfi);
                    tmp.toY = float.Parse(doc.GetAttribute("toY"), nfi);
                    lines.Add(tmp);
                }
            }
            doc.Close();
            Redraw();
            if (Update != null)
                Update(this, EventArgs.Empty);
        }

        public void Clear()
        {
            nodes.Clear();
            relations.Clear();
            lines.Clear();
            Redraw();
        }
    }
}
