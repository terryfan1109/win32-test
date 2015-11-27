namespace uiButtonSheet001
{
  partial class RemoveAccountSheet
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
      this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
      this.label1 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(1, 364);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(281, 53);
      this.button1.TabIndex = 0;
      this.button1.Text = "Complete";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // flowLayoutPanel1
      // 
      this.flowLayoutPanel1.AutoScroll = true;
      this.flowLayoutPanel1.Location = new System.Drawing.Point(15, 76);
      this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
      this.flowLayoutPanel1.Name = "flowLayoutPanel1";
      this.flowLayoutPanel1.Size = new System.Drawing.Size(252, 265);
      this.flowLayoutPanel1.TabIndex = 2;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("PMingLiU", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
      this.label1.Location = new System.Drawing.Point(75, 30);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(135, 19);
      this.label1.TabIndex = 5;
      this.label1.Text = "Remove Account";
      // 
      // RemoveAccountSheet
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(285, 420);
      this.ControlBox = false;
      this.Controls.Add(this.label1);
      this.Controls.Add(this.flowLayoutPanel1);
      this.Controls.Add(this.button1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "RemoveAccountSheet";
      this.ShowIcon = false;
      this.Text = "RemoveAccountSheet";
      this.Load += new System.EventHandler(this.RemoveAccountSheet_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    private System.Windows.Forms.Label label1;
  }
}