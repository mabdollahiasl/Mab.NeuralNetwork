Public Class UniformCrossOver
    Implements ICrossOver

    Public Function DoCrossOver(Father As Chromosome, Mother As Chromosome, RandomGenerator As Random) As Chromosome() Implements ICrossOver.DoCrossOver
        Dim ChromosomeLength As Integer = Father.Genes.Length

        Dim Male As New Chromosome(ChromosomeLength)
        Dim FeMale As New Chromosome(ChromosomeLength)

        For i As Integer = 0 To ChromosomeLength - 1
            Dim Coin = RandomGenerator.NextDouble()
            If Coin < 0.5 Then
                FeMale.Genes(i) = Father.Genes(i)
                Male.Genes(i) = Mother.Genes(i)
            Else
                Male.Genes(i) = Father.Genes(i)
                FeMale.Genes(i) = Mother.Genes(i)
            End If
        Next
        Return New Chromosome() {Male, FeMale}
    End Function
End Class
