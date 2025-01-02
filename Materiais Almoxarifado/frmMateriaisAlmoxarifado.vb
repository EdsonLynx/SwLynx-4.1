Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO

Public Class frmMateriaisAlmoxarifado

    Dim QtdeEntrada As Double = 0

    Dim Peso As Double = 0
    Dim TotalValor As Double = 0
    Dim IdMaterial As Integer = 0


    Private Sub TimerDgvMaterial_Tick(sender As Object, e As EventArgs) Handles TimerDgvMaterial.Tick

        Dim query As String = " SELECT IdMaterial, CodMatFabricante,
DescResumo, DescDetal, 
Peso, Unidade, 
CodigoJuridicoMat, 
PercIPI, vIPI, 
PercICMS, vICMS, 
TotalValor
    FROM material
    WHERE DescDetal LIKE '%" & TxtPesqDesc1.Text & "%'
  AND DescDetal LIKE '%" & TxtPesqDesc2.Text & "%'
  AND DescDetal LIKE '%" & TxtPesqDesc3.Text & "%'
  AND CodigoJuridicoMat LIKE '%" & TxtPesqJuridico.Text & "%'
  AND CodMatFabricante LIKE '%" & TxtPesqCod.Text & "%'
  AND pecamanufat IS NULL
ORDER BY DescDetal limit 500;"

        dgvMaterial.DataSource = cl_BancoDados.CarregarDados(query)

        TimerDgvMaterial.Enabled = False



    End Sub

    Private Sub frmMateriaisAlmoxarifado_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        TimerDgvMaterial.Enabled = True

        If dgvMaterial.Rows.Count < 0 Then

            MsgBox("Não há materiais na lista de material para ser selecionado", vbInformation, "Atenção")

            btnAssociarMaterialM2.Enabled = False
            btnAssociarMaterialML.Enabled = False
            btnRelacao1para1.Enabled = False

        Else

            btnAssociarMaterialM2.Enabled = True
            btnAssociarMaterialML.Enabled = True
            btnRelacao1para1.Enabled = True

            ' Verifica se LarguraBlank e ComprimentoBlank são nulos antes de usá-los
            Dim larguraBlank As Double = Replace(DadosArquivoCorrente.LarguraBlank, ".", ",")
            Dim comprimentoBlank As Double = Replace(DadosArquivoCorrente.ComprimentoBlank, ".", ",")

            ' Preenchendo as dimensões da largura e comprimento em metros quadrados
            Me.txtLarguram2.Text = DadosArquivoCorrente.LarguraBlank
            Me.txtComprimentom2.Text = DadosArquivoCorrente.ComprimentoBlank

            ' Calculando o valor total de m² e verificando se os valores são válidos
            If larguraBlank > 0 And comprimentoBlank > 0 Then
                Me.txtm2Total.Text = (((larguraBlank / 1000) * (comprimentoBlank / 1000)) / 3.6).ToString("F2")
            Else
                Me.txtm2Total.Text = "0.00"
            End If

            ' Calculando o comprimento em metros lineares e verificando se o comprimento é válido
            If comprimentoBlank > 0 Then
                Me.txtComprimentoML.Text = (comprimentoBlank / 1000).ToString("F2")
            Else
                Me.txtComprimentoML.Text = "0.00"
            End If
        End If
    End Sub

    Private Sub TxtPesqCod_TextChanged(sender As Object, e As EventArgs) Handles TxtPesqCod.TextChanged
        TimerDgvMaterial.Enabled = True
    End Sub

    Private Sub TxtPesqDesc1_TextChanged(sender As Object, e As EventArgs) Handles TxtPesqDesc1.TextChanged
        TimerDgvMaterial.Enabled = True
    End Sub

    Private Sub TxtPesqDesc2_TextChanged(sender As Object, e As EventArgs) Handles TxtPesqDesc2.TextChanged
        TimerDgvMaterial.Enabled = True
    End Sub

    Private Sub TxtPesqDesc3_TextChanged(sender As Object, e As EventArgs) Handles TxtPesqDesc3.TextChanged
        TimerDgvMaterial.Enabled = True
    End Sub

    Private Sub TxtPesqJuridico_TextChanged(sender As Object, e As EventArgs) Handles TxtPesqJuridico.TextChanged
        TimerDgvMaterial.Enabled = True
    End Sub

    Private Sub btnAssociarMaterialM2_Click(sender As Object, e As EventArgs) Handles btnAssociarMaterialM2.Click

        If Double.TryParse(txtm2Total.Text, QtdeEntrada) Then
            ' QtdeEntrada agora contém o valor convertido com sucesso

            'Calculo de Area de chapa
            ' Me.txtValorMaterialCalculado.Text = TotalValor * QtdeEntrada
            ' Me.txtPesoMaterialCalculado.Text = Peso * QtdeEntrada

            ' TotalValor = TotalValor * QtdeEntrada
            ' Peso = Peso * QtdeEntrada


            SalvarMaterial()

        End If



    End Sub


    Private Function SalvarMaterial()


        Try

            If (DadosArquivoCorrente.IdMaterial <> 0) And (IdMaterial > 0) Then

                Dim dt As DataTable

                dt = cl_BancoDados.CarregarDados("Select CodMatFabricante, IdMontaPeca, idmaterialpeca, descdetal,
IdMaterial, DescDetal,
DescFamilia, CodMatFabricante, CodigoJuridicoMat,
d_e_l_e_t_e, Valor, pecaqtde, Peso
From viewmontapeca1
Where CodMatFabricante = '" & DadosArquivoCorrente.NomeArquivoSemExtensao.Trim & "'
And (d_e_l_e_t_e IS NOT NULL or d_e_l_e_t_e is null)
order by descdetal")

                For i As Integer = 0 To dt.Rows.Count - 1

                    If dt.Rows(i)("idmaterial") = IdMaterial Then

                        MsgBox("Este material já esta lançado para este desenho, você podera excluir a lançar a nova quantidade!", vbInformation, "Atenção")

                        Exit Function

                    End If

                Next


                cl_BancoDados.Salvar("insert into montapeca (CodMatFabricante,TipoPeca,IdMaterial, PecaQtde, IdMaterialPeca, Peso, Valor)
                                                                         values ('" & DadosArquivoCorrente.NomeArquivoSemExtensao.Trim & "','0','" & IdMaterial & "','" _
                                                                                     & Replace(QtdeEntrada, ",", ".") & "','" _
                                                                                     & DadosArquivoCorrente.IdMaterial & "','" & Replace(Peso, ",", ".") & "','" & Replace(TotalValor, ",", ".") & "')")
                MsgBox("Material Inserido com Sucesso!", vbInformation, "Salvamento com Sucesso!")

                MyTaskPanelHost.TimerMontaPeca.Enabled = True


            End If

        Catch ex As Exception

            '     MsgBox(ex.Message & " ERRO do monta peça", vbCritical, "Atenção")

        Finally

        End Try

    End Function

    Private Sub dgvMaterial_Click(sender As Object, e As EventArgs) Handles dgvMaterial.Click

        If DadosArquivoCorrente.IdMaterial <> 0 Then

            IdMaterial = dgvMaterial.CurrentRow.Cells("IdMaterial").Value

            If Double.TryParse(txtm2Total.Text, QtdeEntrada) Then
                ' QtdeEntrada agora contém o valor convertido com sucesso

                Try
                    IdMaterial = If(IsDBNull(dgvMaterial.CurrentRow.Cells("IdMaterial").Value), 0, Convert.ToInt32(dgvMaterial.CurrentRow.Cells("IdMaterial").Value))
                    TotalValor = If(IsDBNull(dgvMaterial.CurrentRow.Cells("TotalValor").Value), 0, Convert.ToDouble(dgvMaterial.CurrentRow.Cells("TotalValor").Value))
                    Peso = If(IsDBNull(dgvMaterial.CurrentRow.Cells("Peso").Value), 0, Convert.ToDouble(dgvMaterial.CurrentRow.Cells("Peso").Value))
                Catch ex As Exception
                    ' Em caso de erro, as variáveis já estão com os valores padrão
                End Try


                'Calculo de Area de chapa
                Me.txtValorMaterialCalculado.Text = TotalValor * QtdeEntrada
                Me.txtPesoMaterialCalculado.Text = Peso * QtdeEntrada

                TotalValor = TotalValor * QtdeEntrada
                Peso = Peso * QtdeEntrada

                txtPesoMaterialCalculado.Text = Peso
                txtValorMaterialCalculado.Text = TotalValor
            Else

                ' Caso o valor não seja um número válido, QtdeEntrada permanece 0
                MsgBox("Valor inválido. Por favor, insira um número válido.", vbExclamation, "Atenção")

            End If

        End If

    End Sub

    Private Sub btnAssociarMaterialML_Click(sender As Object, e As EventArgs) Handles btnAssociarMaterialML.Click

        If Double.TryParse(txtMetroLinearotal.Text, QtdeEntrada) Then
            ' QtdeEntrada agora contém o valor convertido com sucesso

            'Calculo de Area de chapa
            QtdeEntrada = Me.txtMetroLinearotal.Text
            'Me.txtPesoMaterialCalculado.Text = Peso * QtdeEntrada


            TotalValor = TotalValor * QtdeEntrada
            Peso = Peso * QtdeEntrada


            SalvarMaterial()

        End If


    End Sub

    Private Sub btnRelacao1para1_Click(sender As Object, e As EventArgs) Handles btnRelacao1para1.Click

        If Double.TryParse(txtQtde1para1.Text, QtdeEntrada) Then
            ' QtdeEntrada agora contém o valor convertido com sucesso

            'Calculo de Area de chapa
            ' Me.txtMetroLinearotal.Text = TotalValor * QtdeEntrada
            'Me.txtPesoMaterialCalculado.Text = Peso * QtdeEntrada

            TotalValor = TotalValor * QtdeEntrada
            Peso = Peso * QtdeEntrada

            SalvarMaterial()

        End If

    End Sub

    Private Sub frmMateriaisAlmoxarifado_Closed(sender As Object, e As EventArgs) Handles Me.Closed

        ' Obter o documento ativo
        swModel = swapp.ActiveDoc
        DadosArquivoCorrente.EnderecoArquivo = swModel.GetPathName().ToUpper()
        DadosArquivoCorrente.NomeArquivoComExtensao = Path.GetFileName(DadosArquivoCorrente.EnderecoArquivo).ToUpper()
        DadosArquivoCorrente.NomeArquivoSemExtensao = Path.GetFileNameWithoutExtension(DadosArquivoCorrente.EnderecoArquivo)


        '   MyTaskPanelHost.TimerMontaPeca.Enabled = True


    End Sub

    Private Sub dgvMaterial_CellContentClick(sender As Object, e As Windows.Forms.DataGridViewCellEventArgs) Handles dgvMaterial.CellContentClick

    End Sub
End Class