using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
namespace JCBSCH
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            radioButton1.Checked = true;
        }
        System.Timers.Timer atimer = new System.Timers.Timer();
        int circle = 1;//循环次数
        List<JCB> readyList = new List<JCB>();
        List<JCB> finishList = new List<JCB>();
   //     List<JCB> execList=new List<JCB>();
        List<JCB> waitList = new List<JCB>();
        int clock = 0;//时钟源
        int clickCount = 0;
        class JCB
        {
            private string name;          //作业名
            private int atime;         //作业到达时间
            public int runtime;       //作业服务时间
            public int exeT;//作业运行时间
            private int ftime;         //作业完成时间
            private int total;           //周转时间
            private float welght;        //带权周转时间（周转系数）
            private string state;          //作业运行状态
            public JCB(string name, int atime, int runtime)
            {
                this.name = name;
                this.atime = atime;
                this.runtime = runtime;
                this.state = "wait";
                this.exeT = 0;
            }
            public JCB()
            {
                this.name = "";
                this.runtime = 0;
                this.atime = 0;
                this.exeT = 0;
            }
            public string get_name() { return this.name; }
            public int get_atime() { return this.atime; }
            public int get_runtime() { return this.runtime; }
            public int get_ftime() { return this.ftime; }
            public bool vaildState()
            {
                if (this.state.Equals("wait") != true && this.state.Equals("exec") != true && this.state.Equals("finish") != true) return false;
                return true;
            }
            public string get_nameANDexeT() {
                string s1 = "进程名："+this.name;
                s1+="运行时间"+this.exeT;
                return s1;
            }
            public int get_total() { return this.total; }
            public string get_state() { return this.state; }
            public int get_exeT() { return this.exeT; }
            public float get_welght() { return this.welght; }
            public void set_welght(float f) { this.welght = f; }
            public void set_exeT(int exeT) 
            {
                if (exeT >= this.get_runtime())return; //参数错误
                this.exeT = exeT;
                return;
            }
            public void set_total(int total) { this.total = total; }
            public void set_state(string state)
            {
                string s1 = this.state;
                this.state = state;
                if (vaildState() == false)
                {
                    this.state = s1;
                    return;
                }
                return;
            }
            public void set_ftime(int ft) { this.ftime = ft; }
            public string Print_my() {
                string s1 = " 进程名字： ";
                s1 += this.get_name();
                s1 += " 需要服务时间： " + this.get_runtime();
                s1 += " 到达时间： " + this.get_atime();
            //    s1 += " 带权周转时间系数： " + this.get_welght();
                s1 += " 完成时间： " + this.get_ftime();
                return s1;
            
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //if (radioButton1.Checked == true)
            //{
            //    this.listBox1.Items.Clear();
            //    JCB j = new JCB(textBox1.Text, int.Parse(textBox2.Text), int.Parse(textBox3.Text));
            //    readyList.Add(j);
            //    MessageBox.Show("进程已经加入到队列");
            //    this.textBox1.Text = "";
            //    this.textBox2.Text = "";
            //    this.textBox3.Text = "";
            //    foreach (JCB c in readyList)
            //        this.listBox1.Items.Add(c.Print_my());
            //}
        //    if (radioButton2.Checked == true||radioButton3.Checked==true)
         //   {
                this.listBox4.Items.Clear();
                JCB j = new JCB(textBox1.Text, int.Parse(textBox2.Text), int.Parse(textBox3.Text));
                waitList.Add(j);
                MessageBox.Show("进程已经加入到队列");
                this.textBox1.Text = "";
                this.textBox2.Text = "";
                this.textBox3.Text = "";
                foreach(JCB c in waitList)
                this.listBox4.Items.Add(c.Print_my());
         //   }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            readyList.Clear();
            waitList.Clear();
            finishList.Clear();
            MessageBox.Show("所有作业已清空！");
            this.listBox1.Items.Clear();
            this.listBox2.Items.Clear();
            circle = 1;
        }
        //public void printTIMER(object source, System.Timers.ElapsedEventArgs e)
        //{
        //    count += 1;
        //    SetTB(count.ToString());
        //}
        private delegate void SetTbMethodInvok(string value);
        private void setTB(string value)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetTbMethodInvok(setTB), value);
            }
            else
            {    this.listBox1.Items.Clear();
                //this.listBox2.Items.Clear();
                this.listBox2.Items.Add(value);
                foreach (JCB K in readyList)
                {
                    this.listBox1.Items.Add(K.Print_my());
                }
               
            }
        }
        private void setTB2(string value)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetTbMethodInvok(setTB), value);
            }
            else
            {
                this.listBox1.Items.Clear();
                //this.listBox2.Items.Clear();
                this.listBox1.Items.Add(value);
                foreach (JCB K in readyList)
                {
                    this.listBox1.Items.Add(K.Print_my());
                }

            }
        }
        private delegate void SetTBMethodInvok(string value);
        private void SetTB(string value)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetTBMethodInvok(SetTB), value);
            }
            else
            {
                this.listBox3.Items.Clear();
                this.listBox3.Items.Add(value);
              //  this.textBox4.Text = value;
            }
        }
        private delegate void setlistBox();
        private void setlistbox3() 
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new setlistBox(setlistbox3), null);
            }
            else
            {
                this.listBox3.Items.Clear();
                JCB k;
                k = readyList.Find((JCB T) => T.get_state() == "exec");
                    this.listBox3.Items.Add(k.get_nameANDexeT());
            }
        }
        private void setlistbox4()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new setlistBox(setlistbox4), null);
            }
            else
            {
                this.listBox4.Items.Clear();
                foreach (JCB k in waitList)
                    this.listBox4.Items.Add(k.Print_my());
            }
        }
        private void setlistbox2() 
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new setlistBox(setlistbox2), null);
            }
            else 
            {
                this.listBox2.Items.Clear();
                foreach (JCB k in finishList)
                    this.listBox2.Items.Add(k.Print_my());
            }
        }
        private void setlistbox1()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new setlistBox(setlistbox1), null);
            }
            else 
            {
                this.listBox1.Items.Clear();
                foreach (JCB k in readyList)
                    this.listBox1.Items.Add(k.Print_my());
                if (readyList.Find((JCB S) => S.get_state() == "exec") != null)
                {
                    JCB f = readyList.Find((JCB S) => S.get_state() == "exec");
                    this.listBox1.Items.Remove(f.get_state());
                
                }
            }
        
        }

        public void FCFSfunction(object source, System.Timers.ElapsedEventArgs e)
        {
            /*  
               如何实现FCFS方法：
               首先从就绪列表readyList里每次选取到达时间最短的，然后将执行时间通过调用 System.Timers.ElapsedEventHandler(METHODS);
               其中METHODS()满足参数要求(object sender, EventArgs e)。对于这个方法只需修改exeT,判断是否与runtime相等，如果相等
               把其状态位置为"finish"并从readyList移出，放入finishList，如果就绪队列空了则结束，算法结束需要弹出messageBox  
              */
            //atimer.Interval = 1000;
            //if (readyList.Count == 0) {
            //    atimer.Stop();
            //    MessageBox.Show("所有作业执行完毕");
            //    clickCount = 0;
            //    return;
            //}
            //clock++;
            //int i = 100;
            //string name = "";
            //JCB F;
            //foreach (JCB j in readyList)
            //{
            //    if (j.get_atime() < i)
            //    {
            //        i = j.get_atime();
            //        name = j.get_name();
            //    }
            //}
            //F = readyList.Find((JCB s) => s.get_name() == name);
            //F.set_state("exec");
            //if (F.exeT == F.runtime)
            //{
            //    int f = clock-circle;
            //    F.set_state("finish");
            //    F.set_ftime(f);
            //    F.set_total(f-F.get_atime());
            //    F.set_welght(F.get_ftime()/F.runtime);
            //    finishList.Add(F);
            //    readyList.Remove(F);

            //    atimer.Stop();
            //    if (MessageBox.Show("作业" + F.get_name() + "已完成!", "是否继续", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) { atimer.Start(); }
            //    circle++;
            //    setTB("进程名："+F.get_name()+"作业状态："+F.get_state()+"作业周转时间："+F.get_total()+"带权周转系数："+F.get_welght().ToString());
            //    //atimer.Stop();
            //}
            //else
            //{
            //    F.exeT++;
            //    SetTB(F.get_nameANDexeT());
            //}
            //// this.listBox3.Items.Add(F.get_nameANDexeT());
            //Console.Write(F.get_nameANDexeT());
            //Console.WriteLine();
            atimer.Interval = 1000;
            if (readyList.Count == 0 && waitList.Count == 0)
            {
                atimer.Stop();
                MessageBox.Show("所有作业执行完毕");
                clickCount = 0;
                return;
            }
            JCB f;
            for (int i = 0; i <= waitList.Count - 1; i++)
            {
                f = waitList.ElementAt<JCB>(i);
                if (f.get_atime() == clock)
                {
                    readyList.Add(f);
                    waitList.Remove(f);
                    setlistbox4();
                    setlistbox1();
                }
            }

            if (readyList.Count != 0)
            {
                if (readyList.FindAll((JCB t) => t.get_state() == "exec").Count == 0)
                {
                    JCB Q;
                    int k = 100;
                    Q = readyList.ElementAt<JCB>(0);
                    Q.set_state("exec");
                }
                JCB T = readyList.Find((JCB t) => t.get_state() == "exec");
                if (T.exeT == T.runtime)
                {
                    int ft = clock;
                    T.set_state("finish");
                    T.set_ftime(ft);
                    T.set_total(T.get_ftime() - T.runtime);
                    finishList.Add(T);
                    readyList.Remove(T);
                    setlistbox2();
                    setlistbox1();
                    if (readyList.Count == 0) return;
                    T = readyList.ElementAt<JCB>(0);
                    T.set_state("exec");
                }


                JCB h;
                h = readyList.Find((JCB t) => t.get_state() == "exec");
                h.exeT++;
                setlistbox3();
                Console.Write(h.Print_my());
                Console.WriteLine();
            }
            //if (readyList.Count == 0) return;
            clock++;
        }

        public void SJFfunction(object source, System.Timers.ElapsedEventArgs e)
        {
            /*
               如何实现SJF算法：
               首先先选取时间最短来执行的，当执个更短的作业到达时将，将原来的作业调出并把状态为置为"wait"，将其移动到waitList，如果此
               时就绪队列中作业数小于4，则把waitList中的runtime-exeT最小的调入一个进到就绪队列。
               如果waitList.count+readyList.count<2,则把所有在等待队列的作业调入到就绪队列，如果runtime==exeT则把其状态位置为"finish"
               并从readyList移出，放入finishList。如果就绪队列和等待队列都空了，则算法结束。算法结束需要弹出messageBox。
               */
            atimer.Interval = 1000;
            if (readyList.Count == 0 && waitList.Count == 0)
            {
                atimer.Stop();
                MessageBox.Show("所有作业执行完毕");
                clickCount = 0;
                return;
            }
            JCB f;
            for (int i = 0; i <= waitList.Count - 1;i++ )
            {
                f = waitList.ElementAt<JCB>(i);
                if (f.get_atime() == clock)
                {
                    readyList.Add(f);
                    waitList.Remove(f);
                    setlistbox4();
                    setlistbox1();
                }
            }

               if(readyList.Count!=0)
              {
                if (readyList.FindAll((JCB t) => t.get_state() == "exec").Count == 0)
                {
                    JCB Q;
                    int k=100;
                    for (int i = 0; i <= readyList.Count - 1; i++)
                    {
                        Q = readyList.ElementAt<JCB>(i);
                        if (Q.runtime < k) { k = Q.runtime; }
                    }
                    readyList.Find((JCB t) =>t.runtime==k).set_state("exec");
                }
                JCB T = readyList.Find((JCB t) => t.get_state() == "exec");
                if (T.exeT == T.runtime)
                {
                    int ft = clock;
                    T.set_state("finish");
                    T.set_ftime(ft);
                    T.set_total(T.get_ftime()-T.runtime);
                    finishList.Add(T);
                    readyList.Remove(T);
                    setlistbox2();
                    setlistbox1();
                    int time=100;
                    JCB P;
                    for (int z = 0; z <= readyList.Count - 1; z++)
                    {
                        P = readyList.ElementAt<JCB>(z);
                        if (P.runtime < time) { time = P.runtime; }
                    }
                        T = readyList.Find((JCB t) => t.runtime == time);
                        if (readyList.Count == 0) return;
                        T.set_state("exec");
                }
               

                JCB h;
                h = readyList.Find((JCB t) => t.get_state() == "exec");
                h.exeT++;
                setlistbox3();
                Console.Write(h.Print_my());
                Console.WriteLine();
            }
               //if (readyList.Count == 0) return;
            clock++;
        }
        public void HRNfunction(object source, System.Timers.ElapsedEventArgs e) 
        {
            atimer.Interval = 1000;
            if (readyList.Count == 0 && waitList.Count == 0)
            {
                atimer.Stop();
                MessageBox.Show("所有作业执行完毕");
                clickCount = 0;
                return;
            }
            JCB f;
            for (int i = 0; i <= waitList.Count - 1; i++)
            {
                f = waitList.ElementAt<JCB>(i);
                if (f.get_atime() == clock)
                {
                    readyList.Add(f);
                    waitList.Remove(f);
                    setlistbox4();
                    setlistbox1();
                }
            }

            if (readyList.Count != 0)
            {
                if (readyList.FindAll((JCB t) => t.get_state() == "exec").Count == 0)
                {
                    JCB Q;
                    int z = 0;
                    string s1=readyList.ElementAt<JCB>(0).get_name();
                    double k = 100;//等待时间+要求服务时间除以要求服务时间 //等待时间clock -atime,要求服务时间 runtime
                    for (int i = 0; i <= readyList.Count - 1; i++)
                    {
                        Q = readyList.ElementAt<JCB>(i);//此处需要修改
                        z=clock-Q.get_atime()+Q.runtime;
                        if (k < ((1.0)* z / Q.runtime)) { k = ((1.0) * z / Q.runtime); s1 = Q.get_name(); }
                    }
                   readyList.Find((JCB t) => t.get_name() == s1).set_state("exec");    
                }
                JCB T = readyList.Find((JCB t) => t.get_state() == "exec");
                if (T.exeT == T.runtime)
                {
                    int ft = clock;
                    T.set_state("finish");
                    T.set_ftime(ft);
                    T.set_total(ft-T.get_atime());
                    T.set_welght(T.get_total()/T.runtime);
                   // T.set_total(T.get_ftime() - T.runtime);
                    finishList.Add(T);
                    readyList.Remove(T);
                    setlistbox2();
                    setlistbox1();
                    if (readyList.Count == 0 && waitList.Count() == 0) return;
                    string s2=readyList.ElementAt<JCB>(0).get_name();
                        int mg=0;
                   double weight = 100;
                    JCB P;
                    for (int z = 0; z <= readyList.Count - 1; z++)
                    {
                        P = readyList.ElementAt<JCB>(z);
                        mg=clock-P.get_atime()+P.runtime;
                        if (weight < ((1.0) * mg / P.runtime)) { weight = ((1.0) * mg / P.runtime); s2 = P.get_name(); }//
                    }
                    T = readyList.Find((JCB t) => t.get_name() == s2);//
                    if (readyList.Count == 0) return;
                    T.set_state("exec");
                }


                JCB h;
                h = readyList.Find((JCB t) => t.get_state() == "exec");
                h.exeT++;
                setlistbox3();
                Console.Write(h.Print_my());
                Console.WriteLine();
            }
            //if (readyList.Count == 0) return;
            clock++;
        }
        private void button3_Click(object sender, EventArgs e)
        {
           if(clickCount==0) {
                if (radioButton1.Checked == true)
                {


                    // atimer.Interval = 1000;
                    atimer.Enabled = true;
                    atimer.Elapsed += new System.Timers.ElapsedEventHandler(FCFSfunction);
                    //JCB K;
                    //K = readyList.Find((JCB T)=>T==T);
                    //this.listBox3.Items.Add(K.get_nameANDexeT());


                }//算法编辑框
                if (radioButton2.Checked == true)
                {
                    //atimer.Interval = 1000;
                    atimer.Enabled = true;
                    atimer.Elapsed += new System.Timers.ElapsedEventHandler(SJFfunction);


                }//算法编辑框
                if (radioButton3.Checked == true)
                {
                    atimer.Interval = 1000;
                    atimer.Enabled = true;
                    atimer.Elapsed += new System.Timers.ElapsedEventHandler(HRNfunction);
                }//算法编辑框
            }
           atimer.Start();
           clickCount++;
        }
        private void textBox4_TextChanged(object sender, EventArgs e) { }
        private void button4_Click(object sender, EventArgs e)
        {
            atimer.Stop();
           // atimer.Enabled = false;
           //count = 0;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            atimer.Start();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            this.listBox2.Items.Clear();
            this.listBox3.Items.Clear();
            this.textBox1.Text = "";
            this.textBox2.Text = "";
            this.textBox3.Text = "";
            this.textBox4.Text = "";
            clock = 0;
            circle = 1;
        }

        
    }
}
