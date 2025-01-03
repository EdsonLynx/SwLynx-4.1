
Imports MySql.Data.MySqlClient

Public Class ClQualidadeSGQ


    Public idDimencionalReferencia As Integer
    Public CodMatFabricante As String
    Public NomeCota As String
    Public TipoCota As String
    Public ValorCota As String
    Public DataCriacao As String
    Public CriadoPor As String
    Public Revisao As String

    Public Sub SalvarDados()

        Dim sucesso As Boolean = False
        Try

            Dim query As String = "INSERT INTO dimencionalreferencia (CodMatFabricante, NomeCota, TipoCota, ValorCota, DataCriacao, CriadoPor, Revisao) VALUES
                                                        (@CodMatFabricante, @NomeCota, @TipoCota, @ValorCota, @DataCriacao, @CriadoPor, @Revisao)"
            Using cmd As New MySqlCommand(query, myconect)

                ' Defina os valores para os parâmetros
                cmd.Parameters.AddWithValue("@CodMatFabricante", CodMatFabricante)
                cmd.Parameters.AddWithValue("@NomeCota", NomeCota)
                cmd.Parameters.AddWithValue("@TipoCota", TipoCota)
                cmd.Parameters.AddWithValue("@ValorCota", ValorCota)
                cmd.Parameters.AddWithValue("@DataCriacao", DataCriacao)
                cmd.Parameters.AddWithValue("@CriadoPor", CriadoPor)
                cmd.Parameters.AddWithValue("@Revisao", Revisao)


                ' Exiba o comando SQL para depuração
                ' MsgBox("Comando SQL: " & cmd.CommandText)

                cmd.ExecuteNonQuery()

                sucesso = True
            End Using
        Catch ex As Exception
            ' Tratar exceções aqui conforme necessário
            sucesso = False
        End Try

    End Sub


    Public Sub UpdateDados()
        Dim sucesso As Boolean = False
        Try

            Dim query As String = "UPDATE dimencionalreferencia SET NomeCota = @NomeCota,
                                          TipoCota = @TipoCota,
                                          ValorCota = @ValorCota,
                                          Revisao = @Revisao
                                  WHERE IdDimencionalReferencia = @IdDimencionalReferencia"

            Using cmd As New MySqlCommand(query, myconect)
                ' Defina os valores para os parâmetros
                cmd.Parameters.AddWithValue("@NomeCota", NomeCota)
                cmd.Parameters.AddWithValue("@TipoCota", TipoCota)
                cmd.Parameters.AddWithValue("@ValorCota", ValorCota)
                cmd.Parameters.AddWithValue("@Revisao", Revisao)
                cmd.Parameters.AddWithValue("@IdDimencionalReferencia", idDimencionalReferencia)

                cmd.ExecuteNonQuery()

                sucesso = True
            End Using
        Catch ex As Exception
            ' Tratar exceções aqui conforme necessário
            sucesso = False
        End Try


    End Sub


End Class
