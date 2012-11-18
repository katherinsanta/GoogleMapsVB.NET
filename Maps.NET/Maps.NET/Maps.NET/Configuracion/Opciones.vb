﻿Public Class Opciones
    Sub borrarAutocompletado()
        Dim aspectoFOrm As New AspectoFormulario
        Dim resultado = MessageBox.Show("¿Está seguro de borrar todo el contenido de autocompletado?." & vbCrLf & "Esta opción no se puede deshacer.", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If resultado = vbYes Then
            'Borramos el contenido de la sesión
            listaAutocompletar.Clear() 'Borramos el arraylist
            MySource.Clear()
            Try
                Kill("Autocompletado.xml")
            Catch ex As Exception
            End Try

        End If

    End Sub
    Sub desactivarAuto()
        CheckedListBox1.Enabled = False
        DescripcionConfig(-1)
    End Sub
    Sub activarAuto()
        CheckedListBox1.Enabled = True
        DescripcionConfig(-2)
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            activarAuto()
        Else
            desactivarAuto()
        End If
    End Sub
    Private Sub Opciones_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListBox1.SelectedIndex = 0
        CheckedListBox1.SelectedIndex = 0
        'Activar/desactivar autocompltea
        If My.Settings.Autocompletado = True Then CheckBox1.Checked = True : activarAuto() Else CheckBox1.Checked = False : desactivarAuto()
        'Comprobamos si está activado/desactivada la función de guardar autocompletar al cerrar programa
        If My.Settings.SettingAutocompletar = True Then CheckedListBox1.SetItemChecked(0, True) Else CheckedListBox1.SetItemChecked(0, False)
        CheckedListBox1.SetItemChecked(1, False)
        'Comprobamos si está activado/desactivada la función de guardar autocompletar en direcciones encontradas
        If My.Settings.SetDireccionEncontrada = True Then CheckedListBox1.SetItemChecked(2, True) Else CheckedListBox1.SetItemChecked(2, False)

        'Comprobamos si está activado/desactivada la función de guardar registro http
        If My.Settings.GuardarLOG = True Then CheckedListBox2.SetItemChecked(0, True) Else CheckedListBox1.SetItemChecked(0, False)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Función de autocompletado
        If CheckBox1.Checked = True Then
            My.Settings.Autocompletado = True
        Else
            My.Settings.Autocompletado = False
        End If
        'Guardamos si está activado/desactivada la función de guardar autocompletar al cerrar programa
        If CheckedListBox1.GetItemCheckState(0) = CheckState.Checked Then
            My.Settings.SettingAutocompletar = True
        Else
            My.Settings.SettingAutocompletar = False
        End If

        'Borrar autocompletado
        If CheckedListBox1.GetItemCheckState(1) = CheckState.Checked Then
            borrarAutocompletado()
        End If

        'Guardamos si está activado/desactivada la función de guardar autocompletar en direcciones encontradas
        If CheckedListBox1.GetItemCheckState(2) = CheckState.Checked Then
            My.Settings.SetDireccionEncontrada = True
        Else
            My.Settings.SetDireccionEncontrada = False
        End If

        Me.Close()
    End Sub


    Private Sub CheckedListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CheckedListBox1.SelectedIndexChanged
        DescripcionConfig(CheckedListBox1.SelectedIndex)
    End Sub

    

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        desactivarPaneles()
        Select Case ListBox1.SelectedIndex
            Case 0
                Panel2.Visible = True
                If CheckBox1.Checked = True Then
                    DescripcionConfig(-2)
                Else
                    DescripcionConfig(-1)
                End If

            Case 1
                Panel3.Visible = True

                DescripcionConfig(3)
                CheckedListBox2.SelectedIndex = 0


        End Select
    End Sub
    Sub desactivarPaneles()
        Panel2.Visible = False
        Panel3.Visible = False
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim respuesta = MessageBox.Show("¿Realmente desea restablecer la configuración predeterminada?", "Advertencia", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
        If respuesta = Windows.Forms.DialogResult.Yes Then
            CheckBox1.Checked = True
            CheckedListBox1.SetItemChecked(0, True)
            CheckedListBox1.SetItemChecked(1, False)
            CheckedListBox1.SetItemChecked(2, False)
            CheckedListBox2.SetItemChecked(0, False)
            My.Settings.Autocompletado = True
            My.Settings.SetDireccionEncontrada = False
            My.Settings.SettingAutocompletar = True
            My.Settings.GuardarLOG = False
        End If

    End Sub



    Sub DescripcionConfig(ByVal indice As Integer)
        Select Case indice
            Case -2
                RichTextBox1.Text = "Función de autocompletado activada."
            Case -1
                RichTextBox1.Text = "Función de autocompletado desactivada."
            Case 0
                RichTextBox1.Text = "Al cerrar el programa, todas las funciones de autocompletado quedan guardadas, por lo tanto cuando vuelva a abrir la aplicación los cuadros de texto se rellenarán con la información de las sesiones anteriores."
            Case 1
                RichTextBox1.Text = "Se eliminará todo el historial de autocompletado de la aplicación. Esta opción no se puede deshacer."
            Case 2
                RichTextBox1.Text = "Al activar esta opción se guardarán todas las direcciones que encuentre la aplicación. Cuando usted busca una dirección y se le sugiere o muestra una dirección encontrada, por ejemplo, ésta se mostrará en los cuadros de autocompletado."
            Case 3
                RichTextBox1.Text = "Al activar esta opción se guardarán todas las peticiones HTTP realizadas por la aplicación. Esta opción sólo almacenará las peticiones en su pc, no las enviará a ningún servidor."

        End Select

    End Sub
End Class