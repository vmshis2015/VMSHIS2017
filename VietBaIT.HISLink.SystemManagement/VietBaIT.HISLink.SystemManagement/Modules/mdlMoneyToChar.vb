Module mdlMoneyToChar
    'Hàm chuy?n t? s? ti?n  sang thành ký t? ti?n
    Function ConvertToChar(ByVal strNumber As String) As String
        Dim chu As String, lng As Integer, cd As String, dd As Integer
        cd = ""
        If strNumber.Trim <> "" Then
            cd = strNumber
            lng = cd.Length
            If lng < 13 Then
                Select Case lng
                    Case 0 : chu = " "
                    Case 1 : chu = DonVi(cd)
                    Case 2 : chu = Chuc(cd)
                    Case 3 : chu = Tram(cd)
                    Case 4, 5, 6 : chu = Nghin(cd)
                    Case 7, 8, 9 : chu = Trieu(cd)
                    Case 10, 11, 12 : chu = Ti(cd)
                End Select
                dd = Len(Trim(chu))
                ConvertToChar = UCase$(Mid$(Trim(chu), 1, 1)) + Mid$(Trim(chu), 2, dd - 1) + " d?ng"
                ConvertToChar = Replace(ConvertToChar, " không tram  nghìn", "")
                ConvertToChar = Replace(ConvertToChar, " không tram  tri?u", "")
                ConvertToChar = Replace(ConvertToChar, "  ", " ")
            Else
                ConvertToChar = "Out Of Number"
            End If
        Else
            ConvertToChar = ""
            Exit Function
        End If
    End Function
    Function ChucHao(ByVal d As String) As String
        Dim tmp As String
        If Len(d) <> 2 Then
            tmp = "Error"
        Else : Select Case Mid$(d, 1, 1)
                Case "0"
                    tmp = " " + DonVi(Mid$(d, 2, 1))
                Case "1"
                    If Mid$(d, 2, 1) = "0" Then
                        tmp = "mu?i "
                    ElseIf Mid$(d, 2, 1) = "5" Then
                        tmp = "lam "
                    Else : tmp = "mu?i " + DonVi(Mid$(d, 2, 1))
                    End If
                Case "2", "3", "4", "5", "6", "7", "8", "9"
                    If Mid$(d, 2, 1) = "0" Then
                        tmp = DonVi(Mid$(d, 1, 1)) + "muoi "
                    ElseIf Mid$(d, 2, 1) = "1" Then
                        tmp = DonVi(Mid$(d, 1, 1)) + "muoi m?t "
                    ElseIf Mid$(d, 2, 1) = "4" Then
                        tmp = DonVi(Mid$(d, 1, 1)) + "muoi tu "
                    ElseIf Mid$(d, 2, 1) = "5" Then
                        tmp = DonVi(Mid$(d, 1, 1)) + "muoi nam "
                    Else : tmp = DonVi(Mid$(d, 1, 1)) + "muoi " + DonVi(Mid$(d, 2, 1))
                    End If
            End Select
        End If
        ChucHao = tmp
    End Function
    Function DonVi(ByVal d As String) As String
        Dim tmp As Integer
        tmp = Len(d)
        If tmp = 0 Then
            DonVi = " "
        Else : Select Case Mid$(d, 1, 1)
                Case "0" : DonVi = "không "
                Case "1" : DonVi = "m?t "
                Case "2" : DonVi = "hai "
                Case "3" : DonVi = "ba "
                Case "4" : DonVi = "b?n "
                Case "5" : DonVi = "nam "
                Case "6" : DonVi = "sáu "
                Case "7" : DonVi = "b?y "
                Case "8" : DonVi = "tám "
                Case "9" : DonVi = "chín "
            End Select
        End If
    End Function
    Function Chuc(ByVal d As String) As String
        Dim tmp As String
        If Len(d) <> 2 Then
            tmp = "Error"
        Else : Select Case Mid$(d, 1, 1)
                Case "1"
                    If Mid$(d, 2, 1) = "0" Then
                        tmp = "mu?i "
                    ElseIf Mid$(d, 2, 1) = "5" Then
                        tmp = "mu?i lam "
                    Else : tmp = "mu?i " + DonVi(Mid$(d, 2, 1))
                    End If
                Case "2", "3", "4", "5", "6", "7", "8", "9"
                    If Mid$(d, 2, 1) = "0" Then
                        tmp = DonVi(Mid$(d, 1, 1)) + "muoi "
                    ElseIf Mid$(d, 2, 1) = "1" Then
                        tmp = DonVi(Mid$(d, 1, 1)) + "muoi m?t "
                    ElseIf Mid$(d, 2, 1) = "4" Then
                        tmp = DonVi(Mid$(d, 1, 1)) + "muoi tu "
                    ElseIf Mid$(d, 2, 1) = "5" Then
                        tmp = DonVi(Mid$(d, 1, 1)) + "muoi nam "
                    Else : tmp = DonVi(Mid$(d, 1, 1)) + "muoi " + DonVi(Mid$(d, 2, 1))
                    End If
            End Select
        End If
        Chuc = tmp
    End Function
    Function Tram(ByVal d As String) As String
        Dim tmp As String, d1 As String, d2 As String, d3 As String
        Dim temp As String

        d1 = Mid$(d, 1, 1) : d2 = Mid$(d, 2, 1) : d3 = Mid$(d, 3, 1)
        If Len(d) <> 3 Then
            temp = "Error"
        Else
            Select Case d2
                Case "0"
                    If d3 = "0" Then
                        tmp = DonVi(d1) + "tram  "
                    ElseIf d3 = "1" Then
                        tmp = DonVi(d1) + "tram linh m?t "
                    ElseIf d3 = "4" Then
                        tmp = DonVi(d1) + "tram linh tu "
                    ElseIf d3 = "5" Then
                        tmp = DonVi(d1) + "tram linh nam "
                    Else
                        tmp = DonVi(d1) + "tram linh " + DonVi(d3)
                    End If
                Case "1"
                    If d3 = "0" Then
                        tmp = DonVi(d1) + "tram mu?i "
                    Else
                        tmp = DonVi(d1) + "tram " + Chuc(d2 + d3)
                    End If
                Case "2", "3", "4", "5", "6", "7", "8", "9"
                    tmp = DonVi(d1) + "tram " + Chuc(d2 + d3)
            End Select
            Tram = tmp
        End If
    End Function
    Function Nghin(ByVal d As String) As String
        Dim tmp As String, s1 As String, s2 As String, tmp1 As String
        Dim dai As Integer, ln As Integer
        dai = Len(d)
        ln = dai - 3
        s1 = Mid$(d, 1, ln) : s2 = Mid$(d, ln + 1, 3)
        If s2 = "000" Then
            tmp1 = "nghìn "
        Else
            tmp1 = "nghìn " + Tram(s2)
        End If
        Select Case Len(s1)
            Case 1
                tmp = DonVi(s1) + tmp1
            Case 2
                tmp = Chuc(s1) + tmp1
            Case 3
                tmp = Tram(s1) + tmp1
        End Select
        Nghin = tmp
    End Function
    Function Trieu(ByVal d As String) As String
        Dim tmp As String, s1 As String, s2 As String, tmp1 As String
        Dim dai As Integer, ln As Integer
        dai = Len(d)
        ln = dai - 6
        s1 = Mid$(d, 1, ln) : s2 = Mid$(d, ln + 1, 6)
        If s2 = "000000" Then
            tmp1 = "tri?u "
        Else
            tmp1 = "tri?u " + Nghin(s2)
        End If
        Select Case Len(s1)
            Case 1
                tmp = DonVi(s1) + tmp1
            Case 2
                tmp = Chuc(s1) + tmp1
            Case 3
                tmp = Tram(s1) + tmp1
        End Select
        Trieu = tmp
    End Function
    Function Ti(ByVal d As String) As String
        Dim tmp As String, s1 As String, s2 As String, tmp1 As String
        Dim dai As Integer, ln As Integer
        dai = Len(d)
        ln = dai - 9
        s1 = Mid$(d, 1, ln) : s2 = Mid$(d, ln + 1, 9)

        If s2 = "000000000" Then
            tmp1 = "t? "
        Else
            tmp1 = "t? " + Trieu(s2)
        End If

        Select Case Len(s1)
            Case 1
                tmp = DonVi(s1) + tmp1
            Case 2
                tmp = Chuc(s1) + tmp1
            Case 3
                tmp = Tram(s1) + tmp1
            Case 4
                tmp = Trieu(s1) + tmp1
        End Select
        Ti = tmp
    End Function
    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    ' Convert DoLa to text
    Private Function NumToString(ByVal nNumber As Decimal) As String

        Dim bNegative As Boolean
        Dim bHundred As Boolean

        If nNumber < 0 Then
            bNegative = True
        End If

        nNumber = Math.Abs(Int(nNumber))

        If nNumber < 1000 Then
            If nNumber \ 100 > 0 Then
                NumToString = NumToString & _
                     NumToString(nNumber \ 100) & " HUNDRED"
                bHundred = True
            End If
            nNumber = nNumber - ((nNumber \ 100) * 100)
            Dim bNoFirstDigit As Boolean
            bNoFirstDigit = False
            Select Case nNumber \ 10
                Case 0
                    Select Case nNumber Mod 10
                        Case 0
                            If Not bHundred Then
                                NumToString = NumToString & " ZERO"
                            End If
                        Case 1 : NumToString = NumToString & " ONE"
                        Case 2 : NumToString = NumToString & " TWO"
                        Case 3 : NumToString = NumToString & " THREE"
                        Case 4 : NumToString = NumToString & " FOUR"
                        Case 5 : NumToString = NumToString & " FIVE"
                        Case 6 : NumToString = NumToString & " SIX"
                        Case 7 : NumToString = NumToString & " SEVEN"
                        Case 8 : NumToString = NumToString & " EIGHT"
                        Case 9 : NumToString = NumToString & " NINE"
                    End Select
                    bNoFirstDigit = True
                Case 1
                    Select Case nNumber Mod 10
                        Case 0 : NumToString = NumToString & " TEN"
                        Case 1 : NumToString = NumToString & " ELEVEN"
                        Case 2 : NumToString = NumToString & " TWELVE"
                        Case 3 : NumToString = NumToString & " THIRTEEN"
                        Case 4 : NumToString = NumToString & " FOURTEEN"
                        Case 5 : NumToString = NumToString & " FIFTEEN"
                        Case 6 : NumToString = NumToString & " SIXTEEN"
                        Case 7 : NumToString = NumToString & " SEVENTEEN"
                        Case 8 : NumToString = NumToString & " EIGHTEEN"
                        Case 9 : NumToString = NumToString & " NINETEEN"
                    End Select
                    bNoFirstDigit = True
                Case 2 : NumToString = NumToString & " TWENTY"
                Case 3 : NumToString = NumToString & " THIRTY"
                Case 4 : NumToString = NumToString & " FORTY"
                Case 5 : NumToString = NumToString & " FIFTY"
                Case 6 : NumToString = NumToString & " SIXTY"
                Case 7 : NumToString = NumToString & " SEVENTY"
                Case 8 : NumToString = NumToString & " EIGHTY"
                Case 9 : NumToString = NumToString & " NINETY"
            End Select
            If Not bNoFirstDigit Then
                If nNumber Mod 10 <> 0 Then
                    NumToString = NumToString & "-" & _
                                  Mid(NumToString(nNumber Mod 10), 2)
                End If
            End If
        Else
            Dim nTemp As String
            nTemp = 10 ^ 12 'trillion
            Do While nTemp >= 1
                If nNumber >= nTemp Then
                    NumToString = NumToString & _
                                  NumToString(Int(nNumber / nTemp))
                    Select Case Int(Math.Log(nTemp) / Math.Log(10) + 0.5)
                        Case 12 : NumToString = NumToString & " TRILLION"
                        Case 9 : NumToString = NumToString & " BILLION"
                        Case 6 : NumToString = NumToString & " MILLION"
                        Case 3 : NumToString = NumToString & " THOUSAND"
                    End Select

                    nNumber = nNumber - (Int(nNumber / nTemp) * nTemp)
                End If
                nTemp = nTemp / 1000
            Loop
        End If

        If bNegative Then
            NumToString = " NEGATIVE" & NumToString
        End If

    End Function

    Public Function DollarToString(ByVal nAmount As Decimal) As String

        Dim nDollar As Decimal
        Dim nCent As Decimal

        nDollar = Int(nAmount)
        nCent = (Math.Abs(nAmount) * 100) Mod 100

        DollarToString = NumToString(nDollar) & " US DOLLARS"

        If Math.Abs(nDollar) <> 1 Then
            DollarToString = DollarToString & "S"
        End If

        DollarToString = DollarToString & " AND" & _
                         NumToString(nCent) & " CENT"

        If Math.Abs(nCent) <> 1 Then
            DollarToString = DollarToString & "S"
        End If

    End Function

    Public Function DollarToString2(ByVal nAmount As Decimal) As String

        Dim nDollar As Decimal

        nDollar = Int(nAmount)

        DollarToString2 = NumToString(nDollar) & " VND"
    End Function

    Public Function VNDToENG(ByVal nAmount As Decimal) As String
        Dim nDollar As Decimal
        nDollar = Int(nAmount)
        VNDToENG = NumToString(nDollar)
        If Math.Abs(nDollar) <> 1 Then
            VNDToENG = VNDToENG & "S"
        End If
        VNDToENG = VNDToENG & " VND"
    End Function

End Module
