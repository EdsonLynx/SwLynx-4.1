<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExportarParaOS
    Inherits System.Windows.Forms.Form

    'Descartar substituições de formulário para limpar a lista de componentes.
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

    'Exigido pelo Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'OBSERVAÇÃO: o procedimento a seguir é exigido pelo Windows Form Designer
    'Pode ser modificado usando o Windows Form Designer.  
    'Não o modifique usando o editor de códigos.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmExportarParaOS))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.optParametroExportarDXF2 = New System.Windows.Forms.RadioButton()
        Me.optParametroExportarDXF1 = New System.Windows.Forms.RadioButton()
        Me.btnSalvar = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.optNaoLiberaSemMaterial = New System.Windows.Forms.RadioButton()
        Me.optLiberaSemMaterial = New System.Windows.Forms.RadioButton()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.chkCaixaDelimitadora = New System.Windows.Forms.CheckBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.optParametroExportarDXF2)
        Me.GroupBox1.Controls.Add(Me.optParametroExportarDXF1)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 13)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(493, 86)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Arquivos DXF"
        Me.ToolTip1.SetToolTip(Me.GroupBox1, resources.GetString("GroupBox1.ToolTip"))
        '
        'optParametroExportarDXF2
        '
        Me.optParametroExportarDXF2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.optParametroExportarDXF2.Location = New System.Drawing.Point(6, 53)
        Me.optParametroExportarDXF2.Name = "optParametroExportarDXF2"
        Me.optParametroExportarDXF2.Size = New System.Drawing.Size(472, 23)
        Me.optParametroExportarDXF2.TabIndex = 27
        Me.optParametroExportarDXF2.Text = "Qtde - Numero Desenho - Material - Espessura.DXF/LXDS/DFT"
        Me.optParametroExportarDXF2.UseVisualStyleBackColor = True
        '
        'optParametroExportarDXF1
        '
        Me.optParametroExportarDXF1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.optParametroExportarDXF1.Location = New System.Drawing.Point(6, 21)
        Me.optParametroExportarDXF1.Name = "optParametroExportarDXF1"
        Me.optParametroExportarDXF1.Size = New System.Drawing.Size(484, 26)
        Me.optParametroExportarDXF1.TabIndex = 27
        Me.optParametroExportarDXF1.Text = "Espessura - Material - Qtde - Numero Desenho DXF/LXDS/DFT"
        Me.optParametroExportarDXF1.UseVisualStyleBackColor = True
        '
        'btnSalvar
        '
        Me.btnSalvar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSalvar.Image = CType(resources.GetObject("btnSalvar.Image"), System.Drawing.Image)
        Me.btnSalvar.Location = New System.Drawing.Point(337, 445)
        Me.btnSalvar.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnSalvar.Name = "btnSalvar"
        Me.btnSalvar.Size = New System.Drawing.Size(164, 51)
        Me.btnSalvar.TabIndex = 3
        Me.btnSalvar.Text = "Salvar/Sair"
        Me.btnSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSalvar.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.RadioButton1)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 105)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(490, 57)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Arquivos PDF"
        Me.ToolTip1.SetToolTip(Me.GroupBox2, resources.GetString("GroupBox2.ToolTip"))
        '
        'RadioButton1
        '
        Me.RadioButton1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadioButton1.Checked = True
        Me.RadioButton1.Location = New System.Drawing.Point(9, 21)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(368, 23)
        Me.RadioButton1.TabIndex = 27
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "Qtde - Numero Desenho"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.optNaoLiberaSemMaterial)
        Me.GroupBox3.Controls.Add(Me.optLiberaSemMaterial)
        Me.GroupBox3.Location = New System.Drawing.Point(6, 168)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(490, 97)
        Me.GroupBox3.TabIndex = 6
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Liberação de OS sem Material do desenho"
        Me.ToolTip1.SetToolTip(Me.GroupBox3, "Dicas de uso:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Esta opção permite liberar as peças da Ordem de Serviço (OS) com o" &
        "u " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "sem o Material SW, conforme necessário.")
        '
        'optNaoLiberaSemMaterial
        '
        Me.optNaoLiberaSemMaterial.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.optNaoLiberaSemMaterial.Location = New System.Drawing.Point(6, 60)
        Me.optNaoLiberaSemMaterial.Name = "optNaoLiberaSemMaterial"
        Me.optNaoLiberaSemMaterial.Size = New System.Drawing.Size(478, 23)
        Me.optNaoLiberaSemMaterial.TabIndex = 29
        Me.optNaoLiberaSemMaterial.Text = "Não pode Liberar a OS sem o material do desenho (Material SW)"
        Me.optNaoLiberaSemMaterial.UseVisualStyleBackColor = True
        '
        'optLiberaSemMaterial
        '
        Me.optLiberaSemMaterial.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.optLiberaSemMaterial.Location = New System.Drawing.Point(6, 25)
        Me.optLiberaSemMaterial.Name = "optLiberaSemMaterial"
        Me.optLiberaSemMaterial.Size = New System.Drawing.Size(478, 23)
        Me.optLiberaSemMaterial.TabIndex = 27
        Me.optLiberaSemMaterial.Text = "Pode Liberar a OS sem o material do desenho (Material SW)"
        Me.optLiberaSemMaterial.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.Button3)
        Me.GroupBox4.Controls.Add(Me.Button2)
        Me.GroupBox4.Controls.Add(Me.Button1)
        Me.GroupBox4.Location = New System.Drawing.Point(7, 313)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(489, 127)
        Me.GroupBox4.TabIndex = 7
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Buscar Modelo de Formato para o Detalhamento"
        '
        'Button3
        '
        Me.Button3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.Location = New System.Drawing.Point(7, 84)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(475, 23)
        Me.Button3.TabIndex = 2
        Me.Button3.Text = "Buscar Formato A4 Deitado"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.Location = New System.Drawing.Point(8, 56)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(475, 23)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "Buscar Formato A4"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(8, 27)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(475, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Buscar Formato A3"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'chkCaixaDelimitadora
        '
        Me.chkCaixaDelimitadora.AutoSize = True
        Me.chkCaixaDelimitadora.Location = New System.Drawing.Point(6, 271)
        Me.chkCaixaDelimitadora.Name = "chkCaixaDelimitadora"
        Me.chkCaixaDelimitadora.Size = New System.Drawing.Size(495, 20)
        Me.chkCaixaDelimitadora.TabIndex = 8
        Me.chkCaixaDelimitadora.Text = "Ative esta função para que a Caixa Delimitadora seja criada automaticamente."
        Me.chkCaixaDelimitadora.UseVisualStyleBackColor = True
        '
        'frmExportarParaOS
        '
        Me.AcceptButton = Me.btnSalvar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(511, 505)
        Me.Controls.Add(Me.chkCaixaDelimitadora)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btnSalvar)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmExportarParaOS"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Modelos de "
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents optParametroExportarDXF2 As Windows.Forms.RadioButton
    Friend WithEvents optParametroExportarDXF1 As Windows.Forms.RadioButton
    Friend WithEvents btnSalvar As Windows.Forms.Button
    Friend WithEvents GroupBox2 As Windows.Forms.GroupBox
    Friend WithEvents RadioButton1 As Windows.Forms.RadioButton
    Friend WithEvents GroupBox3 As Windows.Forms.GroupBox
    Friend WithEvents optNaoLiberaSemMaterial As Windows.Forms.RadioButton
    Friend WithEvents optLiberaSemMaterial As Windows.Forms.RadioButton
    Friend WithEvents ToolTip1 As Windows.Forms.ToolTip
    Friend WithEvents GroupBox4 As Windows.Forms.GroupBox
    Friend WithEvents Button2 As Windows.Forms.Button
    Friend WithEvents Button1 As Windows.Forms.Button
    Friend WithEvents Button3 As Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As Windows.Forms.OpenFileDialog
    Friend WithEvents chkCaixaDelimitadora As Windows.Forms.CheckBox
End Class
