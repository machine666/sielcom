﻿Imports MySql.Data.MySqlClient
Public Class ifabricacion
    Private Sub ifabricacion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        formatear_fechas()
    End Sub

    Sub formatear_fechas()
        Fecha_inicio.Format = DateTimePickerFormat.Custom
        Fecha_inicio.CustomFormat = "yyyy-MM-dd"
        fechatermino.Format = DateTimePickerFormat.Custom
        fechatermino.CustomFormat = "yyyy-MM-dd"
    End Sub
    
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        bodega_materias_primas_principal.Show()
    End Sub

    Private Sub VolverToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VolverToolStripMenuItem.Click
        bodega_materias_primas_principal.Show()
    End Sub

    Sub llenar_datagrid()
        Conexion.open()
        Dim query As String = "SELECT libro_salida_mp.codigo_mp'Codigo materia Prima', materia_prima.nombre_mp'Nombre', libro_salida_mp.cantidad_salida'Cantidad', materia_prima.unidad_medida_mp'Unidad de medida' FROM libro_salida_mp INNER JOIN materia_prima ON materia_prima.codigo_mp=libro_salida_mp.codigo_mp  WHERE id_salida = '" & Me.TextBox1.Text & "'"
        Dim Adpt As New MySqlDataAdapter(query, Conexion.conn)
        Dim ds As New DataSet()
        Adpt.Fill(ds, "Emp")
        DataGridView1.DataSource = ds.Tables(0)
        Conexion.close()
    End Sub

    Sub nuevo_proceso_fabricacion()
        Dim sqlquery As String = "INSERT INTO proceso_fabricacion (nombre_prod_fab,id_salida,fecha_inicio_fab,fecha_termino_fab)VALUES('" & Me.txt_productofabricar.Text & "','" & Me.TextBox1.Text & "','" & Me.Fecha_inicio.Text & "','" & Me.fechatermino.Text & "')"
        Dim cmd As New MySqlCommand(sqlquery, Conexion.conn)
        Try
            Conexion.open()
            cmd.ExecuteNonQuery()
            Conexion.close()
            MessageBox.Show("Proceso de fabricacion Guardado", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        llenar_datagrid()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        nuevo_proceso_fabricacion()
    End Sub
End Class