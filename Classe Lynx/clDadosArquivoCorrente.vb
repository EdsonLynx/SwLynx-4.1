Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Data.SqlTypes
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.Net
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports Google.Protobuf.WellKnownTypes
Imports Microsoft.Office.Interop.Excel
Imports MySql.Data.MySqlClient
Imports Mysqlx.Crud
Imports netDxf
Imports SolidWorks.Interop.sldworks
Imports SolidWorks.Interop.swconst
Imports SolidWorks.Interop.swpublished
Imports SolidWorksTools
Imports SolidWorksTools.File
Imports SwLynx_4._1.My

Imports iText.Kernel.Pdf
Imports iText.Kernel.Pdf.Canvas
Imports iText.Kernel.Font
Imports iText.IO.Font.Constants
Imports SolidWorks.Interop.dsgnchk
Imports System.Data.SqlClient
Imports ZstdSharp.Unsafe


Public Class ClDadosArquivoCorrente

    Public IdMaterial As Integer
    Public NomeArquivoComExtensao As String
    Public NomeArquivoSemExtensao As String
    Public EnderecoArquivo As String

    Public EnderecoArquivoAterior As String

    Public Extencao As String

    Public DataCriacaDesenho As String
    Public DataUltimoSalvamento As String
    Public SalvoUltimaVezPor As String

    Public Titulo As String
    Public AssuntoSubiTitulo As String
    Public Comentarios As String
    Public Author As String
    Public PalavraChave As String

    'Processo
    Public soldagem As String

    Public Acabamento As String
    Public TipoDesenho As String
    Public Corte As String
    Public Dobra As String
    Public Solda As String
    Public Pintura As String
    Public Montagem As String
    Public ItemEstoque As String
    Public rnc As String
    'Public Sobra_Fabrica As String
    Public qtde As String

    Public ArquivoPdf As String
    Public ArquivoDxf As String
    Public ArquivoDft As String
    Public ArquivoLXDS As String

    'Caixa Delimitadora
    Public Profundidadeaixadelimitadora As String

    Public Larguracaixadelimitadora As String
    Public Alturacaixadelimitadora As String

    'Lista de Corte
    Public ComprimentoBlank As String = Nothing

    Public LarguraBlank As String = Nothing
    Public Espessura As String = Nothing
    Public PerimetroCorteExterno As String
    Public PerimetroCorteInterno As String
    Public NumeroDobras As String
    Public Massa As String
    Public material As String
    Public AreaPintura As String

    Public propertyName As String
    Public propertyValue As String
    Public propertyNames As Object = Nothing
    Public propertyValues As Object = Nothing
    Public propertyTypes As Object = Nothing
    Public wasResolvedLC As Object = Nothing
    Public NomePropriedadeListCut As String
    Public DescricaoPendencia As String
    Public Sobra_Fabrica As String

    Public EnderecoFichaTecnica As String
    Public EnderecoIsometrico As String



    Public Function ArquivoCorrente(ByVal swModel As ModelDoc2) As Boolean


        MyTaskPanelHost.LimparTelaVariaveis()

        IntanciaSolidWorks.ConectarSolidWorks()
        ' swApparq = CreateObject("SldWorks.Application")

        swModel = swapp.ActiveDoc
        'swModel = swApparq.ActiveDoc

        If swModel Is Nothing Then

            Exit Function
        Else

            ' Obtém o tipo do documento atual
            Dim docType As Integer
            docType = swModel.GetType()

            EnderecoArquivoAterior = swModel.GetPathName.ToUpper

            ' Declara uma variável para armazenar o manipulador do documento
            Dim docHandler As Object

            ' Usa Select Case para diferenciar o tipo do documento
            Select Case docType
                Case swDocumentTypes_e.swDocPART

                    docHandler = New PartEventHandler()
                    ' Adicione o código específico para manipular documentos de peça aqui
                    LendoDadosComunsPartAssembly(swModel)

                Case swDocumentTypes_e.swDocASSEMBLY

                    docHandler = New AssemblyEventHandler()
                    ' Adicione o código específico para manipular documentos de montagem aqui
                    LendoDadosComunsPartAssembly(swModel)

                Case swDocumentTypes_e.swDocDRAWING

                    docHandler = New DrawingEventHandler()
                    ' Adicione o código específico para manipular documentos de desenho aqui

                Case Else

                    ' Tipo de documento não reconhecido
                    Return False

            End Select

            ' Se chegou aqui, o tipo de documento foi reconhecido
            Return True

        End If

        ' MyTaskPanelHost.TimerMontaPeca.Enabled = True

    End Function

    Public Function LendoDadosComunsPartAssembly(ByRef swModel As ModelDoc2) As Boolean

        Try


            IntanciaSolidWorks.ConectarSolidWorks()
            swModel = swapp.ActiveDoc

            If swModel Is Nothing Then

                Exit Function
                Return False
            End If

            If swModel.GetType() = swDocumentTypes_e.swDocPART Or swModel.GetType() = swDocumentTypes_e.swDocASSEMBLY Then
                swModel.Visible = True
                swModelDocExt = swModel.Extension
                MyMassProp = swModelDocExt.CreateMassProperty

                EnderecoArquivo = swModel.GetPathName().ToUpper()
                NomeArquivoComExtensao = Path.GetFileName(EnderecoArquivo).ToUpper()
                NomeArquivoSemExtensao = Path.GetFileNameWithoutExtension(EnderecoArquivo)

                ' Manipulação de arquivos
                ArquivoDxf = Replace(EnderecoArquivo, ".SLDASM", ".DXF").Replace(".SLDPRT", ".DXF")
                ArquivoPdf = Replace(EnderecoArquivo, ".SLDASM", ".PDF").Replace(".SLDPRT", ".PDF")
                ArquivoDft = Replace(EnderecoArquivo, ".SLDASM", ".DFT").Replace(".SLDPRT", ".DFT")

                ' Obtenção de informações
                Titulo = swModel.SummaryInfo(swSummInfoField_e.swSumInfoTitle)
                AssuntoSubiTitulo = swModel.SummaryInfo(swSummInfoField_e.swSumInfoSubject)
                Comentarios = swModel.SummaryInfo(swSummInfoField_e.swSumInfoComment)
                Author = swModel.SummaryInfo(swSummInfoField_e.swSumInfoAuthor)
                PalavraChave = swModel.SummaryInfo(swSummInfoField_e.swSumInfoKeywords)
                DataCriacaDesenho = swModel.SummaryInfo(swSummInfoField_e.swSumInfoCreateDate).ToString()
                DataUltimoSalvamento = swModel.SummaryInfo(swSummInfoField_e.swSumInfoSaveDate).ToString()
                SalvoUltimaVezPor = swModel.SummaryInfo(swSummInfoField_e.swSumInfoSavedBy).ToUpper()

                ' Cálculo da área de pintura
                AreaPintura = If(MyMassProp IsNot Nothing, MyMassProp.SurfaceArea.ToString("F2", Globalization.CultureInfo.InvariantCulture), "0.00")

                EnderecoFichaTecnica = cl_BancoDados.RetornaCampoDaPesquisa("Select EnderecoFichaTecnica from  " & ComplementoTipoBanco & "material where CodMatFabricante = '" & NomeArquivoSemExtensao & "'", "EnderecoFichaTecnica")
                EnderecoIsometrico = cl_BancoDados.RetornaCampoDaPesquisa("Select EnderecoIsometrico from  " & ComplementoTipoBanco & "material where CodMatFabricante = '" & NomeArquivoSemExtensao & "'", "EnderecoIsometrico")


                ' Obtendo propriedades personalizadas
                If swModel IsNot Nothing Then
                    swCustProp = swModelDocExt.CustomPropertyManager("")
                    ' Try

                    soldagem = GetCustomProperty(swCustProp, "txtsoldagem")
                    Acabamento = GetCustomProperty(swCustProp, "txtacabamento")
                    TipoDesenho = GetCustomProperty(swCustProp, "txtTipoDesenho")
                    Corte = GetCustomProperty(swCustProp, "txtcorte")
                    Dobra = GetCustomProperty(swCustProp, "txtdobra")
                    Solda = GetCustomProperty(swCustProp, "txtsolda")
                    Pintura = GetCustomProperty(swCustProp, "txtpintura")
                    Montagem = GetCustomProperty(swCustProp, "txtmontagem")
                    ItemEstoque = GetCustomProperty(swCustProp, "txtitemestoque")

                    ' Catch ex As Exception
                    ' Registrar a exceção, se necessário
                    'End Try'
                    'R'eturn True




                End If
            End If

            Return False


            ' PercorrerPropriedadesDaListaDeCorte(swModel)

        Catch ex As Exception


            '  MsgBox(ex.Message & " Erro ao abrir")

        Finally

        End Try
    End Function


    Private Sub LimparPropriedadesListaDeCorte()
        ' Reseta todas as propriedades para seus valores padrões
        ComprimentoBlank = ""
        LarguraBlank = ""
        Espessura = ""
        PerimetroCorteExterno = ""
        PerimetroCorteInterno = ""
        NumeroDobras = ""
        Massa = ""
        material = ""
    End Sub

    Public Function PercorrerPropriedadesDaListaDeCorte(ByVal swModel As ModelDoc2) As Boolean

        Try


            swModel = swapp.ActiveDoc

            ' Verifica se o modelo foi aberto corretamente
            If swModel Is Nothing Then
                ' Retorna falso se o modelo não for válido
                Return False
            End If




            ' Verifica se o documento é do tipo peça
            If swModel.GetType() <> swDocumentTypes_e.swDocPART Then
                ' Caso não seja uma peça, limpa as propriedades e retorna falso
                LimparPropriedadesListaDeCorte()

                Massa = GetCustomProperty(swCustProp, "Peso")
                ' material = GetCustomProperty(swCustProp, "material")


                Try

                    If IsNumeric(Massa) = False Then

                        Massa = "0"

                    End If

                Catch ex As Exception
                    Massa = "0"
                Finally

                End Try


                Exit Function

            Else



                Try


                    material = GetCustomProperty(swCustProp, "material")


                    ' O nome do material é o primeiro valor no array de propriedades
                    ' material = materialProperties(0) ' O nome do material é armazenado no índice 0

                Catch ex As Exception

                    material = ""

                End Try

                ' MsgBox(material)



                Try
                    ' Converte o documento em um PartDoc para acessar as funcionalidades da peça
                    Dim part As PartDoc = CType(swModel, PartDoc)

                    ' Força a reconstrução para garantir que a lista de corte esteja atualizada
                    ' swModel.ForceRebuild3(False) ' Força a reconstrução
                    swModel.ForceRebuild3(True)

                    Dim cutListFeature As Feature = part.FirstFeature()

                    Dim feat As Feature = swModel.FirstFeature()
                    Dim cutListFolder As Object

                    Espessura = "0"

                    While cutListFeature IsNot Nothing

                        If cutListFeature.GetTypeName() = "CutListFolder" Or
                            cutListFeature.GetTypeName() = "Lista de corte(1)" Or
                             cutListFeature.GetTypeName().Contains("Item da lista de") Then

                            cutListFolder = cutListFeature.GetSpecificFeature2()

                            cutListFolder.UpdateCutList() ' Força a atualização da lista de corte
                            ' cutListFolder.SetAutomaticUpdate(True) ' Força a atualização automática da lista de corte
                            cutListFolder.SetAutomaticUpdate(True) ' Força a atualização automática da lista de corte

                            ' Obtém as propriedades da lista de corte
                            Dim cutListProperties As CustomPropertyManager = cutListFeature.CustomPropertyManager()

                            ' Captura todas as propriedades da lista de corte
                            cutListProperties.GetAll3(propertyNames, propertyValues, propertyTypes, wasResolvedLC, False)

                            ' Percorre as propriedades
                            For i As Integer = 0 To propertyNames.Length - 1
                                propertyName = CType(propertyNames(i), String)
                                propertyValue = CType(propertyValues(i), String)
                                NomePropriedadeListCut = CType(propertyNames(i), String)

                                If String.Equals(NomePropriedadeListCut.ToString(), "Comprimento da Caixa delimitadora", StringComparison.OrdinalIgnoreCase) Or
   String.Equals(NomePropriedadeListCut.ToString(), "Bounding Box Length", StringComparison.OrdinalIgnoreCase) Then

                                    Try
                                        ' Garante que o valor inicial seja tratado como String e evita nulos.
                                        ComprimentoBlank = If(propertyTypes(i)?.ToString(), "0")

                                        ' Verifica se é numérico; caso contrário, atribui "0".
                                        If Not IsNumeric(ComprimentoBlank) Then
                                            ComprimentoBlank = "0"
                                        Else
                                            ' Converte para Decimal e formata com 2 casas decimais usando ponto como separador.
                                            Dim comprimentoNumerico As Decimal = Decimal.Parse(ComprimentoBlank, Globalization.CultureInfo.InvariantCulture)
                                            ComprimentoBlank = comprimentoNumerico.ToString("F2", Globalization.CultureInfo.InvariantCulture)
                                        End If

                                    Catch ex As Exception
                                        ' Em caso de erro, atribui o valor padrão "0.00".
                                        ComprimentoBlank = "0.00"
                                    End Try


                                ElseIf String.Equals(NomePropriedadeListCut.ToString(), "Largura da Caixa delimitadora", StringComparison.OrdinalIgnoreCase) Or
       String.Equals(NomePropriedadeListCut.ToString(), "Bounding Box Width", StringComparison.OrdinalIgnoreCase) Then

                                    Try
                                        ' Garante que o valor inicial seja tratado como String e evita nulos.
                                        LarguraBlank = If(propertyTypes(i)?.ToString(), "0")

                                        ' Verifica se é numérico; caso contrário, atribui "0".
                                        If Not IsNumeric(LarguraBlank) Then
                                            LarguraBlank = "0"
                                        Else
                                            ' Converte para Decimal e formata com 2 casas decimais usando ponto como separador.
                                            Dim larguraNumerica As Decimal = Decimal.Parse(LarguraBlank, Globalization.CultureInfo.InvariantCulture)
                                            LarguraBlank = larguraNumerica.ToString("F2", Globalization.CultureInfo.InvariantCulture)
                                        End If

                                    Catch ex As Exception
                                        ' Em caso de erro, atribui o valor padrão "0.00".
                                        LarguraBlank = "0.00"
                                    End Try


                                ElseIf String.Equals(NomePropriedadeListCut.ToString(), "Espessura da Chapa metálica", StringComparison.OrdinalIgnoreCase) Or
       String.Equals(NomePropriedadeListCut.ToString(), "Sheet Metal Thickness", StringComparison.OrdinalIgnoreCase) Then

                                    Try
                                        ' Garante que o valor inicial seja tratado como String e evita nulos.
                                        Espessura = If(propertyTypes(i)?.ToString(), "0")

                                        ' Se não estiver vazio, formata como número com ponto como separador decimal.
                                        If Not String.IsNullOrWhiteSpace(Espessura) Then
                                            Dim espessuraNumerica As Decimal = Decimal.Parse(Espessura, Globalization.CultureInfo.InvariantCulture)
                                            Espessura = espessuraNumerica.ToString("F2", Globalization.CultureInfo.InvariantCulture)
                                        Else
                                            Espessura = "0.00" ' Formato padrão com ponto.
                                        End If

                                    Catch ex As Exception
                                        ' Caso ocorra erro, retorna valor padrão formatado.
                                        Espessura = "0.00"
                                    End Try
                                ElseIf String.Equals(NomePropriedadeListCut.ToString(), "Perímetro de corte-Externo", StringComparison.OrdinalIgnoreCase) Or
       String.Equals(NomePropriedadeListCut.ToString(), "Cutting Length-Outer", StringComparison.OrdinalIgnoreCase) Then

                                    Try
                                        ' Garante que o valor inicial seja tratado como String e evita nulos.
                                        PerimetroCorteExterno = If(propertyTypes(i)?.ToString(), "0")

                                        ' Verifica se o valor é numérico.
                                        If Not IsNumeric(PerimetroCorteExterno) Then
                                            PerimetroCorteExterno = "0"
                                        Else
                                            ' Converte para Decimal e formata com 2 casas decimais usando ponto como separador.
                                            Dim perimetroNumerico As Decimal = Decimal.Parse(PerimetroCorteExterno, Globalization.CultureInfo.InvariantCulture)
                                            PerimetroCorteExterno = perimetroNumerico.ToString("F2", Globalization.CultureInfo.InvariantCulture)
                                        End If

                                    Catch ex As Exception
                                        ' Em caso de erro, atribui o valor padrão "0.00".
                                        PerimetroCorteExterno = "0.00"
                                    End Try

                                ElseIf String.Equals(NomePropriedadeListCut.ToString(), "Perímetro interno de corte", StringComparison.OrdinalIgnoreCase) Or
       String.Equals(NomePropriedadeListCut.ToString(), "Cutting Length-Inner", StringComparison.OrdinalIgnoreCase) Then

                                    Try
                                        ' Garante que o valor inicial seja tratado como String e evita nulos.
                                        PerimetroCorteInterno = If(propertyTypes(i)?.ToString(), "0")

                                        ' Verifica se o valor é numérico.
                                        If Not IsNumeric(PerimetroCorteInterno) Then
                                            PerimetroCorteInterno = "0"
                                        Else
                                            ' Converte para Decimal e formata com 2 casas decimais usando ponto como separador.
                                            Dim perimetroNumerico As Decimal = Decimal.Parse(PerimetroCorteInterno, Globalization.CultureInfo.InvariantCulture)
                                            PerimetroCorteInterno = perimetroNumerico.ToString("F2", Globalization.CultureInfo.InvariantCulture)
                                        End If

                                    Catch ex As Exception
                                        ' Em caso de erro, atribui o valor padrão "0.00".
                                        PerimetroCorteInterno = "0.00"
                                    End Try

                                ElseIf String.Equals(NomePropriedadeListCut.ToString(), "Dobras", StringComparison.OrdinalIgnoreCase) Or
        String.Equals(NomePropriedadeListCut.ToString(), "Bends", StringComparison.OrdinalIgnoreCase) Then

                                    Try
                                        ' Garante que o valor inicial seja tratado como String e evita nulos.
                                        NumeroDobras = If(propertyTypes(i)?.ToString(), "0")

                                        ' Verifica se o valor é numérico.
                                        If Not IsNumeric(NumeroDobras) Then
                                            NumeroDobras = "0"
                                        Else
                                            ' Converte para Integer e garante que seja um número inteiro.
                                            Dim numeroDobrasInt As Integer = Convert.ToInt32(Decimal.Parse(NumeroDobras, Globalization.CultureInfo.InvariantCulture))
                                            NumeroDobras = numeroDobrasInt.ToString()
                                        End If

                                    Catch ex As Exception
                                        ' Em caso de erro, atribui o valor padrão "0".
                                        NumeroDobras = "0"
                                    End Try


                                ElseIf String.Equals(NomePropriedadeListCut.ToString(), "Massa", StringComparison.OrdinalIgnoreCase) Or
       String.Equals(NomePropriedadeListCut.ToString(), "Mass", StringComparison.OrdinalIgnoreCase) Then

                                    Try
                                        ' Garante que o valor inicial seja tratado como String e evita nulos.
                                        Massa = If(propertyTypes(i)?.ToString(), "0")

                                        ' Verifica se o valor é numérico.
                                        If Not IsNumeric(Massa) Then
                                            Massa = "0"
                                        Else
                                            ' Converte para Decimal e formata com 2 casas decimais usando ponto como separador.
                                            Dim massaNumerica As Decimal = Decimal.Parse(Massa, Globalization.CultureInfo.InvariantCulture)
                                            Massa = massaNumerica.ToString("F2", Globalization.CultureInfo.InvariantCulture)
                                        End If

                                    Catch ex As Exception
                                        ' Em caso de erro, atribui o valor padrão "0.00".
                                        Massa = "0.00"
                                    End Try

                                ElseIf String.Equals(NomePropriedadeListCut.ToString(), "material", StringComparison.OrdinalIgnoreCase) Then

                                    ' Garante que o valor inicial seja tratado como String e evita nulos.
                                    material = If(propertyTypes(i)?.ToString(), String.Empty)

                                    ' Se o valor for "True", redefine como uma string vazia.
                                    If String.Equals(material, "True", StringComparison.OrdinalIgnoreCase) Then
                                        material = String.Empty
                                    End If

                                End If

                            Next

                            'cutListFolder.UpdateCutList() ' Força a atualização da lista de corte
                            cutListFolder.SetAutomaticUpdate(False) ' Força a atualização automática da lista de corte


                        End If


                        cutListFeature = cutListFeature.GetNextFeature()

                        '  Exit While

                    End While

                    If Espessura.ToString = "" Or Espessura.ToString = "0" Or Espessura = Nothing Then

                        ComprimentoBlank = ""
                        LarguraBlank = ""
                        Espessura = ""
                        PerimetroCorteExterno = ""
                        PerimetroCorteInterno = ""
                        NumeroDobras = ""
                        Massa = ""
                        material = ""

                    End If


                Catch ex As Exception
                    ComprimentoBlank = ""
                    LarguraBlank = ""
                    Espessura = ""
                    PerimetroCorteExterno = ""
                    PerimetroCorteInterno = ""
                    NumeroDobras = ""
                    Massa = ""
                    material = ""

                Finally
                End Try

            End If

        Catch ex As Exception
        Finally
        End Try

    End Function

    Public Sub LerDadosCaixaDelimitadora(ByVal swModel As ModelDoc2)

        Try

            If My.Settings.CaixaDelimitadora = "SIM" Then

                'So se aplica caixa delimitadora para Metta
                ' If My.Settings.BancoDadosAtivo = "mettapaineis" Then

                '  Dim swApp As Object
                '  Dim swModel As Object
                ' Dim swModelDocExt As SolidWorks.Interop.sldworks.ModelDocExtension
                Dim swConfMgr As SolidWorks.Interop.sldworks.ConfigurationManager
                Dim swConf As SolidWorks.Interop.sldworks.Configuration
                Dim swCustPropMgr As SolidWorks.Interop.sldworks.CustomPropertyManager
                ' Dim bool As Boolean
                Dim valout As String = String.Empty
                Dim success As Boolean = False

                'swModel.Rebuild(2)

                ' Conectar ao SolidWorks
                'IntanciaSolidWorks.ConectarSolidWorks()

                ' Obter o documento ativo
                swModel = swapp.ActiveDoc

                Try

                    ' Verifique se o swModel foi aberto com sucesso
                    If Not swModel Is Nothing Then

                        ' Usa Select Case para diferenciar o tipo do documento
                        If swModel.GetType() = swDocumentTypes_e.swDocPART Or swModel.GetType() = swDocumentTypes_e.swDocASSEMBLY Then

                            ' Obtém o gerenciador de recursos da peça
                            Dim swFeatMgr As FeatureManager = swModel.FeatureManager
                            If swFeatMgr Is Nothing Then
                                MessageBox.Show("Erro ao obter o gerenciador de recursos da peça.")
                                ' Return
                            End If

                            ' Cria a definição da caixa delimitadora
                            Dim swFeatData As Object = swFeatMgr.CreateDefinition(swFeatureNameID_e.swFmBoundingBox)
                            If swFeatData Is Nothing Then
                                MessageBox.Show("Erro ao criar a definição da caixa delimitadora.")
                                ' Return
                            End If

                            ' Configura as propriedades da caixa delimitadora
                            swFeatData.IncludeHiddenBodies = False
                            swFeatData.IncludeSurfaces = False
                            swFeatData.ReferenceFaceOrPlane = 1

                            ' Cria a caixa delimitadora
                            Dim swFeat As Feature = swFeatMgr.CreateFeature(swFeatData)
                            If swFeat Is Nothing Then
                                ' MessageBox.Show("Erro ao criar a caixa delimitadora.")
                                ' Return
                            End If

                            ' Limpa a seleção
                            swModel.ClearSelection2(True)

                            swConfMgr = swModel.ConfigurationManager

                            ' Obtém a configuração ativa
                            swConf = swConfMgr.ActiveConfiguration

                            ' Obtém o gerenciador de propriedades personalizadas da configuração ativa
                            swCustPropMgr = swModelDocExt.CustomPropertyManager(swConf.Name)

                            '' Verifica se o gerenciador de propriedades personalizadas foi obtido com sucesso
                            'If swCustPropMgr Is Nothing Then
                            '    Return "Erro ao obter o gerenciador de propriedades personalizadas da configuração ativa."
                            'End If

                            ' Tenta obter a propriedade "Comprimento total da caixa delimitadora"
                            success = swCustPropMgr.Get4("Comprimento total da caixa delimitadora", False, valout, valout)

                            ' Atribui o valor de valout a Alturacaixadelimitadora com formatação de 2 casas decimais, se for numérico
                            Alturacaixadelimitadora = If(success AndAlso IsNumeric(valout), Convert.ToDecimal(valout).ToString("F2", Globalization.CultureInfo.InvariantCulture), "")

                            ' Tenta obter a propriedade "Largura total da caixa delimitadora"
                            success = swCustPropMgr.Get4("Largura total da caixa delimitadora", False, valout, valout)

                            ' Atribui o valor de valout a Larguracaixadelimitadora com formatação de 2 casas decimais, se for numérico
                            Larguracaixadelimitadora = If(success AndAlso IsNumeric(valout), Convert.ToDecimal(valout).ToString("F2", Globalization.CultureInfo.InvariantCulture), "")

                            ' Tenta obter a propriedade "Espessura total da caixa delimitadora"
                            success = swCustPropMgr.Get4("Espessura total da caixa delimitadora", False, valout, valout)

                            ' Atribui o valor de valout a Profundidadeaixadelimitadora com formatação de 2 casas decimais, se for numérico
                            Profundidadeaixadelimitadora = If(success AndAlso IsNumeric(valout), Convert.ToDecimal(valout).ToString("F2", Globalization.CultureInfo.InvariantCulture), "")
                        End If
                    End If

                    '  ExcluirCaixaDelimitadora(swModel)
                Catch ex As Exception
                    '  Return "Ocorreu um problema: " & ex.Message
                Finally

                End Try

            ElseIf My.Settings.CaixaDelimitadora = "NÃO" Then

                ExcluirCaixaDelimitadora(swModel)

            End If
        Catch ex As Exception
        Finally

        End Try
    End Sub

    Public Sub ExcluirCaixaDelimitadora(ByVal swModel As ModelDoc)

        Try


            ' Conectar ao SolidWorks
            'IntanciaSolidWorks.ConectarSolidWorks()

            ' Obter o documento ativo
            swModel = swapp.ActiveDoc

            Try

                ' Verifique se o swModel foi aberto com sucesso
                If Not swModel Is Nothing Then

                    ' Usa Select Case para diferenciar o tipo do documento
                    If swModel.GetType() = swDocumentTypes_e.swDocPART Or swModel.GetType() = swDocumentTypes_e.swDocASSEMBLY Then

                        ' Selecionar a feature da caixa delimitadora
                        Dim boolstatus As Boolean
                        boolstatus = swModel.Extension.SelectByID2("Caixa delimitadora", "BBOXSKETCH", 0, 0, 0, False, 0, Nothing, 0)

                        If Not boolstatus Then
                            ' MessageBox.Show("Não foi possível selecionar a caixa delimitadora.")
                            ' Return
                        End If

                        ' Excluir a feature selecionada
                        swModel.EditDelete()

                        ' Ocultar o esboço selecionado
                        swModel.BlankSketch()

                        ' Limpar a seleção
                        swModel.ClearSelection2(True)

                        '  MessageBox.Show("Caixa delimitadora excluída com sucesso.")

                    End If
                End If
            Catch ex As Exception
                ' Finally
            End Try

        Catch ex As Exception
        Finally
        End Try

    End Sub

    Public Function GetCustomProperty(swModelDocExt As CustomPropertyManager, propertyName As String) As String
        Try
            Dim bool As Boolean = False
            Dim valout As String = ""

            bool = swModelDocExt.Get4(propertyName, False, valout, valout)
            If bool Then
                Return valout
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function



    Public Sub GarantirOuCriarPropriedade(ByVal swModel As ModelDoc2, ByVal nomePropriedade As String, ByVal valorPadrao As String, ByRef VariavelRecebeValor As String)

        Try
            ' Verifique se o modelo é válido
            If swModel IsNot Nothing Then
                ' Obter o tipo do documento
                Dim modelType As Integer = swModel.GetType()

                ' Verifique se é uma peça ou uma montagem
                If modelType = swDocumentTypes_e.swDocPART OrElse modelType = swDocumentTypes_e.swDocASSEMBLY Then

                    ' Obtenha o gerenciador de propriedades personalizadas
                    Dim swCustPropMgr As CustomPropertyManager = swModel.Extension.CustomPropertyManager("")

                    ' Verifique se a propriedade já existe
                    Dim valorAtual As String = ""
                    Dim foiResolvido As Boolean = swCustPropMgr.Get4(nomePropriedade, False, valorAtual, valorAtual)

                    ' Atualizar ou criar a propriedade com o valor fornecido pelo usuário
                    swCustPropMgr.Add3(nomePropriedade, swCustomInfoType_e.swCustomInfoText, valorPadrao, swCustomPropertyAddOption_e.swCustomPropertyReplaceValue)

                    ' Define a variável com o valor informado pelo usuário
                    VariavelRecebeValor = valorPadrao
                Else
                    Throw New Exception("O modelo não é uma peça nem uma montagem.")
                End If
            Else
                Throw New Exception("O modelo fornecido é inválido (Nothing).")
            End If

        Catch ex As Exception
            ' Relatar o erro (se necessário, pode incluir logs aqui)
            Console.WriteLine($"Erro ao processar propriedade: {ex.Message}")
            VariavelRecebeValor = ""
        End Try

    End Sub

    Public Function VerificarProcessodaPecaCorrente(ByVal swModel As ModelDoc2, ByVal msg As Boolean) As Boolean

        VerificarProcessodaPecaCorrente = True

        Try



            Dim MensagemErros As String = "Lista de Ações Pendentes no Arquivo: " & DadosArquivoCorrente.NomeArquivoSemExtensao & vbCrLf & vbCrLf & vbCrLf

            'Usa Select Case para diferenciar o tipo do documento
            If swModel.GetType() = swDocumentTypes_e.swDocPART Then

                If DadosArquivoCorrente.NumeroDobras <> "" And DadosArquivoCorrente.TipoDesenho <> "CHAPARIA" Then

                    MensagemErros = MensagemErros & "Erro 01 - Peças com Dobras são consideraras chapa, favor verificar os dados do cadastro da peça, o tipo de desenho deve ser 'CHAPARIA'" & vbCrLf


                    VerificarProcessodaPecaCorrente = False


                End If

                If DadosArquivoCorrente.TipoDesenho = "" Then

                    MensagemErros = MensagemErros & "Erro 02 - O tipo de desenho não foi informado, favor informar!" & vbCrLf



                    VerificarProcessodaPecaCorrente = False


                End If

                If DadosArquivoCorrente.TipoDesenho = "CHAPARIA" Then

                    If (DadosArquivoCorrente.Espessura = "" Or DadosArquivoCorrente.Espessura = "0.00") And DadosArquivoCorrente.Corte <> "1" Then

                        MensagemErros = MensagemErros & "Erro 03 - Como o tipo de desenho esta marcado como 'Chaparia' e obriogatorio a criação da Lista de corte" & vbCrLf

                        VerificarProcessodaPecaCorrente = False


                    End If

                    If DadosArquivoCorrente.Corte <> "1" Then

                        MensagemErros = MensagemErros & "Erro 04 - O Setor de corte deve estar marcado" & vbCrLf

                        VerificarProcessodaPecaCorrente = False


                    End If


                    If DadosArquivoCorrente.Espessura.ToString = "" Then


                        MensagemErros = MensagemErros & "Erro 05 - Com o tipo de desenho como 'CHAPARIA' a espessura deve ser infomada, crie a lista de corte!" & vbCrLf

                        VerificarProcessodaPecaCorrente = False


                    End If


                End If

                If DadosArquivoCorrente.TipoDesenho <> "CHAPARIA" Then

                    If DadosArquivoCorrente.Corte = "" And
                            DadosArquivoCorrente.Solda = Dobra = "" And
                            DadosArquivoCorrente.Pintura = "" And
                            DadosArquivoCorrente.Montagem = "" Then

                        MsgBox("E necessario a indição de pelo menos (1)um processo!")

                    Else

                        VerificarProcessodaPecaCorrente = False

                    End If

                End If

            ElseIf swModel.GetType() = swDocumentTypes_e.swDocASSEMBLY Then

                If DadosArquivoCorrente.TipoDesenho = "CHAPARIA" Then

                    MensagemErros = MensagemErros & "Erro 08 - Os arquivos com extensão 'SLDASM' conjuntos do SolidWorks, não podem ser do tipo 'CHAPARIA'" & vbCrLf

                    VerificarProcessodaPecaCorrente = False


                End If

                If DadosArquivoCorrente.TipoDesenho = "" Then

                    MensagemErros = MensagemErros & "Erro 09 - O tipo de desenho e de preenchimento Obrigatorio" & vbCrLf

                    VerificarProcessodaPecaCorrente = False


                End If

                If DadosArquivoCorrente.Corte = "" And
                      DadosArquivoCorrente.Dobra = "" And
                      DadosArquivoCorrente.Solda = "" And
                      DadosArquivoCorrente.Pintura = "" And
                      DadosArquivoCorrente.Montagem = "" Then

                    MensagemErros = MensagemErros & "Erro 10 - Os arquivos com extensão 'SLDASM' deve conter pelomenos algum processo!" & vbCrLf

                    VerificarProcessodaPecaCorrente = False



                End If

            End If

            Dim qtdernc As Integer = Convert.ToInt16(cl_BancoDados.RetornaCampoDaPesquisa("SELECT count(idordemservicoitempendencia) as qtdernc 
                    FROM  " & ComplementoTipoBanco & "ordemservicoitempendencia where
                              (Estatus <> 'FINALIZADA' OR  Estatus =  '' OR  Estatus  IS NULL) 
                    and CodMatFabricante = '" & DadosArquivoCorrente.NomeArquivoSemExtensao & "';", "qtdernc"))

            If qtdernc > 0 Then

                MensagemErros = MensagemErros & "Erro 11 - Existem RNC's em aberto no arquivo corrente!" & vbCrLf

                VerificarProcessodaPecaCorrente = False

            End If

            If msg = True Then

                If VerificarProcessodaPecaCorrente = False Then

                    MessageBox.Show(MensagemErros, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error)

                End If

            End If

        Catch ex As Exception

            '  VerificarProcessodaPecaCorrente = False

        Finally

        End Try



    End Function

    Public Sub AtualizaDesenho(ByVal swModel As ModelDoc2)

        If TipoBanco = "MYSQL" Then

            Try


                Dim dt As System.Data.DataTable
                Dim query As String
                Dim isUpdate As Boolean = False

                dt = cl_BancoDados.CarregarDados("Select CodMatFabricante FROM  " & ComplementoTipoBanco & "material WHERE PecaManuFat = 'S' and CodMatFabricante = '" & DadosArquivoCorrente.NomeArquivoSemExtensao & "'")

                ' Verifica se o DataTable tem pelo menos uma linha
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    ' Verifica se o valor da coluna "CodMatFabricante" não é DBNull
                    If dt.Rows(0)("CodMatFabricante") IsNot DBNull.Value Then
                        isUpdate = True
                    Else
                        isUpdate = False
                    End If
                Else
                    isUpdate = False
                End If


                ' Definir a consulta SQL com base na existência do item
                If isUpdate Then
                    query = "UPDATE material SET DescResumo = @DescResumo, DescDetal = @DescDetal, PecaManuFat = @PecaManuFat, " &
            "Autor = @Autor, Palavrachave = @Palavrachave, Notas = @Notas, Espessura = @Espessura, AreaPintura = @AreaPintura, " &
            "NumeroDobras = @NumeroDobras, Peso = @Peso, Unidade = @Unidade, Altura = @Altura, Largura = @Largura, " &
            "Profundidade = @Profundidade, UsuarioAlteracao = @UsuarioAlteracao, DtAlteracao = @DtCad, CodigoJuridicoMat = @CodigoJuridicoMat, " &
            "StatusMat = @StatusMat, MaterialSW = @MaterialSW, EnderecoArquivo = @EnderecoArquivo, Acabamento = @Acabamento, " &
            "txtSoldagem = @txtSoldagem, txtTipoDesenho = @txtTipoDesenho, txtCorte = @txtCorte, txtDobra = @txtDobra, " &
            "txtSolda = @txtSolda, txtPintura = @txtPintura, txtMontagem = @txtMontagem, Comprimentocaixadelimitadora = @Compcx, " &
            "Larguracaixadelimitadora = @Largcx, Espessuracaixadelimitadora = @Espcx, txtItemEstoque = @txtItemEstoque " &
            "WHERE CodMatFabricante = @CodMatFabricante"
                Else
                    query = "INSERT INTO material (DescResumo, DescDetal, PecaManuFat, Autor, Palavrachave, Notas, Espessura, AreaPintura, NumeroDobras, Peso, " &
            "Unidade, Altura, Largura, Profundidade, CodMatFabricante, DtCad, UsuarioCriacao, UsuarioAlteracao, DtAlteracao, CodigoJuridicoMat, " &
            "StatusMat, MaterialSW, EnderecoArquivo, Acabamento, txtSoldagem, txtTipoDesenho, txtCorte, txtDobra, txtSolda, txtPintura, " &
            "txtMontagem, Comprimentocaixadelimitadora, Larguracaixadelimitadora, Espessuracaixadelimitadora, txtItemEstoque) " &
            "VALUES (@DescResumo, @DescDetal, @PecaManuFat, @Autor, @Palavrachave, @Notas, @Espessura, @AreaPintura, @NumeroDobras, @Peso, @Unidade, " &
            "@Altura, @Largura, @Profundidade, @CodMatFabricante, @DtCad, @UsuarioCriacao, @UsuarioAlteracao, @DtAlteracao, @CodigoJuridicoMat, " &
            "@StatusMat, @MaterialSW, @EnderecoArquivo, @Acabamento, @txtSoldagem, @txtTipoDesenho, @txtCorte, @txtDobra, @txtSolda, @txtPintura, " &
            "@txtMontagem, @Compcx, @Largcx, @Espcx, @txtItemEstoque)"
                End If

                Using cmd As New MySqlCommand(query, myconect)

                    Try


                        ' VerificarProcessodaPecaCorrente(swModel, False)


                        ' Adicione os parâmetros ao comando
                        AddTextParameterMysql(cmd, "@DescResumo", UCase(DadosArquivoCorrente.Titulo))
                        AddTextParameterMysql(cmd, "@DescDetal", UCase(DadosArquivoCorrente.AssuntoSubiTitulo))
                        AddTextParameterMysql(cmd, "@PecaManuFat", "S")
                        AddTextParameterMysql(cmd, "@Autor", UCase(DadosArquivoCorrente.Author))
                        AddTextParameterMysql(cmd, "@Palavrachave", UCase(DadosArquivoCorrente.PalavraChave))
                        AddTextParameterMysql(cmd, "@Notas", UCase(DadosArquivoCorrente.Comentarios))
                        AddTextParameterMysql(cmd, "@Espessura", UCase(DadosArquivoCorrente.Espessura))
                        AddTextParameterMysql(cmd, "@AreaPintura", UCase(DadosArquivoCorrente.AreaPintura))
                        AddTextParameterMysql(cmd, "@NumeroDobras", UCase(DadosArquivoCorrente.NumeroDobras))
                        AddTextParameterMysql(cmd, "@Peso", UCase(DadosArquivoCorrente.Massa))
                        AddTextParameterMysql(cmd, "@Unidade", "PC")
                        AddTextParameterMysql(cmd, "@Altura", UCase(DadosArquivoCorrente.ComprimentoBlank))
                        AddTextParameterMysql(cmd, "@Largura", UCase(DadosArquivoCorrente.LarguraBlank))
                        AddTextParameterMysql(cmd, "@Profundidade", String.Empty)
                        AddTextParameterMysql(cmd, "@CodMatFabricante", UCase(DadosArquivoCorrente.NomeArquivoSemExtensao))
                        AddTextParameterMysql(cmd, "@DtCad", DateTime.Now.Date) ' Formato ISO
                        AddTextParameterMysql(cmd, "@UsuarioCriacao", Usuario.NomeCompleto.ToString.ToUpper)
                        AddTextParameterMysql(cmd, "@UsuarioAlteracao", UCase(DadosArquivoCorrente.SalvoUltimaVezPor))
                        AddTextParameterMysql(cmd, "@DtAlteracao", DadosArquivoCorrente.DataUltimoSalvamento.ToString) ' Formato ISO
                        AddTextParameterMysql(cmd, "@CodigoJuridicoMat", String.Empty)
                        AddTextParameterMysql(cmd, "@StatusMat", "A")
                        AddTextParameterMysql(cmd, "@MaterialSW", UCase(DadosArquivoCorrente.material))
                        AddTextParameterMysql(cmd, "@EnderecoArquivo", UCase(DadosArquivoCorrente.EnderecoArquivo))
                        AddTextParameterMysql(cmd, "@Acabamento", UCase(DadosArquivoCorrente.Acabamento))
                        AddTextParameterMysql(cmd, "@txtSoldagem", UCase(DadosArquivoCorrente.soldagem))
                        AddTextParameterMysql(cmd, "@txtTipoDesenho", UCase(DadosArquivoCorrente.TipoDesenho))
                        AddTextParameterMysql(cmd, "@txtCorte", UCase(DadosArquivoCorrente.Corte))
                        AddTextParameterMysql(cmd, "@txtDobra", UCase(DadosArquivoCorrente.Dobra))
                        AddTextParameterMysql(cmd, "@txtSolda", UCase(DadosArquivoCorrente.Solda))
                        AddTextParameterMysql(cmd, "@txtPintura", UCase(DadosArquivoCorrente.Pintura))
                        AddTextParameterMysql(cmd, "@txtMontagem", UCase(DadosArquivoCorrente.Montagem))
                        AddTextParameterMysql(cmd, "@Compcx", DadosArquivoCorrente.Alturacaixadelimitadora)
                        AddTextParameterMysql(cmd, "@Largcx", DadosArquivoCorrente.Larguracaixadelimitadora)
                        AddTextParameterMysql(cmd, "@Espcx", DadosArquivoCorrente.Profundidadeaixadelimitadora)
                        AddTextParameterMysql(cmd, "@txtItemEstoque", DadosArquivoCorrente.ItemEstoque)



                        '    cmd.ExecuteNonQuery()

                        Dim maxTentativas As Integer = 3 ' Quantidade máxima de tentativas
                        Dim tentativaAtual As Integer = 0
                        Dim sucesso As Boolean = False

                        Do While Not sucesso And tentativaAtual < maxTentativas
                            Try
                                cmd.ExecuteNonQuery()
                                sucesso = True ' Se chegou aqui, a execução foi bem-sucedida
                                Threading.Thread.Sleep(CInt(My.Settings.TempoRespostaServidor))
                            Catch ex As Exception
                                tentativaAtual += 1
                                If tentativaAtual < maxTentativas Then
                                    '  MsgBox($"Erro na execução. Tentando novamente em 30 segundos... ({tentativaAtual}/{maxTentativas})")
                                    Threading.Thread.Sleep(CInt(My.Settings.TempoRespostaServidor))

                                    cl_BancoDados.AbrirBanco()

                                Else
                                    ' Lançar exceção após atingir o limite de tentativas
                                    ' Throw New Exception($"Falha ao executar o comando após {maxTentativas} tentativas.", ex)

                                    cl_BancoDados.AbrirBanco()

                                End If

                            End Try

                        Loop

                    Catch ex As Exception

                        ' Exibir mensagem de erro
                        MessageBox.Show("Erro ao atualizar ou inserir dados no banco: " & ex.Message)

                        ClasseEmail.EmailTratamentoErro("Erro ao atualizar ou inserir dados no banco: " & ex.Message)

                    Finally

                    End Try
                End Using

            Catch ex As Exception

            Finally

            End Try

        ElseIf TipoBanco = "SQL" Then

            Try


                Dim dt As System.Data.DataTable
                Dim query As String
                Dim isUpdate As Boolean = False

                dt = cl_BancoDados.CarregarDados("Select CodMatFabricante FROM  " & ComplementoTipoBanco & "material WHERE PecaManuFat = 'S' and CodMatFabricante = '" & DadosArquivoCorrente.NomeArquivoSemExtensao & "'")

                ' Verifica se o DataTable tem pelo menos uma linha
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    ' Verifica se o valor da coluna "CodMatFabricante" não é DBNull
                    If dt.Rows(0)("CodMatFabricante") IsNot DBNull.Value Then
                        isUpdate = True
                    Else
                        isUpdate = False
                    End If
                Else
                    isUpdate = False
                End If


                ' Definir a consulta SQL com base na existência do item
                If isUpdate Then
                    query = "UPDATE " & ComplementoTipoBanco & " material SET DescResumo = @DescResumo, DescDetal = @DescDetal, PecaManuFat = @PecaManuFat, " &
            "Autor = @Autor, Palavrachave = @Palavrachave, Notas = @Notas, Espessura = @Espessura, AreaPintura = @AreaPintura, " &
            "NumeroDobras = @NumeroDobras, Peso = @Peso, Unidade = @Unidade, Altura = @Altura, Largura = @Largura, " &
            "Profundidade = @Profundidade, UsuarioAlteracao = @UsuarioAlteracao, DtAlteracao = @DtCad, CodigoJuridicoMat = @CodigoJuridicoMat, " &
            "StatusMat = @StatusMat, MaterialSW = @MaterialSW, EnderecoArquivo = @EnderecoArquivo, Acabamento = @Acabamento, " &
            "txtSoldagem = @txtSoldagem, txtTipoDesenho = @txtTipoDesenho, txtCorte = @txtCorte, txtDobra = @txtDobra, " &
            "txtSolda = @txtSolda, txtPintura = @txtPintura, txtMontagem = @txtMontagem, Comprimentocaixadelimitadora = @Compcx, " &
            "Larguracaixadelimitadora = @Largcx, Espessuracaixadelimitadora = @Espcx, txtItemEstoque = @txtItemEstoque " &
            "WHERE CodMatFabricante = @CodMatFabricante"
                Else
                    query = "INSERT INTO " & ComplementoTipoBanco & " material (DescResumo, DescDetal, PecaManuFat, Autor, Palavrachave, Notas, Espessura, AreaPintura, NumeroDobras, Peso, " &
            "Unidade, Altura, Largura, Profundidade, CodMatFabricante, DtCad, UsuarioCriacao, UsuarioAlteracao, DtAlteracao, CodigoJuridicoMat, " &
            "StatusMat, MaterialSW, EnderecoArquivo, Acabamento, txtSoldagem, txtTipoDesenho, txtCorte, txtDobra, txtSolda, txtPintura, " &
            "txtMontagem, Comprimentocaixadelimitadora, Larguracaixadelimitadora, Espessuracaixadelimitadora, txtItemEstoque) " &
            "VALUES (@DescResumo, @DescDetal, @PecaManuFat, @Autor, @Palavrachave, @Notas, @Espessura, @AreaPintura, @NumeroDobras, @Peso, @Unidade, " &
            "@Altura, @Largura, @Profundidade, @CodMatFabricante, @DtCad, @UsuarioCriacao, @UsuarioAlteracao, @DtAlteracao, @CodigoJuridicoMat, " &
            "@StatusMat, @MaterialSW, @EnderecoArquivo, @Acabamento, @txtSoldagem, @txtTipoDesenho, @txtCorte, @txtDobra, @txtSolda, @txtPintura, " &
            "@txtMontagem, @Compcx, @Largcx, @Espcx, @txtItemEstoque)"
                End If

                Using cmd As New SqlCommand(query, myconectSQL)
                    Try


                        VerificarProcessodaPecaCorrente(swModel, True)


                        ' Adicione os parâmetros ao comando
                        AddTextParameterSql(cmd, "@DescResumo", UCase(DadosArquivoCorrente.Titulo))
                        AddTextParameterSql(cmd, "@DescDetal", UCase(DadosArquivoCorrente.AssuntoSubiTitulo))
                        AddTextParameterSql(cmd, "@PecaManuFat", "S")
                        AddTextParameterSql(cmd, "@Autor", UCase(DadosArquivoCorrente.Author))
                        AddTextParameterSql(cmd, "@Palavrachave", UCase(DadosArquivoCorrente.PalavraChave))
                        AddTextParameterSql(cmd, "@Notas", UCase(DadosArquivoCorrente.Comentarios))
                        AddTextParameterSql(cmd, "@Espessura", UCase(DadosArquivoCorrente.Espessura))
                        AddTextParameterSql(cmd, "@AreaPintura", UCase(DadosArquivoCorrente.AreaPintura))
                        AddTextParameterSql(cmd, "@NumeroDobras", UCase(DadosArquivoCorrente.NumeroDobras))
                        AddTextParameterSql(cmd, "@Peso", UCase(DadosArquivoCorrente.Massa))
                        AddTextParameterSql(cmd, "@Unidade", "PC")
                        AddTextParameterSql(cmd, "@Altura", UCase(DadosArquivoCorrente.ComprimentoBlank))
                        AddTextParameterSql(cmd, "@Largura", UCase(DadosArquivoCorrente.LarguraBlank))
                        AddTextParameterSql(cmd, "@Profundidade", String.Empty)
                        AddTextParameterSql(cmd, "@CodMatFabricante", UCase(DadosArquivoCorrente.NomeArquivoSemExtensao))
                        AddTextParameterSql(cmd, "@DtCad", DateTime.Now.Date) ' Formato ISO
                        AddTextParameterSql(cmd, "@UsuarioCriacao", Usuario.NomeCompleto.ToString.ToUpper)
                        AddTextParameterSql(cmd, "@UsuarioAlteracao", UCase(DadosArquivoCorrente.SalvoUltimaVezPor))
                        AddTextParameterSql(cmd, "@DtAlteracao", DadosArquivoCorrente.DataUltimoSalvamento.ToString) ' Formato ISO
                        AddTextParameterSql(cmd, "@CodigoJuridicoMat", String.Empty)
                        AddTextParameterSql(cmd, "@StatusMat", "A")
                        AddTextParameterSql(cmd, "@MaterialSW", UCase(DadosArquivoCorrente.material))
                        AddTextParameterSql(cmd, "@EnderecoArquivo", UCase(DadosArquivoCorrente.EnderecoArquivo))
                        AddTextParameterSql(cmd, "@Acabamento", UCase(DadosArquivoCorrente.Acabamento))
                        AddTextParameterSql(cmd, "@txtSoldagem", UCase(DadosArquivoCorrente.soldagem))
                        AddTextParameterSql(cmd, "@txtTipoDesenho", UCase(DadosArquivoCorrente.TipoDesenho))
                        AddTextParameterSql(cmd, "@txtCorte", UCase(DadosArquivoCorrente.Corte))
                        AddTextParameterSql(cmd, "@txtDobra", UCase(DadosArquivoCorrente.Dobra))
                        AddTextParameterSql(cmd, "@txtSolda", UCase(DadosArquivoCorrente.Solda))
                        AddTextParameterSql(cmd, "@txtPintura", UCase(DadosArquivoCorrente.Pintura))
                        AddTextParameterSql(cmd, "@txtMontagem", UCase(DadosArquivoCorrente.Montagem))
                        AddTextParameterSql(cmd, "@Compcx", DadosArquivoCorrente.Alturacaixadelimitadora)
                        AddTextParameterSql(cmd, "@Largcx", DadosArquivoCorrente.Larguracaixadelimitadora)
                        AddTextParameterSql(cmd, "@Espcx", DadosArquivoCorrente.Profundidadeaixadelimitadora)
                        AddTextParameterSql(cmd, "@txtItemEstoque", DadosArquivoCorrente.ItemEstoque)



                        cmd.ExecuteNonQuery()

                        swModel.SaveSilent()



                    Catch ex As Exception

                        ' Exibir mensagem de erro
                        '  MessageBox.Show("Erro ao atualizar ou inserir dados no banco: " & ex.Message)

                        ClasseEmail.EmailTratamentoErro("Erro ao atualizar ou inserir dados no banco: " & ex.Message)


                    Finally

                    End Try
                End Using

            Catch ex As Exception

            Finally

            End Try

        End If


    End Sub


    ' Função para adicionar parâmetros como texto
    Public Function AddTextParameterMysql(ByRef cmd As MySqlCommand, ByVal paramName As String, ByVal value As Object)

        cmd.Parameters.AddWithValue(paramName, If(value Is Nothing, String.Empty, value.ToString().Trim()))

    End Function

    Public Function AddTextParameterSql(ByRef cmd As SqlCommand, ByVal paramName As String, ByVal value As Object)

        cmd.Parameters.AddWithValue(paramName, If(value Is Nothing, String.Empty, value.ToString().Trim()))

    End Function


    ' Função para gerar o SQL com parâmetros substituídos (para depuração)
    Function GenerateSqlWithParams(ByVal cmd As MySqlCommand) As String
        Dim sql As String = cmd.CommandText
        For Each param As MySqlParameter In cmd.Parameters
            Dim paramValue As String
            If param.Value Is DBNull.Value Then
                paramValue = "NULL"
            Else
                paramValue = "'" & param.Value.ToString().Replace("'", "''") & "'"
            End If
            sql = sql.Replace(param.ParameterName, paramValue)
        Next
        Return sql
    End Function


    Public Function ExportDXF(ByVal swModel As ModelDoc2, ByVal ManterAberto As Boolean, ByVal ExcluirLxds As Boolean) As Boolean


        Try


            ExportDXF = False
            ExcluirLxds = False


            If Not swModel Is Nothing Then
                ' Verifica se o modelo é uma peça (PART)
                If swModel.GetType() = swDocumentTypes_e.swDocPART Then
                    Dim swPart As PartDoc
                    swPart = swModel
                    Dim sModelName As String
                    Dim sPathName As String
                    Dim varAlignment As Object
                    Dim dataAlignment(11) As Double
                    Dim varViews As Object
                    Dim dataViews(1) As String
                    Dim options As Integer

                    ' Verifica se a peça contém uma feature de chapa metálica
                    If IsSheetMetalPart(swModel) Then
                        ' Acessa a feature de Flat-Pattern diretamente
                        Dim swFlatPatternFeature As Feature
                        swFlatPatternFeature = swPart.FeatureByName("Flat-Pattern")

                        ' Se a feature de Flat-Pattern não existir ou não estiver visível, forçar a planificação
                        If swFlatPatternFeature Is Nothing OrElse swFlatPatternFeature.IsSuppressed() Then
                            ' Forçar a planificação da peça de chapa metálica, caso a feature de Flat-Pattern não exista ou esteja suprimida
                            Dim swSheetMetal As Feature
                            swSheetMetal = swPart.FeatureByName("Sheet-Metal")

                            If Not swSheetMetal Is Nothing Then
                                ' Dessupressa todas as dobras que possam estar suprimidas
                                Dim swFeature As Feature
                                swFeature = swPart.GetFirstFeature()
                                Do While Not swFeature Is Nothing
                                    If swFeature.GetTypeName2() = "SheetMetal" Then
                                        ' Dessupressando as dobras
                                        Dim swBend As Feature
                                        swBend = swFeature.GetSubFeature("Bend")
                                        If Not swBend Is Nothing Then
                                            swBend.EditUnsuppress2()
                                        End If
                                    End If
                                    swFeature = swFeature.GetNextFeature()
                                Loop

                                ' Dessupressa a feature Flat-Pattern, caso necessário
                                swPart.EditUnsuppressFeature(swFlatPatternFeature)
                            End If

                        End If

                        sModelName = swModel.GetPathName
                        sPathName = Left(sModelName, Len(sModelName) - 6) & "dxf"


                        If File.Exists(sPathName) Then

                            File.Delete(sPathName)

                        End If

                        ' Cria uma anotação de texto na vista de anotação
                        Dim swAnnotation As Annotation
                        Dim swNote As Note
                        Dim swText As String
                        swText = swModel.GetTitle

                        ' Obtém a caixa delimitadora (bounding box) do modelo
                        Dim minPt(3) As Double
                        Dim maxPt(3) As Double

                        ' Obtém as coordenadas da caixa delimitadora
                        swPart.GetPartBox(True)

                        ' Calcula a posição dentro da geometria, por exemplo, na parte inferior direita
                        Dim posX As Double
                        Dim posY As Double

                        ' Posiciona a anotação na parte inferior direita da geometria
                        posX = maxPt(0) - 0.1 ' Ajuste conforme necessário para deixar um espaço de margem
                        posY = minPt(1) + 0.1 ' Ajuste conforme necessário para deixar um espaço de margem

                        ' Insere a nota no modelo
                        swNote = swModel.InsertNote(swText)
                        swAnnotation = swNote.GetAnnotation

                        ' Define o estilo do texto
                        swAnnotation.Width = 1 '0.04
                        swAnnotation.SetPosition2(posX, posY, 0) ' Define a posição da anotação

                        ' Define a cor do texto (branco, por exemplo)
                        swAnnotation.Color = RGB(255, 255, 255) ' Define a cor do texto como branco

                        ' Garantir que a anotação está configurada para ser exportada no DXF
                        swAnnotation.Visible = True ' Garante que a anotação será visível na exportação

                        ' Define os alinhamentos de geometria para exportação
                        dataAlignment(0) = 0.0#
                        dataAlignment(1) = 0.0#
                        dataAlignment(2) = 0.0#
                        dataAlignment(3) = 1.0#
                        dataAlignment(4) = 0.0#
                        dataAlignment(5) = 0.0#
                        dataAlignment(6) = 0.0#
                        dataAlignment(7) = 1.0#
                        dataAlignment(8) = 0.0#
                        dataAlignment(9) = 0.0#
                        dataAlignment(10) = 0.0#
                        dataAlignment(11) = 1.0#

                        varAlignment = dataAlignment

                        ' Define as vistas
                        dataViews(0) = "*Current"
                        dataViews(1) = "*Front"

                        varViews = dataViews

                        ' Configurações para exportação de chapas metálicas
                        options = 1 ' Inclui a geometria do flat-pattern

                        ' Exporta o arquivo para DXF
                        swPart.ExportToDWG2(sPathName, sModelName, swExportToDWG_e.swExportToDWG_ExportSheetMetal, True, varAlignment, False, False, options, Nothing)

                        ' Fecha o documento se necessário
                        If ManterAberto = False Then
                            swapp.CloseDoc(sModelName)
                        End If

                    Else
                        ' Para peças que não são de chapa metálica, apenas exporte o DXF
                        sModelName = swModel.GetPathName
                        sPathName = Left(sModelName, Len(sModelName) - 6) & "dxf"

                        ' Exporta o arquivo para DXF
                        swPart.ExportToDWG2(sPathName, sModelName, swExportToDWG_e.swExportToDWG_ExportSheetMetal, True, varAlignment, False, False, options, Nothing)

                        ' Fecha o documento se necessário
                        If ManterAberto = False Then
                            swapp.CloseDoc(swModel.GetPathName)
                        End If
                    End If

                    ExportDXF = True

                    If ExcluirLxds = True Then

                        sModelName = swModel.GetPathName
                        sPathName = Left(sModelName, Len(sModelName) - 6) & "lxds"

                        If File.Exists(sPathName) Then

                            File.Delete(sPathName)

                        End If

                    End If

                End If

            End If
        Catch ex As Exception

            ClasseEmail.EmailTratamentoErro(ex.Message.ToString)
        Finally

        End Try

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="swModel"></param>
    ''' <returns></returns>

    ' Função para verificar se a peça contém uma feature de chapa metálica
    Private Function IsSheetMetalPart(ByVal swModel As ModelDoc2) As Boolean
        Dim swFeature As Feature = swModel.FirstFeature

        ' Itera pelas features da peça
        While Not swFeature Is Nothing
            ' Verifica se a feature é uma feature de chapa metálica
            If swFeature.GetTypeName2() = "SheetMetal" Then
                Return True ' Encontrou uma feature de chapa metálica
            End If
            swFeature = swFeature.GetNextFeature()
        End While

        ' Retorna False se nenhuma feature de chapa metálica for encontrada
        Return False
    End Function


    Public Sub ExportToPDF(ByVal swModel As ModelDoc2, ByVal filePath As String, ByVal ManterAberto As Boolean)

        IntanciaSolidWorks.ConectarSolidWorks()

        swModel = swapp.ActiveDoc

        Try

            If Not swModel Is Nothing Then
                ' Exporta o arquivo para PDF
                Dim swModelDocExt As ModelDocExtension = swModel.Extension
                Dim pdfFilePath As String = Path.ChangeExtension(filePath, ".pdf")

                If File.Exists(pdfFilePath) Then

                    File.Delete(pdfFilePath)

                End If


                Dim pdfExportData As ExportPdfData = swapp.GetExportFileData(swExportDataFileType_e.swExportPdfData)
                Dim v = pdfExportData.SetSheets(1, True)

                ' Exportar o arquivo para PDF sem abrir o leitor de PDF
                swModelDocExt.SaveAs(pdfFilePath,
                                 swSaveAsVersion_e.swSaveAsCurrentVersion,
                                 swSaveAsOptions_e.swSaveAsOptions_Silent, ' Salvamento silencioso
                                 Nothing, 0, 0)

                'pdfsinco.ExcreverPdf(pdfFilePath, pdfFilePath, "")

                Threading.Thread.Sleep(CInt(My.Settings.TempoRespostaServidor))

                '  swModel.SaveAs(pdfFilePath)

                If ManterAberto = False Then

                    swapp.CloseDoc(filePath)

                End If
                ' Else
                ' Se o arquivo não pôde ser aberto, exiba uma mensagem de erro
                ' MessageBox.Show("Não foi possível abrir o arquivo.")

            End If
        Catch ex As Exception
        Finally
        End Try

    End Sub

End Class

Public Class ClSolidWorks
    ' Private swApp As SldWorks
    ' Private model As ModelDoc2
    'Private swPart As PartDoc
    ' Private swAssembly As AssemblyDoc
    'Private swDrawing As DrawingDoc

    Public Function ConectarSolidWorks() As Boolean
        Try
            swapp = Marshal.GetActiveObject("SldWorks.Application")
            Return True
        Catch ex As COMException
            'MessageBox.Show("Erro ao conectar ao SolidWorks: " & ex.Message)
            Return False
        End Try
    End Function

    ' Função para liberar os objetos COM do SolidWorks
    Public Function LiberarRecurso(ByVal SwModel As ModelDoc2) As Boolean
        If SwModel IsNot Nothing Then
            Marshal.ReleaseComObject(SwModel)
        End If
        SwModel = Nothing
    End Function


    Public Sub SelecionarModelo()
        swModel = swapp.ActiveDoc
        Select Case swModel.GetType
            Case swDocumentTypes_e.swDocPART
                swPart = CType(swModel, PartDoc)
            Case swDocumentTypes_e.swDocASSEMBLY
                swAssembly = CType(swModel, AssemblyDoc)
            Case swDocumentTypes_e.swDocDRAWING
                swDrawing = CType(swModel, DrawingDoc)
            Case Else
                MessageBox.Show("Documento não suportado")
        End Select
    End Sub

    Public Function ObterInformacoes() As String
        If swModel Is Nothing Then
            MessageBox.Show("Nenhum documento ativo encontrado.")
            Return String.Empty
        End If

        Dim info As String = "Tipo de documento: " & swModel.GetType().ToString() & vbCrLf

        Select Case swModel.GetType
            Case swDocumentTypes_e.swDocPART
                info &= "Part Name: " & swPart.GetTitle()
            Case swDocumentTypes_e.swDocASSEMBLY
                info &= "Assembly Name: " & swAssembly.GetTitle()
            Case swDocumentTypes_e.swDocDRAWING
                info &= "Drawing Name: " & swDrawing.GetTitle()
        End Select

        Return info
    End Function

End Class

Public Class clPdf

    Public Sub EscreverPdf(EnderecoCompleto As String, caminhoArquivoDestino As String, Parametro As String)
        Try
            ' Substitui extensões ".sldprt" ou ".sldasm" por ".pdf"
            Dim inputPdf As String = System.Text.RegularExpressions.Regex.Replace(EnderecoCompleto, ".sldprt$|.sldasm$", ".pdf", System.Text.RegularExpressions.RegexOptions.IgnoreCase)

            ' Verifica se o arquivo de entrada existe
            If Not System.IO.File.Exists(inputPdf) Then
                MsgBox("O arquivo PDF de entrada não existe: " & inputPdf)
                Exit Sub
            End If

            ' Extrai o nome do arquivo de entrada
            Dim fileName As String = System.IO.Path.GetFileName(inputPdf)

            ' Monta o caminho do arquivo de saída corretamente
            Dim outputPdf As String = System.IO.Path.Combine(caminhoArquivoDestino, "qtde_" & fileName)

            ' Verifica ou cria o diretório de saída
            Dim outputDirectory As String = System.IO.Path.GetDirectoryName(outputPdf)
            If Not System.IO.Directory.Exists(outputDirectory) Then
                System.IO.Directory.CreateDirectory(outputDirectory)
            End If

            ' Inicializa o PdfReader
            Dim pdfReader As iText.Kernel.Pdf.PdfReader = Nothing
            Try
                pdfReader = New iText.Kernel.Pdf.PdfReader(inputPdf)
            Catch ex As Exception
                MsgBox("Erro ao abrir o arquivo PDF de entrada: " & ex.Message)
                Exit Sub
            End Try

            ' Inicializa o PdfWriter e PdfDocument
            Dim pdfWriter As New iText.Kernel.Pdf.PdfWriter(outputPdf)
            Dim pdfDocument As New iText.Kernel.Pdf.PdfDocument(pdfReader, pdfWriter)

            ' Obtém a primeira página do PDF
            Dim page As iText.Kernel.Pdf.PdfPage = pdfDocument.GetFirstPage()
            Dim canvas As New iText.Kernel.Pdf.Canvas.PdfCanvas(page)

            ' Configura o texto a ser inserido
            Dim font As iText.Kernel.Font.PdfFont = iText.Kernel.Font.PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA)
            canvas.BeginText()
            canvas.SetFontAndSize(font, 12)
            canvas.MoveText(50, 800) ' Define a posição do texto
            canvas.ShowText($"OS: {Parametro} - Data de Emissão do Desenho: {DateTime.Now:dd/MM/yyyy}")
            canvas.EndText()

            ' Fecha o documento PDF
            pdfDocument.Close()

            MsgBox("Texto inserido com sucesso no arquivo: " & outputPdf)

        Catch ex As Exception
            MsgBox("Erro ao processar o PDF: " & ex.Message & vbCrLf & ex.StackTrace)
            If ex.InnerException IsNot Nothing Then
                MsgBox("Erro interno: " & ex.InnerException.Message)
            End If
        End Try
    End Sub




    Public Function EditarPdf(Origem As String, Parametro As String)
        Try
            ' Cria o leitor e escritor para o mesmo arquivo
            Using pdfReader As New PdfReader(Origem)
                Using pdfWriter As New PdfWriter(Origem & ".tmp") ' Escreve em um arquivo temporário
                    ' Cria o documento PDF em modo de edição
                    Dim pdfDocument As New PdfDocument(pdfReader, pdfWriter)

                    ' Acessa a primeira página do PDF
                    Dim page As PdfPage = pdfDocument.GetPage(1)
                    ' Cria um Canvas para desenhar na página
                    Dim canvas As New PdfCanvas(page)

                    ' Define a fonte para o texto
                    Dim font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA)

                    ' Define a posição e escreve o texto no PDF
                    canvas.BeginText()
                    canvas.SetFontAndSize(font, 12)
                    canvas.MoveText(50, 800) ' Posição X, Y na página
                    canvas.ShowText(Parametro & " Data Emissão do desenho: " & Date.Now.ToString("dd/MM/yyyy"))
                    canvas.EndText()

                    ' Fecha o documento PDF (salva as alterações)
                    pdfDocument.Close()
                End Using
            End Using

            ' Substitui o arquivo original pelo editado
            If System.IO.File.Exists(Origem & ".tmp") Then
                System.IO.File.Delete(Origem)
                System.IO.File.Move(Origem & ".tmp", Origem)
            End If

            Console.WriteLine("PDF editado com sucesso.")
        Catch ex As Exception
            Console.WriteLine("Erro ao editar PDF: " & ex.Message)
        End Try
    End Function


End Class