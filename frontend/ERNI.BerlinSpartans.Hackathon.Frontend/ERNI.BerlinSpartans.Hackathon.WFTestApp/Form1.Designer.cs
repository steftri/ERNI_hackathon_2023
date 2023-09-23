namespace ERNI.BerlinSpartans.Hackathon.WFTestApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtBrokerAddress = new TextBox();
            label1 = new Label();
            btnConnect = new Button();
            btnUp = new Button();
            btnDown = new Button();
            btnLeft = new Button();
            btnRight = new Button();
            txtLog = new TextBox();
            SuspendLayout();
            // 
            // txtBrokerAddress
            // 
            txtBrokerAddress.Location = new Point(12, 27);
            txtBrokerAddress.Name = "txtBrokerAddress";
            txtBrokerAddress.Size = new Size(290, 23);
            txtBrokerAddress.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(86, 15);
            label1.TabIndex = 1;
            label1.Text = "Broker Address";
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(308, 27);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(75, 23);
            btnConnect.TabIndex = 2;
            btnConnect.Text = "&Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // btnUp
            // 
            btnUp.Location = new Point(89, 89);
            btnUp.Name = "btnUp";
            btnUp.Size = new Size(75, 23);
            btnUp.TabIndex = 3;
            btnUp.Text = "&UP";
            btnUp.UseVisualStyleBackColor = true;
            // 
            // btnDown
            // 
            btnDown.Location = new Point(89, 147);
            btnDown.Name = "btnDown";
            btnDown.Size = new Size(75, 23);
            btnDown.TabIndex = 4;
            btnDown.Text = "&DOWN";
            btnDown.UseVisualStyleBackColor = true;
            // 
            // btnLeft
            // 
            btnLeft.Location = new Point(16, 118);
            btnLeft.Name = "btnLeft";
            btnLeft.Size = new Size(75, 23);
            btnLeft.TabIndex = 4;
            btnLeft.Text = "&LEFT";
            btnLeft.UseVisualStyleBackColor = true;
            // 
            // btnRight
            // 
            btnRight.Location = new Point(163, 118);
            btnRight.Name = "btnRight";
            btnRight.Size = new Size(75, 23);
            btnRight.TabIndex = 4;
            btnRight.Text = "&RIGHT";
            btnRight.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            txtLog.Dock = DockStyle.Bottom;
            txtLog.Location = new Point(0, 319);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.Size = new Size(393, 131);
            txtLog.TabIndex = 5;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(393, 450);
            Controls.Add(txtLog);
            Controls.Add(btnRight);
            Controls.Add(btnLeft);
            Controls.Add(btnDown);
            Controls.Add(btnUp);
            Controls.Add(btnConnect);
            Controls.Add(label1);
            Controls.Add(txtBrokerAddress);
            Name = "Form1";
            Text = "Mqtt Control test App";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtBrokerAddress;
        private Label label1;
        private Button btnConnect;
        private Button btnUp;
        private Button btnDown;
        private Button btnLeft;
        private Button btnRight;
        private TextBox txtLog;
    }
}