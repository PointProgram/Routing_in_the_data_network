using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Course_prj
{
    //chanell types
    public enum ConnectType
    {
        half_duplex,
        duplex,
    }
    //node structure
    public struct Node
    {
        public float x;
        public float y;
        public float side;
        public bool station_el;
        public bool active_el;
        public bool path_el;
    }
    //node connection between nodes structre
    public struct Connection
    {
        public int from;
        public int to;
        public float weight;
        public ConnectType type;
        public bool path_el;
    }

    //drawing line
    public struct Line
    {
        public float fromX;
        public float fromY;
        public float toX;
        public float toY;
        public bool path_el;
    }

    public struct Path
    {
        public List<int> path;
        public float weight;
    }

    public struct RouteRes
    {
        public int querys;
        public int info;
        public int time;
    }

    public struct Info
    {
        public ulong connect;
        public ulong disconnect;
        public ulong information;
        public ulong ack;
        public ulong servTime;
        public ulong discover;
        public ulong time;
        public Path path;
    }
    //initialization of main forms using interface structure with restricted access
    public interface data_objects
    {
        float nodeSide
        {
            get;
        }

        Graphics target
        {
            get;
            set;
        }

        void Node(Node node, int index, string mod);

        void Station(Node node, int index, string mod);

        void Line(Line line, float weight, ConnectType type, string mod);

        bool inNode(Node node, float x, float y);
    }

    //class for elements with methdods declaration
    public class graphic_elements : data_objects
    {
        private Graphics _target;
        Color half_duplex_line = Color.Blue;
        Color duplex_line = Color.DarkKhaki;

        public float nodeSide
        {
            get
            {
                return 27;
            }
        }

        public Graphics target
        {
            get
            {
                return _target;
            }
            set
            {
                _target = value;
            }
        }
        #region [ ~InterfaceMethods]

        public void Node(Node node, int index, string mod)
        {
            //var src = new Bitmap("station.png");
            if(mod == "node")
                target.FillEllipse(new SolidBrush(Color.DarkGray), node.x, node.y, node.side, node.side);
            else if (mod == "active")
                target.FillEllipse(new SolidBrush(Color.DarkGreen), node.x, node.y, node.side, node.side);
            else if (mod == "short")
                target.FillEllipse(new SolidBrush(Color.DeepSkyBlue), node.x, node.y, node.side, node.side);
            target.DrawEllipse(new Pen(Color.Black), node.x, node.y, node.side, node.side);
            target.DrawString(index.ToString(), new Font(FontFamily.GenericSansSerif, 7), Brushes.Black, new PointF(node.x + nodeSide / 8, node.y + nodeSide / 2 - 9));
           
        }

        public void Station(Node node, int index, string mod)
        {
            if (mod == "station")
                target.FillRectangle(new SolidBrush(Color.Gray), node.x, node.y, node.side, node.side);
            else if (mod == "active")
                target.FillRectangle(new SolidBrush(Color.DarkGreen), node.x, node.y, node.side, node.side);
            else if (mod == "short")
                target.FillRectangle(new SolidBrush(Color.DeepSkyBlue), node.x, node.y, node.side, node.side);
            target.DrawRectangle(new Pen(Color.Black), node.x, node.y, node.side, node.side);
            target.DrawRectangle(new Pen(Color.Black), node.x - 10, node.y+27, node.side + 20, node.side-15);
            if (mod == "station")
                target.FillRectangle(new SolidBrush(Color.LightGray), node.x - 9, node.y + 28, node.side + 19, node.side - 16);
            else if (mod == "active")
                target.FillRectangle(new SolidBrush(Color.LightGreen), node.x - 9, node.y + 28, node.side + 19, node.side - 16);
            else if (mod == "short")
                target.FillRectangle(new SolidBrush(Color.DeepSkyBlue), node.x - 9, node.y + 28, node.side + 19, node.side - 16);
            target.DrawString(index.ToString(), new Font(FontFamily.GenericSansSerif, 7), Brushes.Black, new PointF(node.x - 7 , node.y + 27));
        }

        public void Line(Line line, float weight, ConnectType type, string mod)
        {
            // target.DrawLine(new Pen((type==ConnectType.Ground?groundLine:satelliteLine), 2), line.fromX, line.fromY, line.toX, line.toY);
            
            if (mod == "normal") 
            {
                Pen dashed_pen = new Pen(Color.DarkKhaki, 2);
                if (type == ConnectType.duplex)
                {
                    target.DrawLine(dashed_pen, line.fromX, line.fromY, line.toX, line.toY);
                }
                else
                {
                    dashed_pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    target.DrawLine(dashed_pen, line.fromX, line.fromY, line.toX, line.toY);
                }
            }
            else if (mod == "short")
            {
                Pen dashed_pen = new Pen(Color.MidnightBlue, 2);
                if (type == ConnectType.duplex)
                {
                    target.DrawLine(dashed_pen, line.fromX, line.fromY, line.toX, line.toY);
                }
                else 
                {
                    dashed_pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    target.DrawLine(dashed_pen, line.fromX, line.fromY, line.toX, line.toY);
                }
            }
            target.DrawString(weight.ToString(), new Font(FontFamily.GenericSansSerif, 9, FontStyle.Bold), Brushes.Black, new PointF((line.fromX + line.toX) / 2, (line.fromY + line.toY) / 2));
        }

        public bool inNode(Node node, float x, float y)
        {
            return (Math.Pow(x - node.x, 2) + Math.Pow(y - node.y, 2)) <= Math.Pow(node.side, 2);
        }

        #endregion

    }
}
