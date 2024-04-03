Imports System.Text
Imports System.Data.SqlClient

Public Class DBLogin

    'Public ConnectionString As String
    Public ConnectionStringError As String

    Public Database As String

    Private t As Threading.Thread

    Private Delegate Sub UpdateDatabaseListDelegate(ByVal Databases As List(Of String))
    Private Sub UpdateDatabaseList(ByVal Databases As List(Of String))

        If Me.InvokeRequired Then
            Me.Invoke(New UpdateDatabaseListDelegate(AddressOf UpdateDatabaseList), Databases)
        Else
            ' Actually update the list
            'Me.Database = String.Empty
            Me.cboDatabases.Items.Clear()

            For Each db As String In Databases
                Me.cboDatabases.Items.Add(db)
            Next

            Me.cboDatabases.Enabled = Me.cboDatabases.Items.Count
            Me.cmdAccept.Enabled = Me.cboDatabases.Items.Count

        End If
    End Sub

    Private Sub ResetDatabaseRefresh()
        UpdateDatabaseList(New List(Of String))
        t = New Threading.Thread(New Threading.ThreadStart(AddressOf RefreshDatabases))
        t.Start()
    End Sub

    Private Sub DatabaseAuthMethod_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles rbSQLAuth.CheckedChanged, rbWindowsAuth.CheckedChanged

        Me.lblUsername.Enabled = Me.rbSQLAuth.Checked
        Me.lblPassword.Enabled = Me.rbSQLAuth.Checked
        Me.txtUsername.Enabled = Me.rbSQLAuth.Checked
        Me.txtPassword.Enabled = Me.rbSQLAuth.Checked
        UpdateDatabaseList(New List(Of String))

    End Sub

    Private Sub cmdAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAccept.Click

        If VerifyConnectionString() Then

            SQLConnectionProperties.Server = Me.txtServername.Text
            SQLConnectionProperties.IntegratedSecurity = Me.rbWindowsAuth.Checked
            SQLConnectionProperties.Username = Me.txtUsername.Text
            SQLConnectionProperties.Password = Me.txtPassword.Text
            SQLConnectionProperties.Database = Me.cboDatabases.Text

            t.Abort()

            Me.Close()
        End If

    End Sub

    Private Sub txtServername_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtServername.TextChanged
        ResetDatabaseRefresh()
    End Sub

    Private Sub txtUsername_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUsername.TextChanged
        ResetDatabaseRefresh()
    End Sub

    Private Sub txtPassword_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPassword.TextChanged
        ResetDatabaseRefresh()
    End Sub


    Private Function BuildConnectionString() As String

        BuildConnectionString = String.Empty

        If Me.txtServername.Text.Trim = String.Empty Then
            ConnectionStringError = "Please enter a servername"
            Return String.Empty
        End If

        If Me.rbSQLAuth.Checked And Me.txtUsername.Text.Trim = String.Empty Then
            ConnectionStringError = "Enter a username or change to Windows Authentication"
            Return String.Empty
        End If

        Dim CS As New StringBuilder
        CS.Append(String.Concat("Data Source=", Me.txtServername.Text, ";"))
        If Me.rbWindowsAuth.Checked Then
            CS.Append("Trusted_Connection=True;")
        Else
            CS.Append(String.Concat("User Id=", Me.txtUsername.Text, ";Password=", Me.txtPassword.Text, ";"))
        End If
        If Me.Database <> String.Empty Then
            CS.Append(String.Concat("Initial Catalog=", Me.cboDatabases.Text, ";"))
        End If

        BuildConnectionString = CS.ToString
    End Function

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub RefreshDatabases()


        UpdateDatabaseList(New List(Of String))

        'If Me.cboDatabases.Items.Count > 0 Then
        '    Exit Sub
        'End If

        If Not VerifyConnectionString() Then
            Exit Sub
        End If

        Dim CS As String = BuildConnectionString()

        If CS <> String.Empty Then

            Dim sc As New SqlConnection(CS)
            sc.Open()

            Dim sql As New SqlCommand("select name from master.sys.databases", sc)
            Dim ds As New DataSet
            Dim da As New SqlDataAdapter(sql)

            da.Fill(ds)

            Dim databases As New List(Of String)
            For Each dr As DataRow In ds.Tables(0).Rows
                sql = New SqlCommand(String.Concat("USE ", dr.Item(0)), sc)
                Try
                    sql.ExecuteNonQuery()
                    databases.Add(dr.Item(0))
                Catch ex As SqlException
                    ' Database is not accessible - skip it
                End Try
            Next

            UpdateDatabaseList(databases)

        End If

    End Sub

    Private Function VerifyConnectionString() As Boolean

        Dim CS As String = BuildConnectionString()
        If CS = String.Empty Then Return False

        Dim c As SqlClient.SqlConnection

        Try
            c = New SqlClient.SqlConnection(CS.ToString)
            c.Open()
            c.Close()

            Return True

        Catch ex As SqlClient.SqlException
            ConnectionStringError = ex.Message
            Return False
        Catch ex As InvalidOperationException
            ConnectionStringError = "No connections available in pool"
            Return False
        End Try

    End Function

    Private Sub DBLogin_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.rbWindowsAuth.Text = String.Concat(Me.rbWindowsAuth.Text, " (", My.User.Name, ")")

        Me.rbWindowsAuth.Checked = SQLConnectionProperties.IntegratedSecurity
        Me.txtServername.Text = SQLConnectionProperties.Server
        Me.txtUsername.Text = SQLConnectionProperties.Username
        Me.txtPassword.Text = SQLConnectionProperties.Password

    End Sub

    Private Sub cboDatabases_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDatabases.SelectedIndexChanged
        Me.Database = Me.cboDatabases.Text
    End Sub
End Class