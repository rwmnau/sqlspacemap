Imports Microsoft.Research.CommunityTechnologies
Imports System.Data.SqlClient

Public Class MainForm

    Dim DBTablesDictionary As New Dictionary(Of String, DatabaseTable)
    'Dim ConnectionString As String


    Private Sub BuildTreemap()

        tmcMap.Clear()

        Dim MapCollection As New List(Of Treemap.Node)

        For Each table In DBTablesDictionary.Values

            Dim CurrentTableData As Integer = table.DataSize

            Dim TableNode As New Treemap.Node(table.Name, table.TotalSize, 75)
            TableNode.ToolTip = String.Concat("Total size, including indexes: ", KBToString(table.TotalSize))

            Dim DataNode As Treemap.Node = TableNode.Nodes.Add("Data", table.DataSize, 50)
            DataNode.ToolTip = String.Concat("Row Data: ", KBToString(CurrentTableData))

            Dim indexnode As New Treemap.Node("Indexes", table.IndexSize, 50)

            For Each i As IndexDetails In table.Indexes.Values

                Dim Index As New Treemap.Node(i.Name, i.Size, 25)
                Index.ToolTip = String.Concat(i.Name, ControlChars.CrLf, KBToString(i.Size))
                indexnode.Nodes.Add(Index)
            Next

            If table.Indexes.Count > 0 Then
                TableNode.Nodes.Add(indexnode)
            End If

            mapcollection.Add(TableNode)

        Next

        RedrawTreeMap(MapCollection)

    End Sub

    Public Delegate Sub RedrawTreeMapDelegate(ByVal MapCollection As List(Of Treemap.Node))
    Public Sub RedrawTreeMap(ByVal MapCollection As List(Of Treemap.Node))
        If Me.InvokeRequired Then
            Me.Invoke(New RedrawTreeMapDelegate(AddressOf RedrawTreeMap), MapCollection)
        Else
            For Each node As Treemap.Node In MapCollection
                Me.tmcMap.Nodes.Add(node)
            Next
        End If
    End Sub

    Private Sub RefreshDatabaseView()

        Dim sc As New SqlConnection(SQLConnectionProperties.ConnectionString)
        sc.Open()

        Dim CanSeeAllTables As Boolean
        Dim sql As New SqlCommand("SELECT IS_ROLEMEMBER('db_ddladmin') | IS_ROLEMEMBER('db_datareader') | IS_ROLEMEMBER('db_owner') | IS_ROLEMEMBER('db_datawriter') | IS_ROLEMEMBER('db_accessadmin') | IS_ROLEMEMBER('db_securityadmin')", sc)
        CanSeeAllTables = sql.ExecuteScalar

        sql = New SqlCommand("select object_name(t.id) as TableName, t.id, t.dpages*8 as Pages, rowcnt as TableRows  from sysindexes t  join sysobjects so    on t.id = so.id        where(t.id > 100)    and indid in (0,1)    and so.type = 'U' order by object_name(t.id);", sc)
        sql.CommandTimeout = 0
        sql.CommandType = CommandType.Text

        Dim ds As New DataSet
        Dim da As New SqlDataAdapter(sql)

        da.Fill(ds)

        Me.DBTablesDictionary.Clear()

        For Each dr As DataRow In ds.Tables(0).Rows

            Me.DBTablesDictionary.Add(dr.Item("TableName"), New DatabaseTable(dr.Item("TableName"), dr.Item("Pages")))

            sql = New SqlCommand("select name, dpages*8 as Pages from sysindexes    where(id = @ID) and FirstIAM is not null  and indid not in (0,1)", sc)
            sql.Parameters.AddWithValue("@ID", dr.Item("id"))

            Dim ds2 As New DataSet
            Dim da2 As New SqlDataAdapter(sql)

            da2.Fill(ds2)

            For Each dr2 As DataRow In ds2.Tables(0).Rows
                Me.DBTablesDictionary.Item(dr.Item("TableName")).Indexes.Add(dr2.Item("name"), New IndexDetails(dr2.Item("name"), dr2.Item("pages")))
            Next
        Next

        BuildTreemap()

        If Not CanSeeAllTables Then
            MessageBox.Show("This account does not belong to any database admin roles. Some objects may not be visible to this user", _
                            "Low user permissions", MessageBoxButtons.OK)
        End If

        Me.lblCurrentActivity.Text = String.Concat(SQLConnectionProperties.Database, " on ", _
                                           SQLConnectionProperties.Server, " as ", _
                                           If(SQLConnectionProperties.IntegratedSecurity, My.User.Name, SQLConnectionProperties.Username))

    End Sub



    Private Sub mnuConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuConnect.Click

        Using f As New DBLogin
            f.ShowDialog()
            If SQLConnectionProperties.ConnectionString <> String.Empty Then
                Me.mnuDatabases.DropDownItems.Clear()

                For Each db As String In f.cboDatabases.Items
                    Me.mnuDatabases.DropDownItems.Add(db, Me.mnuDatabases.Image, AddressOf ChangeDatabase)
                Next

                Me.mnuDatabases.Enabled = Me.mnuDatabases.DropDownItems.Count
                Me.mnuRefresh.Enabled = Me.mnuDatabases.DropDownItems.Count

                RefreshDatabaseView()
            End If
        End Using

    End Sub

    Private Sub ChangeDatabase(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SQLConnectionProperties.Database = sender.ToString
        RefreshDatabaseView()
    End Sub

    Private Sub mnuExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExit.Click
        End
    End Sub

    Private Sub mnuAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAbout.Click
        About.ShowDialog()
    End Sub

    Private Sub mnuRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRefresh.Click
        RefreshDatabaseView()
    End Sub

    Private Function KBToString(ByVal KB As Long) As String
        Dim Bytes As Decimal = KB
        Dim M As String = "KB"

        If Bytes > 1024 Then
            Bytes = Bytes / 1024
            M = "MB"
        End If

        If Bytes > 1024 Then
            Bytes = Bytes / 1024
            M = "GB"
        End If

        If Bytes > 1024 Then
            Bytes = Bytes / 1024
            M = "TB"
        End If

        Return String.Concat(Decimal.Round(Bytes, 1), " ", M)

    End Function

    Private Sub mnuDatabases_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDatabases.Click

    End Sub
End Class
