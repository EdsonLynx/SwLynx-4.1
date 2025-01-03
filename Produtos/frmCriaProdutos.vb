''Imports System.Windows.Forms
''Imports System.IO
''Imports System.ComponentModel

''Public Class frmCriaProdutos
''    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click

''        Dim salvar As Boolean = False

''        OrdemServico.ProdutoPadrao = "SIM"
''        OrdemServico.CodDesenhoProduto = Me.txCodDesenhoProduto.Text
''        OrdemServico.CodOmie = Me.txtCodOmie.Text
''        OrdemServico.DescricaoProduto = Me.txtDescricaoProduto.Text
''        OrdemServico.EnderecoFichaTecnica = Me.lblEnderecoFichaTecnica.Text
''        OrdemServico.EnderecoIsometrico = Me.lblEnderecoIsometrico.Text
''        OrdemServico.ProdutoCriadoPor = Usuario.NomeCompleto
''        OrdemServico.DataCriacaoProduto = Date.Now.Date.ToShortDateString

''        ValidarDadosOrdemServico()

''    End Sub


''    Private Sub ValidarDadosOrdemServico()
''        ' Verifica se o ID do item da ordem de serviço é válido
''        If String.IsNullOrWhiteSpace(OrdemServico.IdOrdemServico.ToString()) OrElse OrdemServico.IdOrdemServico = 0 Then
''            MessageBox.Show("O ID do item da ordem de serviço é inválido ou não foi informado.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error)
''            Exit Sub
''        End If

''        ' Verifica se o código de desenho do produto foi informado
''        If String.IsNullOrWhiteSpace(OrdemServico.CodDesenhoProduto) Then
''            MessageBox.Show("O código de desenho do produto é obrigatório. Por favor, preencha este campo.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error)
''            Exit Sub
''        End If

''        ' Verifica se o código Omie foi informado
''        If String.IsNullOrWhiteSpace(OrdemServico.CodOmie) Then
''            MessageBox.Show("O código Omie é obrigatório. Por favor, preencha este campo.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error)
''            Exit Sub
''        End If

''        ' Verifica se a descrição do produto foi informada
''        If String.IsNullOrWhiteSpace(OrdemServico.DescricaoProduto) Then
''            MessageBox.Show("A descrição do produto é obrigatória. Por favor, preencha este campo.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error)
''            Exit Sub
''        End If

''        ' Verifica se o endereço da ficha técnica foi informado e se é um arquivo PDF válido
''        If String.IsNullOrWhiteSpace(OrdemServico.EnderecoFichaTecnica) Then
''            MessageBox.Show("O endereço da ficha técnica é obrigatório. Por favor, preencha este campo.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error)
''            Exit Sub
''        ElseIf Not File.Exists(OrdemServico.EnderecoFichaTecnica) Then
''            MessageBox.Show("O arquivo da ficha técnica não foi encontrado. Verifique o caminho informado.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error)
''            Exit Sub
''        ElseIf Path.GetExtension(OrdemServico.EnderecoFichaTecnica).ToLower() <> ".pdf" Then
''            MessageBox.Show("O arquivo da ficha técnica não é um PDF válido. Por favor, forneça um arquivo PDF.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error)
''            Exit Sub
''        End If

''        ' Verifica se o endereço do isométrico foi informado e se é um arquivo PDF válido
''        If String.IsNullOrWhiteSpace(OrdemServico.EnderecoIsometrico) Then
''            MessageBox.Show("O endereço do isométrico é obrigatório. Por favor, preencha este campo.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error)
''            Exit Sub
''        ElseIf Not File.Exists(OrdemServico.EnderecoIsometrico) Then
''            MessageBox.Show("O arquivo do isométrico não foi encontrado. Verifique o caminho informado.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error)
''            Exit Sub
''        ElseIf Path.GetExtension(OrdemServico.EnderecoIsometrico).ToLower() <> ".pdf" Then
''            MessageBox.Show("O arquivo do isométrico não é um PDF válido. Por favor, forneça um arquivo PDF.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error)
''            Exit Sub
''        End If

''        ' Define os campos automáticos relacionados ao usuário e à data
''        OrdemServico.ProdutoCriadoPor = Usuario.NomeCompleto
''        OrdemServico.DataCriacaoProduto = Date.Now.Date.ToShortDateString


''        cl_BancoDados.AlteracaoEspecificaDadosOSProduto(OrdemServico.IdOrdemServico)
''        MessageBox.Show("Dados validados e salvos com sucesso!", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Information)

''        ' Caso todas as validações passem
''        'MessageBox.Show("Dados validados com sucesso!", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Information)
''    End Sub

''    Public Function SelecionarArquivoPDF() As String
''        ' Cria o diálogo de seleção de arquivo
''        Using dialog As New OpenFileDialog()
''            dialog.Filter = "Arquivos PDF (*.pdf)|*.pdf" ' Filtra apenas arquivos PDF
''            dialog.Title = "Selecione um arquivo PDF"
''            dialog.Multiselect = False ' Permite selecionar apenas um arquivo

''            ' Exibe a caixa de diálogo e verifica se o usuário clicou em OK
''            If dialog.ShowDialog() = DialogResult.OK Then
''                Return dialog.FileName ' Retorna o caminho do arquivo selecionado
''            Else
''                Return String.Empty ' Retorna vazio se o usuário cancelar
''            End If
''        End Using
''    End Function

''    Private Sub btnBuscarFichaTecnica_Click(sender As Object, e As EventArgs) Handles btnBuscarFichaTecnica.Click
''        Dim caminhoArquivo As String = SelecionarArquivoPDF()

''        If Not String.IsNullOrEmpty(caminhoArquivo) Then
''            OrdemServico.EnderecoFichaTecnica = caminhoArquivo
''            lblEnderecoFichaTecnica.Text = caminhoArquivo
''            MessageBox.Show("Arquivo PDF selecionado: " & caminhoArquivo, "Seleção de Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Information)
''        Else
''            MessageBox.Show("Nenhum arquivo foi selecionado.", "Seleção de Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning)
''        End If

''    End Sub

''    Private Sub btnBuscarIsometrico_Click(sender As Object, e As EventArgs) Handles btnBuscarIsometrico.Click

''        Dim caminhoArquivo As String = SelecionarArquivoPDF()

''        If Not String.IsNullOrEmpty(caminhoArquivo) Then
''            OrdemServico.EnderecoIsometrico = caminhoArquivo
''            lblEnderecoIsometrico.Text = caminhoArquivo
''            MessageBox.Show("Arquivo PDF selecionado: " & caminhoArquivo, "Seleção de Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Information)
''        Else
''            MessageBox.Show("Nenhum arquivo foi selecionado.", "Seleção de Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning)
''        End If

''    End Sub

''    Private Sub lblEnderecoFichaTecnica_Click(sender As Object, e As EventArgs) Handles lblEnderecoFichaTecnica.Click

''    End Sub

''    Private Sub lblEnderecoFichaTecnica_DoubleClick(sender As Object, e As EventArgs) Handles lblEnderecoFichaTecnica.DoubleClick


''        Try


''            Dim ArquivoPdf As String = lblEnderecoFichaTecnica.Text.ToString

''            ' Substitui extensões ".SLDASM" e ".SLDPRT" por ".DDF"
''            ArquivoPdf = Path.ChangeExtension(ArquivoPdf, ".PDF")

''            ' Obtém o caminho completo
''            ArquivoPdf = Path.GetFullPath(ArquivoPdf)

''            ' Verifica se o arquivo existe e o abre
''            If File.Exists(ArquivoPdf) Then
''                Using p As New Diagnostics.Process
''                    p.StartInfo = New ProcessStartInfo(ArquivoPdf)

''                    p.Start()
''                    p.WaitForExit()

''                End Using
''            End If
''        Catch ex As Exception
''            MsgBox("Arquivo não encontrado!", vbCritical, "Atenção")
''        Finally

''        End Try

''    End Sub

''    Private Sub lblEnderecoIsometrico_Click(sender As Object, e As EventArgs) Handles lblEnderecoIsometrico.Click

''        Try


''            Dim ArquivoPdf As String = lblEnderecoIsometrico.Text.ToString

''            ' Substitui extensões ".SLDASM" e ".SLDPRT" por ".DDF"
''            ArquivoPdf = Path.ChangeExtension(ArquivoPdf, ".PDF")

''            ' Obtém o caminho completo
''            ArquivoPdf = Path.GetFullPath(ArquivoPdf)

''            ' Verifica se o arquivo existe e o abre
''            If File.Exists(ArquivoPdf) Then
''                Using p As New Diagnostics.Process
''                    p.StartInfo = New ProcessStartInfo(ArquivoPdf)

''                    p.Start()
''                    p.WaitForExit()

''                End Using
''            End If
''        Catch ex As Exception
''            MsgBox("Arquivo não encontrado!", vbCritical, "Atenção")
''        Finally

''        End Try


''    End Sub

''    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

''        Me.Close()

''    End Sub



''    Private Sub frmCriaProdutos_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

''        OrdemServico.IdOrdemServico = 0


''    End Sub
''End Class

Imports System.Windows.Forms
Imports System.IO
Imports System.ComponentModel

Public Class frmCriaProdutos
    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click

        OrdemServico.ProdutoPadrao = "SIM"
        OrdemServico.CodDesenhoProduto = Me.txCodDesenhoProduto.Text
        OrdemServico.CodOmie = Me.txtCodOmie.Text
        OrdemServico.DescricaoProduto = Me.txtDescricaoProduto.Text
        OrdemServico.EnderecoFichaTecnica = Me.lblEnderecoFichaTecnica.Text
        OrdemServico.EnderecoIsometrico = Me.lblEnderecoIsometrico.Text
        OrdemServico.ProdutoCriadoPor = Usuario.NomeCompleto
        OrdemServico.DataCriacaoProduto = Date.Now.Date.ToShortDateString

        ValidarDadosOrdemServico()
    End Sub

    Private Sub ValidarDadosOrdemServico()
        ' Verifica se o ID do item da ordem de serviço é válido
        If Not ValidarIDOrdemServico() Then Exit Sub

        ' Verifica se o código de desenho do produto foi informado
        If Not ValidarCampoObrigatorio(OrdemServico.CodDesenhoProduto, "O código de desenho do produto é obrigatório. Por favor, preencha este campo.") Then Exit Sub

        ' Verifica se o código Omie foi informado
        If Not ValidarCampoObrigatorio(OrdemServico.CodOmie, "O código Omie é obrigatório. Por favor, preencha este campo.") Then Exit Sub

        ' Verifica se a descrição do produto foi informada
        If Not ValidarCampoObrigatorio(OrdemServico.DescricaoProduto, "A descrição do produto é obrigatória. Por favor, preencha este campo.") Then Exit Sub

        ' Verifica se o endereço da ficha técnica foi informado e se é um arquivo PDF válido
        ' If Not ValidarArquivoPDF(OrdemServico.EnderecoFichaTecnica, "O endereço da ficha técnica é obrigatório. Por favor, preencha este campo.", "O arquivo da ficha técnica não foi encontrado. Verifique o caminho informado.", "O arquivo da ficha técnica não é um PDF válido. Por favor, forneça um arquivo PDF.") Then Exit Sub

        ' Verifica se o endereço do isométrico foi informado e se é um arquivo PDF válido
        ' If Not ValidarArquivoPDF(OrdemServico.EnderecoIsometrico, "O endereço do isométrico é obrigatório. Por favor, preencha este campo.", "O arquivo do isométrico não foi encontrado. Verifique o caminho informado.", "O arquivo do isométrico não é um PDF válido. Por favor, forneça um arquivo PDF.") Then Exit Sub

        ' Define os campos automáticos relacionados ao usuário e à data
        OrdemServico.ProdutoCriadoPor = Usuario.NomeCompleto
        OrdemServico.DataCriacaoProduto = Date.Now.Date.ToShortDateString

        cl_BancoDados.AlteracaoEspecificaDadosOSProduto(OrdemServico.IdOrdemServico)
        MessageBox.Show("Dados validados e salvos com sucesso!", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    Private Function ValidarIDOrdemServico() As Boolean
        If String.IsNullOrWhiteSpace(OrdemServico.IdOrdemServico.ToString()) OrElse OrdemServico.IdOrdemServico = 0 Then
            MessageBox.Show("O ID do item da ordem de serviço é inválido ou não foi informado.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        Return True
    End Function

    Private Function ValidarCampoObrigatorio(campo As String, mensagemErro As String) As Boolean
        If String.IsNullOrWhiteSpace(campo) Then
            MessageBox.Show(mensagemErro, "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        Return True
    End Function

    Private Function ValidarArquivoPDF(caminhoArquivo As String, mensagemErroCampo As String, mensagemErroNaoEncontrado As String, mensagemErroInvalido As String) As Boolean
        If String.IsNullOrWhiteSpace(caminhoArquivo) Then
            MessageBox.Show(mensagemErroCampo, "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        ElseIf Not File.Exists(caminhoArquivo) Then
            MessageBox.Show(mensagemErroNaoEncontrado, "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        ElseIf Path.GetExtension(caminhoArquivo).ToLower() <> ".pdf" Then
            MessageBox.Show(mensagemErroInvalido, "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        Return True
    End Function



    Private Sub btnBuscarFichaTecnica_Click(sender As Object, e As EventArgs) Handles btnBuscarFichaTecnica.Click
        Dim caminhoArquivo As String = cl_BancoDados.SelecionarArquivoPDF()

        If Not String.IsNullOrEmpty(caminhoArquivo) Then
            OrdemServico.EnderecoFichaTecnica = caminhoArquivo
            lblEnderecoFichaTecnica.Text = caminhoArquivo
            MessageBox.Show("Arquivo PDF selecionado: " & caminhoArquivo, "Seleção de Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Nenhum arquivo foi selecionado.", "Seleção de Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnBuscarIsometrico_Click(sender As Object, e As EventArgs) Handles btnBuscarIsometrico.Click
        Dim caminhoArquivo As String = cl_BancoDados.SelecionarArquivoPDF()

        If Not String.IsNullOrEmpty(caminhoArquivo) Then
            OrdemServico.EnderecoIsometrico = caminhoArquivo
            lblEnderecoIsometrico.Text = caminhoArquivo
            MessageBox.Show("Arquivo PDF selecionado: " & caminhoArquivo, "Seleção de Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Nenhum arquivo foi selecionado.", "Seleção de Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub lblEnderecoFichaTecnica_DoubleClick(sender As Object, e As EventArgs) Handles lblEnderecoFichaTecnica.DoubleClick
        AbrirArquivoPDF(lblEnderecoFichaTecnica.Text)
    End Sub

    Private Sub lblEnderecoIsometrico_DoubleClick(sender As Object, e As EventArgs) Handles lblEnderecoIsometrico.DoubleClick
        AbrirArquivoPDF(lblEnderecoIsometrico.Text)
    End Sub

    Private Sub AbrirArquivoPDF(caminhoArquivo As String)
        Try
            Dim arquivoPdf As String = Path.ChangeExtension(caminhoArquivo, ".PDF")
            arquivoPdf = Path.GetFullPath(arquivoPdf)

            If File.Exists(arquivoPdf) Then
                Using p As New Diagnostics.Process
                    p.StartInfo = New ProcessStartInfo(arquivoPdf)
                    p.Start()
                    p.WaitForExit()
                End Using
            Else
                MsgBox("Arquivo não encontrado!", vbCritical, "Atenção")
            End If
        Catch ex As Exception
            MsgBox("Erro ao abrir o arquivo: " & ex.Message, vbCritical, "Atenção")
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub frmCriaProdutos_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        OrdemServico.IdOrdemServico = 0
    End Sub

    Private Sub frmCriaProdutos_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class