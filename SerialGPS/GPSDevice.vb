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

Public Class GPSDevice
    Implements IDisposable

    Private WithEvents m_SerialPort As SerialPort
    Public Event NewLine(ByVal Line As String)

    Public Sub New(ByVal ComPort As String)
        m_SerialPort = New SerialPort(ComPort)
        m_SerialPort.BaudRate = 4800
        m_SerialPort.DataBits = 8
        m_SerialPort.StopBits = 1
        m_SerialPort.Parity = Parity.None
        m_SerialPort.ParityReplace = 63
        m_SerialPort.ReadBufferSize = 4096
    End Sub

    Public Function StartGPS() As Boolean
        Try
            If Not m_SerialPort.IsOpen Then
                m_SerialPort.Open()
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub StopGPS()
        If Not m_SerialPort Is Nothing Then
            If m_SerialPort.IsOpen Then
                m_SerialPort.Close()
            End If
        End If
    End Sub

    Private Sub m_SerialPort_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles m_SerialPort.DataReceived
        CheckForData()
    End Sub

    Private Sub CheckForData()
        Dim RawData As String

        If m_SerialPort.IsOpen Then
            If m_SerialPort.BytesToRead > 20 Then
                RawData = m_SerialPort.ReadLine

                If Not String.IsNullOrEmpty(RawData) Then
                    If RawData.Length > 10 Then
                        For Each Line As String In RawData.Split("$")
                            RaiseEvent NewLine(Line)
                        Next
                    End If
                End If
            End If
        End If
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()

        Try
            If Not m_SerialPort Is Nothing Then
                If m_SerialPort.IsOpen Then
                    m_SerialPort.Close()
                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free other state (managed objects).
            End If
            m_SerialPort.Close()
            m_SerialPort.Dispose()
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
