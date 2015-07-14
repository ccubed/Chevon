<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Parsed = New System.Windows.Forms.RichTextBox()
        Me.SubverseTB = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(3, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Info"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Parsed
        '
        Me.Parsed.Location = New System.Drawing.Point(3, 41)
        Me.Parsed.Name = "Parsed"
        Me.Parsed.Size = New System.Drawing.Size(815, 353)
        Me.Parsed.TabIndex = 1
        Me.Parsed.Text = ""
        '
        'SubverseTB
        '
        Me.SubverseTB.Location = New System.Drawing.Point(84, 15)
        Me.SubverseTB.Name = "SubverseTB"
        Me.SubverseTB.ShortcutsEnabled = False
        Me.SubverseTB.Size = New System.Drawing.Size(178, 20)
        Me.SubverseTB.TabIndex = 3
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(830, 406)
        Me.Controls.Add(Me.SubverseTB)
        Me.Controls.Add(Me.Parsed)
        Me.Controls.Add(Me.Button1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Parsed As RichTextBox
    Friend WithEvents SubverseTB As TextBox
End Class
