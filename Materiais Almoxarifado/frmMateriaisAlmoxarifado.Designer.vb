<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMateriaisAlmoxarifado
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
        Me.TabControlUnidadeMaterial = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtValorMaterialCalculado = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPesoMaterialCalculado = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnAssociarMaterialM2 = New System.Windows.Forms.Button()
        Me.txtm2Total = New System.Windows.Forms.TextBox()
        Me.txtLarguram2 = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtComprimentom2 = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.btnAssociarMaterialML = New System.Windows.Forms.Button()
        Me.txtMetroLinearotal = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtComprimentoML = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.btnRelacao1para1 = New System.Windows.Forms.Button()
        Me.txtQtde1para1 = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.TxtPesqJuridico = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.TxtPesqDesc3 = New System.Windows.Forms.TextBox()
        Me.TxtPesqDesc2 = New System.Windows.Forms.TextBox()
        Me.TxtPesqDesc1 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TxtPesqCod = New System.Windows.Forms.TextBox()
        Me.dgvMaterial = New System.Windows.Forms.DataGridView()
        Me.TimerDgvMaterial = New System.Windows.Forms.Timer(Me.components)
        Me.TabControlUnidadeMaterial.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.TabPage6.SuspendLayout()
        CType(Me.dgvMaterial, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControlUnidadeMaterial
        '
        Me.TabControlUnidadeMaterial.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControlUnidadeMaterial.Controls.Add(Me.TabPage1)
        Me.TabControlUnidadeMaterial.Controls.Add(Me.TabPage5)
        Me.TabControlUnidadeMaterial.Controls.Add(Me.TabPage6)
        Me.TabControlUnidadeMaterial.Location = New System.Drawing.Point(8, 9)
        Me.TabControlUnidadeMaterial.Name = "TabControlUnidadeMaterial"
        Me.TabControlUnidadeMaterial.SelectedIndex = 0
        Me.TabControlUnidadeMaterial.Size = New System.Drawing.Size(1052, 115)
        Me.TabControlUnidadeMaterial.TabIndex = 89
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.txtValorMaterialCalculado)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.txtPesoMaterialCalculado)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.btnAssociarMaterialM2)
        Me.TabPage1.Controls.Add(Me.txtm2Total)
        Me.TabPage1.Controls.Add(Me.txtLarguram2)
        Me.TabPage1.Controls.Add(Me.Label22)
        Me.TabPage1.Controls.Add(Me.Label18)
        Me.TabPage1.Controls.Add(Me.txtComprimentom2)
        Me.TabPage1.Controls.Add(Me.Label20)
        Me.TabPage1.Controls.Add(Me.Label21)
        Me.TabPage1.Location = New System.Drawing.Point(4, 25)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1044, 86)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Calculo de chapa M²"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(684, 13)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(93, 16)
        Me.Label4.TabIndex = 93
        Me.Label4.Text = "Valor Material:"
        '
        'txtValorMaterialCalculado
        '
        Me.txtValorMaterialCalculado.Location = New System.Drawing.Point(686, 32)
        Me.txtValorMaterialCalculado.Name = "txtValorMaterialCalculado"
        Me.txtValorMaterialCalculado.Size = New System.Drawing.Size(72, 22)
        Me.txtValorMaterialCalculado.TabIndex = 92
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(558, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(93, 16)
        Me.Label2.TabIndex = 91
        Me.Label2.Text = "Peso Material:"
        '
        'txtPesoMaterialCalculado
        '
        Me.txtPesoMaterialCalculado.Location = New System.Drawing.Point(560, 29)
        Me.txtPesoMaterialCalculado.Name = "txtPesoMaterialCalculado"
        Me.txtPesoMaterialCalculado.Size = New System.Drawing.Size(72, 22)
        Me.txtPesoMaterialCalculado.TabIndex = 90
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(410, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(119, 16)
        Me.Label1.TabIndex = 89
        Me.Label1.Text = "Taxa de Utilização"
        '
        'btnAssociarMaterialM2
        '
        Me.btnAssociarMaterialM2.Location = New System.Drawing.Point(874, 12)
        Me.btnAssociarMaterialM2.Name = "btnAssociarMaterialM2"
        Me.btnAssociarMaterialM2.Size = New System.Drawing.Size(164, 46)
        Me.btnAssociarMaterialM2.TabIndex = 88
        Me.btnAssociarMaterialM2.Text = "Salvar Material"
        Me.btnAssociarMaterialM2.UseVisualStyleBackColor = True
        '
        'txtm2Total
        '
        Me.txtm2Total.Location = New System.Drawing.Point(412, 29)
        Me.txtm2Total.Name = "txtm2Total"
        Me.txtm2Total.Size = New System.Drawing.Size(72, 22)
        Me.txtm2Total.TabIndex = 6
        '
        'txtLarguram2
        '
        Me.txtLarguram2.Location = New System.Drawing.Point(306, 18)
        Me.txtLarguram2.Name = "txtLarguram2"
        Me.txtLarguram2.Size = New System.Drawing.Size(72, 22)
        Me.txtLarguram2.TabIndex = 3
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(392, 32)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(14, 16)
        Me.Label22.TabIndex = 5
        Me.Label22.Text = "="
        '
        'Label18
        '
        Me.Label18.Location = New System.Drawing.Point(7, 3)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(182, 65)
        Me.Label18.TabIndex = 0
        Me.Label18.Text = "Calculo Chapa área tamanho padrão 1200 x 3000 mm = 3,6m²:"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtComprimentom2
        '
        Me.txtComprimentom2.Location = New System.Drawing.Point(306, 46)
        Me.txtComprimentom2.Name = "txtComprimentom2"
        Me.txtComprimentom2.Size = New System.Drawing.Size(72, 22)
        Me.txtComprimentom2.TabIndex = 4
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(247, 21)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(56, 16)
        Me.Label20.TabIndex = 1
        Me.Label20.Text = "Largura:"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(213, 49)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(90, 16)
        Me.Label21.TabIndex = 2
        Me.Label21.Text = "Comprimento:"
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.btnAssociarMaterialML)
        Me.TabPage5.Controls.Add(Me.txtMetroLinearotal)
        Me.TabPage5.Controls.Add(Me.Label23)
        Me.TabPage5.Controls.Add(Me.txtComprimentoML)
        Me.TabPage5.Controls.Add(Me.Label26)
        Me.TabPage5.Location = New System.Drawing.Point(4, 25)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(1044, 86)
        Me.TabPage5.TabIndex = 1
        Me.TabPage5.Text = "Calculo por Metro Linear"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'btnAssociarMaterialML
        '
        Me.btnAssociarMaterialML.Location = New System.Drawing.Point(874, 12)
        Me.btnAssociarMaterialML.Name = "btnAssociarMaterialML"
        Me.btnAssociarMaterialML.Size = New System.Drawing.Size(164, 46)
        Me.btnAssociarMaterialML.TabIndex = 87
        Me.btnAssociarMaterialML.Text = "Salvar Material"
        Me.btnAssociarMaterialML.UseVisualStyleBackColor = True
        '
        'txtMetroLinearotal
        '
        Me.txtMetroLinearotal.Location = New System.Drawing.Point(288, 25)
        Me.txtMetroLinearotal.Name = "txtMetroLinearotal"
        Me.txtMetroLinearotal.Size = New System.Drawing.Size(72, 22)
        Me.txtMetroLinearotal.TabIndex = 13
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(268, 28)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(14, 16)
        Me.Label23.TabIndex = 12
        Me.Label23.Text = "="
        '
        'txtComprimentoML
        '
        Me.txtComprimentoML.Location = New System.Drawing.Point(182, 25)
        Me.txtComprimentoML.Name = "txtComprimentoML"
        Me.txtComprimentoML.Size = New System.Drawing.Size(72, 22)
        Me.txtComprimentoML.TabIndex = 11
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(20, 28)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(156, 16)
        Me.Label26.TabIndex = 9
        Me.Label26.Text = "Comprimento em Metros:"
        '
        'TabPage6
        '
        Me.TabPage6.Controls.Add(Me.btnRelacao1para1)
        Me.TabPage6.Controls.Add(Me.txtQtde1para1)
        Me.TabPage6.Controls.Add(Me.Label24)
        Me.TabPage6.Controls.Add(Me.TextBox2)
        Me.TabPage6.Controls.Add(Me.Label25)
        Me.TabPage6.Location = New System.Drawing.Point(4, 25)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage6.Size = New System.Drawing.Size(1044, 86)
        Me.TabPage6.TabIndex = 2
        Me.TabPage6.Text = "Material Relação 1/1"
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'btnRelacao1para1
        '
        Me.btnRelacao1para1.Location = New System.Drawing.Point(874, 12)
        Me.btnRelacao1para1.Name = "btnRelacao1para1"
        Me.btnRelacao1para1.Size = New System.Drawing.Size(164, 46)
        Me.btnRelacao1para1.TabIndex = 92
        Me.btnRelacao1para1.Text = "Salvar Material"
        Me.btnRelacao1para1.UseVisualStyleBackColor = True
        '
        'txtQtde1para1
        '
        Me.txtQtde1para1.Location = New System.Drawing.Point(264, 24)
        Me.txtQtde1para1.Name = "txtQtde1para1"
        Me.txtQtde1para1.Size = New System.Drawing.Size(72, 22)
        Me.txtQtde1para1.TabIndex = 91
        Me.txtQtde1para1.Text = "1"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(244, 27)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(11, 16)
        Me.Label24.TabIndex = 90
        Me.Label24.Text = "/"
        '
        'TextBox2
        '
        Me.TextBox2.Enabled = False
        Me.TextBox2.Location = New System.Drawing.Point(158, 24)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(72, 22)
        Me.TextBox2.TabIndex = 89
        Me.TextBox2.Text = "1"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(16, 27)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(138, 16)
        Me.Label25.TabIndex = 88
        Me.Label25.Text = "Informe a Quantidade:"
        '
        'TxtPesqJuridico
        '
        Me.TxtPesqJuridico.BackColor = System.Drawing.Color.White
        Me.TxtPesqJuridico.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtPesqJuridico.Location = New System.Drawing.Point(454, 148)
        Me.TxtPesqJuridico.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtPesqJuridico.Name = "TxtPesqJuridico"
        Me.TxtPesqJuridico.Size = New System.Drawing.Size(103, 22)
        Me.TxtPesqJuridico.TabIndex = 98
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(454, 128)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(77, 16)
        Me.Label17.TabIndex = 97
        Me.Label17.Text = "Fornecedor"
        '
        'TxtPesqDesc3
        '
        Me.TxtPesqDesc3.BackColor = System.Drawing.Color.White
        Me.TxtPesqDesc3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtPesqDesc3.Location = New System.Drawing.Point(342, 148)
        Me.TxtPesqDesc3.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtPesqDesc3.Name = "TxtPesqDesc3"
        Me.TxtPesqDesc3.Size = New System.Drawing.Size(103, 22)
        Me.TxtPesqDesc3.TabIndex = 95
        '
        'TxtPesqDesc2
        '
        Me.TxtPesqDesc2.BackColor = System.Drawing.Color.White
        Me.TxtPesqDesc2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtPesqDesc2.Location = New System.Drawing.Point(230, 148)
        Me.TxtPesqDesc2.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtPesqDesc2.Name = "TxtPesqDesc2"
        Me.TxtPesqDesc2.Size = New System.Drawing.Size(103, 22)
        Me.TxtPesqDesc2.TabIndex = 94
        '
        'TxtPesqDesc1
        '
        Me.TxtPesqDesc1.BackColor = System.Drawing.Color.White
        Me.TxtPesqDesc1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtPesqDesc1.Location = New System.Drawing.Point(118, 148)
        Me.TxtPesqDesc1.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtPesqDesc1.Name = "TxtPesqDesc1"
        Me.TxtPesqDesc1.Size = New System.Drawing.Size(103, 22)
        Me.TxtPesqDesc1.TabIndex = 93
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(117, 128)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 16)
        Me.Label3.TabIndex = 91
        Me.Label3.Text = "Descrição"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(14, 128)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(51, 16)
        Me.Label6.TabIndex = 90
        Me.Label6.Text = "Código"
        '
        'TxtPesqCod
        '
        Me.TxtPesqCod.BackColor = System.Drawing.Color.White
        Me.TxtPesqCod.Location = New System.Drawing.Point(13, 148)
        Me.TxtPesqCod.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtPesqCod.Name = "TxtPesqCod"
        Me.TxtPesqCod.Size = New System.Drawing.Size(96, 22)
        Me.TxtPesqCod.TabIndex = 92
        '
        'dgvMaterial
        '
        Me.dgvMaterial.AllowUserToAddRows = False
        Me.dgvMaterial.AllowUserToDeleteRows = False
        Me.dgvMaterial.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvMaterial.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader
        Me.dgvMaterial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMaterial.Location = New System.Drawing.Point(10, 178)
        Me.dgvMaterial.Margin = New System.Windows.Forms.Padding(4)
        Me.dgvMaterial.Name = "dgvMaterial"
        Me.dgvMaterial.ReadOnly = True
        Me.dgvMaterial.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders
        Me.dgvMaterial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvMaterial.Size = New System.Drawing.Size(1050, 538)
        Me.dgvMaterial.TabIndex = 88
        '
        'TimerDgvMaterial
        '
        Me.TimerDgvMaterial.Interval = 500
        '
        'frmMateriaisAlmoxarifado
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1067, 729)
        Me.Controls.Add(Me.TabControlUnidadeMaterial)
        Me.Controls.Add(Me.TxtPesqJuridico)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.TxtPesqDesc3)
        Me.Controls.Add(Me.TxtPesqDesc2)
        Me.Controls.Add(Me.TxtPesqDesc1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TxtPesqCod)
        Me.Controls.Add(Me.dgvMaterial)
        Me.Name = "frmMateriaisAlmoxarifado"
        Me.Text = "Materiais do Almoxarifado"
        Me.TabControlUnidadeMaterial.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage5.PerformLayout()
        Me.TabPage6.ResumeLayout(False)
        Me.TabPage6.PerformLayout()
        CType(Me.dgvMaterial, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TabControlUnidadeMaterial As Windows.Forms.TabControl
    Friend WithEvents TabPage1 As Windows.Forms.TabPage
    Friend WithEvents btnAssociarMaterialM2 As Windows.Forms.Button
    Friend WithEvents txtm2Total As Windows.Forms.TextBox
    Friend WithEvents txtLarguram2 As Windows.Forms.TextBox
    Friend WithEvents Label22 As Windows.Forms.Label
    Friend WithEvents Label18 As Windows.Forms.Label
    Friend WithEvents txtComprimentom2 As Windows.Forms.TextBox
    Friend WithEvents Label20 As Windows.Forms.Label
    Friend WithEvents Label21 As Windows.Forms.Label
    Friend WithEvents TabPage5 As Windows.Forms.TabPage
    Friend WithEvents btnAssociarMaterialML As Windows.Forms.Button
    Friend WithEvents txtMetroLinearotal As Windows.Forms.TextBox
    Friend WithEvents Label23 As Windows.Forms.Label
    Friend WithEvents txtComprimentoML As Windows.Forms.TextBox
    Friend WithEvents Label26 As Windows.Forms.Label
    Friend WithEvents TabPage6 As Windows.Forms.TabPage
    Friend WithEvents btnRelacao1para1 As Windows.Forms.Button
    Friend WithEvents txtQtde1para1 As Windows.Forms.TextBox
    Friend WithEvents Label24 As Windows.Forms.Label
    Friend WithEvents TextBox2 As Windows.Forms.TextBox
    Friend WithEvents Label25 As Windows.Forms.Label
    Friend WithEvents TxtPesqJuridico As Windows.Forms.TextBox
    Friend WithEvents Label17 As Windows.Forms.Label
    Friend WithEvents TxtPesqDesc3 As Windows.Forms.TextBox
    Friend WithEvents TxtPesqDesc2 As Windows.Forms.TextBox
    Friend WithEvents TxtPesqDesc1 As Windows.Forms.TextBox
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents TxtPesqCod As Windows.Forms.TextBox
    Friend WithEvents dgvMaterial As Windows.Forms.DataGridView
    Friend WithEvents TimerDgvMaterial As Windows.Forms.Timer
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents txtValorMaterialCalculado As Windows.Forms.TextBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents txtPesoMaterialCalculado As Windows.Forms.TextBox
    Friend WithEvents Label1 As Windows.Forms.Label
End Class
