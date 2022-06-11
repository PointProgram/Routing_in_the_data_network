using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Course_prj
{
    public partial class application_view : Form
    {
        graphic_controler container;
        int delNode1 = -1, delNode2;
        bool deleting;
        bool add_node_bool = false, start_add_node = false, del_node_bool = false, del_rely_bool = false, con_rely_bool = false;
        bool least_time_bool = false, least_nodes_bool = false, datagram_bool = false, virtual_bool = false, add_station_bool = false, start_line = false;
        bool message_bool = false, least_table_bool = false, if_del = false;
        public ConnectType type;
        public int weight;
        List<Path> res;
        Point lst_point;
        string txt;
        public List<int> list_weight;

        public application_view()
        {
            InitializeComponent();
            list_weight = new List<int>();
            list_weight.Add(2);
            list_weight.Add(4);
            list_weight.Add(5);
            list_weight.Add(9);
            list_weight.Add(10);
            list_weight.Add(12);
            list_weight.Add(18);
            list_weight.Add(21);
            list_weight.Add(23);
            list_weight.Add(26);
            list_weight.Add(28);
        }

        void highlight_elements()
        {
            int beg;

            for (int i = 1; i < container.lines.Count; i++)
            {
                beg = i - 1;
                container.highlightConnection(beg, i, true);
            }
            for (int i = 0; i < container.nodes.Count; i++)
            {
                container.highlightPath(i, true);
            }
        }
        private void network_app_Load(object sender, EventArgs e)
        {
            container = new graphic_controler(main_frame, new graphic_elements());
            container.Update += new EventHandler(container_Update);
            rely_param.Visible = false;
        }

        void container_Update(object sender, EventArgs e)
        {
            int con = 0;
            for (int i = 0; i < container.relations.Count; i++)
                if (container.relations[i].type == ConnectType.duplex)
                    con++;
                else
                    con++;
            node_num.Text = container.nodes.Count.ToString();
            relation_num.Text = con.ToString();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void exit_MouseEnter(object sender, EventArgs e)
        {
            exit.ForeColor = Color.Red;
        }

        private void exit_MouseLeave(object sender, EventArgs e)
        {
            exit.ForeColor = Color.Black;
        }

        
        private void network_app_MouseMove(object sender, MouseEventArgs e)
        {
            /*if(e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lst_point.X;
                this.Top += e.Y - lst_point.Y;
            }*/
        }

        private void main_frame_Click(object sender, EventArgs e)
        {
        
        }

        private void main_frame_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button != MouseButtons.Right)
            {

                if (container.startMove(e.X, e.Y))
                {
                    start_add_node = true;
                }
                else
                    start_add_node = false;
                
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }


        private void rely_btn_MouseClick(object sender, MouseEventArgs e)
        {
            if(con_rely_bool)
            {
                con_rely_bool = false;
                rely_btn.BackColor = Color.White;
            }
            else
            {
                con_rely_bool = true;
                rely_btn.BackColor = Color.Red;
            }
            del_node_bool = false;
            del_node.BackColor = Color.White;
            add_node_bool = false;
            add_node.BackColor = Color.White;
            rely_param.Visible = true;
            del_rely_bool = false;
            rely_del.BackColor = Color.White;
            lst.SelectedIndex = 0;
            weight = -1;
            least_time_bool = false;
            least_time.BackColor = Color.White;
            least_nodes_bool = false;
            least_nodes.BackColor = Color.White;
            datagram_bool = false;
            datagram.BackColor = Color.White;
            virtual_bool = false;
            virtual_ch.BackColor = Color.White;
            packet_info.Visible = false;
            add_station_bool = false;
            new_station.BackColor = Color.White;
        }

        private void rely_del_Click(object sender, EventArgs e)
        {
            highlight_elements();
            Caption.Text = "Tap on two busy nodes";
            Caption.Visible = true;
            hide_table.Visible = false;
            close_table.Visible = false;
            message.Visible = false;
            list_time.Visible = false;
            if (del_rely_bool)
            {
                del_rely_bool = false;
                rely_del.BackColor = Color.White;
            }
            else
            {
                del_rely_bool = true;
                rely_del.BackColor = Color.Red;
            }
            add_node_bool = false;
            add_node.BackColor = Color.White;
            del_node_bool = false;
            rely_btn.BackColor = Color.White;
            con_rely_bool = false;
            rely_param.Visible = false;
            least_time_bool = false;
            least_time.BackColor = Color.White;
            least_nodes_bool = false;
            least_nodes.BackColor = Color.White;
            datagram_bool = false;
            datagram.BackColor = Color.White;
            virtual_bool = false;
            virtual_ch.BackColor = Color.White;
            packet_info.Visible = false;
            add_station_bool = false;
            new_station.BackColor = Color.White;

        }

        private void label2_Click(object sender, EventArgs e)
        {
           
        }

        private void least_time_Click(object sender, EventArgs e)
        {
            highlight_elements();
            Caption.Text = "Tap on the sender station";
            Caption.Visible = true;
            least_table_bool = true;
            message_bool = false;
            hide_table.Visible = false;
            close_table.Visible = false;
            message.Visible = false;
            if (least_time_bool)
            {
                least_time_bool = false;
                least_time.BackColor = Color.White;
            }
            else
            {
                least_time_bool = true;
                least_time.BackColor = Color.Red;
            }
            add_node_bool = false;
            add_node.BackColor = Color.White;
            del_node_bool = false;
            del_node.BackColor = Color.White;
            del_rely_bool = false;
            rely_btn.BackColor = Color.White;
            con_rely_bool = false;
            rely_param.Visible = false;
            del_rely_bool = false;
            rely_del.BackColor = Color.White;
            least_nodes_bool = false;
            least_nodes.BackColor = Color.White;
            datagram_bool = false;
            datagram.BackColor = Color.White;
            virtual_bool = false;
            virtual_ch.BackColor = Color.White;
            packet_info.Visible = false;
            add_station_bool = false;
            new_station.BackColor = Color.White;
        }

        private void close_table_Click(object sender, EventArgs e)
        {
            hide_table.Visible = false;
            list_time.Visible = false;
            close_table.Visible = false;
            message.Visible = false;
            message_bool = false;
            least_table_bool = false;

            list_time.Items.Clear();
        }

        private void datagram_Click(object sender, EventArgs e)
        {
            highlight_elements();
            Caption.Text = "Tap on the sender station";
            Caption.Visible = true;
            least_table_bool = true;
            message_bool = false;
            hide_table.Visible = false;
            close_table.Visible = false;
            message.Visible = false;
            if (datagram_bool)
            {
                datagram_bool = false;
                datagram.BackColor = Color.White;
            }
            else
            {
                datagram_bool = true;
                datagram.BackColor = Color.Red;
            }
            least_time_bool = false;
            least_time.BackColor = Color.White;
            add_node_bool = false;
            add_node.BackColor = Color.White;
            del_node_bool = false;
            del_node.BackColor = Color.White;
            del_rely_bool = false;
            rely_btn.BackColor = Color.White;
            con_rely_bool = false;
            rely_param.Visible = false;
            del_rely_bool = false;
            rely_del.BackColor = Color.White;
            least_nodes_bool = false;
            least_nodes.BackColor = Color.White;
            virtual_bool = false;
            virtual_ch.BackColor = Color.White;
            packet_info.Visible = true;
            add_station_bool = false;
            new_station.BackColor = Color.White;
        }

        private void virtual_ch_Click(object sender, EventArgs e)
        {
            if(virtual_bool)
            {
                virtual_bool = false;
                virtual_ch.BackColor = Color.White;
            }
            else
            {
                virtual_bool = true;
                virtual_ch.BackColor = Color.Red;
            }
            least_time_bool = false;
            least_time.BackColor = Color.White;
            add_node_bool = false;
            add_node.BackColor = Color.White;
            del_node_bool = false;
            del_node.BackColor = Color.White;
            del_rely_bool = false;
            rely_btn.BackColor = Color.White;
            con_rely_bool = false;
            rely_param.Visible = false;
            del_rely_bool = false;
            rely_del.BackColor = Color.White;
            least_nodes_bool = false;
            least_nodes.BackColor = Color.White;
            datagram_bool = false;
            datagram.BackColor = Color.White;
            packet_info.Visible = true;
            add_station_bool = false;
            new_station.BackColor = Color.White;
        }

        private void clear_all_Click(object sender, EventArgs e)
        {
            container.Clear();
            hide_table.Visible = false;
            close_table.Visible = false;
            message.Visible = false;
            list_time.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult save = saveFile.ShowDialog();
            if (save == DialogResult.OK)
                container.Save(saveFile.FileName);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFile.ShowDialog() == DialogResult.OK)
                container.Load(openFile.FileName);
        }

        private void add_node_Click(object sender, EventArgs e)
        {
            highlight_elements();
            Caption.Text = "Tap on the main area";
            Caption.Visible = true;
            hide_table.Visible = false;
            close_table.Visible = false;
            message.Visible = false;
            list_time.Visible = false;
        }

        private void rely_param_Paint(object sender, PaintEventArgs e)
        {

        }

        private void list_time_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void del_node_Click(object sender, EventArgs e)
        {
            highlight_elements();
            Caption.Text = "Tap on the node/station";
            Caption.Visible = true;
            hide_table.Visible = false;
            close_table.Visible = false;
            message.Visible = false;
            list_time.Visible = false;
        }

        private void duplex_r_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void list_time_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void message_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void rely_btn_Click(object sender, EventArgs e)
        {
            highlight_elements();
            Caption.Text = "Tap on two free nodes/stations";
            Caption.Visible = true;
            hide_table.Visible = false;
            close_table.Visible = false;
            message.Visible = false;
            list_time.Visible = false;
        }

        private void hide_table_Click(object sender, EventArgs e)
        {
            if(message_bool)
            {
                list_time.Visible = false;

                if (hide_table.Text == "Show table")
                {
                    message.Visible = true;
                    hide_table.Text = "Hide table";
                }
                else
                {
                    message.Visible = false;
                    hide_table.Text = "Show table";
                }
               
            }
            else if (least_table_bool)
            {
                message.Visible = false;
                if (hide_table.Text == "Show table")
                {
                    list_time.Visible = true;
                    hide_table.Text = "Hide table";
                }
                else 
                {
                    list_time.Visible = false;
                    hide_table.Text = "Show table";
                }
                    
            }
            
            
        }

        private void new_station_Click(object sender, EventArgs e)
        {
            highlight_elements();
            Caption.Text = "Tap on the main area";
            Caption.Visible = true;
            hide_table.Visible = false;
            close_table.Visible = false;
            message.Visible = false;
            list_time.Visible = false;
            if (add_station_bool)
            {
                add_station_bool = false;
                new_station.BackColor = Color.White;
            }
            else
            {
                add_station_bool = true;
                new_station.BackColor = Color.Red;
            }
            add_node_bool = false;
            add_node.BackColor = Color.White;
            del_node_bool = false;
            del_node.BackColor = Color.White;
            del_rely_bool = false;
            rely_btn.BackColor = Color.White;
            con_rely_bool = false;
            rely_param.Visible = false;
            del_rely_bool = false;
            rely_del.BackColor = Color.White;
            least_time_bool = false;
            least_time.BackColor = Color.White;
            least_nodes_bool = false;
            least_nodes.BackColor = Color.White;
            datagram_bool = false;
            datagram.BackColor = Color.White;
            virtual_bool = false;
            virtual_ch.BackColor = Color.White;
            packet_info.Visible = false;
        }

        private void least_nodes_Click(object sender, EventArgs e)
        {
            highlight_elements();
            Caption.Text = "Tap on the sender station";
            Caption.Visible = true;
            message_bool = true;
            least_table_bool = false;
            hide_table.Visible = false;
            close_table.Visible = false;
            list_time.Visible = false;
            if (least_nodes_bool)
            {
                least_nodes_bool = false;
                least_nodes.BackColor = Color.White;
            }
            else
            {
                least_nodes_bool = true;
                least_nodes.BackColor = Color.Red;
            }
            least_time_bool = false;
            least_time.BackColor = Color.White;
            add_node_bool = false;
            add_node.BackColor = Color.White;
            del_node_bool = false;
            del_node.BackColor = Color.White;
            del_rely_bool = false;
            rely_btn.BackColor = Color.White;
            con_rely_bool = false;
            rely_param.Visible = false;
            del_rely_bool = false;
            rely_del.BackColor = Color.White;
            datagram_bool = false;
            datagram.BackColor = Color.White;
            virtual_bool = false;
            virtual_ch.BackColor = Color.White;
            packet_info.Visible = false;
            add_station_bool = false;
            new_station.BackColor = Color.White;
        }

        private void send_Click(object sender, EventArgs e)
        {
            /*Simulation form = new Simulation();
            form.Text = text;
            form.data = (ulong)numericUpDown1.Value;
            form.packetsize = (ulong)numericUpDown2.Value;
            form.service = (ulong)numericUpDown3.Value;
            form.results = results;
            form.from = from;
            form.gr = gr;
            form.scan = checkBox1.Checked;
            form.Show();
            this.Close();*/
            packet_info.Visible = false;
        }

        private void list_time_Click(object sender, EventArgs e)
        {
            var firstSelectedItem = list_time.SelectedItems[0];
            int t = firstSelectedItem.Index;
            int beg;
            
            Caption.Text = "From - to: " + firstSelectedItem.SubItems[0].Text;
            txt = Caption.Text;

            highlight_elements();

            for (int k = 0; k < res[t].path.Count; k++)
            {
                    container.highlightPath(res[t].path[k], false);
            }
            
            for(int k = 1; k < res[t].path.Count; k++)
            {
                beg = k - 1;
                container.highlightConnection(res[t].path[beg], res[t].path[k], false);
            }
            
            Caption.Visible = true;
            
        }

        private void approve_btn_Click(object sender, EventArgs e)
        {
            if (rand.Checked)
            {
                Random rnd = new Random();
                weight = list_weight[rnd.Next(0, list_weight.Count - 1)];
            }
            else
                weight = (int)list_weight[lst.SelectedIndex];
            type = (duplex_r.Checked ? ConnectType.duplex : ConnectType.half_duplex);
            rely_param.Visible = false;
        }

        private void add_node_MouseClick(object sender, MouseEventArgs e)
        {
            if (add_node_bool)
            {
                add_node_bool = false;
                add_node.BackColor = Color.White;
            }
            else
            {
                add_node_bool = true;
                add_node.BackColor = Color.Red;
            }
            del_node_bool = false;
            del_node.BackColor = Color.White;
            del_rely_bool = false;
            rely_btn.BackColor = Color.White;
            con_rely_bool = false;
            rely_param.Visible = false;
            del_rely_bool = false;
            rely_del.BackColor = Color.White;
            least_time_bool = false;
            least_time.BackColor = Color.White;
            least_nodes_bool = false;
            least_nodes.BackColor = Color.White;
            datagram_bool = false;
            datagram.BackColor = Color.White;
            virtual_bool = false;
            virtual_ch.BackColor = Color.White;
            packet_info.Visible = false; 
            add_station_bool = false;
            new_station.BackColor = Color.White;
        }

        private void del_node_MouseClick(object sender, MouseEventArgs e)
        {
            if (del_node_bool)
            {
                del_node_bool = false;
                del_node.BackColor = Color.White;
            }
            else
            {
                del_node_bool = true;
                del_node.BackColor = Color.Red;
            }
            add_node_bool = false;
            add_node.BackColor = Color.White;
            del_rely_bool = false;
            rely_btn.BackColor = Color.White;
            con_rely_bool = false;
            rely_param.Visible = false;
            del_rely_bool = false;
            rely_del.BackColor = Color.White;
            least_time_bool = false;
            least_time.BackColor = Color.White;
            least_nodes_bool = false;
            least_nodes.BackColor = Color.White;
            datagram_bool = false;
            datagram.BackColor = Color.White;
            virtual_bool = false;
            virtual_ch.BackColor = Color.White;
            packet_info.Visible = false;
            add_station_bool = false;
            new_station.BackColor = Color.White;

        }

        private void main_frame_MouseUp(object sender, MouseEventArgs e)
        {
            if (add_node_bool && !start_add_node)
            {
                container.AddNode(e.X, e.Y, ConnectType.duplex);
                start_add_node = false;
            }
            else if (add_station_bool && !start_add_node)
            {
                container.AddStation(e.X, e.Y, ConnectType.duplex);
                start_add_node = false;
            }
            else if (del_node_bool)
            {
                container.deleteNode(e.X, e.Y);

            }
            else if (con_rely_bool)
            {
                container.highlightNode(e.X, e.Y);
                if (auto.Checked)
                {
                    if (rand.Checked)
                    {
                        Random rnd = new Random();
                        weight = list_weight[rnd.Next(0, list_weight.Count - 1)];
                    }
                    else
                        weight = (int)list_weight[lst.SelectedIndex];
                    type = (duplex_r.Checked ? ConnectType.duplex : ConnectType.half_duplex);
                }
                if (weight > 0 && !start_line)
                {
                    if (container.startLine(e.X, e.Y, weight, type))
                        start_line = true;
                    if (!auto.Checked)
                    {
                        con_rely_bool = false;
                        rely_btn.BackColor = Color.Yellow;
                    }
                    return;
                }
            }
            else if (del_rely_bool && delNode1 < 0)
            {
                if_del = true;
                delNode1 = container.getNode(e.X, e.Y);

                return;
            }
            else if (least_time_bool || least_nodes_bool)
            {
                string text;
                list_time.Items.Clear();
                int node;
                hide_table.Visible = true;
                hide_table.Text = "Hide table";
                Caption.Text = "Tap the row to show path";
                Caption.Visible = true;
                algorithm_model gr = new algorithm_model(container.relations, container.nodes);
                node = container.getNode(e.X, e.Y);
                if (node >= 0)
                {
                    if (least_time_bool)
                        res = gr.LeastWeight(node, new List<int>());
                    else
                        res = gr.LeastCount(node);
                }
                if (res == null)
                {
                    Caption.Text = "Error, wrong station";
                    Caption.Visible = true;
                    hide_table.Visible = false;

                }
                else
                {
                    for (int i = 0; i < res.Count; i++)
                    {
                        if(!container.nodes[i].station_el)
                        {
                            continue;
                        }
                        string[] subs = new string[4];
                        subs[0] = node.ToString() + " - " + i.ToString();
                        if (res[i].weight > 0)
                            subs[1] = res[i].weight.ToString();
                        else
                            subs[1] = "-";
                        if (res[i].weight > 0)
                            subs[3] = res[i].path.Count.ToString();
                        else
                            subs[3] = "-";

                        if (res[i].path.Count > 0)
                        {
                            subs[2] = res[i].path[0].ToString();
                            for (int j = 1; j < res[i].path.Count; j++)
                                subs[2] += " > " + res[i].path[j].ToString();
                        }
                        else
                            subs[2] = "-";
                        list_time.Items.Add(new ListViewItem(subs));
                    }
                    list_time.Visible = true;
                    close_table.Visible = true;
                }
                
            }
            else if (datagram_bool || virtual_bool)
            {
                int frm = container.getNode(e.X, e.Y);
                algorithm_model gr = new algorithm_model(container.relations, container.nodes);
                List<Path> results = gr.LeastWeight(frm, new List<int>());

                ulong data = (ulong)data_n.Value;
                List<Info> stats = new List<Info>(); ;
                ulong packetsize = (ulong)datap_n.Value;
                ulong service = (ulong)syst_n.Value;


                bool scan = check.Checked;

                message.Items.Clear();
                stats.Clear();
                ulong packets;
                if(data % packetsize == 0)
                    packets = (ulong)Math.Round((double)data / (double)packetsize);
                else
                    packets = (ulong)Math.Round((double)data / (double)packetsize) + 1;
                for (int i = 0; i < results.Count; i++)
                {
                    string[] subs = new string[6];
                    if (virtual_bool)
                    {
                        if (!container.nodes[i].station_el)
                        {
                            continue;
                        }
                        Info inf = gr.logical(results[i], (int)packets, (ulong)packetsize, (int)service, false, scan);
                        subs[0] = frm.ToString();
                        subs[1] = i.ToString();
                        if (scan)
                            subs[2] = (inf.connect + inf.disconnect + inf.ack + inf.discover).ToString();
                        else
                            subs[2] = (inf.connect + inf.disconnect + inf.ack).ToString();
                        subs[3] = (inf.information).ToString();

                        if (results[i].path.Count > 0)
                        {
                            subs[5] = results[i].path[0].ToString();
                            for (int j = 1; j < results[i].path.Count; j++)
                            {
                                subs[5] += " - " + results[i].path[j].ToString();
                            }
                            subs[4] = inf.time.ToString();
                        }
                        else
                        {
                            subs[2] = "-";
                            subs[3] = "-";
                            subs[4] = "-";
                            inf.information = 0;
                        }
                        stats.Add(inf);
                    }
                    else
                    {
                        if (!container.nodes[i].station_el)
                        {
                            continue;
                        }
                        Info inf = gr.DataGramm(results[i], (int)packets, (ulong)packetsize, (int)service, scan);
                        subs[0] = frm.ToString();
                        subs[1] = i.ToString();
                        subs[2] = (inf.ack + inf.discover).ToString();
                        subs[3] = (inf.information).ToString();

                        if (results[i].path.Count > 0)
                        {
                            subs[5] = results[i].path[0].ToString();
                            for (int j = 1; j < results[i].path.Count; j++)
                            {
                                subs[5] += " -> " + results[i].path[j].ToString();
                            }
                            if (inf.path.path.Count > 0)
                            {
                                subs[5] += " and " + inf.path.path[0].ToString();
                                for (int j = 1; j < inf.path.path.Count; j++)
                                {
                                    subs[5] += " -> " + inf.path.path[j].ToString();
                                }
                            }
                            subs[4] = inf.time.ToString();
                        }
                        else
                        {
                            subs[2] = "Узел недостижим";
                            subs[3] = "-";
                            subs[4] = "-";
                            inf.information = 0;
                        }
                        stats.Add(inf);
                    }
                    message.Items.Add(new ListViewItem(subs));
                    close_table.Visible = true;
                    message.Visible = true;
                }

            }
            if (e.Button != MouseButtons.Right)
            {
                container.endMove();
                container.highlightNode(e.X, e.Y);
                if (container.endLine(e.X, e.Y))
                    start_line = false;
                else
                    start_line = false;
                if (if_del)
                {
                    delNode2 = container.getNode(e.X, e.Y);
                    if (delNode2 < 0)
                        if_del = false;
                    else
                        container.deleteLine(delNode1, delNode2);
                    delNode1 = -1;
                }
            }
        }

        private void network_app_MouseDown(object sender, MouseEventArgs e)
        {
            //lst_point = new Point(e.X, e.Y);
        }

        private void main_frame_MouseMove(object sender, MouseEventArgs e)
        {
            if (!del_rely_bool)
                container.Move(e.X, e.Y);
        }

    }
}
