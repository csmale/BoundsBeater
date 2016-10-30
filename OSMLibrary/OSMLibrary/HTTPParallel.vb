Imports System.Threading.Tasks

''' <summary>
''' Provides a facility for queuing multiple http requests for multiple worker threads
''' </summary>
Public Class HttpParallel
    Public ReadOnly Property MaxThreads As Integer = 5
    Private MyThreads() As Task
    Private reqQueue As New Queue
    Public ReadOnly Property QueueLength
        Get
            Return reqQueue.Count
        End Get
    End Property
    Public Sub New()
        MyBase.New
    End Sub
    Public Sub New(MaxThreads As Integer)
        MyBase.New()
        _MaxThreads = MaxThreads
    End Sub
    Public Function AddRequest(x) As Boolean
        reqQueue.Enqueue(x)
    End Function
    Public Function WaitForAll()

    End Function
End Class
