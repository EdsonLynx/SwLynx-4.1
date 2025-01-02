Imports System.IO
Imports System.IO.Compression
Imports System.Net
Imports System.Net.Mail
Imports System.Windows.Forms

Public Class clEmail

    Public Function EnviarEmailComOs(ByVal enderecoBase As String, ByVal OSDesvricao As String, ByVal DataLiberacaoEngenharia As String)

        ' Ativar envio de e-mail
        Dim enviarEmail As Boolean = True
        If Not enviarEmail Then Exit Function

        Try
            ' Obter caminho da OS
            ' Dim enderecoBase As String = Trim(ENDERECO)
            If String.IsNullOrEmpty(enderecoBase) Then
                MsgBox("Endereço inválido para geração do arquivo ZIP.", vbCritical, "Erro")
                Exit Function
            End If

            ' Determinar nome do arquivo ZIP disponível
            Dim arquivoZip As String = EncontrarArquivoZipDisponivel(enderecoBase)

            ' Criar o arquivo ZIP
            ZipFile.CreateFromDirectory(enderecoBase, arquivoZip)

            ' Preparar mensagem e anexar o arquivo
            Dim mensagem As String = PrepararMensagemEmail(OSDesvricao, DataLiberacaoEngenharia)

            ' Enviar e-mail
            Dim destinatario As String = "edson@lynxsolucoesmecanicas.com.br"
            Dim remetente As String = "edson@lynxsolucoesmecanicas.com.br" 'lynxemssistema@gmail.com
            Dim assunto As String = "Lynx - Aviso de Liberação de OS para Produção"
            Dim servidorSmtp As String = "smtps.uhserver.com"
            Dim portaSmtp As String = "465"
            Dim senhaEmail As String = "10207597Eds@$" '10207597Eds@$  Mover para configurações seguras, como variáveis de ambiente

            Email.EnviarEmail(
            destinatario, assunto, remetente,
            "Sistema de Gestão", servidorSmtp, portaSmtp, senhaEmail, assunto,
            mensagem, arquivoZip
        )

            MsgBox("E-mail enviado com sucesso!", vbInformation, "Sucesso")

        Catch ex As Exception
            MsgBox("Erro ao enviar e-mail: " & ex.Message, vbCritical, "Erro")
        End Try

    End Function

    Public Function EnviarEmailComOs01(ByVal enderecoBase As String, ByVal OSDesvricao As String, ByVal DataLiberacaoEngenharia As String)
        Try
            ' Verifica se o endereço é válido
            If String.IsNullOrEmpty(enderecoBase) Then
                MsgBox("Endereço inválido para geração do arquivo ZIP.", vbCritical, "Erro")
                Exit Function
            End If

            ' Determinar nome do arquivo ZIP disponível
            ' Dim arquivoZip As String = EncontrarArquivoZipDisponivel(enderecoBase)

            ' Criar o arquivo ZIP
            '  ZipFile.CreateFromDirectory(enderecoBase, arquivoZip)

            ' Configuração do e-mail
            Dim smtpClient As New SmtpClient("smtp.gmail.com") With {
            .Port = 587, ' Porta do Gmail para TLS
            .EnableSsl = True, ' Ativa conexão segura
            .Credentials = New NetworkCredential("lynxemssistema@gmail.com", "10207597Eds@$") ' Substitua pela senha correta ou senha de app
        }

            Dim mailMessage As New MailMessage() With {
            .From = New MailAddress("lynxemssistema@gmail.com", "Sistema Lynx"),
            .Subject = "Lynx - Aviso de Liberação de OS para Produção",
            .Body = "Mensagem de exemplo", ' Substitua com o conteúdo real
            .IsBodyHtml = True ' Define como HTML se necessário
        }

            ' Adiciona destinatário
            mailMessage.To.Add("edson@lynxsolucoesmecanicas.com.br")

            ' Anexa o arquivo ZIP
            ' mailMessage.Attachments.Add(New Attachment(arquivoZip))

            ' Envia o e-mail
            smtpClient.Send(mailMessage)

            MsgBox("E-mail enviado com sucesso!", vbInformation, "Sucesso")

        Catch ex As Exception
            MsgBox("Erro ao enviar e-mail: " & ex.Message, vbCritical, "Erro")
        End Try
    End Function



    ' Método para encontrar o próximo nome de arquivo ZIP disponível
    Private Function EncontrarArquivoZipDisponivel(enderecoBase As String) As String
        For i As Integer = 0 To 50
            Dim arquivoZip As String = $"{enderecoBase}-{i}.Zip"
            If Not File.Exists(arquivoZip) Then
                Return arquivoZip
            End If
        Next
        Throw New IOException("Não foi possível gerar um nome de arquivo ZIP disponível.")
    End Function

    ' Método para preparar a mensagem do e-mail
    Private Function PrepararMensagemEmail(ByVal osDESCRICAO As String, ByVal DATA_LIBERACAO_ENGENHARIA As String) As String
        Return "Prezados, este é um e-mail do sistema de gestão de fábrica." & vbCrLf & vbCrLf &
           "Descrição da OS: " & Trim(osDESCRICAO.ToString()) & vbCrLf &
           "Favor não responder este e-mail!" & vbCrLf & vbCrLf &
           "Data de Liberação da Engenharia: " & Trim(DATA_LIBERACAO_ENGENHARIA.ToString())
    End Function

    'Private Function EnviarEmail(ByVal emailEnvio As String,
    '    ByVal NomeEnvio As String,
    '    ByVal emailDestino As String,
    '    ByVal NomeDestino As String,
    '    ByVal smtp As String,
    '    ByVal porta As String,
    '    ByVal senha As String,
    '    ByVal assunto As String,
    '    ByVal Mensagem As String,
    '    ByVal EnderecoAnexo As String) As Boolean

    '    Dim objenvio As Net.Mail.SmtpClient = Nothing
    '    Dim ObjEmail As MailMessage = Nothing

    '    Dim blnRetorno As Boolean = False

    '    Try


    '        objenvio = New SmtpClient(smtp, porta)
    '        ObjEmail = New MailMessage

    '        'email destino
    '        ObjEmail.To.Add(New MailAddress(emailDestino, NomeDestino))

    '        'email de envio
    '        ObjEmail.From = New MailAddress(emailEnvio, NomeEnvio)

    '        'Seta o Assunto do e-mail
    '        ObjEmail.Subject = assunto


    '        'prioridade
    '        ObjEmail.Priority = MailPriority.High



    '        'seta confirmação de leitura
    '        'ObjEmail.Headers.Add("Disposition.Notification-to", emailEnvio)


    '        'seta a mensagem de texto
    '        Dim msgMensagem As AlternateView = AlternateView.CreateAlternateViewFromString(Mensagem, Nothing, Mime.MediaTypeNames.Text.Plain)
    '        ObjEmail.AlternateViews.Add(msgMensagem)


    '        'Verificar se a MSG tem anexo 
    '        If EnderecoAnexo.ToString <> "" Then

    '            'Inserir anexo
    '            Dim anexo As System.Net.Mail.Attachment
    '            anexo = New System.Net.Mail.Attachment(EnderecoAnexo)
    '            ObjEmail.Attachments.Add(anexo)

    '        End If


    '        'seto as credenciais
    '        Dim credencial As New NetworkCredential(emailEnvio, senha)

    '        objenvio.Credentials = credencial
    '        objenvio.EnableSsl = True

    '        'Envia email
    '        objenvio.Send(ObjEmail)

    '        'retorna
    '        blnRetorno = True



    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString())
    '    Finally
    '    End Try


    '    Return blnRetorno

    'End Function

    '    Private Function EnviarEmail(
    '    ByVal emailEnvio As String,
    '    ByVal nomeEnvio As String,
    '    ByVal emailDestino As String,
    '    ByVal nomeDestino As String,
    '    ByVal smtp As String,
    '    ByVal porta As String,
    '    ByVal senha As String,
    '    ByVal assunto As String,
    '    ByVal mensagem As String,
    '    ByVal enderecoAnexo As String
    ') As Boolean

    '        ' Verificar parâmetros básicos
    '        If String.IsNullOrWhiteSpace(emailEnvio) OrElse String.IsNullOrWhiteSpace(emailDestino) Then
    '            MessageBox.Show("Os endereços de e-mail de envio e destino são obrigatórios.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            Return False
    '        End If

    '        ' Inicializar retorno como falso
    '        Dim blnRetorno As Boolean = False

    '        Try
    '            ' Criar o objeto MailMessage
    '            Using objEmail As New MailMessage()
    '                ' Configurar remetente e destinatário
    '                objEmail.From = New MailAddress(emailEnvio, nomeEnvio)
    '                objEmail.To.Add(New MailAddress(emailDestino, nomeDestino))

    '                ' Configurar assunto e prioridade
    '                objEmail.Subject = assunto
    '                objEmail.Priority = MailPriority.High

    '                ' Adicionar mensagem de texto
    '                objEmail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(mensagem, Nothing, Mime.MediaTypeNames.Text.Plain))

    '                ' Adicionar anexo (se existir)
    '                If Not String.IsNullOrWhiteSpace(enderecoAnexo) Then
    '                    AdicionarAnexo(objEmail, enderecoAnexo)
    '                End If

    '                ' Configurar cliente SMTP
    '                Using objEnvio As New SmtpClient(smtp, Convert.ToInt32(porta))
    '                    objEnvio.Credentials = New NetworkCredential(emailEnvio, senha)
    '                    objEnvio.EnableSsl = True

    '                    ' Enviar o e-mail
    '                    objEnvio.Send(objEmail)
    '                End Using
    '            End Using

    '            ' Retornar sucesso
    '            blnRetorno = True

    '        Catch ex As SmtpException
    '            MessageBox.Show($"Erro SMTP: {ex.Message}", "Erro de Envio", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Catch ex As ArgumentException
    '            MessageBox.Show($"Parâmetro inválido: {ex.Message}", "Erro de Parâmetro", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Catch ex As Exception
    '            MessageBox.Show($"Erro inesperado: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End Try

    '        Return blnRetorno

    '    End Function

    Private Function EnviarEmail(
    ByVal emailEnvio As String,
    ByVal nomeEnvio As String,
    ByVal emailDestino As String,
    ByVal nomeDestino As String,
    ByVal smtp As String,
    ByVal porta As String,
    ByVal senha As String,
    ByVal assunto As String,
    ByVal mensagem As String,
    ByVal enderecoAnexo As String
) As Boolean

        ' Verificar parâmetros básicos
        If String.IsNullOrWhiteSpace(emailEnvio) OrElse String.IsNullOrWhiteSpace(emailDestino) Then
            MessageBox.Show("Os endereços de e-mail de envio e destino são obrigatórios.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        ' Inicializar retorno como falso
        Dim blnRetorno As Boolean = False

        Try
            ' Criar o objeto MailMessage
            Using objEmail As New MailMessage()
                ' Configurar remetente e destinatário
                objEmail.From = New MailAddress(emailEnvio, nomeEnvio)
                objEmail.To.Add(New MailAddress(emailDestino, nomeDestino))

                ' Configurar assunto e prioridade
                objEmail.Subject = assunto
                objEmail.Priority = MailPriority.High

                ' Adicionar mensagem de texto
                objEmail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(
                                            mensagem, Nothing, Mime.MediaTypeNames.Text.Plain))

                ' Adicionar anexo (se existir)
                If Not String.IsNullOrWhiteSpace(enderecoAnexo) Then
                    objEmail.Attachments.Add(New Attachment(enderecoAnexo))
                End If

                '''' Configurar cliente SMTP
                '''Using objEnvio As New SmtpClient(smtp, Convert.ToInt32(porta))
                '''    ' Configuração de autenticação e SSL/TLS
                '''    objEnvio.Credentials = New NetworkCredential(emailEnvio, senha)
                '''    objEnvio.EnableSsl = True ' Habilita SSL
                '''    objEnvio.DeliveryMethod = SmtpDeliveryMethod.Network
                '''    objEnvio.TargetName = "STARTTLS/smtps.uhserver.com"
                '''    objEnvio.Timeout = 100000 ' Aumenta o tempo limite para 100 segundos


                '''    ' Enviar o e-mail
                '''    objEnvio.Send(objEmail)
                '''End Using


                ' Configurar cliente SMTP
                Using objEnvio As New SmtpClient(smtp, Convert.ToInt32(porta))
                    ' Configuração de autenticação e SSL/TLS
                    objEnvio.Credentials = New NetworkCredential(emailEnvio, senha)
                    objEnvio.EnableSsl = True
                    objEnvio.Timeout = 100000 ' Aumenta o tempo limite para 100 segundos
                    objEnvio.TargetName = "STARTTLS/smtps.uhserver.com"
                    ' Habilitar autenticação de senha de segurança
                    objEnvio.UseDefaultCredentials = False

                    ' Enviar o e-mail
                    objEnvio.Send(objEmail)
                End Using


            End Using

            ' Retornar sucesso
            blnRetorno = True

        Catch ex As SmtpException
            MessageBox.Show($"Erro SMTP: {ex.Message}", "Erro de Envio", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As ArgumentException
            MessageBox.Show($"Parâmetro inválido: {ex.Message}", "Erro de Parâmetro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show($"Erro inesperado: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return blnRetorno

    End Function


    ' Método para adicionar anexo ao e-mail
    Private Sub AdicionarAnexo(ByRef objEmail As MailMessage, ByVal enderecoAnexo As String)
        Try
            If File.Exists(enderecoAnexo) Then
                Dim anexo As New Attachment(enderecoAnexo)
                objEmail.Attachments.Add(anexo)
            Else
                MessageBox.Show($"Anexo não encontrado: {enderecoAnexo}", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show($"Erro ao adicionar anexo: {ex.Message}", "Erro de Anexo", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


End Class
