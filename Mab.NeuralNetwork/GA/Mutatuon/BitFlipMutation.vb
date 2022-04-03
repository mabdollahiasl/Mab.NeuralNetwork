Public Class BitFlipMutation
    Implements IMutation

    Public Sub DoMutation(Selected As Chromosome, GeneCount As Integer, MinGeneValue As Integer, MaxGeneValue As Integer, RandomGenerator As Random) Implements IMutation.DoMutation
        For i = 0 To GeneCount - 1
            Dim GeneIndex = RandomGenerator.Next(Selected.Genes.Length)
            If Selected.Genes(GeneIndex) = 0 Then
                Selected.Genes(GeneIndex) = 1
            Else
                Selected.Genes(GeneIndex) = 0
            End If
        Next
    End Sub
End Class
