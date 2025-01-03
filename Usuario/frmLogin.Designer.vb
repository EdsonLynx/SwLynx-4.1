<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLogin
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLogin))
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkSalvarDadosEntrada = New System.Windows.Forms.CheckBox()
        Me.chkMostrarSenha = New System.Windows.Forms.CheckBox()
        Me.btnEntrar = New System.Windows.Forms.Button()
        Me.btnFechar = New System.Windows.Forms.Button()
        Me.mskSenha = New System.Windows.Forms.MaskedTextBox()
        Me.txtLogin = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.dgvListaUsuario = New System.Windows.Forms.DataGridView()
        Me.cboTipoBanco = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvListaUsuario, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Image = CType(resources.GetObject("Label2.Image"), System.Drawing.Image)
        Me.Label2.Location = New System.Drawing.Point(132, 72)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 66)
        Me.Label2.TabIndex = 10083
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(4, 20)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(124, 157)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 10082
        Me.PictureBox1.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(201, 73)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(94, 13)
        Me.Label4.TabIndex = 10081
        Me.Label4.Text = "Senha de Acesso:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(201, 15)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(90, 13)
        Me.Label3.TabIndex = 10080
        Me.Label3.Text = "Login do Usuário:"
        '
        'chkSalvarDadosEntrada
        '
        Me.chkSalvarDadosEntrada.AutoSize = True
        Me.chkSalvarDadosEntrada.BackColor = System.Drawing.Color.Transparent
        Me.chkSalvarDadosEntrada.Location = New System.Drawing.Point(200, 127)
        Me.chkSalvarDadosEntrada.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.chkSalvarDadosEntrada.Name = "chkSalvarDadosEntrada"
        Me.chkSalvarDadosEntrada.Size = New System.Drawing.Size(90, 17)
        Me.chkSalvarDadosEntrada.TabIndex = 10079
        Me.chkSalvarDadosEntrada.Text = "Salvar Dados"
        Me.chkSalvarDadosEntrada.UseVisualStyleBackColor = False
        '
        'chkMostrarSenha
        '
        Me.chkMostrarSenha.AutoSize = True
        Me.chkMostrarSenha.BackColor = System.Drawing.Color.Transparent
        Me.chkMostrarSenha.Location = New System.Drawing.Point(317, 127)
        Me.chkMostrarSenha.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.chkMostrarSenha.Name = "chkMostrarSenha"
        Me.chkMostrarSenha.Size = New System.Drawing.Size(92, 17)
        Me.chkMostrarSenha.TabIndex = 10078
        Me.chkMostrarSenha.Text = "Mostra Senha"
        Me.chkMostrarSenha.UseVisualStyleBackColor = False
        '
        'btnEntrar
        '
        Me.btnEntrar.Location = New System.Drawing.Point(134, 149)
        Me.btnEntrar.Name = "btnEntrar"
        Me.btnEntrar.Size = New System.Drawing.Size(116, 32)
        Me.btnEntrar.TabIndex = 10077
        Me.btnEntrar.Text = "Entrar"
        Me.btnEntrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnEntrar.UseVisualStyleBackColor = True
        '
        'btnFechar
        '
        Me.btnFechar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnFechar.Location = New System.Drawing.Point(297, 149)
        Me.btnFechar.Name = "btnFechar"
        Me.btnFechar.Size = New System.Drawing.Size(116, 32)
        Me.btnFechar.TabIndex = 10076
        Me.btnFechar.Text = "Fechar"
        Me.btnFechar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnFechar.UseVisualStyleBackColor = True
        '
        'mskSenha
        '
        Me.mskSenha.Location = New System.Drawing.Point(200, 94)
        Me.mskSenha.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.mskSenha.Name = "mskSenha"
        Me.mskSenha.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.mskSenha.Size = New System.Drawing.Size(213, 20)
        Me.mskSenha.TabIndex = 10075
        '
        'txtLogin
        '
        Me.txtLogin.Location = New System.Drawing.Point(200, 37)
        Me.txtLogin.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.txtLogin.Name = "txtLogin"
        Me.txtLogin.Size = New System.Drawing.Size(213, 20)
        Me.txtLogin.TabIndex = 10074
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Image = CType(resources.GetObject("Label1.Image"), System.Drawing.Image)
        Me.Label1.Location = New System.Drawing.Point(132, 2)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 66)
        Me.Label1.TabIndex = 10073
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.dgvListaUsuario)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(4, 242)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox1.Size = New System.Drawing.Size(411, 0)
        Me.GroupBox1.TabIndex = 10084
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Lista de Usuarios ↑↑ ↓↓"
        '
        'dgvListaUsuario
        '
        Me.dgvListaUsuario.AllowUserToAddRows = False
        Me.dgvListaUsuario.AllowUserToDeleteRows = False
        Me.dgvListaUsuario.AllowUserToOrderColumns = True
        Me.dgvListaUsuario.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader
        Me.dgvListaUsuario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvListaUsuario.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvListaUsuario.Location = New System.Drawing.Point(2, 14)
        Me.dgvListaUsuario.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.dgvListaUsuario.Name = "dgvListaUsuario"
        Me.dgvListaUsuario.ReadOnly = True
        Me.dgvListaUsuario.RowHeadersWidth = 51
        Me.dgvListaUsuario.RowTemplate.Height = 24
        Me.dgvListaUsuario.Size = New System.Drawing.Size(407, 0)
        Me.dgvListaUsuario.TabIndex = 0
        '
        'cboTipoBanco
        '
        Me.cboTipoBanco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTipoBanco.FormattingEnabled = True
        Me.cboTipoBanco.Items.AddRange(New Object() {"MYSQL", "SQL", "ACCESS"})
        Me.cboTipoBanco.Location = New System.Drawing.Point(5, 216)
        Me.cboTipoBanco.Name = "cboTipoBanco"
        Me.cboTipoBanco.Size = New System.Drawing.Size(352, 21)
        Me.cboTipoBanco.TabIndex = 10085
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 199)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(179, 13)
        Me.Label5.TabIndex = 10086
        Me.Label5.Text = "Selecionate o Tipo Banco de Dados"
        '
        'frmLogin
        '
        Me.AcceptButton = Me.btnEntrar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SwLynx_4._1.My.Resources.Resources.EYE_SNOW
        Me.CancelButton = Me.btnFechar
        Me.ClientSize = New System.Drawing.Size(423, 188)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cboTipoBanco)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.chkSalvarDadosEntrada)
        Me.Controls.Add(Me.chkMostrarSenha)
        Me.Controls.Add(Me.btnEntrar)
        Me.Controls.Add(Me.btnFechar)
        Me.Controls.Add(Me.mskSenha)
        Me.Controls.Add(Me.txtLogin)
        Me.Controls.Add(Me.Label1)
        Me.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "frmLogin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Login"
        Me.TopMost = True
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.dgvListaUsuario, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents PictureBox1 As Windows.Forms.PictureBox
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents chkSalvarDadosEntrada As Windows.Forms.CheckBox
    Friend WithEvents chkMostrarSenha As Windows.Forms.CheckBox
    Friend WithEvents btnEntrar As Windows.Forms.Button
    Friend WithEvents btnFechar As Windows.Forms.Button
    Friend WithEvents mskSenha As Windows.Forms.MaskedTextBox
    Friend WithEvents txtLogin As Windows.Forms.TextBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents dgvListaUsuario As Windows.Forms.DataGridView
    Friend WithEvents cboTipoBanco As Windows.Forms.ComboBox
    Friend WithEvents Label5 As Windows.Forms.Label
End Class
