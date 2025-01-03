<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInspecaoQualidade
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInspecaoQualidade))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblCodMatFabricante = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dgvDados = New System.Windows.Forms.DataGridView()
        Me.txtValorCota = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnSalvar = New System.Windows.Forms.Button()
        Me.btnNovo = New System.Windows.Forms.Button()
        Me.btnFechar = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblidDimencionalReferencia = New System.Windows.Forms.Label()
        Me.TimerdgvDados = New System.Windows.Forms.Timer(Me.components)
        Me.CboNomeCota = New System.Windows.Forms.ComboBox()
        Me.CboTipoCota = New System.Windows.Forms.ComboBox()
        CType(Me.dgvDados, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(174, 70)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(116, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Numero Desenho:"
        '
        'lblCodMatFabricante
        '
        Me.lblCodMatFabricante.BackColor = System.Drawing.Color.White
        Me.lblCodMatFabricante.Location = New System.Drawing.Point(174, 94)
        Me.lblCodMatFabricante.Name = "lblCodMatFabricante"
        Me.lblCodMatFabricante.Size = New System.Drawing.Size(484, 27)
        Me.lblCodMatFabricante.TabIndex = 1
        Me.lblCodMatFabricante.Text = "Numero Desenho:"
        Me.lblCodMatFabricante.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 136)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 16)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Nome da Cota:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 191)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 16)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Tipo de Cota:"
        '
        'dgvDados
        '
        Me.dgvDados.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvDados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader
        Me.dgvDados.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedHeaders
        Me.dgvDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDados.Location = New System.Drawing.Point(12, 246)
        Me.dgvDados.Name = "dgvDados"
        Me.dgvDados.RowHeadersWidth = 51
        Me.dgvDados.RowTemplate.Height = 24
        Me.dgvDados.Size = New System.Drawing.Size(925, 323)
        Me.dgvDados.TabIndex = 7
        '
        'txtValorCota
        '
        Me.txtValorCota.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtValorCota.Location = New System.Drawing.Point(506, 155)
        Me.txtValorCota.MaxLength = 9
        Me.txtValorCota.Name = "txtValorCota"
        Me.txtValorCota.Size = New System.Drawing.Size(145, 22)
        Me.txtValorCota.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(506, 136)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(92, 16)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Valor da Cota:"
        '
        'btnSalvar
        '
        Me.btnSalvar.Location = New System.Drawing.Point(177, 13)
        Me.btnSalvar.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSalvar.Name = "btnSalvar"
        Me.btnSalvar.Size = New System.Drawing.Size(155, 39)
        Me.btnSalvar.TabIndex = 10045
        Me.btnSalvar.Text = "Salvar"
        Me.btnSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSalvar.UseVisualStyleBackColor = True
        '
        'btnNovo
        '
        Me.btnNovo.Location = New System.Drawing.Point(11, 13)
        Me.btnNovo.Margin = New System.Windows.Forms.Padding(4)
        Me.btnNovo.Name = "btnNovo"
        Me.btnNovo.Size = New System.Drawing.Size(155, 39)
        Me.btnNovo.TabIndex = 10046
        Me.btnNovo.Text = "Novo"
        Me.btnNovo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnNovo.UseVisualStyleBackColor = True
        '
        'btnFechar
        '
        Me.btnFechar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFechar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnFechar.Location = New System.Drawing.Point(782, 13)
        Me.btnFechar.Margin = New System.Windows.Forms.Padding(4)
        Me.btnFechar.Name = "btnFechar"
        Me.btnFechar.Size = New System.Drawing.Size(155, 39)
        Me.btnFechar.TabIndex = 10047
        Me.btnFechar.Text = "Fechar"
        Me.btnFechar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnFechar.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 70)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(48, 16)
        Me.Label5.TabIndex = 10048
        Me.Label5.Text = "Id Ref.:"
        '
        'lblidDimencionalReferencia
        '
        Me.lblidDimencionalReferencia.BackColor = System.Drawing.Color.White
        Me.lblidDimencionalReferencia.Location = New System.Drawing.Point(16, 94)
        Me.lblidDimencionalReferencia.Name = "lblidDimencionalReferencia"
        Me.lblidDimencionalReferencia.Size = New System.Drawing.Size(138, 27)
        Me.lblidDimencionalReferencia.TabIndex = 10049
        Me.lblidDimencionalReferencia.Text = "Id"
        Me.lblidDimencionalReferencia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TimerdgvDados
        '
        '
        'CboNomeCota
        '
        Me.CboNomeCota.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboNomeCota.FormattingEnabled = True
        Me.CboNomeCota.Items.AddRange(New Object() {"Largura Total", "Comprimento Total", "Profundidade Total", "Medida A", "Medida B", "Medida C", "Medida D", "Medida E", "Medida F", "Medida G", "Medida H", "Comprimento Blank", "Largura Blank", "Espessura", ""})
        Me.CboNomeCota.Location = New System.Drawing.Point(14, 155)
        Me.CboNomeCota.Name = "CboNomeCota"
        Me.CboNomeCota.Size = New System.Drawing.Size(486, 28)
        Me.CboNomeCota.TabIndex = 10050
        '
        'CboTipoCota
        '
        Me.CboTipoCota.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboTipoCota.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboTipoCota.FormattingEnabled = True
        Me.CboTipoCota.Items.AddRange(New Object() {"Diâmetro ", "Perpendicularidade", "Paralelismo ", "Perfil de linha", "Circularidade", "Concentricidade", "Simetria", "Inclinação", "Desvio"})
        Me.CboTipoCota.Location = New System.Drawing.Point(14, 210)
        Me.CboTipoCota.Name = "CboTipoCota"
        Me.CboTipoCota.Size = New System.Drawing.Size(481, 28)
        Me.CboTipoCota.TabIndex = 5
        '
        'frmInspecaoQualidade
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(949, 579)
        Me.Controls.Add(Me.CboNomeCota)
        Me.Controls.Add(Me.lblidDimencionalReferencia)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnFechar)
        Me.Controls.Add(Me.btnSalvar)
        Me.Controls.Add(Me.btnNovo)
        Me.Controls.Add(Me.txtValorCota)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.dgvDados)
        Me.Controls.Add(Me.CboTipoCota)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblCodMatFabricante)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmInspecaoQualidade"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cria criterios Inpeção da Qualidade/Dimensional."
        CType(Me.dgvDados, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents lblCodMatFabricante As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents dgvDados As Windows.Forms.DataGridView
    Friend WithEvents txtValorCota As Windows.Forms.TextBox
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents btnSalvar As Windows.Forms.Button
    Friend WithEvents btnNovo As Windows.Forms.Button
    Friend WithEvents btnFechar As Windows.Forms.Button
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents lblidDimencionalReferencia As Windows.Forms.Label
    Friend WithEvents TimerdgvDados As Windows.Forms.Timer
    Friend WithEvents CboNomeCota As Windows.Forms.ComboBox
    Friend WithEvents CboTipoCota As Windows.Forms.ComboBox
End Class
