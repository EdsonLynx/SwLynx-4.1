Imports System.Drawing
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient

Public Class frmRNC

    Dim DescricaoFinalizacao As String


    Private Sub TimerdgvDados_Tick(sender As Object, e As EventArgs) Handles TimerdgvDados.Tick

        Dim sql As String

        If chkConcluidas.Checked = False Then

            sql = " (estatus <> 'FINALIZADA' OR  estatus =  '' OR  estatus  IS NULL ) AND "

        ElseIf chkConcluidas.Checked = True Then


            sql = "" '" (estatus = 'FINALIZADA' OR  estatus =  '' OR  estatus  IS NULL ) AND "


        End If

        dgvDados.DataSource = cl_BancoDados.CarregarDados("SELECT idordemservicoitempendencia, IDOrdemServicoITEM, 
IDOrdemServico, CodMatFabricante, DescricaoPendencia,
estatus, UltimaPendencia FROM ordemservicoitempendencia
where " & sql & "codmatfabricante = '" & DadosArquivoCorrente.NomeArquivoSemExtensao & "'")

        TimerdgvDados.Enabled = False

    End Sub

    Private Sub frmRNC_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        TimerdgvDados.Enabled = True

    End Sub

    Private Sub btnFechar_Click(sender As Object, e As EventArgs) Handles btnFechar.Click
        Me.Close()

    End Sub

    Private Sub btnFinalizarRNC_Click(sender As Object, e As EventArgs) Handles btnFinalizarRNC.Click


        Dim resultado As DialogResult = MessageBox.Show("Tem certeza que deseja marcar as pendencias do desenho como finalizads na lista de pendências?", "Confirmação", MessageBoxButtons.YesNo)

        If resultado = DialogResult.No Then

            Exit Sub
        End If


        OrdemServicoItemPendencia.DescricaoFinalizacao = InputBox("Descrição da Finalização da RNC - Lista de Pendência:", "Descrição", "RNC - Lista de Pendência Finalizada").ToString.ToUpper

        If OrdemServicoItemPendencia.DescricaoFinalizacao.ToString <> "" Then


            ' Me.txtDescricaoPendencia.Clear()
            If dgvDados.Rows.Count > 0 Then

                For I As Integer = 0 To dgvDados.Rows.Count - 1

                    If dgvDados.Rows(I).Cells("dgvSelecao").Value = True And dgvDados.Rows(I).Cells("ESTATUS").Value.ToString <> "FINALIZADA" Then

                        If DadosArquivoCorrente.NomeArquivoSemExtensao = dgvDados.Rows(I).Cells("codmatfabricante").Value.ToString Then

                            ' dgvDados.Rows(I).Cells("DescricaoFinalizacao").Value = OrdemServicoItemPendencia.DescricaoFinalizacao
                            OrdemServicoItemPendencia.idordemservicoitempendencia = dgvDados.Rows(I).Cells("idordemservicoitempendencia").Value

                            UpdateDadosFinalizarPendencia()

                            dgvDados.Rows(I).Cells("dgvSelecao").Value = False
                            dgvDados.Rows(I).Cells("dgvIconeestatus").Value = My.Resources.verificado
                            'dgvDados.Rows(I).Cells("ESTATUS").Value = "FINALIZADA"

                        End If

                    End If

                Next

            End If

            DadosArquivoCorrente.rnc = cl_BancoDados.RetornaCampoDaPesquisa("SELECT count(idordemservicoitempendencia) as qtdernc 
                    FROM ordemservicoitempendencia where
                              (estatus <> 'FINALIZADA' OR  estatus =  '' OR  estatus  IS NULL) 
                    and codmatFabricante = '" & DadosArquivoCorrente.NomeArquivoSemExtensao & "';", "qtdernc")

            If DadosArquivoCorrente.rnc.ToString = "0" Or DadosArquivoCorrente.rnc.ToString = "" Or DadosArquivoCorrente.rnc = Nothing Then

                MyTaskPanelHost.btnPendencias.Image = My.Resources.verificado1
                cl_BancoDados.AlteracaoEspecifica("material", "RNC", "", "codmatfabricante", DadosArquivoCorrente.NomeArquivoSemExtensao)
                DadosArquivoCorrente.rnc = ""
                MyTaskPanelHost.btnPendencias.Enabled = False

            Else

                MyTaskPanelHost.btnPendencias.Image = My.Resources.atencao
                MsgBox("Há RNC em aberto para o desenho corrente", vbCritical, "Atenção")
                DadosArquivoCorrente.rnc = "1"
                MyTaskPanelHost.btnPendencias.Enabled = True

            End If

        Else

            MsgBox("A descrição da finalização da pendência e obrigatoria", vbCritical, "Atenção")


        End If

    End Sub

    Public Sub UpdateDadosFinalizarPendencia()

        Try

            Dim query As String = "UPDATE ordemservicoitempendencia SET
                              UsuarioProjeto = @UsuarioProjeto,
                              DataAcertoProjeto = @DataAcertoProjeto,
                              DescricaoFinalizacao = @DescricaoFinalizacao,
                              estatus = @estatus
                              WHERE idordemservicoitempendencia = @idordemservicoitempendencia"

            If myconect.State = myconect.State.Closed Then
                myconect.Open()
            End If

            Using cmd As New MySqlCommand(query, myconect)
                ' Defina os valores para os parâmetros
                cmd.Parameters.AddWithValue("@UsuarioProjeto", Usuario.NomeCompleto)
                cmd.Parameters.AddWithValue("@DataAcertoProjeto", Date.Now.Date)
                cmd.Parameters.AddWithValue("@estatus", "FINALIZADA")
                cmd.Parameters.AddWithValue("@DescricaoFinalizacao", OrdemServicoItemPendencia.DescricaoFinalizacao)
                cmd.Parameters.AddWithValue("@idordemservicoitempendencia", OrdemServicoItemPendencia.idordemservicoitempendencia)

                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            ' Tratar exceções aqui conforme necessário
        End Try
    End Sub

    Private Sub dgvDados_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvDados.DataBindingComplete

        Try

            For I As Integer = 0 To dgvDados.Rows.Count - 1

                If dgvDados.Rows(I).Cells("estatus").Value.ToString = "FINALIZADA" Then

                    dgvDados.Rows(I).Cells("dgvIconeestatus").Value = My.Resources.verificado

                Else

                    dgvDados.Rows(I).Cells("dgvIconeestatus").Value = My.Resources.atencao

                End If

            Next

        Catch ex As Exception

        Finally

        End Try

    End Sub

    Private Sub frmRNC_Closed(sender As Object, e As EventArgs) Handles Me.Closed

        If DadosArquivoCorrente.rnc = "" Then

            MyTaskPanelHost.btnPendencias.Enabled = False
            MyTaskPanelHost.btnPendencias.Image = My.Resources.atencao
            MyTaskPanelHost.btnPendencias.Refresh()

        End If


    End Sub

    Private Sub chkConcluidas_CheckedChanged(sender As Object, e As EventArgs) Handles chkConcluidas.CheckedChanged

        TimerdgvDados.Enabled = True

    End Sub
End Class