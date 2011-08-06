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

Imports System.IO.Ports

Public Class ScanPorts

    Public Shared Function ScanPorts() As String
        Try
            For Each Port As String In SerialPort.GetPortNames
                Try
                    If CheckPortForGPS(Port) Then
                        Return Port
                    End If
                Catch ex As Exception
                End Try
            Next

            Return ""

        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Shared Function CheckPortForGPS(ByVal Port As String) As Boolean
        Dim SP As SerialPort
        Dim TestData As String

        Try
            SP = New SerialPort(Port)

            If SP.IsOpen Then
                Return False
            End If

            SP.BaudRate = 4800
            SP.DataBits = 8
            SP.StopBits = 1
            SP.Parity = Parity.None
            SP.ParityReplace = 63
            SP.ReadBufferSize = 4096

            SP.Open()

            If SP.IsOpen Then
                For i As Integer = 1 To 2 ' Give each port 2 trys
                    Threading.Thread.Sleep(250)
                    TestData = SP.ReadExisting
                    If TestData.Contains("$GPGGA") OrElse TestData.Contains("$GPGLL") OrElse _
                       TestData.Contains("$GPGSA") OrElse TestData.Contains("$GPGSV") OrElse TestData.Contains("$GPRMC") Then

                        SP.Close()

                        Return True
                    End If
                Next

                SP.Close()
            End If

            Return False
        Catch ex As Exception
            Return False
        End Try

    End Function
End Class
