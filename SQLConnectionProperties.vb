Imports System.Text

Public Class SQLConnectionProperties

    Public Shared Server As String = String.Empty

    Public Shared IntegratedSecurity As Boolean = True

    Public Shared Username As String = String.Empty
    Public Shared Password As String = String.Empty

    Public Shared Database As String = String.Empty

    Public Shared ReadOnly Property ConnectionString As String
        Get

            If Server = String.Empty Or (IntegratedSecurity = False And Username = String.Empty) Then
                Return String.Empty
            End If

            Dim CS As New StringBuilder
            CS.Append(String.Concat("Data Source=", Server, ";"))
            If IntegratedSecurity Then
                CS.Append("Trusted_Connection=True;")
            Else
                CS.Append(String.Concat("User Id=", Username, ";Password=", Password.ToString, ";"))
            End If
            If Database <> String.Empty Then
                CS.Append(String.Concat("Initial Catalog=", Database, ";"))
            End If

            Return CS.ToString
        End Get
    End Property

End Class
