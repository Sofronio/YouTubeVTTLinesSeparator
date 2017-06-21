using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace YouTubeVTTLinesSeparator
{
    public partial class Form1 : Form
    {
        string str_path = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\";
        string str_fileVTT = "";
        string str_fileSubtitle = "";
        List<string> list_youtube_1stProcess = new List<string>();
        List<string> list_ass_1stProcess = new List<string>();
        List<string> list_text_result = new List<string>();
        List<Obj_Unit> list_youtube_2ndProcess;
        int int_AssTimePosition = 0;
        public Form1()
        {
            InitializeComponent();
            listBox_youtubeVTT.AllowDrop = true;
            listBox_youtubeVTT.DragEnter += new DragEventHandler(ListBox_DragEnter);
            listBox_youtubeVTT.DragDrop += new DragEventHandler(ListBox1_DragDrop);
            listBox_assSubtitle.AllowDrop = true;
            listBox_assSubtitle.DragEnter += new DragEventHandler(ListBox_DragEnter);
            listBox_assSubtitle.DragDrop += new DragEventHandler(ListBox_Subtitle_DragDrop);
        }

        void ListBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        void ListBox1_DragDrop(object sender, DragEventArgs e)
        {
            listBox_youtubeVTT.Items.Clear();
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                using (StreamReader stRead = new StreamReader(file))
                {
                    while (!stRead.EndOfStream)
                    {
                        listBox_youtubeVTT.Items.Add(stRead.ReadLine());
                    }
                }
            }
            str_fileVTT = files[0];
        }

        void ListBox_Subtitle_DragDrop(object sender, DragEventArgs e)
        {
            listBox_assSubtitle.Items.Clear();
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                using (StreamReader stRead = new StreamReader(file))
                {
                    while (!stRead.EndOfStream)
                    {
                        listBox_assSubtitle.Items.Add(stRead.ReadLine());
                    }
                }
            }
            str_fileSubtitle = files[0];
        }

        private int fn_stringToMs(string str_input)
        {
            str_input = str_input.Replace("<", "");
            str_input = str_input.Replace(">", "");
            int int_result = 0;
            string[] str_time = str_input.Split(':');
            string[] str_second = str_time[2].Split('.');
            //this.Text = str_time[0];
            int_result = Convert.ToInt32(str_second[1]) + 1000 * (Convert.ToInt32(str_time[0]) * 3600 + Convert.ToInt32(str_time[1]) * 60 + Convert.ToInt32(str_second[0]));
            return int_result;
        }

        private string fn_msTOString(int int_input)
        {
            int int_hour = int_input / 1000 / 3600;
            int int_min = (int_input - int_hour * 1000 * 3600) / 1000 / 60;
            int int_sec = (int_input - int_hour * 1000 * 3600 - int_min * 1000 * 60) / 1000;
            int int_ms = int_input - int_hour * 1000 * 3600 - int_min * 1000 * 60 - int_sec * 1000;
            //0:00:17.01
            string str_result = int_hour.ToString() + ":" + int_min.ToString("D2") + ":" + int_sec.ToString("D2") + "." + (int_ms / 10).ToString("D2");
            return str_result;
        }

        private void fn_process_2()
        {
            listBox_timePairs.Items.Clear();
            list_ass_1stProcess.Clear();
            list_text_result.Clear();
            for (int i = 0; i < listBox_assSubtitle.Items.Count; i++)
            {
                //Dialogue: 0,0:00:20.58,0:00:23.62,1080GR Eng48,,0,0,0,,
                string pattern = @"(\d{1}:\d{2}:\d{2}\.\d{2}),(\d{1}:\d{2}:\d{2}\.\d{2})";
                Regex rx = new Regex(pattern);
                string str_line = listBox_assSubtitle.Items[i].ToString();
                foreach (Match match in rx.Matches(str_line))
                {
                    if (int_AssTimePosition == 0)
                        int_AssTimePosition = i;
                    string str_input = match.Value.ToString();
                    string[] str_temp = str_input.Split(',');
                    list_ass_1stProcess.Add(fn_stringToMs(str_temp[0]).ToString());
                    list_ass_1stProcess.Add(fn_stringToMs(str_temp[1]).ToString());
                }
            }
            int int_startLineNumber = 0;//remember last search position
            for (int i = 0; i < list_ass_1stProcess.Count; i = i + 2)
            {
                string str_content = "";
                for (int j = int_startLineNumber; j < list_youtube_2ndProcess.Count; j++)
                {
                    int int_timeShift = Convert.ToInt16(textBox1.Text);
                    int int_assStart = Convert.ToInt32(list_ass_1stProcess[i]);
                    int int_assEnd = Convert.ToInt32(list_ass_1stProcess[i + 1]);
                    int int_youtubeStart = list_youtube_2ndProcess[j].int_start + int_timeShift;
                    if (int_youtubeStart > int_assStart && int_youtubeStart < int_assEnd)
                    {
                        str_content = str_content + " " + list_youtube_2ndProcess[j].str_content;
                    }
                    int_startLineNumber = j;
                    if (int_youtubeStart > int_assEnd)
                        break;
                }
                listBox_timePairs.Items.Add(str_content);
                list_text_result.Add(str_content);
            }
            this.Text = list_ass_1stProcess.Count + " lines loaded, " + listBox_timePairs.Items.Count + " lines processed";
        }

        private void fn_process_1()
        {
            List<string> list_str_result = new List<string>();
            list_youtube_1stProcess.Clear();

            for (int i = 0; i < listBox_youtubeVTT.Items.Count; i++)
            {
                if (listBox_youtubeVTT.Items[i].ToString().Contains("align:start position:"))
                {
                    //Timeline process
                    int[] int_time = new int[2];
                    int[] int_timeNext = new int[2];

                    //00:00:16.280 --> 00:00:23.760 align:start position:19%
                    string[] str_temp = listBox_youtubeVTT.Items[i].ToString().Split(new string[] { " --> " }, StringSplitOptions.None);
                    string str_lineStart = str_temp[0];
                    str_temp = str_temp[1].ToString().Split(new string[] { " align:" }, StringSplitOptions.None);
                    string str_lineEnd = str_temp[0];
                    //str_LineStart 00:00:16.280
                    //str_lineEnd   00:00:23.760
                    //Text process
                    string str_line = listBox_youtubeVTT.Items[i + 1].ToString();
                    string pattern = @"</?c.*?>";
                    // /? match 0 or 1 '/'
                    // c.*? match cxxxxxxxxxxxxx ? for lazy
                    // this will cut off any </c> <c> <c.colorE5E5E5>
                    string replacement = @"";
                    string input = str_line;
                    str_line = Regex.Replace(input, pattern, replacement);
                    list_youtube_1stProcess.Add(str_lineStart);
                    //list_youtube_1stProcess.Add(str_line);
                    Regex rx = new Regex(@"<\d{2}:\d{2}:\d{2}\.\d{3}>");
                    string str_rest = str_line; //fisrt time, get whole line.
                    string str_word = "";
                    string str_time = "";
                    foreach (Match match in rx.Matches(str_line))
                    {
                        str_time = match.Value.ToString().Replace("<", "").Replace(">", "");
                        str_temp = str_rest.Split(new string[] { match.Value.ToString() }, StringSplitOptions.None);
                        str_word = str_temp[0];
                        str_rest = str_temp[1];
                        list_youtube_1stProcess.Add(str_word);
                        list_youtube_1stProcess.Add(str_time);
                        list_youtube_1stProcess.Add(str_time);
                    }
                    list_youtube_1stProcess.Add(str_rest); // add last word
                    list_youtube_1stProcess.Add(str_lineEnd);
                }
            }
            list_youtube_2ndProcess = fn_youtube1stProcess(list_youtube_1stProcess);
        }

        private void button_doit_Click(object sender, EventArgs e)
        {
            fn_process_1();
            fn_process_2();

        }
        class Obj_Unit
        {
            public int int_start { get; set; }
            public int int_end { get; set; }
            public string str_content { get; set; }
        }

        private List<Obj_Unit> fn_youtube1stProcess(List<string> input_str_list)
        {
            List<Obj_Unit> list_Result = new List<Obj_Unit>();
            for (int i = 0; i < input_str_list.Count; i = i + 3)
            {
                Obj_Unit obj_unit = new Obj_Unit();
                obj_unit.int_start = fn_stringToMs(input_str_list[i].ToString());
                obj_unit.int_end = fn_stringToMs(input_str_list[i + 2].ToString());
                obj_unit.str_content = input_str_list[i + 1].ToString().Trim();
                list_Result.Add(obj_unit);
                //this.Text = i.ToString();
            }
            return list_Result;
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (str_fileSubtitle != "")
            {
                string input = str_fileSubtitle;
                string pattern = @"(.*)\.ass";
                string replacement = @"$1.youtube_vtt_lines.ass";
                string str_fileOutput = Regex.Replace(input, pattern, replacement);
                List<string> list_ass_result = new List<string>();
                for (int i = 0; i < listBox_assSubtitle.Items.Count; i++)
                {
                    if (i < int_AssTimePosition)
                        list_ass_result.Add(listBox_assSubtitle.Items[i].ToString());
                    else

                        list_ass_result.Add(listBox_assSubtitle.Items[i].ToString() + list_text_result[i - int_AssTimePosition].ToString().Trim());
                }
                File.WriteAllLines(str_fileOutput, list_ass_result);
            }
        }
    }
}


