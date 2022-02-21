Public Class PerceptronData
    Public ReadOnly Property Inputs As Double()
    Public Property Output As Double
    Public Sub New(InputCount As Integer)
        ReDim Inputs(InputCount - 1)
    End Sub
    Public Sub New(Output As Double, ParamArray Inputs() As Double)
        Me.Output = Output
        Me.Inputs = Inputs
    End Sub
End Class
