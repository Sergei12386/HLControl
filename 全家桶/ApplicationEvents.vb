﻿Imports Microsoft.VisualBasic.ApplicationServices

Namespace My

    Friend Class MyApplication

        Private Sub MyApplication_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
            Dim s As String = 本程序.路径.ToLower
            If 包含全部(s, "c:\users\", "local\temp") Then
                报错退出("请勿在压缩包内直接打开本软件！")
            End If
            s = 本程序.文件名.ToLower
            If s <> "走過去的全家桶" Then
                报错退出("请勿修改本程序文件！")
            End If
            Dim ps As Process() = Process.GetProcessesByName(s)
            If ps.Length > 1 Then
                报错退出("本程序不适合多开运行。")
            End If
            删除文件(缓存文件夹)
            Directory.CreateDirectory(缓存文件夹)
            缓存文件夹保护文件 = File.OpenWrite(缓存文件夹 + "86.txt")
        End Sub

        Private Sub MyApplication_UnhandledException(sender As Object, e As UnhandledExceptionEventArgs) Handles Me.UnhandledException
            Dim s As String = e.Exception.Message + vbCrLf + e.Exception.StackTrace
            报错退出(s)
        End Sub

        Private Sub 报错退出(s As String)
            MessageBox.Show(s, "出错了！" + 标题)
            本程序.退出()
        End Sub

    End Class

End Namespace
