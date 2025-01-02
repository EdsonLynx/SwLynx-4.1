<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRNC
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
        Me.dgvDados = New System.Windows.Forms.DataGridView()
        Me.dgvSelecao = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.dgvIconeestatus = New System.Windows.Forms.DataGridViewImageColumn()
        Me.TimerdgvDados = New System.Windows.Forms.Timer(Me.components)
        Me.chkConcluidas = New System.Windows.Forms.CheckBox()
        Me.btnFechar = New System.Windows.Forms.Button()
        Me.btnFinalizarRNC = New System.Windows.Forms.Button()
        CType(Me.dgvDados, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvDados
        '
        Me.dgvDados.AllowUserToAddRows = False
        Me.dgvDados.AllowUserToDeleteRows = False
        Me.dgvDados.AllowUserToOrderColumns = True
        Me.dgvDados.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvDados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader
        Me.dgvDados.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders
        Me.dgvDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDados.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgvSelecao, Me.dgvIconeestatus})
        Me.dgvDados.Location = New System.Drawing.Point(5, 97)
        Me.dgvDados.Name = "dgvDados"
        Me.dgvDados.RowHeadersWidth = 51
        Me.dgvDados.RowTemplate.Height = 24
        Me.dgvDados.Size = New System.Drawing.Size(1069, 540)
        Me.dgvDados.TabIndex = 0
        '
        'dgvSelecao
        '
        Me.dgvSelecao.Frozen = True
        Me.dgvSelecao.HeaderText = "dgvSelecao"
        Me.dgvSelecao.MinimumWidth = 6
        Me.dgvSelecao.Name = "dgvSelecao"
        Me.dgvSelecao.Width = 6
        '
        'dgvIconeestatus
        '
        Me.dgvIconeestatus.Frozen = True
        Me.dgvIconeestatus.HeaderText = "dgvIconeestatus"
        Me.dgvIconeestatus.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.dgvIconeestatus.MinimumWidth = 6
        Me.dgvIconeestatus.Name = "dgvIconeestatus"
        Me.dgvIconeestatus.Width = 6
        '
        'TimerdgvDados
        '
        '
        'chkConcluidas
        '
        Me.chkConcluidas.AutoSize = True
        Me.chkConcluidas.Location = New System.Drawing.Point(12, 71)
        Me.chkConcluidas.Name = "chkConcluidas"
        Me.chkConcluidas.Size = New System.Drawing.Size(212, 20)
        Me.chkConcluidas.TabIndex = 1
        Me.chkConcluidas.Text = "Mostrar tambem as concluidas"
        Me.chkConcluidas.UseVisualStyleBackColor = True
        '
        'btnFechar
        '
        Me.btnFechar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ' Me.btnFechar.Image = Global.SwLynx_4._1.My.Resources.Resources.Cancelar
        Me.btnFechar.Location = New System.Drawing.Point(899, 12)
        Me.btnFechar.Name = "btnFechar"
        Me.btnFechar.Size = New System.Drawing.Size(175, 44)
        Me.btnFechar.TabIndex = 2
        Me.btnFechar.Text = "             Fechar"
        Me.btnFechar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnFechar.UseVisualStyleBackColor = True
        '
        'btnFinalizarRNC
        '
        'Me.btnFinalizarRNC.Image = Global.SwLynx_4._1.My.Resources.Resources.atualizar
        Me.btnFinalizarRNC.Location = New System.Drawing.Point(12, 12)
        Me.btnFinalizarRNC.Name = "btnFinalizarRNC"
        Me.btnFinalizarRNC.Size = New System.Drawing.Size(175, 44)
        Me.btnFinalizarRNC.TabIndex = 3
        Me.btnFinalizarRNC.Text = "Finalizar RNC Selecionadas"
        Me.btnFinalizarRNC.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnFinalizarRNC.UseVisualStyleBackColor = True
        '
        'frmRNC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1082, 643)
        Me.Controls.Add(Me.btnFinalizarRNC)
        Me.Controls.Add(Me.btnFechar)
        Me.Controls.Add(Me.chkConcluidas)
        Me.Controls.Add(Me.dgvDados)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmRNC"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Lista de Registro de Não Conformidades (RNC)"
        CType(Me.dgvDados, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dgvDados As Windows.Forms.DataGridView
    Friend WithEvents TimerdgvDados As Windows.Forms.Timer
    Friend WithEvents chkConcluidas As Windows.Forms.CheckBox
    Friend WithEvents dgvSelecao As Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents dgvIconeestatus As Windows.Forms.DataGridViewImageColumn
    Friend WithEvents btnFechar As Windows.Forms.Button
    Friend WithEvents btnFinalizarRNC As Windows.Forms.Button
End Class
