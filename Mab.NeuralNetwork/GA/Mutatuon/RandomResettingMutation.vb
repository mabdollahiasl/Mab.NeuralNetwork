Public Class RandomResettingMutation
    Implements IMutation

    Public Sub DoMutation(Selected As Chromosome, GeneCount As Integer, MinGeneValue As Integer, MaxGeneValue As Integer, RandomGenerator As Random) Implements IMutation.DoMutation
        For i = 0 To GeneCount - 1
            Dim GeneIndex = RandomGenerator.Next(Selected.Genes.Length)
            Selected.Genes(GeneIndex) = RandomGenerator.Next(MinGeneValue, MaxGeneValue)
        Next
    End Sub
End Class
