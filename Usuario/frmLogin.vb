Public Class frmLogin
    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Text = "Sistema SINCO - Lynx - Cliente: " & My.Settings.BancoDadosAtivo

        If My.Settings.UsuarioLogado <> "" Then

            Me.txtLogin.Text = My.Settings.UsuarioLogado
            Me.mskSenha.Text = My.Settings.SenhaUsuarioLogado
            Me.chkSalvarDadosEntrada.Checked = True

        Else

            Me.txtLogin.Text = Nothing
            Me.mskSenha.Text = Nothing
            Me.chkSalvarDadosEntrada.Checked = False

        End If

        dgvListaUsuario.DataSource = cl_BancoDados.CarregarDados("SELECT
    idUsuario, 
    NomeCompleto,
    Login,
    Senha,
    Sigla 
FROM
     " & ComplementoTipoBanco & " usuario
WHERE
    (D_E_L_E_T_E <> '*' OR D_E_L_E_T_E IS NULL)
    AND Login = Senha order by idUsuario")



    End Sub

    Private Sub btnEntrar_Click(sender As Object, e As EventArgs) Handles btnEntrar.Click

        Dim TotalUsuario As Integer
        'Verificar a quantide de usuarios cadastrado no sistema
        TotalUsuario = cl_BancoDados.RetornaCampoDaPesquisa("SELECT count(idUsuario) as qtde FROM  " & ComplementoTipoBanco & "usuario", "qtde")
        Usuario.RetornaDadosConfiguracao()

        Dim entrar As Boolean

        Try

            If Usuario.RetornaDadosUsuario(Me.txtLogin.Text, Me.mskSenha.Text) = True Then

                entrar = True

                If chkSalvarDadosEntrada.Checked = True Then

                    My.Settings.UsuarioLogado = Me.txtLogin.Text
                    My.Settings.SenhaUsuarioLogado = Me.mskSenha.Text
                    ' My.Settings.TipoUsuario = "A"
                Else

                    My.Settings.UsuarioLogado = Nothing
                    My.Settings.SenhaUsuarioLogado = Nothing
                    'My.Settings.TipoUsuario = Nothing

                End If

                Me.Hide()

                My.Settings.Save()
                '  BancoDados.FecharBanco()

                Me.Hide()

                ' F'ormulariofrmPrincial.ShowDialog()
            Else

                entrar = False

                MsgBox("Usuário ou senha inválidos!", vbInformation, "Falha ao entrar no sitema SINCO !!")

            End If
        Catch ex As Exception

            MsgBox("Usuário ou senha inválidos", vbInformation, "Falha ao entrar no sitema SINCO !!")

        Finally

        End Try

    End Sub

    Private Sub btnFechar_Click(sender As Object, e As EventArgs) Handles btnFechar.Click


        Me.Close()


    End Sub

    Private Sub chkMostrarSenha_CheckedChanged(sender As Object, e As EventArgs) Handles chkMostrarSenha.CheckedChanged

        If chkMostrarSenha.Checked = False Then

            mskSenha.PasswordChar = "********"
        Else

            mskSenha.PasswordChar = Nothing

        End If


    End Sub

    Private Sub chkSalvarDadosEntrada_CheckedChanged(sender As Object, e As EventArgs) Handles chkSalvarDadosEntrada.CheckedChanged


        If chkSalvarDadosEntrada.Checked = True Then

            My.Settings.UsuarioLogado = Me.txtLogin.Text
            My.Settings.SenhaUsuarioLogado = Me.mskSenha.Text
        Else

            My.Settings.UsuarioLogado = Nothing
            My.Settings.SenhaUsuarioLogado = Nothing

        End If

        My.Settings.Save()

    End Sub
End Class