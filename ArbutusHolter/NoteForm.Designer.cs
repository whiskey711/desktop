using System.Runtime.CompilerServices;

namespace Uvic_Ecg_ArbutusHolter
{
    partial class NoteForm
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
            this.commentLs = new System.Windows.Forms.ListView();
            this.startTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.endTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.comment = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            //
            // commentLs
            //
            this.commentLs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[]
            {
                this.startTime,
                this.endTime,
                this.comment
            });
            this.commentLs.FullRowSelect = true;
            this.commentLs.GridLines = true;
            this.commentLs.HideSelection = false;
            this.commentLs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commentLs.Location = new System.Drawing.Point(0, 0);
            this.commentLs.Name = "commentLs";
            this.commentLs.TabIndex = 0;
            this.commentLs.ShowItemToolTips = true;
            this.commentLs.UseCompatibleStateImageBehavior = false;
            this.commentLs.View = System.Windows.Forms.View.Details;
            this.commentLs.SelectedIndexChanged += new System.EventHandler(this.CommentLs_SelectedIndexChanged);
            //
            // startTime
            //
            this.startTime.Text = "Start Time";
            this.startTime.Width = 130;
            //
            // endTime
            //
            this.endTime.Text = "End Time";
            this.endTime.Width = 130;
            //
            // comment
            //
            this.comment.Text = "Comment";
            this.comment.Width = 550;
            // 
            // NoteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.commentLs);
            this.Name = "NoteForm";
            this.Text = "NoteForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView commentLs;
        private System.Windows.Forms.ColumnHeader startTime;
        private System.Windows.Forms.ColumnHeader endTime;
        private System.Windows.Forms.ColumnHeader comment;
    }
}