namespace WinFormsApps
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnStreamTesting = new System.Windows.Forms.Button();
            this.btnSerializer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(42, 66);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 34);
            this.button1.TabIndex = 0;
            this.button1.Text = "hehe";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(42, 123);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(132, 34);
            this.button2.TabIndex = 1;
            this.button2.Text = "hengheng";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnStreamTesting
            // 
            this.btnStreamTesting.Location = new System.Drawing.Point(244, 66);
            this.btnStreamTesting.Name = "btnStreamTesting";
            this.btnStreamTesting.Size = new System.Drawing.Size(132, 34);
            this.btnStreamTesting.TabIndex = 2;
            this.btnStreamTesting.Text = "Stream Testing";
            this.btnStreamTesting.UseVisualStyleBackColor = true;
            // 
            // btnSerializer
            // 
            this.btnSerializer.Location = new System.Drawing.Point(244, 123);
            this.btnSerializer.Name = "btnSerializer";
            this.btnSerializer.Size = new System.Drawing.Size(132, 34);
            this.btnSerializer.TabIndex = 3;
            this.btnSerializer.Text = "Serializer Testing";
            this.btnSerializer.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 240);
            this.Controls.Add(this.btnSerializer);
            this.Controls.Add(this.btnStreamTesting);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnStreamTesting;
        private System.Windows.Forms.Button btnSerializer;
    }
}