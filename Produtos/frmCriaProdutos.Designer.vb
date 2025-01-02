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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblAlturaProduto = New System.Windows.Forms.Label()
        Me.lblLarguraProduto = New System.Windows.Forms.Label()
        Me.lblComprimentoProduto = New System.Windows.Forms.Label()
        Me.lblPesoProduto = New System.Windows.Forms.Label()
        Me.txtCodMatFabricante = New System.Windows.Forms.TextBox()
        Me.txtIdOmie = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtDescricaoProduto = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lblAlrguraEmbalagem = New System.Windows.Forms.Label()
        Me.lblAlturaEmbalagem = New System.Windows.Forms.Label()
        Me.lblPesoEmbalagem = New System.Windows.Forms.Label()
        Me.lblComprimentoEmbalagem = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblFichaTecnica = New System.Windows.Forms.Label()
        Me.lblIsometrico = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.lblIsometrico)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.lblFichaTecnica)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.txtDescricaoProduto)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtIdOmie)
        Me.GroupBox1.Controls.Add(Me.txtCodMatFabricante)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 49)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(355, 601)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Dados do Produto"
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
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(5, 71)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(90, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Codigo OMIE:"
        '
        'lblAlturaProduto
        '
        Me.lblAlturaProduto.AutoSize = True
        Me.lblAlturaProduto.BackColor = System.Drawing.SystemColors.Window
        Me.lblAlturaProduto.Location = New System.Drawing.Point(79, 22)
        Me.lblAlturaProduto.Name = "lblAlturaProduto"
        Me.lblAlturaProduto.Size = New System.Drawing.Size(44, 16)
        Me.lblAlturaProduto.TabIndex = 2
        Me.lblAlturaProduto.Text = "Altura:"
        '
        'lblLarguraProduto
        '
        Me.lblLarguraProduto.AutoSize = True
        Me.lblLarguraProduto.BackColor = System.Drawing.SystemColors.Window
        Me.lblLarguraProduto.Location = New System.Drawing.Point(79, 47)
        Me.lblLarguraProduto.Name = "lblLarguraProduto"
        Me.lblLarguraProduto.Size = New System.Drawing.Size(56, 16)
        Me.lblLarguraProduto.TabIndex = 3
        Me.lblLarguraProduto.Text = "Largura:"
        '
        'lblComprimentoProduto
        '
        Me.lblComprimentoProduto.AutoSize = True
        Me.lblComprimentoProduto.BackColor = System.Drawing.SystemColors.Window
        Me.lblComprimentoProduto.Location = New System.Drawing.Point(79, 72)
        Me.lblComprimentoProduto.Name = "lblComprimentoProduto"
        Me.lblComprimentoProduto.Size = New System.Drawing.Size(49, 16)
        Me.lblComprimentoProduto.TabIndex = 4
        Me.lblComprimentoProduto.Text = "Comp.:"
        '
        'lblPesoProduto
        '
        Me.lblPesoProduto.AutoSize = True
        Me.lblPesoProduto.BackColor = System.Drawing.SystemColors.Window
        Me.lblPesoProduto.Location = New System.Drawing.Point(79, 97)
        Me.lblPesoProduto.Name = "lblPesoProduto"
        Me.lblPesoProduto.Size = New System.Drawing.Size(42, 16)
        Me.lblPesoProduto.TabIndex = 5
        Me.lblPesoProduto.Text = "Peso:"
        '
        'txtCodMatFabricante
        '
        Me.txtCodMatFabricante.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCodMatFabricante.Location = New System.Drawing.Point(6, 46)
        Me.txtCodMatFabricante.Name = "txtCodMatFabricante"
        Me.txtCodMatFabricante.Size = New System.Drawing.Size(333, 22)
        Me.txtCodMatFabricante.TabIndex = 6
        '
        'txtIdOmie
        '
        Me.txtIdOmie.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtIdOmie.Location = New System.Drawing.Point(6, 90)
        Me.txtIdOmie.Name = "txtIdOmie"
        Me.txtIdOmie.Size = New System.Drawing.Size(333, 22)
        Me.txtIdOmie.TabIndex = 7
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
        'txtDescricaoProduto
        '
        Me.txtDescricaoProduto.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDescricaoProduto.Location = New System.Drawing.Point(6, 144)
        Me.txtDescricaoProduto.Multiline = True
        Me.txtDescricaoProduto.Name = "txtDescricaoProduto"
        Me.txtDescricaoProduto.Size = New System.Drawing.Size(333, 124)
        Me.txtDescricaoProduto.TabIndex = 9
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.lblLarguraProduto)
        Me.GroupBox2.Controls.Add(Me.lblAlturaProduto)
        Me.GroupBox2.Controls.Add(Me.lblPesoProduto)
        Me.GroupBox2.Controls.Add(Me.lblComprimentoProduto)
        Me.GroupBox2.Location = New System.Drawing.Point(11, 280)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(155, 125)
        Me.GroupBox2.TabIndex = 10
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Dados do Produto:"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.lblAlrguraEmbalagem)
        Me.GroupBox3.Controls.Add(Me.lblAlturaEmbalagem)
        Me.GroupBox3.Controls.Add(Me.lblPesoEmbalagem)
        Me.GroupBox3.Controls.Add(Me.lblComprimentoEmbalagem)
        Me.GroupBox3.Location = New System.Drawing.Point(184, 280)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(155, 125)
        Me.GroupBox3.TabIndex = 11
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Dados  Expedição:"
        '
        'lblAlrguraEmbalagem
        '
        Me.lblAlrguraEmbalagem.AutoSize = True
        Me.lblAlrguraEmbalagem.BackColor = System.Drawing.SystemColors.Window
        Me.lblAlrguraEmbalagem.Location = New System.Drawing.Point(68, 47)
        Me.lblAlrguraEmbalagem.Name = "lblAlrguraEmbalagem"
        Me.lblAlrguraEmbalagem.Size = New System.Drawing.Size(56, 16)
        Me.lblAlrguraEmbalagem.TabIndex = 3
        Me.lblAlrguraEmbalagem.Text = "Largura:"
        '
        'lblAlturaEmbalagem
        '
        Me.lblAlturaEmbalagem.AutoSize = True
        Me.lblAlturaEmbalagem.BackColor = System.Drawing.SystemColors.Window
        Me.lblAlturaEmbalagem.Location = New System.Drawing.Point(68, 22)
        Me.lblAlturaEmbalagem.Name = "lblAlturaEmbalagem"
        Me.lblAlturaEmbalagem.Size = New System.Drawing.Size(44, 16)
        Me.lblAlturaEmbalagem.TabIndex = 2
        Me.lblAlturaEmbalagem.Text = "Altura:"
        '
        'lblPesoEmbalagem
        '
        Me.lblPesoEmbalagem.AutoSize = True
        Me.lblPesoEmbalagem.BackColor = System.Drawing.SystemColors.Window
        Me.lblPesoEmbalagem.Location = New System.Drawing.Point(68, 97)
        Me.lblPesoEmbalagem.Name = "lblPesoEmbalagem"
        Me.lblPesoEmbalagem.Size = New System.Drawing.Size(42, 16)
        Me.lblPesoEmbalagem.TabIndex = 5
        Me.lblPesoEmbalagem.Text = "Peso:"
        '
        'lblComprimentoEmbalagem
        '
        Me.lblComprimentoEmbalagem.AutoSize = True
        Me.lblComprimentoEmbalagem.BackColor = System.Drawing.SystemColors.Window
        Me.lblComprimentoEmbalagem.Location = New System.Drawing.Point(68, 72)
        Me.lblComprimentoEmbalagem.Name = "lblComprimentoEmbalagem"
        Me.lblComprimentoEmbalagem.Size = New System.Drawing.Size(49, 16)
        Me.lblComprimentoEmbalagem.TabIndex = 4
        Me.lblComprimentoEmbalagem.Text = "Comp.:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(8, 417)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(140, 16)
        Me.Label12.TabIndex = 12
        Me.Label12.Text = "Buscar Ficha Tecnica:"
        '
        'lblFichaTecnica
        '
        Me.lblFichaTecnica.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFichaTecnica.BackColor = System.Drawing.SystemColors.Window
        Me.lblFichaTecnica.Location = New System.Drawing.Point(8, 444)
        Me.lblFichaTecnica.Name = "lblFichaTecnica"
        Me.lblFichaTecnica.Size = New System.Drawing.Size(331, 23)
        Me.lblFichaTecnica.TabIndex = 13
        Me.lblFichaTecnica.Text = "Buscar Ficha Tecnica:"
        '
        'lblIsometrico
        '
        Me.lblIsometrico.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblIsometrico.BackColor = System.Drawing.SystemColors.Window
        Me.lblIsometrico.Location = New System.Drawing.Point(8, 506)
        Me.lblIsometrico.Name = "lblIsometrico"
        Me.lblIsometrico.Size = New System.Drawing.Size(331, 23)
        Me.lblIsometrico.TabIndex = 15
        Me.lblIsometrico.Text = "Buscar Ficha Tecnica:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(8, 479)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(117, 16)
        Me.Label15.TabIndex = 14
        Me.Label15.Text = "Buscar Isometrico:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(17, 47)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 16)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Largura:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(29, 22)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(44, 16)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Altura:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(31, 97)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(42, 16)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Peso:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(24, 72)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(49, 16)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Comp.:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 47)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(56, 16)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Largura:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(18, 22)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(44, 16)
        Me.Label9.TabIndex = 6
        Me.Label9.Text = "Altura:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(20, 97)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(42, 16)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "Peso:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(13, 72)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(49, 16)
        Me.Label11.TabIndex = 8
        Me.Label11.Text = "Comp.:"
        '
        'frmCriaProdutos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1159, 658)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmCriaProdutos"
        Me.Text = "Criar Produtos"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents txtDescricaoProduto As Windows.Forms.TextBox
    Friend WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents txtIdOmie As Windows.Forms.TextBox
    Friend WithEvents txtCodMatFabricante As Windows.Forms.TextBox
    Friend WithEvents lblPesoProduto As Windows.Forms.Label
    Friend WithEvents lblComprimentoProduto As Windows.Forms.Label
    Friend WithEvents lblLarguraProduto As Windows.Forms.Label
    Friend WithEvents lblAlturaProduto As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents GroupBox3 As Windows.Forms.GroupBox
    Friend WithEvents lblAlrguraEmbalagem As Windows.Forms.Label
    Friend WithEvents lblAlturaEmbalagem As Windows.Forms.Label
    Friend WithEvents lblPesoEmbalagem As Windows.Forms.Label
    Friend WithEvents lblComprimentoEmbalagem As Windows.Forms.Label
    Friend WithEvents GroupBox2 As Windows.Forms.GroupBox
    Friend WithEvents lblFichaTecnica As Windows.Forms.Label
    Friend WithEvents Label12 As Windows.Forms.Label
    Friend WithEvents lblIsometrico As Windows.Forms.Label
    Friend WithEvents Label15 As Windows.Forms.Label
    Friend WithEvents Label8 As Windows.Forms.Label
    Friend WithEvents Label9 As Windows.Forms.Label
    Friend WithEvents Label10 As Windows.Forms.Label
    Friend WithEvents Label11 As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents Label6 As Windows.Forms.Label
End Class
