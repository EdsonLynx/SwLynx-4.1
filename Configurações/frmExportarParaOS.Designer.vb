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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTempoRespostaServidor = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtEmailPCP = New System.Windows.Forms.TextBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.optParametroExportarDXF2)
        Me.GroupBox1.Controls.Add(Me.optParametroExportarDXF1)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 11)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox1.Size = New System.Drawing.Size(370, 70)
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
        Me.optParametroExportarDXF2.Location = New System.Drawing.Point(4, 43)
        Me.optParametroExportarDXF2.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.optParametroExportarDXF2.Name = "optParametroExportarDXF2"
        Me.optParametroExportarDXF2.Size = New System.Drawing.Size(354, 19)
        Me.optParametroExportarDXF2.TabIndex = 27
        Me.optParametroExportarDXF2.Text = "qtde - Numero Desenho - material - Espessura.DXF/LXDS/DFT"
        Me.optParametroExportarDXF2.UseVisualStyleBackColor = True
        '
        'optParametroExportarDXF1
        '
        Me.optParametroExportarDXF1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.optParametroExportarDXF1.Location = New System.Drawing.Point(4, 17)
        Me.optParametroExportarDXF1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.optParametroExportarDXF1.Name = "optParametroExportarDXF1"
        Me.optParametroExportarDXF1.Size = New System.Drawing.Size(363, 21)
        Me.optParametroExportarDXF1.TabIndex = 27
        Me.optParametroExportarDXF1.Text = "Espessura - material - qtde - Numero Desenho DXF/LXDS/DFT"
        Me.optParametroExportarDXF1.UseVisualStyleBackColor = True
        '
        'btnSalvar
        '
        Me.btnSalvar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSalvar.Image = CType(resources.GetObject("btnSalvar.Image"), System.Drawing.Image)
        Me.btnSalvar.Location = New System.Drawing.Point(251, 475)
        Me.btnSalvar.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnSalvar.Name = "btnSalvar"
        Me.btnSalvar.Size = New System.Drawing.Size(123, 41)
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
        Me.GroupBox2.Location = New System.Drawing.Point(4, 85)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox2.Size = New System.Drawing.Size(368, 46)
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
        Me.RadioButton1.Location = New System.Drawing.Point(7, 17)
        Me.RadioButton1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(276, 19)
        Me.RadioButton1.TabIndex = 27
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "qtde - Numero Desenho"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.optNaoLiberaSemMaterial)
        Me.GroupBox3.Controls.Add(Me.optLiberaSemMaterial)
        Me.GroupBox3.Location = New System.Drawing.Point(4, 136)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox3.Size = New System.Drawing.Size(368, 79)
        Me.GroupBox3.TabIndex = 6
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Liberação de OS sem material do desenho"
        Me.ToolTip1.SetToolTip(Me.GroupBox3, "Dicas de uso:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Esta opção permite liberar as peças da Ordem de Serviço (OS) com o" &
        "u " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "sem o material SW, conforme necessário.")
        '
        'optNaoLiberaSemMaterial
        '
        Me.optNaoLiberaSemMaterial.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.optNaoLiberaSemMaterial.Location = New System.Drawing.Point(4, 49)
        Me.optNaoLiberaSemMaterial.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.optNaoLiberaSemMaterial.Name = "optNaoLiberaSemMaterial"
        Me.optNaoLiberaSemMaterial.Size = New System.Drawing.Size(358, 19)
        Me.optNaoLiberaSemMaterial.TabIndex = 29
        Me.optNaoLiberaSemMaterial.Text = "Não pode Liberar a OS sem o material do desenho (material SW)"
        Me.optNaoLiberaSemMaterial.UseVisualStyleBackColor = True
        '
        'optLiberaSemMaterial
        '
        Me.optLiberaSemMaterial.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.optLiberaSemMaterial.Location = New System.Drawing.Point(4, 20)
        Me.optLiberaSemMaterial.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.optLiberaSemMaterial.Name = "optLiberaSemMaterial"
        Me.optLiberaSemMaterial.Size = New System.Drawing.Size(358, 19)
        Me.optLiberaSemMaterial.TabIndex = 27
        Me.optLiberaSemMaterial.Text = "Pode Liberar a OS sem o material do desenho (material SW)"
        Me.optLiberaSemMaterial.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.Button3)
        Me.GroupBox4.Controls.Add(Me.Button2)
        Me.GroupBox4.Controls.Add(Me.Button1)
        Me.GroupBox4.Location = New System.Drawing.Point(9, 364)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox4.Size = New System.Drawing.Size(367, 103)
        Me.GroupBox4.TabIndex = 7
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Buscar Modelo de Formato para o Detalhamento"
        '
        'Button3
        '
        Me.Button3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.Location = New System.Drawing.Point(5, 68)
        Me.Button3.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(356, 19)
        Me.Button3.TabIndex = 2
        Me.Button3.Text = "Buscar Formato A4 Deitado"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.Location = New System.Drawing.Point(6, 46)
        Me.Button2.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(356, 19)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "Buscar Formato A4"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(6, 22)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(356, 19)
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
        Me.chkCaixaDelimitadora.Location = New System.Drawing.Point(4, 220)
        Me.chkCaixaDelimitadora.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.chkCaixaDelimitadora.Name = "chkCaixaDelimitadora"
        Me.chkCaixaDelimitadora.Size = New System.Drawing.Size(394, 17)
        Me.chkCaixaDelimitadora.TabIndex = 8
        Me.chkCaixaDelimitadora.Text = "Ative esta função para que a Caixa Delimitadora seja criada automaticamente."
        Me.chkCaixaDelimitadora.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 252)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(148, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Tempo de Resposta Servidor:"
        '
        'txtTempoRespostaServidor
        '
        Me.txtTempoRespostaServidor.Location = New System.Drawing.Point(154, 250)
        Me.txtTempoRespostaServidor.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.txtTempoRespostaServidor.Name = "txtTempoRespostaServidor"
        Me.txtTempoRespostaServidor.Size = New System.Drawing.Size(76, 20)
        Me.txtTempoRespostaServidor.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 279)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(182, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Dica:  (30000) ' Esperar 30 segundos"
        '
        'txtEmailPCP
        '
        Me.txtEmailPCP.Location = New System.Drawing.Point(4, 17)
        Me.txtEmailPCP.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.txtEmailPCP.Name = "txtEmailPCP"
        Me.txtEmailPCP.Size = New System.Drawing.Size(357, 20)
        Me.txtEmailPCP.TabIndex = 12
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.txtEmailPCP)
        Me.GroupBox5.Location = New System.Drawing.Point(9, 303)
        Me.GroupBox5.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox5.Size = New System.Drawing.Size(369, 56)
        Me.GroupBox5.TabIndex = 13
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Email para cominicação de liberação OS"
        '
        'frmExportarParaOS
        '
        Me.AcceptButton = Me.btnSalvar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(383, 526)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtTempoRespostaServidor)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.chkCaixaDelimitadora)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btnSalvar)
        Me.Controls.Add(Me.GroupBox1)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "frmExportarParaOS"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Configurações do LynxSW"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
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
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents txtTempoRespostaServidor As Windows.Forms.TextBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents txtEmailPCP As Windows.Forms.TextBox
    Friend WithEvents GroupBox5 As Windows.Forms.GroupBox
End Class
