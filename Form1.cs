﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ProgressBar = System.Windows.Forms.ProgressBar;

namespace Timer
{
    public partial class TimerForm : Form
    {

        #region Setup of the Variables

        int TimeVar;
        int TimeVarEDIT;
        int HR;
        int Min;
        int Sec;
        int Zero;
        int TimeDone_FINISHED;
        string AudioFile;
        NotifyIcon NotifyIcon;
        Icon IconNote_IDL;
        Icon IconNote_RUNNING;

        #endregion

        public TimerForm()
        {
            IconNote_IDL = new Icon("icon.ico");
            IconNote_RUNNING = new Icon("icon.ico");
            
            NotifyIcon = new NotifyIcon();
            NotifyIcon.Icon = IconNote_IDL;
            NotifyIcon.Visible = true;

            Zero = 0;
            InitializeComponent();
            TimeRemaining.Visible = false;
            TimeRemainingLable.Visible = false;
            buttonPause.Enabled = false;
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;
            timer1.Stop();


            openFileDialog1.Filter = "Meida Files|*.mp3;*.wav;*.mp4;*.mov*.wmv;*.mpg;|All Files|*.*";
            openFileDialog1.InitialDirectory = Environment.CurrentDirectory;
            AudioFile = Environment.CurrentDirectory + @"\Cat_Purring.wav";
            label1.Text = "Son: "+Path.GetFileName(AudioFile);

        }
        
        #region Setup of all of the Buttons and Actions
        private void TimerForm_Load(object sender, EventArgs e)
        {
            

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (TimeVarEDIT == Zero)
            {
                if (TimeDone_FINISHED == Convert.ToInt32("1"))
                {
                    return;
                }
                else
                {
                    TimeDone();
                    return;
                }
            }

            TimeVarEDIT = TimeVarEDIT - 1;
            TimeRemaining.Text = Convert.ToString(TimeVarEDIT) + " Seconds";
            //Total_Time.Text = Convert.ToDateTime(TimeMinValue);
            TimeSpan OutputTime = TimeSpan.FromSeconds(TimeVarEDIT);
            if (TimeVar > Convert.ToInt32("86400"))
            {
                string CompleteOutput = OutputTime.ToString(@"dd\:hh\:mm\:ss");
                Total_Time.Text = CompleteOutput;
            }
            else
            {
                string CompleteOutput = OutputTime.ToString(@"hh\:mm\:ss");
                Total_Time.Text = CompleteOutput;
            }

            progressBar1.Value = TimeVarEDIT;

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            StartTime();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            USERSTOP();
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            TIMERPAUSE();
        }

        private void Menu_StartTimer_Click(object sender, EventArgs e)
        {
            StartTime();
        }

        private void Menu_PauseTimer_Click(object sender, EventArgs e)
        {
            TIMERPAUSE();
        }

        private void Menu_StopTimmer_Click(object sender, EventArgs e)
        {
            USERSTOP();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            AudioFile = openFileDialog1.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AudioFile = Environment.CurrentDirectory + @"\Cat_Purring.wav";
        }
        #endregion


        #region Functions

        private void TIMERPAUSE()
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
                buttonPause.Text = "Resume";
            }
            else
            {
                timer1.Start();
                buttonPause.Text = "Pause";
            }
        }

        

        private void USERSTOP()
        {
            TimeHour.ReadOnly =  false;
            TimeMinValue.ReadOnly = false;
            TimeSecValue.ReadOnly = false;

            TimeHour.Enabled = true;
            TimeMinValue.Enabled = true;
            TimeSecValue.Enabled = true;

            TimeRemaining.Visible = false;
            Total_Time.Visible = false;
            TimeRemainingLable.Visible = false;
            progressBar1.Maximum = 10;
            progressBar1.Value = 10;

            buttonStop.Enabled = false;
            buttonPause.Enabled = false;
            buttonStart.Enabled = true;

            Menu_StopTimer.Enabled = false;
            Menu_PauseTimer.Enabled = false;
            Menu_StartTimer.Enabled = true;

            timer1.Stop();
            TimeDone_FINISHED = 0;
            progressBar1.Value = 0;
            MessageBox.Show("Pourquoi t'as stoppé :o !");
            TimeVarEDIT = 0;
            TimeVar = 0;
            return;
        }

        private void TimeDone()
        {
            TimeDone_FINISHED = 1;
            TimeHour.ReadOnly = false;
            TimeMinValue.ReadOnly = false;
            TimeSecValue.ReadOnly = false;

            TimeHour.Enabled = true;
            TimeMinValue.Enabled = true;
            TimeSecValue.Enabled = true;

            TimeRemaining.Visible = false;
            Total_Time.Visible = false;
            TimeRemainingLable.Visible = false;
            progressBar1.Maximum = 10;
            progressBar1.Value = 10;

            buttonStop.Enabled = false;
            buttonPause.Enabled = false;
            buttonStart.Enabled = true;

            Menu_StopTimer.Enabled = false;
            Menu_PauseTimer.Enabled = false;
            Menu_StartTimer.Enabled = true;

            SoundPlayer.URL = AudioFile;
            SoundPlayer.Ctlcontrols.play();
            SoundPlayer.settings.setMode("Loop", true);

            NotifyIcon.Icon = IconNote_IDL;
            NotifyIcon.Visible = true;
            NotifyIcon.BalloonTipTitle = "Meow Meow Meow Meow!";
            NotifyIcon.BalloonTipText = "Dev ça se fait pas tout seul!";
            NotifyIcon.ShowBalloonTip(2000);

            if (MessageBox.Show("Au boulot!") == DialogResult.OK)
            {
                SoundPlayer.Ctlcontrols.stop();
            }

            

            timer1.Stop();
            timer1.Enabled = false;
            return;
        }

        private void StartTime()
        {
            if(TimeHour.Text == string.Empty)
            {
                TimeHour.Text = "0";
            }

            if (TimeMinValue.Text == string.Empty)
            {
                TimeMinValue.Text = "0";
            }

            if (TimeSecValue.Text == string.Empty)
            {
                TimeSecValue.Text = "0";
            }

            HR = Convert.ToInt32(TimeHour.Text);
            Min = Convert.ToInt32(TimeMinValue.Text);
            Sec = Convert.ToInt32(TimeSecValue.Text);
            TimeVar = HR * 60 * 60 + Min * 60 + Sec;

            if(TimeVar == Convert.ToInt32("0"))
            {
                MessageBox.Show("Pas 0 t'abuse");
                return;
            }

            TimeHour.ReadOnly = true;
            TimeMinValue.ReadOnly = true;
            TimeSecValue.ReadOnly = true;

            TimeHour.Enabled = false;
            TimeMinValue.Enabled = false;
            TimeSecValue.Enabled = false;

            TimeRemaining.Visible = true;
            Total_Time.Visible = true;
            TimeRemainingLable.Visible = true;
            TimeVarEDIT = TimeVar;
            progressBar1.Maximum = TimeVar;
            progressBar1.Value = TimeVar;

            

            TimeSpan OutputTime = TimeSpan.FromSeconds(TimeVar);
            if (TimeVar > Convert.ToInt32("86400"))
            {
                string CompleteOutput = OutputTime.ToString(@"dd\:hh\:mm\:ss");
                Total_Time.Text = CompleteOutput;
            }
            else
            {
                string CompleteOutput = OutputTime.ToString(@"hh\:mm\:ss");
                Total_Time.Text = CompleteOutput;
            }

            TimeRemaining.Text = Convert.ToString(TimeVar) + " Seconds";
            TimeDone_FINISHED = 0;
            timer1.Start();
            buttonStop.Enabled = true;
            buttonPause.Enabled = true;
            buttonStart.Enabled = false;

            Menu_StopTimer.Enabled = true;
            Menu_PauseTimer.Enabled = true;
            Menu_StartTimer.Enabled = false;

            NotifyIcon.Icon = IconNote_RUNNING;

            return;
        }

        #endregion

        private void TimeMinValue_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
