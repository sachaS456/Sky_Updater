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

    public sealed class UpdateDetectDialogControl : Sky_framework.Rectangle
    {
        private Sky_framework.Button button1;
        private Sky_framework.Button button2;
        private Sky_framework.Rectangle rectangle1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private LinkLabel linkLabel1;

        public EventBoolHandler ButtonUpdate = null;

        public UpdateDetectDialogControl(string CurrentVersion, string LastVersion)
        {
            InitializeComponent();

            label2.Text += CurrentVersion;
            label3.Text += LastVersion;
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

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateDetectDialogControl));
            this.button1 = new Sky_framework.Button();
            this.button2 = new Sky_framework.Button();
            this.rectangle1 = new Sky_framework.Rectangle();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
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
            this.button1.Location = new System.Drawing.Point(180, 279);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 26);
            this.button1.TabIndex = 0;
            this.button1.Text = "Télécharger";
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
            this.button2.Location = new System.Drawing.Point(13, 279);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 26);
            this.button2.TabIndex = 1;
            this.button2.Text = "Annuler";
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
            this.rectangle1.Text = "rectangle1";
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
            this.label1.Text = "Mise à jour disponible";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(9, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Version actuelle : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(9, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Dernière version : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(3, 163);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(270, 75);
            this.label4.TabIndex = 6;
            this.label4.Text = resources.GetString("label4.Text");
            // 
            // linkLabel1
            // 
            this.linkLabel1.ActiveLinkColor = System.Drawing.Color.OrangeRed;
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkColor = System.Drawing.Color.CornflowerBlue;
            this.linkLabel1.Location = new System.Drawing.Point(1, 251);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(226, 15);
            this.linkLabel1.TabIndex = 7;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Voir les modifications de cette mise à jour";
            // 
            // UpdateDetectDialogControl
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BorderRadius = 10;
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rectangle1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "UpdateDetectDialogControl";
            this.Size = new System.Drawing.Size(291, 317);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
