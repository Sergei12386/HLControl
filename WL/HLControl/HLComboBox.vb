﻿Namespace HLControl

    <DefaultEvent("SelectedIndexChanged")>
    Public Class HLComboBox
        Inherits Control

        Private 列表 As HLListBox, 原高度 As Integer

        Public Sub New()
            DoubleBuffered = True
            列表 = New HLListBox
            Controls.Add(列表)
            With 列表
                .Visible = False
                .Top = 0
                .Width = 1
                .Left = 0
                .ShowScrollBar = True
                AddHandler .Click, Sub()
                                       HideListBox()
                                   End Sub
                AddHandler .SelectedIndexChanged, Sub()
                                                      RaiseEvent SelectedIndexChanged()
                                                  End Sub
            End With
            原高度 = Height
        End Sub

        Public Property HighLightLabel As HLLabel

        Private Sub _MouseWheel(sender As Object, e As MouseEventArgs) Handles Me.MouseWheel
            If Enabled AndAlso e.Y < 原高度 Then
                SelectedIndex += IIf(e.Delta < 0, 1, -1)
            End If
        End Sub

        Private Sub ShowListbox()
            If 为空(Parent) OrElse Visible = False OrElse 列表.Visible OrElse 列表.Items.Count < 1 Then Exit Sub
            With 列表
                Dim c As Integer = .Items.Count
                If c < 1 Then Exit Sub
                .Width = Width
                .Top = Height
                .Left = 0
                c = .FullHeight
                Dim h As Integer = Parent.Height - Bottom - 50 * DPI
                设最大值(c, h)
                设最大值(c, 350 * DPI)
                .Height = c
                .Visible = True
                Height += .Height
                If 非空(HighLightLabel) Then
                    HighLightLabel.HighLight = True
                End If
                BringToFront()
            End With
            Invalidate()
        End Sub

        Private Sub HideListBox()
            With 列表
                If .Visible = False Then Exit Sub
                .Visible = False
                Height -= .Height
            End With
            Invalidate()
        End Sub

        Private Sub _MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
            If 列表.Visible = False Then
                ShowListbox()
            Else
                HideListBox()
            End If
        End Sub

        <Browsable(False)>
        Public ReadOnly Property Items As List(Of String)
            Get
                Return 列表.Items
            End Get
        End Property

        <Browsable(False)>
        Public Property SelectedIndex As Integer
            Get
                Return 列表.SelectedIndex
            End Get
            Set(v As Integer)
                If 列表.SelectedIndex <> v Then
                    列表.SelectedIndex = v
                    Invalidate()
                End If
            End Set
        End Property

        <Browsable(False)>
        Public Property SelectedItem As String
            Get
                Return 列表.SelectedItem
            End Get
            Set(v As String)
                If 列表.SelectedItem <> v Then
                    列表.SelectedItem = v
                    Invalidate()
                End If
            End Set
        End Property

        Public Event SelectedIndexChanged()

        Private Sub _NeedRePaint() Handles Me.SizeChanged, Me.Resize, Me.AutoSizeChanged, Me.FontChanged, Me.EnabledChanged
            Invalidate()
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            修正Dock(Me, True, False)
            MyBase.OnPaint(e)
            If Not 列表.Visible Then
                Height = Font.GetHeight + 6 * DPI
                原高度 = Height
            End If
            If Not Enabled Then
                列表.Visible = False
            End If
            Dim g As Graphics = e.Graphics, c As Rectangle = ClientRectangle
            With g
                绘制基础矩形(g, c, True)
                Dim m As Single = 原高度 * 0.15
                .DrawString("▼", New Font("Segoe UI", 0.4 * 原高度), 内容白笔刷, 点F(Width - 原高度 + m, m))
                m = 3 * DPI
                绘制文本(g, 列表.SelectedItem, Font, m, m, 获取文本状态(Enabled))
            End With
        End Sub

    End Class

End Namespace
