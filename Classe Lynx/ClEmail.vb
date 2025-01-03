Imports System.IO
Imports System.IO.Compression
Imports System.Net
Imports System.Net.Mail
Imports System.Windows.Forms

Public Class clEmail


    Public Function EnviarEmail(
    ByVal emailEnvio As String,
    ByVal nomeEnvio As String,
    ByVal emailDestino As String,
    ByVal nomeDestino As String,
    ByVal smtp As String,
    ByVal porta As String,
    ByVal senha As String,
    ByVal assunto As String,
    ByVal mensagem As String,
    Optional ByVal enderecoAnexo As String = Nothing
) As Boolean
        ' Verificar parâmetros obrigatórios
        If String.IsNullOrWhiteSpace(emailEnvio) OrElse
       String.IsNullOrWhiteSpace(emailDestino) OrElse
       String.IsNullOrWhiteSpace(smtp) OrElse
       String.IsNullOrWhiteSpace(porta) OrElse
       String.IsNullOrWhiteSpace(senha) Then
            MessageBox.Show("Todos os campos obrigatórios devem ser preenchidos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Try
            ' Criar objeto MailMessage
            Using objEmail As New MailMessage()
                ' Configurar remetente e destinatário
                objEmail.From = New MailAddress(emailEnvio, nomeEnvio)
                objEmail.To.Add(New MailAddress(emailDestino, nomeDestino))

                ' Configurar assunto e mensagem
                objEmail.Subject = assunto
                objEmail.Body = mensagem
                objEmail.IsBodyHtml = False ' Configurar como texto simples
                objEmail.Priority = MailPriority.High

                ' Adicionar anexo, se houver
                If Not String.IsNullOrWhiteSpace(enderecoAnexo) Then
                    objEmail.Attachments.Add(New Attachment(enderecoAnexo))
                End If

                ' Configurar cliente SMTP
                Using objEnvio As New SmtpClient(smtp, Convert.ToInt32(porta))
                    objEnvio.Credentials = New NetworkCredential(emailEnvio, senha)
                    objEnvio.EnableSsl = True
                    objEnvio.Timeout = 100000 ' Ajustável conforme necessário

                    ' Enviar o e-mail
                    objEnvio.Send(objEmail)
                End Using
            End Using

            ' Retornar sucesso
            Return True
        Catch ex As SmtpException
            ' MessageBox.Show($"Erro SMTP: {ex.Message}", "Erro de Envio", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As ArgumentException
            ' MessageBox.Show($"Erro nos parâmetros: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            ' MessageBox.Show($"Erro inesperado: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return False
    End Function


    Public Function EmailLiberacaoOS() As Boolean

        If My.Settings.BancoDadosAtivo = "alfatec2" Then

            Dim mensagem As String = "Prezados," & vbCrLf & vbCrLf &
                       "Este é um e-mail do controle de Gestão do SINCO/Alfatec." & vbCrLf & vbCrLf &
                       "Por favor, não responda a este e-mail." & vbCrLf & vbCrLf &
                       "Liberaçao de OS 'Ordem de Serviço' numero: " & OrdemServico.IdOrdemServico & vbCrLf &
                       "Data de Liberação: " & Date.Now.Date.ToLongDateString & vbCrLf & vbCrLf &
                       "Projetista Responsavel: " & Usuario.NomeCompleto & vbCrLf & vbCrLf &
                       "Projeto: " & OrdemServico.Projeto & " / Tag: " & OrdemServico.Tag & vbCrLf & vbCrLf &
                       "Atenciosamente," & vbCrLf & vbCrLf & vbCrLf & vbCrLf &
                       "Equipe Alfatec/Engenharia"

            Dim sucesso As Boolean = ClasseEmail.EnviarEmail(
    emailEnvio:="sinco@alfatec.ind.br",
    nomeEnvio:="Sistema de Controle da Enhenharia",
    emailDestino:=Usuario.EnviarEmailLiberacaoOS.ToString, 'VEM DO BANCO DE DADOS
    nomeDestino:="PCP",
    smtp:="mail.alfatec.ind.br",
    porta:="587",
    senha:="v89v9hmbbrz8",
    assunto:="Registro de Liberação de Ordem de Serviço para Fabricação: " & OrdemServico.IdOrdemServico & " Projeto: " & OrdemServico.Projeto & " / Tag: " & OrdemServico.Tag,
    mensagem:=mensagem,
    enderecoAnexo:="")

        ElseIf My.Settings.BancoDadosAtivo = "amceletrica" Then

            Dim mensagem As String = "Prezados," & vbCrLf & vbCrLf &
                      "Este é um e-mail do controle de Gestão do SINCO/AMC." & vbCrLf & vbCrLf &
                      "Por favor, não responda a este e-mail." & vbCrLf & vbCrLf &
                      "Liberaçao de OS 'Ordem de Serviço' numero: " & OrdemServico.IdOrdemServico & vbCrLf &
                      "Data de Liberação: " & Date.Now.Date.ToLongDateString & vbCrLf & vbCrLf &
                      "Projetista Responsavel: " & Usuario.NomeCompleto & vbCrLf & vbCrLf &
                      "Projeto: " & OrdemServico.Projeto & " / Tag: " & OrdemServico.Tag & vbCrLf & vbCrLf &
                      "Atenciosamente," & vbCrLf & vbCrLf & vbCrLf & vbCrLf &
                      "Equipe AMC/Engenharia"

            Dim sucesso As Boolean = ClasseEmail.EnviarEmail(
    emailEnvio:="protheus@amcsolucoes.com.br",
    nomeEnvio:="Sistema de Controle da Enhenharia",
    emailDestino:=Usuario.EnviarEmailLiberacaoOS.ToString,
    nomeDestino:="PCP",
    smtp:="mail.amcsolucoes.com.br", 'smtp.office365.com", 'smtp.office365.com - 
    porta:="587",
    senha:="1r^Bq6L5j5",
    assunto:="Registro de Liberação de Ordem de Serviço para Fabricação: " & OrdemServico.IdOrdemServico & " Projeto: " & OrdemServico.Projeto & " / Tag: " & OrdemServico.Tag,
    mensagem:=mensagem,
    enderecoAnexo:="")



        End If


    End Function



    Public Function EmailCancelamentoOS() As Boolean

        If My.Settings.BancoDadosAtivo = "alfatec2" Then

            Dim mensagem As String = "Prezados," & vbCrLf & vbCrLf &
                         "Este é um e-mail do controle de Gestão do SINCO/Alfatec." & vbCrLf & vbCrLf &
                         "Por favor, não responda a este e-mail." & vbCrLf & vbCrLf &
                         "Cancelamento de Liberaçao de OS 'Ordem de Serviço' numero: " & OrdemServico.IdOrdemServico & vbCrLf &
                         "Data do Cancelamento: " & Date.Now.Date.ToLongDateString & vbCrLf & vbCrLf &
                         "Projetista Responsavel: " & Usuario.NomeCompleto & vbCrLf & vbCrLf &
                         "Projeto: " & OrdemServico.Projeto & " / Tag: " & OrdemServico.Tag & vbCrLf & vbCrLf &
                         "Endereço da Pasta: " & OrdemServico.EnderecoOrdemServico & vbCrLf &
                         "Atenciosamente," & vbCrLf & vbCrLf & vbCrLf & vbCrLf &
                         "Equipe Alfatec/Engenharia"

            Dim sucesso As Boolean = ClasseEmail.EnviarEmail(
        emailEnvio:="sinco@alfatec.ind.br",
        nomeEnvio:="Sistema de Controle da Enhenharia",
        emailDestino:=Usuario.EnviarEmailLiberacaoOS.ToString,
        nomeDestino:="PCP",
        smtp:="mail.alfatec.ind.br",
        porta:="587",
        senha:="v89v9hmbbrz8",
        assunto:="Registro de Cancelamento de Ordem de Serviço: " & OrdemServico.IdOrdemServico & " Projeto: " & OrdemServico.Projeto & " / Tag: " & OrdemServico.Tag,
        mensagem:=mensagem,
        enderecoAnexo:="")

        ElseIf My.Settings.BancoDadosAtivo = "amceletrica" Then

            Dim mensagem As String = "Prezados," & vbCrLf & vbCrLf &
                      "Este é um e-mail do controle de Gestão do SINCO/AMC." & vbCrLf & vbCrLf &
                      "Por favor, não responda a este e-mail." & vbCrLf & vbCrLf &
                      "Cancelamento de Liberaçao de OS 'Ordem de Serviço' numero: " & OrdemServico.IdOrdemServico & vbCrLf &
                      "Data de Liberação: " & Date.Now.Date.ToLongDateString & vbCrLf & vbCrLf &
                      "Projetista Responsavel: " & Usuario.NomeCompleto & vbCrLf & vbCrLf &
                      "Projeto: " & OrdemServico.Projeto & " / Tag: " & OrdemServico.Tag & vbCrLf & vbCrLf &
                      "Atenciosamente," & vbCrLf & vbCrLf & vbCrLf & vbCrLf &
                      "Equipe AMC/Engenharia"

            Dim sucesso As Boolean = ClasseEmail.EnviarEmail(
    emailEnvio:="protheus@amcsolucoes.com.br",
    nomeEnvio:="Sistema de Controle da Enhenharia",
    emailDestino:=Usuario.EnviarEmailLiberacaoOS.ToString,
    nomeDestino:="PCP",
    smtp:="mail.amcsolucoes.com.br", 'smtp.office365.com", 'smtp.office365.com - 
    porta:="587",
    senha:="1r^Bq6L5j5",
    assunto:="Registro de Liberação de Ordem de Serviço para Fabricação: " & OrdemServico.IdOrdemServico & " Projeto: " & OrdemServico.Projeto & " / Tag: " & OrdemServico.Tag,
    mensagem:=mensagem,
    enderecoAnexo:="")

        End If


    End Function


    Public Function EmailTratamentoErro(ByVal Mensagemex As String) As Boolean

        If My.Settings.BancoDadosAtivo = "alfatec2" Then

            Dim mensagem As String = "Prezados," & vbCrLf & vbCrLf &
                       "Este é um e-mail de tratamento de erro." & vbCrLf & vbCrLf &
                       "Mensagem EX.: " & Mensagemex & vbCrLf & vbCrLf &
                       "Por favor, não responda a este e-mail." & vbCrLf & vbCrLf &
                       "Data de Registro: " & Date.Now.Date.ToLongDateString & vbCrLf & vbCrLf &
                       "Usuario Responsavel: " & Usuario.NomeCompleto & vbCrLf & vbCrLf &
                       "Atenciosamente," & vbCrLf & vbCrLf & vbCrLf & vbCrLf &
                       "Equipe LYNX"

            Dim sucesso As Boolean = ClasseEmail.EnviarEmail(
    emailEnvio:="sinco@alfatec.ind.br",
    nomeEnvio:="Sistema de Controle de Erros",
    emailDestino:="suporte@lynxsolucoesmecanicas.com.br", 'VEM DO BANCO DE DADOS
    nomeDestino:="Sistema",
    smtp:="mail.alfatec.ind.br",
    porta:="587",
    senha:="v89v9hmbbrz8",
    assunto:="Registro de erro Alfatec",
    mensagem:=mensagem,
    enderecoAnexo:="")

        ElseIf My.Settings.BancoDadosAtivo = "amceletrica" Then

            Dim mensagem As String = "Prezados," & vbCrLf & vbCrLf &
                       "Este é um e-mail de tratamento de erro." & vbCrLf & vbCrLf &
                       "Mensagem EX.: " & Mensagemex & vbCrLf & vbCrLf &
                       "Por favor, não responda a este e-mail." & vbCrLf & vbCrLf &
                       "Data de Registro: " & Date.Now.Date.ToLongDateString & vbCrLf & vbCrLf &
                       "Usuario Responsavel: " & Usuario.NomeCompleto & vbCrLf & vbCrLf &
                       "Atenciosamente," & vbCrLf & vbCrLf & vbCrLf & vbCrLf &
                       "Equipe LYNX"

            Dim sucesso As Boolean = ClasseEmail.EnviarEmail(
    emailEnvio:="protheus@amcsolucoes.com.br",
    nomeEnvio:="Sistema de Controle da Enhenharia",
    emailDestino:="suporte@lynxsolucoesmecanicas.com.br", 'VEM DO BANCO DE DADOS
    nomeDestino:="Sistema",
    smtp:="smtp.office365.com",
    porta:="587",
    senha:="1r^Bq6L5j5",
    assunto:="Registro de erro Alfatec",
    mensagem:=mensagem,
    enderecoAnexo:="")

        End If


    End Function

    Public Function EnviarEmailMultiplo(
    ByVal emailEnvio As String,
    ByVal nomeEnvio As String,
    ByVal emailsDestino As String,
    ByVal nomeDestino As String,
    ByVal smtp As String,
    ByVal porta As String,
    ByVal senha As String,
    ByVal assunto As String,
    ByVal mensagem As String,
    ByVal enderecoAnexo As String
) As Boolean

        ' Verificar parâmetros básicos
        If String.IsNullOrWhiteSpace(emailEnvio) OrElse String.IsNullOrWhiteSpace(emailsDestino) Then
            MessageBox.Show("Os endereços de e-mail de envio e destino são obrigatórios.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Dim blnRetorno As Boolean = False

        Try
            ' Criar o objeto MailMessage
            Using objEmail As New MailMessage()
                ' Configurar remetente
                objEmail.From = New MailAddress(emailEnvio, nomeEnvio)

                ' Adicionar destinatários (suporta múltiplos e-mails separados por vírgula)
                For Each email In emailsDestino.Split(","c)
                    If Not String.IsNullOrWhiteSpace(email.Trim()) Then
                        objEmail.To.Add(New MailAddress(email.Trim(), nomeDestino))
                    End If
                Next

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

                ' Configurar cliente SMTP
                Using objEnvio As New SmtpClient(smtp, Convert.ToInt32(porta))
                    objEnvio.Credentials = New NetworkCredential(emailEnvio, senha)
                    objEnvio.EnableSsl = True
                    objEnvio.Timeout = 100000
                    objEnvio.UseDefaultCredentials = False

                    ' Enviar o e-mail
                    objEnvio.Send(objEmail)
                End Using
            End Using

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

End Class



