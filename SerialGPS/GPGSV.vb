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

Public Class GPGSV
    Public Sats As New Generic.List(Of Sat)
    Private m_NumOfSen As Integer = 0

    Public Sub New()
    End Sub

    Public Sub New(ByVal Pieces() As String)
        ParseSats(Pieces)
    End Sub

    Public Sub AddLine(ByVal Pieces() As String)
        ParseSats(Pieces)
    End Sub

    Private Sub ParseSats(ByVal Pieces() As String)
        For i As Integer = 4 To Pieces.Length - 3 Step 4
            If Not String.IsNullOrEmpty(Pieces(i)) Then
                Sats.Add(New Sat(Pieces(i), Pieces(i + 1), Pieces(i + 2), Pieces(i + 3)))
            End If
        Next
        m_NumOfSen += 1
    End Sub

    Public ReadOnly Property NumberOfSatellites() As Integer
        Get
            Return Sats.Count
        End Get
    End Property

    Public ReadOnly Property NumberOfSentences() As Integer
        Get
            Return m_NumOfSen
        End Get
    End Property

End Class
