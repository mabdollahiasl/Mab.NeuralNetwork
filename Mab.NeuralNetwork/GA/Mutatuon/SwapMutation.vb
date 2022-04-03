Public Class SwapMutation
    Implements IMutation

    Public Sub DoMutation(Selected As Chromosome, GeneCount As Integer, MinGeneValue As Integer, MaxGeneValue As Integer, RandomGenerator As Random) Implements IMutation.DoMutation
        Dim ChromosomeLength As Integer = Selected.Genes.Length
        For i = 0 To GeneCount - 1 'swap the range
            Dim FirstGeneIndex = RandomGenerator.Next(ChromosomeLength)
            Dim SecoundGeneIndex = RandomGenerator.Next(ChromosomeLength)

            Dim Temp = Selected.Genes(FirstGeneIndex)
            Selected.Genes(FirstGeneIndex) = Selected.Genes(SecoundGeneIndex)
            Selected.Genes(SecoundGeneIndex) = Temp
        Next
    End Sub
End Class
