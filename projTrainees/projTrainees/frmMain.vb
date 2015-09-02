﻿Imports System.Data.Sql
Imports System.Data.SqlClient

Public Class frmMain
    Dim connectionString As String
    Dim cnn As New SqlConnection
    Dim dt As New DataTable
    Dim ds As New DataSet("Employees")
    Dim iRecCount As Integer
    Dim iIndex As Integer = 0

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'DisableInput()
        ConnectionToDatabase()
        DisplayRecord(iIndex)
        'dtDOB.Value = DateTime.Now
        'dtDateStarted.Value = DateTime.Now

    End Sub

    Private Sub ConnectionToDatabase()
        connectionString = "Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\blue20\Documents\VSprojects\VisualBasic\projTrainees\projTrainees\bin\Debug\dbTrainees.mdf;Integrated Security=True"
        cnn = New SqlConnection(connectionString)

        Try
            cnn.Open()
            Dim str As String = "SELECT * FROM tblDetails"
            Dim da As New SqlDataAdapter(str, cnn)
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey
            da.Fill(ds, "Details")
            dt = ds.Tables("Details")
            iRecCount = dt.Rows.Count

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    'Private Sub DisableInput()
    '    Try

    '        For Each Ctrl As Control In Me.Controls
    '            If TypeOf Ctrl Is TextBox Then
    '                Ctrl.Enabled = False
    '            Else
    '                Ctrl.Enabled = True
    '            End If
    '        Next

    '        For Each GroupBoxControl As Control In Me.Controls
    '            If TypeOf GroupBoxControl Is GroupBox Then
    '                For Each Ctrl As Control In GroupBoxControl.Controls
    '                    If TypeOf Ctrl Is TextBox Then
    '                        Ctrl.Enabled = False
    '                    Else
    '                        Ctrl.Enabled = True
    '                    End If
    '                Next
    '            End If
    '        Next

    '        txtCounty.Visible = True
    '        dtDoB.Visible = True
    '        txtGender.Visible = True
    '        txtDoB.Visible = True

    '        dtDOB.Visible = False
    '        chkMale.Visible = False
    '        chkFemale.Visible = False
    '        dtDoB.Visible = False


    '    Catch ex As Exception
    '        MessageBox.Show("Unable to set up display! ")
    '    End Try
    'End Sub

    Private Sub EnableInput()
        Try

            For Each Ctrl As Control In Me.Controls
                If TypeOf Ctrl Is TextBox Then
                    Ctrl.Enabled = True
                End If
            Next

            For Each GroupBoxControl As Control In Me.Controls
                If TypeOf GroupBoxControl Is GroupBox Then
                    For Each Ctrl As Control In GroupBoxControl.Controls
                        If TypeOf Ctrl Is TextBox Then
                            Ctrl.Enabled = True
                        End If
                    Next
                End If
            Next

            txtCounty.Visible = False
            dtDoB.Visible = False
            txtGender.Visible = False
            txtDoB.Visible = False
            dtDOB.Visible = True
            chkFemale.Visible = True
            chkMale.Visible = True
            dtDoB.Visible = True

        Catch ex As Exception
            MessageBox.Show("Unable to set up display! ")
        End Try
    End Sub

    Private Sub DisplayRecord(Index As Integer)
        Try
            txtID.Text = dt.Rows(Index)("ID").ToString()
            txtFirstName.Text = dt.Rows(Index)("FirstName").ToString()
            txtLastName.Text = dt.Rows(Index)("LastName").ToString()
            txtAddress1.Text = dt.Rows(Index)("Address1").ToString()
            If Len(dt.Rows(Index)("Address2").ToString()) > 0 Then
                txtAddress2.Text = dt.Rows(Index)("Address2").ToString()
            End If
            txtCounty.Text = dt.Rows(Index)("County").ToString()
            dtDoB.Text = dt.Rows(Index)("DateOfBirth").ToString()
            If (dt.Rows(Index)("Gender").ToString()) = "M" Then
                txtGender.Text = "Male"
            Else
                txtGender.Text = "Female"
            End If
            txtDoB.Text = dt.Rows(Index)("DateStarted").ToString()
            If Len(dt.Rows(Index)("Notes").ToString()) > 0 Then
                txtNotes.Text = dt.Rows(Index)("Notes").ToString()
            End If

        Catch ex As Exception
            MessageBox.Show("Unable to set up display!")
        End Try
    End Sub

    Function CheckForAlphaCharacters(ByVal StringToCheck As String)
        For i = 0 To StringToCheck.Length - 1
            If Not Char.IsLetter(StringToCheck.Chars(i)) Then
                Return False
            End If
        Next
        Return True
    End Function

    Private Sub CheckInput()
        Try

            If Not CheckForAlphaCharacters(txtFirstName.ToString()) Then
                MessageBox.Show("Your name must containe only letters.")
            Else
                MessageBox.Show("Jeeeej")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnFirst_Click(sender As Object, e As EventArgs) Handles btnFirst.Click
        iIndex = 0
        DisplayRecord(iIndex)
    End Sub

    Private Sub btnLast_Click(sender As Object, e As EventArgs) Handles btnLast.Click
        iIndex = iRecCount - 1
        DisplayRecord(iIndex)
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If iIndex > 0 Then
            iIndex -= 1
            DisplayRecord(iIndex)
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If iIndex < iRecCount - 1 Then
            iIndex += 1
            DisplayRecord(iIndex)
        End If
    End Sub


    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        If cnn.State = ConnectionState.Open Then
            cnn.Close()
        End If
        Me.Close()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        EnableInput()

        btnFirst.Enabled = False
        btnPrevious.Enabled = False
        btnNext.Enabled = False
        btnLast.Enabled = False
        chkMale.Checked = True

        Try

            Dim row As DataRow = ds.Tables("Employees").NewRow()

            row("FirstName") = "First Name"
            row("LastName") = "Last Name"
            row("Address1") = "Address1"
            row("Address2") = "Address2"
            row("Birth Date") = dtDoB.Value()
            row("Gender") = "M"
            row("Notes") = "Notes"

            ds.Tables("Employees").Rows.Add(row)
            iRecCount = dt.Rows.Count

            iIndex = iRecCount - 1

            DisplayRecord(iIndex)

        Catch ex As Exception
            MessageBox.Show("Bla")
        End Try

    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        EnableInput()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click


    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'DisableInput()
        'CheckInput()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ds.RejectChanges()
        iRecCount = dt.Rows.Count

        iIndex = iRecCount - 1

        DisplayRecord(iIndex)

        'DisableInput()
        btnFirst.Enabled = True
        btnPrevious.Enabled = True
        btnNext.Enabled = True
        btnLast.Enabled = True

    End Sub
End Class