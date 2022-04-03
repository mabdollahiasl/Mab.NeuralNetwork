Public Class TwoPointCrossOver
    Implements ICrossOver

    Public Function DoCrossOver(Father As Chromosome, Mother As Chromosome, RandomGenerator As Random) As Chromosome() Implements ICrossOver.DoCrossOver
        Dim ChromosomeLength As Integer = Father.Genes.Length

        Dim Male As New Chromosome(ChromosomeLength)
        Dim FeMale As New Chromosome(ChromosomeLength)

        Dim SelectedPoint1, SelectedPoint2 As Integer
        SelectedPoint1 = RandomGenerator.Next(ChromosomeLength)
        SelectedPoint2 = RandomGenerator.Next(ChromosomeLength)

        Dim MinPoint = Math.Min(SelectedPoint1, SelectedPoint2)
        Dim MaxPoint = Math.Min(SelectedPoint1, SelectedPoint2)


        For i = 0 To ChromosomeLength - 1
            If i > MinPoint AndAlso i < MaxPoint Then
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
