<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DuplicarOrdemServico
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DuplicarOrdemServico))
        Me.txtDescricao = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtDescricaoTag = New System.Windows.Forms.TextBox()
        Me.txtCliente = New System.Windows.Forms.TextBox()
        Me.cboTag = New System.Windows.Forms.ComboBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.cboProjeto = New System.Windows.Forms.ComboBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtDescricao
        '
        Me.txtDescricao.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDescricao.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDescricao.Location = New System.Drawing.Point(82, 111)
        Me.txtDescricao.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtDescricao.MaxLength = 200
        Me.txtDescricao.Multiline = True
        Me.txtDescricao.Name = "txtDescricao"
        Me.txtDescricao.Size = New System.Drawing.Size(634, 61)
        Me.txtDescricao.TabIndex = 21
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(13, 114)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(72, 16)
        Me.Label25.TabIndex = 20
        Me.Label25.Text = "Descrição:"
        '
        'txtDescricaoTag
        '
        Me.txtDescricaoTag.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDescricaoTag.Enabled = False
        Me.txtDescricaoTag.Location = New System.Drawing.Point(360, 45)
        Me.txtDescricaoTag.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtDescricaoTag.Multiline = True
        Me.txtDescricaoTag.Name = "txtDescricaoTag"
        Me.txtDescricaoTag.Size = New System.Drawing.Size(356, 61)
        Me.txtDescricaoTag.TabIndex = 19
        '
        'txtCliente
        '
        Me.txtCliente.Enabled = False
        Me.txtCliente.Location = New System.Drawing.Point(82, 45)
        Me.txtCliente.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtCliente.Multiline = True
        Me.txtCliente.Name = "txtCliente"
        Me.txtCliente.Size = New System.Drawing.Size(231, 61)
        Me.txtCliente.TabIndex = 18
        '
        'cboTag
        '
        Me.cboTag.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboTag.FormattingEnabled = True
        Me.cboTag.Location = New System.Drawing.Point(360, 17)
        Me.cboTag.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cboTag.Name = "cboTag"
        Me.cboTag.Size = New System.Drawing.Size(356, 24)
        Me.cboTag.TabIndex = 17
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(319, 21)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(35, 16)
        Me.Label24.TabIndex = 16
        Me.Label24.Text = "Tag:"
        '
        'cboProjeto
        '
        Me.cboProjeto.FormattingEnabled = True
        Me.cboProjeto.Location = New System.Drawing.Point(82, 17)
        Me.cboProjeto.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cboProjeto.Name = "cboProjeto"
        Me.cboProjeto.Size = New System.Drawing.Size(231, 24)
        Me.cboProjeto.TabIndex = 15
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(32, 21)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(53, 16)
        Me.Label20.TabIndex = 14
        Me.Label20.Text = "Projeto:"
        '
        'btnCancelar
        '
        Me.btnCancelar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Image)
        Me.btnCancelar.Location = New System.Drawing.Point(552, 176)
        Me.btnCancelar.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(164, 51)
        Me.btnCancelar.TabIndex = 22
        Me.btnCancelar.Text = "Cancelar e Sair"
        Me.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'DuplicarOrdemServico
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(728, 236)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.txtDescricao)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.txtDescricaoTag)
        Me.Controls.Add(Me.txtCliente)
        Me.Controls.Add(Me.cboTag)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.cboProjeto)
        Me.Controls.Add(Me.Label20)
        Me.Name = "DuplicarOrdemServico"
        Me.Text = "Duplicar Ordem Servico"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtDescricao As Windows.Forms.TextBox
    Friend WithEvents Label25 As Windows.Forms.Label
    Friend WithEvents txtDescricaoTag As Windows.Forms.TextBox
    Friend WithEvents txtCliente As Windows.Forms.TextBox
    Friend WithEvents cboTag As Windows.Forms.ComboBox
    Friend WithEvents Label24 As Windows.Forms.Label
    Friend WithEvents cboProjeto As Windows.Forms.ComboBox
    Friend WithEvents Label20 As Windows.Forms.Label
    Friend WithEvents btnCancelar As Windows.Forms.Button
End Class
