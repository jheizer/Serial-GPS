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

Public Class Sat
    Dim Code As String
    Dim Azimuth As Integer
    Dim Elevation As Integer
    Dim SNR As Integer

    Public Sub New()
    End Sub

    Public Sub New(ByVal Code As String, ByVal Azimuth As String, ByVal Elevation As String, ByVal SNR As String)
        Me.Code = Code
        Me.Azimuth = Azimuth
        Me.Elevation = Elevation
        If String.IsNullOrEmpty(SNR) Then
            Me.SNR = 0
        Else
            Me.SNR = SNR
        End If
    End Sub
End Class
