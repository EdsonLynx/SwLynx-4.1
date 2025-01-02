<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBuscarProgramas
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
        Me.dgvDesenhosCadastrados = New System.Windows.Forms.DataGridView()
        Me.dgvsw = New System.Windows.Forms.DataGridViewImageColumn()
        Me.dgvPDF = New System.Windows.Forms.DataGridViewImageColumn()
        Me.dgvDXF = New System.Windows.Forms.DataGridViewImageColumn()
        Me.dgvLXDS = New System.Windows.Forms.DataGridViewImageColumn()
        Me.dgvDFT = New System.Windows.Forms.DataGridViewImageColumn()
        Me.mnudgvDesenhosCadastrados = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Abrir3DDaLinhaSelecionadaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AbrirPDFDaLinhaSelecionadaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AbrirDXFDaLinhaSelecionadaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TimerdgvDesenhosCadastrados = New System.Windows.Forms.Timer(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtPesqEnderecoArquivo = New System.Windows.Forms.TextBox()
        Me.MySqlCommandBuilder1 = New MySqlConnector.MySqlCommandBuilder()
        Me.ProgressBarBuscarArquivos = New System.Windows.Forms.ProgressBar()
        Me.btnBuscarLXDS = New System.Windows.Forms.Button()
        Me.btnBuscarDFT = New System.Windows.Forms.Button()
        CType(Me.dgvDesenhosCadastrados, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnudgvDesenhosCadastrados.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvDesenhosCadastrados
        '
        Me.dgvDesenhosCadastrados.AllowUserToAddRows = False
        Me.dgvDesenhosCadastrados.AllowUserToDeleteRows = False
        Me.dgvDesenhosCadastrados.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvDesenhosCadastrados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader
        Me.dgvDesenhosCadastrados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDesenhosCadastrados.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgvsw, Me.dgvPDF, Me.dgvDXF, Me.dgvLXDS, Me.dgvDFT})
        Me.dgvDesenhosCadastrados.ContextMenuStrip = Me.mnudgvDesenhosCadastrados
        Me.dgvDesenhosCadastrados.Location = New System.Drawing.Point(12, 99)
        Me.dgvDesenhosCadastrados.Name = "dgvDesenhosCadastrados"
        Me.dgvDesenhosCadastrados.ReadOnly = True
        Me.dgvDesenhosCadastrados.RowHeadersWidth = 51
        Me.dgvDesenhosCadastrados.RowTemplate.Height = 24
        Me.dgvDesenhosCadastrados.Size = New System.Drawing.Size(1101, 484)
        Me.dgvDesenhosCadastrados.TabIndex = 0
        '
        'dgvsw
        '
        Me.dgvsw.Frozen = True
        Me.dgvsw.HeaderText = "SW"
        Me.dgvsw.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.dgvsw.MinimumWidth = 6
        Me.dgvsw.Name = "dgvsw"
        Me.dgvsw.ReadOnly = True
        Me.dgvsw.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvsw.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.dgvsw.Width = 6
        '
        'dgvPDF
        '
        Me.dgvPDF.Frozen = True
        Me.dgvPDF.HeaderText = "PDF"
        Me.dgvPDF.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.dgvPDF.MinimumWidth = 6
        Me.dgvPDF.Name = "dgvPDF"
        Me.dgvPDF.ReadOnly = True
        Me.dgvPDF.Width = 6
        '
        'dgvDXF
        '
        Me.dgvDXF.Frozen = True
        Me.dgvDXF.HeaderText = "DXF"
        Me.dgvDXF.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.dgvDXF.MinimumWidth = 6
        Me.dgvDXF.Name = "dgvDXF"
        Me.dgvDXF.ReadOnly = True
        Me.dgvDXF.Width = 6
        '
        'dgvLXDS
        '
        Me.dgvLXDS.Frozen = True
        Me.dgvLXDS.HeaderText = "LXDS"
        Me.dgvLXDS.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.dgvLXDS.MinimumWidth = 6
        Me.dgvLXDS.Name = "dgvLXDS"
        Me.dgvLXDS.ReadOnly = True
        Me.dgvLXDS.Width = 6
        '
        'dgvDFT
        '
        Me.dgvDFT.Frozen = True
        Me.dgvDFT.HeaderText = "DFT"
        Me.dgvDFT.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.dgvDFT.MinimumWidth = 6
        Me.dgvDFT.Name = "dgvDFT"
        Me.dgvDFT.ReadOnly = True
        Me.dgvDFT.Width = 6
        '
        'mnudgvDesenhosCadastrados
        '
        Me.mnudgvDesenhosCadastrados.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.mnudgvDesenhosCadastrados.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Abrir3DDaLinhaSelecionadaToolStripMenuItem, Me.AbrirPDFDaLinhaSelecionadaToolStripMenuItem, Me.AbrirDXFDaLinhaSelecionadaToolStripMenuItem})
        Me.mnudgvDesenhosCadastrados.Name = "mnudgvDesenhosCadastrados"
        Me.mnudgvDesenhosCadastrados.Size = New System.Drawing.Size(292, 82)
        '
        'Abrir3DDaLinhaSelecionadaToolStripMenuItem
        '
        Me.Abrir3DDaLinhaSelecionadaToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.sw
        Me.Abrir3DDaLinhaSelecionadaToolStripMenuItem.Name = "Abrir3DDaLinhaSelecionadaToolStripMenuItem"
        Me.Abrir3DDaLinhaSelecionadaToolStripMenuItem.Size = New System.Drawing.Size(291, 26)
        Me.Abrir3DDaLinhaSelecionadaToolStripMenuItem.Text = "Abrir 3D da Linha Selecionada"
        '
        'AbrirPDFDaLinhaSelecionadaToolStripMenuItem
        '
        Me.AbrirPDFDaLinhaSelecionadaToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.pdf
        Me.AbrirPDFDaLinhaSelecionadaToolStripMenuItem.Name = "AbrirPDFDaLinhaSelecionadaToolStripMenuItem"
        Me.AbrirPDFDaLinhaSelecionadaToolStripMenuItem.Size = New System.Drawing.Size(291, 26)
        Me.AbrirPDFDaLinhaSelecionadaToolStripMenuItem.Text = "Abrir PDF da Linha Selecionada"
        '
        'AbrirDXFDaLinhaSelecionadaToolStripMenuItem
        '
        Me.AbrirDXFDaLinhaSelecionadaToolStripMenuItem.Image = Global.SwLynx_4._1.My.Resources.Resources.dxf
        Me.AbrirDXFDaLinhaSelecionadaToolStripMenuItem.Name = "AbrirDXFDaLinhaSelecionadaToolStripMenuItem"
        Me.AbrirDXFDaLinhaSelecionadaToolStripMenuItem.Size = New System.Drawing.Size(291, 26)
        Me.AbrirDXFDaLinhaSelecionadaToolStripMenuItem.Text = "Abrir DXF da Linha Selecionada"
        '
        'TimerdgvDesenhosCadastrados
        '
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(22, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(148, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Endereço dos Arquivos"
        '
        'txtPesqEnderecoArquivo
        '
        Me.txtPesqEnderecoArquivo.Location = New System.Drawing.Point(19, 42)
        Me.txtPesqEnderecoArquivo.Name = "txtPesqEnderecoArquivo"
        Me.txtPesqEnderecoArquivo.Size = New System.Drawing.Size(532, 22)
        Me.txtPesqEnderecoArquivo.TabIndex = 2
        '
        'MySqlCommandBuilder1
        '
        Me.MySqlCommandBuilder1.DataAdapter = Nothing
        Me.MySqlCommandBuilder1.QuotePrefix = "`"
        Me.MySqlCommandBuilder1.QuoteSuffix = "`"
        '
        'ProgressBarBuscarArquivos
        '
        Me.ProgressBarBuscarArquivos.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBarBuscarArquivos.Location = New System.Drawing.Point(12, 70)
        Me.ProgressBarBuscarArquivos.Name = "ProgressBarBuscarArquivos"
        Me.ProgressBarBuscarArquivos.Size = New System.Drawing.Size(1101, 23)
        Me.ProgressBarBuscarArquivos.TabIndex = 5
        '
        'btnBuscarLXDS
        '
        Me.btnBuscarLXDS.Image = Global.SwLynx_4._1.My.Resources.Resources.CYPCUT
        Me.btnBuscarLXDS.Location = New System.Drawing.Point(683, 12)
        Me.btnBuscarLXDS.Name = "btnBuscarLXDS"
        Me.btnBuscarLXDS.Size = New System.Drawing.Size(120, 53)
        Me.btnBuscarLXDS.TabIndex = 4
        Me.btnBuscarLXDS.Text = "Buscar Arquivo LXDS"
        Me.btnBuscarLXDS.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnBuscarLXDS.UseVisualStyleBackColor = True
        '
        'btnBuscarDFT
        '
        Me.btnBuscarDFT.Image = Global.SwLynx_4._1.My.Resources.Resources.DFT
        Me.btnBuscarDFT.Location = New System.Drawing.Point(557, 11)
        Me.btnBuscarDFT.Name = "btnBuscarDFT"
        Me.btnBuscarDFT.Size = New System.Drawing.Size(120, 53)
        Me.btnBuscarDFT.TabIndex = 3
        Me.btnBuscarDFT.Text = "Buscar Arquivo DFT"
        Me.btnBuscarDFT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnBuscarDFT.UseVisualStyleBackColor = True
        '
        'FrmBuscarProgramas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1125, 595)
        Me.Controls.Add(Me.ProgressBarBuscarArquivos)
        Me.Controls.Add(Me.btnBuscarLXDS)
        Me.Controls.Add(Me.btnBuscarDFT)
        Me.Controls.Add(Me.txtPesqEnderecoArquivo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dgvDesenhosCadastrados)
        Me.Name = "FrmBuscarProgramas"
        Me.Text = "Buscar Programas-Pasta padrão"
        CType(Me.dgvDesenhosCadastrados, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnudgvDesenhosCadastrados.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dgvDesenhosCadastrados As Windows.Forms.DataGridView
    Friend WithEvents TimerdgvDesenhosCadastrados As Windows.Forms.Timer
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents txtPesqEnderecoArquivo As Windows.Forms.TextBox
    Friend WithEvents MySqlCommandBuilder1 As MySqlConnector.MySqlCommandBuilder
    Friend WithEvents btnBuscarDFT As Windows.Forms.Button
    Friend WithEvents btnBuscarLXDS As Windows.Forms.Button
    Friend WithEvents ProgressBarBuscarArquivos As Windows.Forms.ProgressBar
    Friend WithEvents dgvsw As Windows.Forms.DataGridViewImageColumn
    Friend WithEvents dgvPDF As Windows.Forms.DataGridViewImageColumn
    Friend WithEvents dgvDXF As Windows.Forms.DataGridViewImageColumn
    Friend WithEvents dgvLXDS As Windows.Forms.DataGridViewImageColumn
    Friend WithEvents dgvDFT As Windows.Forms.DataGridViewImageColumn
    Friend WithEvents mnudgvDesenhosCadastrados As Windows.Forms.ContextMenuStrip
    Friend WithEvents AbrirPDFDaLinhaSelecionadaToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents AbrirDXFDaLinhaSelecionadaToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents Abrir3DDaLinhaSelecionadaToolStripMenuItem As Windows.Forms.ToolStripMenuItem
End Class
