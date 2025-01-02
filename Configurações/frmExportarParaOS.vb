Imports System.Windows.Forms

Public Class frmExportarParaOS
    Private Sub frmExportarParaOS_Load(sender As Object, e As EventArgs) Handles MyBase.Load




        If My.Settings.ParametroExportarDXF = "1" Then

            optParametroExportarDXF1.Checked = True

        ElseIf My.Settings.ParametroExportarDXF = "2" Then

            optParametroExportarDXF2.Checked = True

        End If


        If My.Settings.LiberaOSsemMaterial = "SIM" Then

            optLiberaSemMaterial.Checked = True
            optNaoLiberaSemMaterial.Checked = False

        ElseIf My.Settings.LiberaOSsemMaterial = "NÃO" Then

            optNaoLiberaSemMaterial.Checked = True
            optLiberaSemMaterial.Checked = False

        End If




        If My.Settings.CaixaDelimitadora = "SIM" Then

            chkCaixaDelimitadora.Checked = True

        ElseIf My.Settings.CaixaDelimitadora = "NÃO" Then


            chkCaixaDelimitadora.Checked = False


        End If



    End Sub

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click

        'bloco exportar DXF
        If optParametroExportarDXF1.Checked = True Then

            My.Settings.ParametroExportarDXF = "1"

            My.Settings.Save()

        ElseIf optParametroExportarDXF2.Checked = True Then

            My.Settings.ParametroExportarDXF = "2"

            My.Settings.Save()

        End If


        If optLiberaSemMaterial.Checked = True Then

            My.Settings.LiberaOSsemMaterial = "SIM"

            My.Settings.Save()

        ElseIf optNaoLiberaSemMaterial.Checked = True Then

            My.Settings.LiberaOSsemMaterial = "NÃO"

            My.Settings.Save()

        End If



        If chkCaixaDelimitadora.Checked = True Then

            My.Settings.CaixaDelimitadora = "SIM"
            ' Salva as configurações
            My.Settings.Save()

        ElseIf chkCaixaDelimitadora.Checked = False Then

            My.Settings.CaixaDelimitadora = "NÃO"
            ' Salva as configurações
            My.Settings.Save()


        End If


        Me.Close()

    End Sub

    Private Sub optLiberaSemMaterial_CheckedChanged(sender As Object, e As EventArgs) Handles optLiberaSemMaterial.CheckedChanged

        If optLiberaSemMaterial.Checked = True Then
            My.Settings.LiberaOSsemMaterial = "SIM"
        End If

        My.Settings.Save()

    End Sub

    Private Sub optNaoLiberaSemMaterial_CheckedChanged(sender As Object, e As EventArgs) Handles optNaoLiberaSemMaterial.CheckedChanged

        If optNaoLiberaSemMaterial.Checked = True Then
            My.Settings.LiberaOSsemMaterial = "NÃO"
        End If

        My.Settings.Save()


    End Sub

    Private Sub btnSair_Click(sender As Object, e As EventArgs)
        Me.Close()

    End Sub

    Private Sub optParametroExportarDXF1_CheckedChanged(sender As Object, e As EventArgs) Handles optParametroExportarDXF1.CheckedChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        ' Configura o filtro para apenas arquivos com extensão .slddrt
        OpenFileDialog1.Filter = "SolidWorks Drawing Templates A3 (*.slddrt)|*.slddrt"
        ' Mostra o OpenFileDialog
        If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            My.Settings.EnderecoNovoFormatoA3 = OpenFileDialog1.FileName

            ' Salva as configurações
            My.Settings.Save()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        ' Configura o filtro para apenas arquivos com extensão .slddrt
        OpenFileDialog1.Filter = "SolidWorks Drawing Templates A4 (*.slddrt)|*.slddrt"

        ' Mostra o OpenFileDialog
        If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            My.Settings.EnderecoNovoFormatoA4 = OpenFileDialog1.FileName

            ' Salva as configurações
            My.Settings.Save()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        ' Configura o filtro para apenas arquivos com extensão .slddrt
        OpenFileDialog1.Filter = "SolidWorks Drawing Templates A4 (*.slddrt)|*.slddrt"

        ' Mostra o OpenFileDialog
        If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            My.Settings.EnderecoNovoFormatoA4Deitado = OpenFileDialog1.FileName

            ' Salva as configurações
            My.Settings.Save()
        End If
    End Sub

    Private Sub chkCaixaDelimitadora_CheckedChanged(sender As Object, e As EventArgs) Handles chkCaixaDelimitadora.CheckedChanged

        If chkCaixaDelimitadora.Checked = True Then

            My.Settings.CaixaDelimitadora = "SIM"
            ' Salva as configurações
            My.Settings.Save()

        ElseIf chkCaixaDelimitadora.Checked = False Then

            My.Settings.CaixaDelimitadora = "NÃO"
            ' Salva as configurações
            My.Settings.Save()


        End If

    End Sub

End Class