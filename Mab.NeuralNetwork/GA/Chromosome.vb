Public Class Chromosome
    Public ReadOnly Property Genes As Integer()

    Public Property Fitness As Double
    Public Sub New(Length As Integer)
        ReDim Genes(Length - 1)
    End Sub

End Class
