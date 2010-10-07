#Region "GPL"
'    This file is part of SerialGPS.

'    SerialGPS is free software: you can redistribute it and/or modify
'    it under the terms of the GNU General Public License as published by
'    the Free Software Foundation, either version 3 of the License, or
'    (at your option) any later version.

'    SerialGPS is distributed in the hope that it will be useful,
'    but WITHOUT ANY WARRANTY; without even the implied warranty of
'    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'    GNU General Public License for more details.

'    You should have received a copy of the GNU General Public License
'    along with SerialGPS.  If not, see <http://www.gnu.org/licenses/>.

'    Copyright 2010 Jonathan Heizer jheizer@gmail.com
#End Region

Public Class PointFunctions

    Public Shared Function ChecksumLine(ByVal Line As String) As Boolean
        Dim Chk As Integer = 0

        If Line.Length < 10 Then
            Return False
        End If

        'Check if it is a complete line
        If Not Line.Trim.Length = Line.IndexOf("*") + 3 Then
            Return False
        End If

        For Each Chr As Char In Line.ToCharArray
            Select Case Chr
                Case Is = "$"
                    'Do nothing

                Case Is = "*"
                    Exit For

                Case Else
                    If Chk = 0 Then
                        Chk = Convert.ToByte(Chr)
                    Else
                        Chk = Chk Xor Convert.ToByte(Chr)
                    End If
            End Select
        Next


        Return (Line.Substring(Line.IndexOf("*") + 1, 2) = Chk.ToString("X2"))

    End Function

    Public Shared Function NMEALatToDecimal(ByVal Data As String, ByVal Hemisphere As String)
        Dim Hours As Decimal = Data.Substring(0, 2) + (Data.Substring(2) / 60)
        If Hemisphere = "S" Then
            Return Hours * -1
        End If
        Return Hours
    End Function

    Public Shared Function NMEALongToDecimal(ByVal Data As String, ByVal Hemisphere As String)
        Dim Hours As Decimal = Data.Substring(0, 3) + (Data.Substring(3) / 60)
        If Hemisphere = "W" Then
            Return Hours * -1
        End If
        Return Hours
    End Function

End Class
