<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCriaProdutos
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnBuscarIsometrico = New System.Windows.Forms.Button()
        Me.btnBuscarFichaTecnica = New System.Windows.Forms.Button()
        Me.lblEnderecoIsometrico = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblEnderecoFichaTecnica = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtDescricaoProduto = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtCodOmie = New System.Windows.Forms.TextBox()
        Me.txCodDesenhoProduto = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnSalvar = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.btnBuscarIsometrico)
        Me.GroupBox1.Controls.Add(Me.btnBuscarFichaTecnica)
        Me.GroupBox1.Controls.Add(Me.lblEnderecoIsometrico)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.lblEnderecoFichaTecnica)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.txtDescricaoProduto)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtCodOmie)
        Me.GroupBox1.Controls.Add(Me.txCodDesenhoProduto)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 82)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(667, 407)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Dados do Produto"
        '
        'btnBuscarIsometrico
        '
        Me.btnBuscarIsometrico.Location = New System.Drawing.Point(611, 369)
        Me.btnBuscarIsometrico.Name = "btnBuscarIsometrico"
        Me.btnBuscarIsometrico.Size = New System.Drawing.Size(40, 23)
        Me.btnBuscarIsometrico.TabIndex = 17
        Me.btnBuscarIsometrico.Text = "..."
        Me.btnBuscarIsometrico.UseVisualStyleBackColor = True
        '
        'btnBuscarFichaTecnica
        '
        Me.btnBuscarFichaTecnica.Location = New System.Drawing.Point(612, 301)
        Me.btnBuscarFichaTecnica.Name = "btnBuscarFichaTecnica"
        Me.btnBuscarFichaTecnica.Size = New System.Drawing.Size(40, 23)
        Me.btnBuscarFichaTecnica.TabIndex = 16
        Me.btnBuscarFichaTecnica.Text = "..."
        Me.btnBuscarFichaTecnica.UseVisualStyleBackColor = True
        '
        'lblEnderecoIsometrico
        '
        Me.lblEnderecoIsometrico.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblEnderecoIsometrico.BackColor = System.Drawing.SystemColors.Window
        Me.lblEnderecoIsometrico.Location = New System.Drawing.Point(8, 369)
        Me.lblEnderecoIsometrico.Name = "lblEnderecoIsometrico"
        Me.lblEnderecoIsometrico.Size = New System.Drawing.Size(596, 23)
        Me.lblEnderecoIsometrico.TabIndex = 15
        Me.lblEnderecoIsometrico.Text = "Buscar Ficha Tecnica:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(8, 342)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(117, 16)
        Me.Label15.TabIndex = 14
        Me.Label15.Text = "Buscar Isometrico:"
        '
        'lblEnderecoFichaTecnica
        '
        Me.lblEnderecoFichaTecnica.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblEnderecoFichaTecnica.BackColor = System.Drawing.SystemColors.Window
        Me.lblEnderecoFichaTecnica.Location = New System.Drawing.Point(8, 301)
        Me.lblEnderecoFichaTecnica.Name = "lblEnderecoFichaTecnica"
        Me.lblEnderecoFichaTecnica.Size = New System.Drawing.Size(596, 23)
        Me.lblEnderecoFichaTecnica.TabIndex = 13
        Me.lblEnderecoFichaTecnica.Text = "Buscar Ficha Tecnica:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(8, 274)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(140, 16)
        Me.Label12.TabIndex = 12
        Me.Label12.Text = "Buscar Ficha Tecnica:"
        '
        'txtDescricaoProduto
        '
        Me.txtDescricaoProduto.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDescricaoProduto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDescricaoProduto.Location = New System.Drawing.Point(6, 144)
        Me.txtDescricaoProduto.Multiline = True
        Me.txtDescricaoProduto.Name = "txtDescricaoProduto"
        Me.txtDescricaoProduto.Size = New System.Drawing.Size(645, 124)
        Me.txtDescricaoProduto.TabIndex = 9
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 125)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(140, 16)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Descrição do produto:"
        '
        'txtCodOmie
        '
        Me.txtCodOmie.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCodOmie.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCodOmie.Location = New System.Drawing.Point(6, 90)
        Me.txtCodOmie.Name = "txtCodOmie"
        Me.txtCodOmie.Size = New System.Drawing.Size(645, 22)
        Me.txtCodOmie.TabIndex = 7
        '
        'txCodDesenhoProduto
        '
        Me.txCodDesenhoProduto.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txCodDesenhoProduto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txCodDesenhoProduto.Location = New System.Drawing.Point(6, 46)
        Me.txCodDesenhoProduto.Name = "txCodDesenhoProduto"
        Me.txCodDesenhoProduto.Size = New System.Drawing.Size(645, 22)
        Me.txCodDesenhoProduto.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(5, 71)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(90, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Codigo OMIE:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(180, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Codigo Desenho do produto:"
        '
        'btnSalvar
        '
        Me.btnSalvar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSalvar.Image = Global.SwLynx_4._1.My.Resources.Resources.salvar
        Me.btnSalvar.Location = New System.Drawing.Point(18, 11)
        Me.btnSalvar.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnSalvar.Name = "btnSalvar"
        Me.btnSalvar.Size = New System.Drawing.Size(164, 51)
        Me.btnSalvar.TabIndex = 4
        Me.btnSalvar.Text = "Salvar/Sair"
        Me.btnSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSalvar.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Image = Global.SwLynx_4._1.My.Resources.Resources.cancelar
        Me.Button1.Location = New System.Drawing.Point(515, 11)
        Me.Button1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(164, 51)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Cancelar"
        Me.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frmCriaProdutos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(691, 506)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnSalvar)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmCriaProdutos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Criar Produtos"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents txtDescricaoProduto As Windows.Forms.TextBox
    Friend WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents txtCodOmie As Windows.Forms.TextBox
    Friend WithEvents txCodDesenhoProduto As Windows.Forms.TextBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents lblEnderecoFichaTecnica As Windows.Forms.Label
    Friend WithEvents Label12 As Windows.Forms.Label
    Friend WithEvents lblEnderecoIsometrico As Windows.Forms.Label
    Friend WithEvents Label15 As Windows.Forms.Label
    Friend WithEvents btnSalvar As Windows.Forms.Button
    Friend WithEvents Button1 As Windows.Forms.Button
    Friend WithEvents btnBuscarIsometrico As Windows.Forms.Button
    Friend WithEvents btnBuscarFichaTecnica As Windows.Forms.Button
End Class
