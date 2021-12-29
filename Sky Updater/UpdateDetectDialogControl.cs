/*--------------------------------------------------------------------------------------------------------------------
 Copyright (C) 2021 Himber Sacha

 This program is free software: you can redistribute it and/or modify
 it under the +terms of the GNU General Public License as published by
 the Free Software Foundation, either version 2 of the License, or
 any later version.

 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU General Public License for more details.

 You should have received a copy of the GNU General Public License
 along with this program.  If not, see https://www.gnu.org/licenses/gpl-2.0.html. 

--------------------------------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Sky_Updater
{
    public delegate void EventBoolHandler(bool Boolean);

    public sealed class UpdateDetectDialogControl : Sky_UI.Rectangle
    {
        private Sky_UI.Button button1;
        private Sky_UI.Button button2;
        private Sky_UI.Rectangle rectangle1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox TextBox1;
        private LinkLabel linkLabel1;

        public EventBoolHandler ButtonUpdate = null;

        public UpdateDetectDialogControl(sbyte langage, string CurrentVersion, string LastVersion, string UpdateNote)
        {
            InitializeComponent(in langage);

            label2.Text += CurrentVersion;
            label3.Text += LastVersion;
            TextBox1.Text = UpdateNote;
        }

        private void button1_Click(object sender, MouseEventArgs e)
        {
            if (ButtonUpdate != null)
            {
                ButtonUpdate(true); // execute update
            }
        }

        private void button2_Click(object sender, MouseEventArgs e)
        {
            if (ButtonUpdate != null)
            {
                ButtonUpdate(false); // n'execute pas update
            }
        }

        private void InitializeComponent(in sbyte lang)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateDetectDialogControl));
            this.button1 = new Sky_UI.Button();
            this.button2 = new Sky_UI.Button();
            this.rectangle1 = new Sky_UI.Rectangle();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new Label();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.Border = false;
            this.button1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button1.borderRadius = 5;
            this.button1.ID = 0;
            this.button1.Location = new System.Drawing.Point(180, 317);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 26);
            this.button1.TabIndex = 0;
            if (lang == 0) // FR
            {
                this.button1.Text = "Télécharger";
            }
            else // EN
            {
                this.button1.Text = "Download";
            }
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.button1.MouseClick += new MouseEventHandler(button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button2.Border = false;
            this.button2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button2.borderRadius = 5;
            this.button2.ID = 0;
            this.button2.Location = new System.Drawing.Point(13, 317);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 26);
            this.button2.TabIndex = 1;
            if (lang == 0)
            {
                this.button2.Text = "Annuler";
            }
            else
            {
                this.button2.Text = "Cancel";
            }
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.button2.MouseClick += new MouseEventHandler(button2_Click);
            // 
            // rectangle1
            // 
            this.rectangle1.BackColor = System.Drawing.Color.RoyalBlue;
            this.rectangle1.Border = false;
            this.rectangle1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rectangle1.BorderRadius = 0;
            this.rectangle1.BorderWidth = 3;
            this.rectangle1.Location = new System.Drawing.Point(0, 0);
            this.rectangle1.Name = "rectangle1";
            this.rectangle1.Size = new System.Drawing.Size(291, 94);
            this.rectangle1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.RoyalBlue;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(39, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 25);
            this.label1.TabIndex = 3;
            if (lang == 0)
            {
                this.label1.Text = "Mise à jour disponible";
            }
            else
            {
                this.label1.Text = "Update available";
            }
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.BackColor = System.Drawing.Color.RoyalBlue;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(9, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 15);
            this.label2.TabIndex = 4;
            if (lang == 0)
            {
                this.label2.Text = "Version actuelle : ";
            }
            else
            {
                this.label2.Text = "Current version : ";
            }
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.BackColor = System.Drawing.Color.RoyalBlue;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(150, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 15);
            this.label3.TabIndex = 5;
            if (lang == 0)
            {
                this.label3.Text = "Dernière version : ";
            }
            else
            {
                this.label3.Text = "Last version : ";
            }
            // 
            // TextBox1
            // 
            this.TextBox1.AutoSize = true;
            this.TextBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TextBox1.ForeColor = System.Drawing.Color.White;
            this.TextBox1.Location = new System.Drawing.Point(3, 144);
            this.TextBox1.Name = "label4";
            this.TextBox1.Size = new System.Drawing.Size(283, 155);
            this.TextBox1.TabIndex = 6;
            this.TextBox1.ReadOnly = true;
            this.TextBox1.Multiline = true;
            this.TextBox1.ScrollBars = ScrollBars.Vertical;
            this.TextBox1.WordWrap = true;
            this.TextBox1.BorderStyle = BorderStyle.FixedSingle;
            this.TextBox1.BackColor = Color.FromArgb(64, 64, 64);
            //
            // label4
            //
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(9, 113);
            this.label4.Name = "label4";
            this.label4.TabIndex = 5;
            if (lang == 0)
            {
                this.label4.Text = "Modification de cette mise à jour :";
            }
            else
            {
                this.label4.Text = "Modification of this update  :";
            }
            // 
            // linkLabel1
            // 
            this.linkLabel1.ActiveLinkColor = System.Drawing.Color.OrangeRed;
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkColor = System.Drawing.Color.CornflowerBlue;
            this.linkLabel1.Location = new System.Drawing.Point(1, 300);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(226, 15);
            this.linkLabel1.TabIndex = 7;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Click += new EventHandler(linkLabel1_Click);
            if (lang == 0)
            {
                this.linkLabel1.Text = "Acceder au site web";
            }
            else
            {
                this.linkLabel1.Text = "Access to web site";
            }
            // 
            // UpdateDetectDialogControl
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BorderRadius = 10;
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rectangle1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "UpdateDetectDialogControl";
            this.Size = new System.Drawing.Size(291, 350);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            //linkLabel1.LinkVisited = true;
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.FileName = "https://serie-sky.netlify.app/index.html";
            process.Start();
            process.Close();
            process = null;
        }
    }
}
