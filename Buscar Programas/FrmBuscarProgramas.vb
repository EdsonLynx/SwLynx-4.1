Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms

Public Class FrmBuscarProgramas
    Private Sub TimerdgvDesenhosCadastrados_Tick(sender As Object, e As EventArgs) Handles TimerdgvDesenhosCadastrados.Tick

        dgvDesenhosCadastrados.DataSource = cl_BancoDados.CarregarDados("SELECT RNC,
        CodMatFabricante, 
        EnderecoArquivo
        FROM  " & ComplementoTipoBanco & "material where PecaManuFat = 'S' and (d_e_l_e_t_e <> '*' or d_e_l_e_t_e is null)
        and (EnderecoArquivo like '%.SLDPRT%' or EnderecoArquivo like '%.SLDASM%')
        and EnderecoArquivo like '%" & txtPesqEnderecoArquivo.Text & "%'")

        TimerdgvDesenhosCadastrados.Enabled = False

    End Sub

    Private Sub FrmBuscarProgramas_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        TimerdgvDesenhosCadastrados.Enabled = True

    End Sub

    Private Sub btnBuscarDFT_Click(sender As Object, e As EventArgs) Handles btnBuscarDFT.Click


        Dim pastaBuscaDFT As String = ""

        ' Seleção da pasta usando OpenFileDialog
        Using openFileDialog As New OpenFileDialog
            openFileDialog.CheckFileExists = False
            openFileDialog.CheckPathExists = True
            openFileDialog.ValidateNames = False
            openFileDialog.FileName = "Selecione uma pasta"

            If openFileDialog.ShowDialog() = DialogResult.OK Then
                ' Remove o nome fictício "Selecione uma pasta" para obter o caminho correto
                pastaBuscaDFT = IO.Path.GetDirectoryName(openFileDialog.FileName)
            Else
                MessageBox.Show("Nenhuma pasta foi selecionada. Operação cancelada.")
                Exit Sub
            End If
        End Using

        ' Configuração da barra de progresso
        ProgressBarBuscarArquivos.Maximum = dgvDesenhosCadastrados.Rows.Count
        ProgressBarBuscarArquivos.Minimum = 0
        ProgressBarBuscarArquivos.Value = 0

        ' Itera por cada linha no DataGridView
        For Each row As DataGridViewRow In dgvDesenhosCadastrados.Rows
            Try
                ' Verifica se a linha é válida
                If Not row.IsNewRow Then
                    ' Obtém o caminho completo do arquivo original
                    Dim caminhoArquivoOrigem As String = row.Cells("EnderecoArquivo").Value.ToString()

                    ' Obtém o nome do arquivo sem a extensão
                    Dim nomeArquivoSemExtensao As String = Path.GetFileNameWithoutExtension(caminhoArquivoOrigem)

                    ' Busca arquivos .dft no diretório e subdiretórios
                    Dim arquivosDFT() As String = Directory.GetFiles(
                pastaBuscaDFT, nomeArquivoSemExtensao & ".dft", SearchOption.AllDirectories
            )

                    ' Verifica se algum arquivo foi encontrado
                    If arquivosDFT.Length > 0 Then
                        ' Pega o primeiro arquivo encontrado
                        Dim caminhoArquivoDFT As String = arquivosDFT(0)

                        ' Define o caminho de destino
                        Dim caminhoArquivoDestino As String = Path.Combine(
                    Path.GetDirectoryName(caminhoArquivoOrigem), Path.GetFileName(caminhoArquivoDFT)
                )

                        ' Copia o arquivo encontrado para o destino
                        File.Copy(caminhoArquivoDFT, caminhoArquivoDestino, True)

                        ' Atualiza o status no DataGridView
                        row.Cells("dgvdft").Value = My.Resources.DFT
                        row.DefaultCellStyle.BackColor = Color.LightGreen
                    Else
                        ' Indica que o arquivo não foi encontrado
                        row.Cells("dgvdft").Value = My.Resources.Sem_Incone
                        row.DefaultCellStyle.BackColor = Color.White
                    End If
                End If

                ' Atualiza a barra de progresso
                ProgressBarBuscarArquivos.Value = row.Index + 1

            Catch ex As Exception
                ' Trata exceções e continua o loop
                row.DefaultCellStyle.BackColor = Color.LightCoral
                Continue For
            End Try
        Next

        ' Finaliza a barra de progresso e exibe mensagem
        ProgressBarBuscarArquivos.Value = 0
        MessageBox.Show("Operação concluída.")



    End Sub

    Private Sub btnBuscarLXDS_Click(sender As Object, e As EventArgs) Handles btnBuscarLXDS.Click


        Dim pastaBuscaLXDS As String = ""

        ' Seleção da pasta
        Using openFileDialog As New OpenFileDialog
            openFileDialog.CheckFileExists = False
            openFileDialog.CheckPathExists = True
            openFileDialog.ValidateNames = False
            openFileDialog.FileName = "Selecione uma pasta"

            If openFileDialog.ShowDialog() = DialogResult.OK Then
                ' Remove o nome fictício "Selecione uma pasta" para obter o caminho correto
                pastaBuscaLXDS = IO.Path.GetDirectoryName(openFileDialog.FileName)
            Else
                MessageBox.Show("Nenhuma pasta foi selecionada. Operação cancelada.")
                Exit Sub
            End If
        End Using

        ' Configuração da barra de progresso
        ProgressBarBuscarArquivos.Maximum = dgvDesenhosCadastrados.Rows.Count
        ProgressBarBuscarArquivos.Minimum = 0
        ProgressBarBuscarArquivos.Value = 0

        ' Itera por cada linha no DataGridView
        For Each row As DataGridViewRow In dgvDesenhosCadastrados.Rows
            Try
                ' Verifica se a linha é válida
                If Not row.IsNewRow Then
                    ' Obtém o caminho do arquivo original
                    Dim caminhoArquivoOrigem As String = row.Cells("EnderecoArquivo").Value.ToString()

                    ' Obtém o nome do arquivo sem a extensão
                    Dim nomeArquivoSemExtensao As String = Path.GetFileNameWithoutExtension(caminhoArquivoOrigem)

                    ' Busca por arquivos .LXDS no diretório e subdiretórios
                    Dim arquivosLXDS() As String = Directory.GetFiles(pastaBuscaLXDS, nomeArquivoSemExtensao & ".LXDS", SearchOption.AllDirectories)

                    ' Verifica se algum arquivo foi encontrado
                    If arquivosLXDS.Length > 0 Then
                        ' Pega o primeiro arquivo encontrado
                        Dim caminhoArquivoLXDS As String = arquivosLXDS(0)

                        ' Define o caminho de destino
                        Dim caminhoArquivoDestino As String = Path.Combine(Path.GetDirectoryName(caminhoArquivoOrigem), Path.GetFileName(caminhoArquivoLXDS))

                        ' Copia o arquivo encontrado para o destino
                        File.Copy(caminhoArquivoLXDS, caminhoArquivoDestino, True)

                        ' Atualiza o status no DataGridView
                        row.Cells("dgvLXDS").Value = My.Resources.CYPCUT
                        row.DefaultCellStyle.BackColor = Color.LightGreen
                    Else
                        ' Indica que o arquivo não foi encontrado
                        row.Cells("dgvLXDS").Value = My.Resources.Sem_Incone
                        row.DefaultCellStyle.BackColor = Color.White
                    End If
                End If

                ' Atualiza a barra de progresso
                ProgressBarBuscarArquivos.Value = row.Index + 1

            Catch ex As Exception
                ' Trata exceções e continua o loop
                row.DefaultCellStyle.BackColor = Color.LightCoral
                Continue For
            End Try
        Next

        ' Finaliza a barra de progresso e exibe mensagem
        ProgressBarBuscarArquivos.Value = 0
        MessageBox.Show("Operação concluída.")

    End Sub

    Private Sub AbrirPDFDaLinhaSelecionadaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbrirPDFDaLinhaSelecionadaToolStripMenuItem.Click

        Try

            Dim ArquivoPdf As String = dgvDesenhosCadastrados.CurrentRow.Cells("EnderecoArquivo").Value.ToString()

            ' Substitui extensões ".SLDASM" e ".SLDPRT" por ".DDF"
            ArquivoPdf = Path.ChangeExtension(ArquivoPdf, ".PDF")

            ' Obtém o caminho completo
            ArquivoPdf = Path.GetFullPath(ArquivoPdf)

            ' Verifica se o arquivo existe e o abre
            If File.Exists(ArquivoPdf) Then
                Using p As New Diagnostics.Process
                    p.StartInfo = New ProcessStartInfo(ArquivoPdf)

                    p.Start()
                    p.WaitForExit()

                    dgvDesenhosCadastrados.CurrentRow.DefaultCellStyle.BackColor = Color.LightCyan
                End Using
            End If
        Catch ex As Exception
            MsgBox("Arquivo não encontrado!", vbCritical, "Atenção")
        Finally

        End Try
    End Sub

    Private Sub AbrirDXFDaLinhaSelecionadaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbrirDXFDaLinhaSelecionadaToolStripMenuItem.Click

        Try

            Dim ArquivoDXF As String = dgvDesenhosCadastrados.CurrentRow.Cells("EnderecoArquivo").Value.ToString()

            ' Substitui extensões ".SLDASM" e ".SLDPRT" por ".DDF"
            ArquivoDXF = Path.ChangeExtension(ArquivoDXF, ".DXF")

            ' Obtém o caminho completo
            ArquivoDXF = Path.GetFullPath(ArquivoDXF)
            ' Verifica se o arquivo existe e o abre
            If File.Exists(ArquivoDXF) Then
                Using p As New Diagnostics.Process
                    p.StartInfo = New ProcessStartInfo(ArquivoDXF)

                    p.Start()
                    p.WaitForExit()

                    dgvDesenhosCadastrados.CurrentRow.DefaultCellStyle.BackColor = Color.LightCyan
                End Using
            End If
        Catch ex As Exception
            MsgBox("Arquivo não encontrado!", vbCritical, "Atenção")
        Finally

        End Try

    End Sub

    Private Sub Abrir3DDaLinhaSelecionadaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Abrir3DDaLinhaSelecionadaToolStripMenuItem.Click

        Try

            Dim ArquivoSW As String = dgvDesenhosCadastrados.CurrentRow.Cells("EnderecoArquivo").Value.ToString()

            ' Substitui extensões ".SLDASM" e ".SLDPRT" por ".DDF"
            ArquivoSW = Path.ChangeExtension(ArquivoSW, ".SLDPRT")

            ' Obtém o caminho completo
            ArquivoSW = Path.GetFullPath(ArquivoSW)
            ' Verifica se o arquivo existe e o abre
            If File.Exists(ArquivoSW) Then
                Using p As New Diagnostics.Process
                    p.StartInfo = New ProcessStartInfo(ArquivoSW)

                    p.Start()
                    p.WaitForExit()

                    dgvDesenhosCadastrados.CurrentRow.DefaultCellStyle.BackColor = Color.LightCyan
                End Using

            Else

                ' Substitui extensões ".SLDASM" e ".SLDPRT" por ".DDF"
                ArquivoSW = Path.ChangeExtension(ArquivoSW, ".SLDASM")

                ' Obtém o caminho completo
                ArquivoSW = Path.GetFullPath(ArquivoSW)
                ' Verifica se o arquivo existe e o abre
                If File.Exists(ArquivoSW) Then
                    Using p As New Diagnostics.Process
                        p.StartInfo = New ProcessStartInfo(ArquivoSW)

                        p.Start()
                        p.WaitForExit()

                        dgvDesenhosCadastrados.CurrentRow.DefaultCellStyle.BackColor = Color.LightCyan
                    End Using


                End If
            End If

        Catch ex As Exception
            MsgBox("Arquivo não encontrado!", vbCritical, "Atenção")
        Finally

        End Try

    End Sub

    Private Sub dgvDesenhosCadastrados_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvDesenhosCadastrados.DataBindingComplete

        Dim enderecoArquivo As String

        Dim dxf, pdf, LXDS, dft As String

        For i As Integer = 0 To dgvDesenhosCadastrados.Rows.Count - 1


            Try

                Try
                    enderecoArquivo = dgvDesenhosCadastrados.Rows(i).Cells("EnderecoArquivo").Value.ToString()

                Catch ex As Exception
                    enderecoArquivo = ""
                    Continue For
                End Try

                If enderecoArquivo <> "" Then


                    ' Verifica se o arquivo é uma peça (.SLDPRT) ou uma montagem (.SLDASM) e altera para .dxf
                    If enderecoArquivo.EndsWith(".SLDPRT", StringComparison.OrdinalIgnoreCase) OrElse
                       enderecoArquivo.EndsWith(".SLDASM", StringComparison.OrdinalIgnoreCase) Then
                        dxf = Path.ChangeExtension(enderecoArquivo, ".dxf")

                        ' Verifica se o arquivo DXF existe
                        If File.Exists(dxf) Then
                            dgvDesenhosCadastrados.Rows(i).Cells("DGVDXF").Value = My.Resources.arquivo_dxf
                        Else
                            dgvDesenhosCadastrados.Rows(i).Cells("DGVDXF").Value = My.Resources.Sem_Incone
                        End If
                    End If

                    ' Altera para .pdf
                    If enderecoArquivo.EndsWith(".SLDPRT", StringComparison.OrdinalIgnoreCase) OrElse
                       enderecoArquivo.EndsWith(".SLDASM", StringComparison.OrdinalIgnoreCase) Then
                        pdf = Path.ChangeExtension(enderecoArquivo, ".pdf")

                        ' Verifica se o arquivo PDF existe
                        If File.Exists(pdf) Then
                            dgvDesenhosCadastrados.Rows(i).Cells("DGVPDF").Value = My.Resources.ficheiro_pdf
                        Else
                            dgvDesenhosCadastrados.Rows(i).Cells("DGVPDF").Value = My.Resources.Sem_Incone
                        End If
                    End If

                    ' Verifica se o arquivo é uma peça (.SLDPRT) ou uma montagem (.SLDASM) e altera para .dxf
                    If enderecoArquivo.EndsWith(".SLDASM", StringComparison.OrdinalIgnoreCase) Then

                        ' Verifica se o arquivo PDF existe
                        If File.Exists(enderecoArquivo) Then
                            dgvDesenhosCadastrados.Rows(i).Cells("DGVSW").Value = My.Resources.IcopneMontagemSW
                        Else
                            dgvDesenhosCadastrados.Rows(i).Cells("DGVSW").Value = My.Resources.Sem_Incone
                        End If

                    End If

                    ' Verifica se o arquivo é uma peça (.SLDPRT) ou uma montagem (.SLDASM) e altera para .dxf
                    If enderecoArquivo.EndsWith(".SLDPRT", StringComparison.OrdinalIgnoreCase) Then

                        ' Verifica se o arquivo PDF existe
                        If File.Exists(enderecoArquivo) Then
                            dgvDesenhosCadastrados.Rows(i).Cells("DGVSW").Value = My.Resources.IcopneMontagemPRT
                        Else
                            dgvDesenhosCadastrados.Rows(i).Cells("DGVSW").Value = My.Resources.Sem_Incone
                        End If

                    End If


                    ' Verifica se o arquivo é uma peça (.SLDPRT) ou uma montagem (.SLDASM) e altera para .dxf
                    If enderecoArquivo.EndsWith(".SLDPRT", StringComparison.OrdinalIgnoreCase) OrElse
                       enderecoArquivo.EndsWith(".SLDASM", StringComparison.OrdinalIgnoreCase) Then
                        LXDS = Path.ChangeExtension(enderecoArquivo, ".LXDS")


                        ' Verifica se o arquivo PDF existe
                        If File.Exists(LXDS) Then
                            dgvDesenhosCadastrados.Rows(i).Cells("DGVLXDS").Value = My.Resources.CYPCUT
                        Else
                            dgvDesenhosCadastrados.Rows(i).Cells("DGVLXDS").Value = My.Resources.Sem_Incone
                        End If
                    End If


                    ' Verifica se o arquivo é uma peça (.SLDPRT) ou uma montagem (.SLDASM) e altera para .dxf
                    If enderecoArquivo.EndsWith(".SLDPRT", StringComparison.OrdinalIgnoreCase) OrElse
                       enderecoArquivo.EndsWith(".SLDASM", StringComparison.OrdinalIgnoreCase) Then
                        dft = Path.ChangeExtension(enderecoArquivo, ".DFT")


                        ' Verifica se o arquivo PDF existe
                        If File.Exists(enderecoArquivo) Then
                            dgvDesenhosCadastrados.Rows(i).Cells("dgvDFT").Value = My.Resources.DFT
                        Else
                            dgvDesenhosCadastrados.Rows(i).Cells("dgvDFT").Value = My.Resources.Sem_Incone
                        End If
                    End If
                End If
            Catch ex As Exception
                '  MsgBox(ex.Message)
                Continue For
            End Try

            'ProgressBarBuscarArquivos.Value = i

        Next
    End Sub
End Class