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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMateriaisAlmoxarifado))
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtValorMaterial = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPesoMaterial = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnAssociarMaterialM2 = New System.Windows.Forms.Button()
        Me.txtm2Total = New System.Windows.Forms.TextBox()
        Me.txtLarguram2 = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtComprimentom2 = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.dgvMaterial, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(530, 21)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(93, 16)
        Me.Label4.TabIndex = 93
        Me.Label4.Text = "Valor material:"
        '
        'txtValorMaterial
        '
        Me.txtValorMaterial.Location = New System.Drawing.Point(629, 18)
        Me.txtValorMaterial.Name = "txtValorMaterial"
        Me.txtValorMaterial.Size = New System.Drawing.Size(72, 22)
        Me.txtValorMaterial.TabIndex = 92
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(340, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(93, 16)
        Me.Label2.TabIndex = 91
        Me.Label2.Text = "Peso material:"
        '
        'txtPesoMaterial
        '
        Me.txtPesoMaterial.Location = New System.Drawing.Point(442, 18)
        Me.txtPesoMaterial.Name = "txtPesoMaterial"
        Me.txtPesoMaterial.Size = New System.Drawing.Size(72, 22)
        Me.txtPesoMaterial.TabIndex = 90
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(593, 93)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(119, 16)
        Me.Label1.TabIndex = 89
        Me.Label1.Text = "Taxa de Utilização"
        '
        'btnAssociarMaterialM2
        '
        Me.btnAssociarMaterialM2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAssociarMaterialM2.Image = Global.SwLynx_4._1.My.Resources.Resources.salvar
        Me.btnAssociarMaterialM2.Location = New System.Drawing.Point(908, 18)
        Me.btnAssociarMaterialM2.Name = "btnAssociarMaterialM2"
        Me.btnAssociarMaterialM2.Size = New System.Drawing.Size(147, 46)
        Me.btnAssociarMaterialM2.TabIndex = 88
        Me.btnAssociarMaterialM2.Text = "Salvar material"
        Me.btnAssociarMaterialM2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnAssociarMaterialM2.UseVisualStyleBackColor = True
        '
        'txtm2Total
        '
        Me.txtm2Total.Location = New System.Drawing.Point(595, 112)
        Me.txtm2Total.Name = "txtm2Total"
        Me.txtm2Total.Size = New System.Drawing.Size(117, 22)
        Me.txtm2Total.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.txtm2Total, resources.GetString("txtm2Total.ToolTip"))
        '
        'txtLarguram2
        '
        Me.txtLarguram2.Location = New System.Drawing.Point(71, 21)
        Me.txtLarguram2.Name = "txtLarguram2"
        Me.txtLarguram2.Size = New System.Drawing.Size(72, 22)
        Me.txtLarguram2.TabIndex = 3
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(575, 115)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(14, 16)
        Me.Label22.TabIndex = 5
        Me.Label22.Text = "="
        '
        'txtComprimentom2
        '
        Me.txtComprimentom2.Location = New System.Drawing.Point(262, 21)
        Me.txtComprimentom2.Name = "txtComprimentom2"
        Me.txtComprimentom2.Size = New System.Drawing.Size(72, 22)
        Me.txtComprimentom2.TabIndex = 4
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(12, 24)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(56, 16)
        Me.Label20.TabIndex = 1
        Me.Label20.Text = "Largura:"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(169, 24)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(90, 16)
        Me.Label21.TabIndex = 2
        Me.Label21.Text = "Comprimento:"
        '
        'TxtPesqJuridico
        '
        Me.TxtPesqJuridico.BackColor = System.Drawing.Color.White
        Me.TxtPesqJuridico.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtPesqJuridico.Location = New System.Drawing.Point(456, 111)
        Me.TxtPesqJuridico.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtPesqJuridico.Name = "TxtPesqJuridico"
        Me.TxtPesqJuridico.Size = New System.Drawing.Size(103, 22)
        Me.TxtPesqJuridico.TabIndex = 98
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(456, 91)
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
        Me.TxtPesqDesc3.Location = New System.Drawing.Point(344, 111)
        Me.TxtPesqDesc3.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtPesqDesc3.Name = "TxtPesqDesc3"
        Me.TxtPesqDesc3.Size = New System.Drawing.Size(103, 22)
        Me.TxtPesqDesc3.TabIndex = 95
        '
        'TxtPesqDesc2
        '
        Me.TxtPesqDesc2.BackColor = System.Drawing.Color.White
        Me.TxtPesqDesc2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtPesqDesc2.Location = New System.Drawing.Point(232, 111)
        Me.TxtPesqDesc2.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtPesqDesc2.Name = "TxtPesqDesc2"
        Me.TxtPesqDesc2.Size = New System.Drawing.Size(103, 22)
        Me.TxtPesqDesc2.TabIndex = 94
        '
        'TxtPesqDesc1
        '
        Me.TxtPesqDesc1.BackColor = System.Drawing.Color.White
        Me.TxtPesqDesc1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtPesqDesc1.Location = New System.Drawing.Point(120, 111)
        Me.TxtPesqDesc1.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtPesqDesc1.Name = "TxtPesqDesc1"
        Me.TxtPesqDesc1.Size = New System.Drawing.Size(103, 22)
        Me.TxtPesqDesc1.TabIndex = 93
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(119, 91)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 16)
        Me.Label3.TabIndex = 91
        Me.Label3.Text = "Descrição"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(16, 91)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(51, 16)
        Me.Label6.TabIndex = 90
        Me.Label6.Text = "Código"
        '
        'TxtPesqCod
        '
        Me.TxtPesqCod.BackColor = System.Drawing.Color.White
        Me.TxtPesqCod.Location = New System.Drawing.Point(15, 111)
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
        Me.dgvMaterial.Location = New System.Drawing.Point(10, 141)
        Me.dgvMaterial.Margin = New System.Windows.Forms.Padding(4)
        Me.dgvMaterial.Name = "dgvMaterial"
        Me.dgvMaterial.ReadOnly = True
        Me.dgvMaterial.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders
        Me.dgvMaterial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvMaterial.Size = New System.Drawing.Size(1050, 575)
        Me.dgvMaterial.TabIndex = 88
        '
        'TimerDgvMaterial
        '
        Me.TimerDgvMaterial.Interval = 500
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Info
        Me.GroupBox1.Controls.Add(Me.txtLarguram2)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.txtValorMaterial)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtComprimentom2)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtPesoMaterial)
        Me.GroupBox1.Location = New System.Drawing.Point(10, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(789, 61)
        Me.GroupBox1.TabIndex = 99
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Material Tipo Chapa"
        Me.ToolTip1.SetToolTip(Me.GroupBox1, resources.GetString("GroupBox1.ToolTip"))
        '
        'ToolTip1
        '
        Me.ToolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.ToolTip1.ToolTipTitle = "Dicas de Uso:"
        '
        'frmMateriaisAlmoxarifado
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1067, 729)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnAssociarMaterialM2)
        Me.Controls.Add(Me.TxtPesqJuridico)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.TxtPesqDesc3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtPesqDesc2)
        Me.Controls.Add(Me.TxtPesqDesc1)
        Me.Controls.Add(Me.txtm2Total)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TxtPesqCod)
        Me.Controls.Add(Me.dgvMaterial)
        Me.Name = "frmMateriaisAlmoxarifado"
        Me.Text = "Materiais do Almoxarifado"
        CType(Me.dgvMaterial, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnAssociarMaterialM2 As Windows.Forms.Button
    Friend WithEvents txtm2Total As Windows.Forms.TextBox
    Friend WithEvents txtLarguram2 As Windows.Forms.TextBox
    Friend WithEvents Label22 As Windows.Forms.Label
    Friend WithEvents txtComprimentom2 As Windows.Forms.TextBox
    Friend WithEvents Label20 As Windows.Forms.Label
    Friend WithEvents Label21 As Windows.Forms.Label
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
    Friend WithEvents txtValorMaterial As Windows.Forms.TextBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents txtPesoMaterial As Windows.Forms.TextBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents ToolTip1 As Windows.Forms.ToolTip
End Class
