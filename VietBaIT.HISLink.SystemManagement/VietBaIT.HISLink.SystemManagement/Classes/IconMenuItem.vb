Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports System.Reflection
Imports System.Resources
Imports System.Windows.Forms

Public Class IconMenuItem : Inherits MenuItem
    Protected Friend mv_sDLLName As String
    Protected Friend mv_sFormName As String
    Protected Friend mv_sAssemblyName As String
    Protected Friend mv_sRoleName As String
    Protected Friend mv_iID As Integer
    Protected Friend mv_intShortCutKey As Integer
    Protected Friend mv_sInconPath As String
    Protected Friend mv_objImgList As ImageList
    Protected Friend mv_intImgIndex As Integer
    Protected Friend mv_sParameterList As String
    Protected Friend Tag As String

    Private m_Icon As Icon
    Private m_Font As Font
    ' By default these are set to the SystemColors Highight and Control values.
    ' This allows the appropriate color to be displayed if the user changes 
    ' themes or display settings.
    ' These can be overriden by calling the appropriate constructor for this 
    ' class.
    Private m_Gradient_Color1 As Color = SystemColors.Highlight
    Private m_Gradient_Color2 As Color = SystemColors.Control

    Public Sub New()
        MyClass.New("", Nothing, Nothing, System.Windows.Forms.Shortcut.None)
    End Sub
    Public Sub New(ByVal sText As String, ByVal pv_sIconPath As String, ByVal onClick As EventHandler)
        MyBase.New(sText, onClick)
        If IO.File.Exists(pv_sIconPath) Then
            m_Icon = New Icon(New IO.FileStream(pv_sIconPath, IO.FileMode.Open, IO.FileAccess.Read), 16, 16)
        Else
            'm_Icon = New Icon("C:\ImagesAndIcons\170.ico")
        End If
        m_Font = New Font("Arial", 8)
        OwnerDraw = True
    End Sub
    Public Sub New(ByVal sText As String, ByVal onClick As EventHandler)
        MyBase.New(sText, onClick)
        m_Font = New Font("Arial", 8)
        OwnerDraw = True
    End Sub
    Public Sub New(ByVal sText As String)
        MyBase.New(sText)
        m_Font = New Font("Arial", 8)
        OwnerDraw = True
    End Sub
    Public Sub New(ByVal sText As String, ByVal pv_sIconPath As String)
        MyBase.New(sText)
        If IO.File.Exists(pv_sIconPath) Then
            m_Icon = New Icon(New IO.FileStream(pv_sIconPath, IO.FileMode.Open, IO.FileAccess.Read), 16, 16)
        Else
            'm_Icon = New Icon("C:\ImagesAndIcons\170.ico")
        End If
        m_Font = New Font("Arial", 8)
        OwnerDraw = True
    End Sub
    Public Sub New(ByVal sText As String, ByVal pv_sIconPath As String, ByVal shortcut As Shortcut)
        MyBase.New(sText)
        MyBase.Shortcut = shortcut
        If IO.File.Exists(pv_sIconPath) Then
            m_Icon = New Icon(New IO.FileStream(pv_sIconPath, IO.FileMode.Open, IO.FileAccess.Read), 16, 16)
        Else
            'm_Icon = New Icon("C:\ImagesAndIcons\170.ico")
        End If
        m_Font = New Font("Arial", 8)
        OwnerDraw = True
    End Sub

    Public Sub New(ByVal text As String, ByVal icon As Icon, ByVal onClick As EventHandler, ByVal shortcut As Shortcut)
        MyBase.New(text, onClick, shortcut)
        ' Owner Draw Property allows you to modify the menu item by handling
        ' OnMeasureItem and OnDrawItem
        OwnerDraw = True
        m_Font = New Font("Arial", 10)
        m_Icon = icon
    End Sub

    ' Additional constructor allows the setting of custom colors for each part of the menu
    ' color gradient.
    Public Sub New(ByVal text As String, ByVal GradientColor1 As System.Drawing.Color, ByVal GradientColor2 As System.Drawing.Color, ByVal icon As Icon, ByVal onClick As EventHandler, ByVal shortcut As Shortcut)
        MyBase.New(text, onClick, shortcut)
        ' Key Property
        OwnerDraw = True
        m_Font = New Font("Arial", 10)
        m_Gradient_Color1 = GradientColor1
        m_Gradient_Color2 = GradientColor2
        m_Icon = icon
    End Sub

    Private Function GetRealText() As String
        Dim s As String = Text
        Try
            ' Append shortcut if one is set and it should be visible
            If ShowShortcut And Shortcut <> Shortcut.None Then
                ' To get a string representation of a Shortcut value, cast
                ' it into a Keys value and use the KeysConverter class (via TypeDescriptor).
                Dim k As Keys = CType(Shortcut, Keys)
                s = s & "     " & Convert.ToChar(9) & TypeDescriptor.GetConverter(GetType(Keys)).ConvertToString(k)
            Else
                Return s
            End If

            Return s
        Catch ex As Exception
            MsgBox("AAAA")
        End Try

    End Function
    Protected Overrides Sub OnDrawItem(ByVal e As DrawItemEventArgs)
        ' OnDrawItem perfoms the task of actually drawing the item after
        ' measurement is complete
        MyBase.OnDrawItem(e)
        Try
            Dim br As Brush
            Dim rcBk As Rectangle = e.Bounds
            If Text.Trim.Equals("-") Then
                rcBk.X += 24
                If CBool(e.State And DrawItemState.Selected) Then
                Else
                    'Draw the main rectangle
                    br = Brushes.DimGray
                    e.Graphics.FillRectangle(br, rcBk)
                End If
            Else
                br = Brushes.Gainsboro
                e.Graphics.FillRectangle(br, e.Bounds)
                ' Draw a background to the menu item with a linear gradient.
                ' This will use system defaults unless colors and have been
                ' passed on menu item instantiation
                'If CBool(e.State And DrawItemState.Disabled) Then

                'End If
            If CBool(e.State And DrawItemState.Selected) And Not CBool(e.State And DrawItemState.Disabled) Then
                br = New LinearGradientBrush(rcBk, Color.LightSteelBlue, Color.LightSteelBlue, 0)
                ' Draw the main rectangle
                e.Graphics.FillRectangle(br, rcBk)
                e.Graphics.DrawRectangle(New Pen(Color.Black, 1), e.Bounds.Left + 1, e.Bounds.Top + 1, e.Bounds.Width - 2, e.Bounds.Height - 2)
            ElseIf CBool(e.State And Not DrawItemState.Disabled) Then
                ' Draw the main rectangle
                rcBk.X += 24
                br = Brushes.WhiteSmoke
                e.Graphics.FillRectangle(br, rcBk)
                e.Graphics.DrawRectangle(New Pen(br), e.Bounds.X + 24, e.Bounds.Top + 1, e.Bounds.Width - 2, e.Bounds.Height - 2)
            End If
            ' Leave room for accelerator key
            Dim sf As StringFormat = New StringFormat
            sf.HotkeyPrefix = HotkeyPrefix.Show

            ' Draw the actual menu text
            br = New SolidBrush(Color.Black)

            If Not m_Icon Is Nothing Then
                If CBool(e.State And DrawItemState.Checked) Then
                    Dim _ico As Icon
                        _ico = New Icon(New IO.FileStream(Application.StartupPath & "\185.ico", IO.FileMode.Open, IO.FileAccess.Read), 16, 16)
                        If CBool(e.State And DrawItemState.Disabled) Then
                            e.Graphics.DrawString(GetRealText(), m_Font, New SolidBrush(Color.Gray), e.Bounds.Left + 30, e.Bounds.Top + 2, sf)
                        Else
                            e.Graphics.DrawString(GetRealText(), m_Font, br, e.Bounds.Left + 30, e.Bounds.Top + 2, sf)
                        End If

                        e.Graphics.DrawIcon(_ico, e.Bounds.Left + 4, e.Bounds.Top + 2)
                    Else
                        If CBool(e.State And DrawItemState.Disabled) Then
                            e.Graphics.DrawString(GetRealText(), m_Font, New SolidBrush(Color.Gray), e.Bounds.Left + 30, e.Bounds.Top + 2, sf)
                        Else
                            e.Graphics.DrawString(GetRealText(), m_Font, br, e.Bounds.Left + 30, e.Bounds.Top + 2, sf)
                        End If
                        e.Graphics.DrawIcon(m_Icon, e.Bounds.Left + 4, e.Bounds.Top + 2)
                    End If

                Else
                        e.Graphics.DrawString(GetRealText(), m_Font, br, e.Bounds.Left + 30, e.Bounds.Top + 2, sf)
            End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message & "CDEF")
        End Try
    End Sub

    Protected Overrides Sub OnMeasureItem(ByVal e As MeasureItemEventArgs)
        ' The MeasureItem event along with the OnDrawItem event are the two key events
        ' that need to be handled in order to create owner drawn menus.
        ' Measure the string that makes up a given menu item and use it to set the 
        ' size of the menu item being drawn.
        Try
            If Text.Trim.Equals("-") Then
                Dim sf As New StringFormat
                sf.HotkeyPrefix = HotkeyPrefix.Show
                MyBase.OnMeasureItem(e)
                e.ItemHeight = 1
            Else
                Dim sf As New StringFormat
                sf.HotkeyPrefix = HotkeyPrefix.Show
                MyBase.OnMeasureItem(e)
                e.ItemHeight = 22
                e.ItemWidth = CInt(e.Graphics.MeasureString(GetRealText(), m_Font, 10000, sf).Width) + 35
            End If
        Catch ex As Exception
            MsgBox(ex.Message & "ABCD")
        End Try

    End Sub


End Class