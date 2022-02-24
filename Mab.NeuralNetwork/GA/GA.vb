Public Class GA

    Private ReadOnly _RandomGen As Random


    Private _Bests As List(Of Chromosome)


    Public Property PopulationCount As Integer = 100

    Public Property MutationRate As Double = 0.02

    Public Property CrossOverMethod As CrossOverMethods
    Public Property MutationMethod As MutationMethods

    Public Property MaxGenration As Integer

    Public ReadOnly Property ChromosomeLength As Integer
    Public ReadOnly Property MinGeneValue As Double
    Public ReadOnly Property MaxGeneValue As Double


    Private _Population As List(Of Chromosome)
    Public Property Population As List(Of Chromosome)
        Get
            Return _Population
        End Get
        Private Set(value As List(Of Chromosome))
            _Population = value
        End Set
    End Property

    Public Sub New(ChromosomeLength As Integer, MinGeneValue As Double, MaxGeneValue As Double)
        Me.ChromosomeLength = ChromosomeLength
        Me.MinGeneValue = MinGeneValue
        Me.MaxGeneValue = MaxGeneValue

        _RandomGen = New Random()
        _Population = New List(Of Chromosome)
    End Sub

    Protected Sub BitFilipMutation(Selected As Chromosome, GeneCount As Integer)
        If Not (MinGeneValue = 0 AndAlso MaxGeneValue = 1) Then
            Throw New Exception("Mutation method not supported!")
        End If
        For i = 0 To GeneCount - 1
            Dim GeneIndex = _RandomGen.Next(ChromosomeLength)
            If Selected.Genes(GeneIndex) = 0 Then
                Selected.Genes(GeneIndex) = 1
            Else
                Selected.Genes(GeneIndex) = 0
            End If
        Next
    End Sub
    Protected Sub InversionMutation(Selected As Chromosome, RangeLength As Integer)
        Dim MaxGeneIndex As Integer = ChromosomeLength - RangeLength
        Dim StartIndex As Integer = _RandomGen.Next(0, MaxGeneIndex)

        Dim GeneRange(RangeLength - 1) As Double
        Array.Copy(Selected.Genes, StartIndex, GeneRange, 0, RangeLength)
        Array.Reverse(GeneRange)

        Array.Copy(GeneRange, 0, Selected.Genes, StartIndex, RangeLength)


    End Sub
    Protected Sub RandomResettingMutation(Selected As Chromosome, GeneCount As Integer)
        For i = 0 To GeneCount - 1
            Dim GeneIndex = _RandomGen.Next(ChromosomeLength)
            Selected.Genes(GeneIndex) = _RandomGen.Next(MinGeneValue, MaxGeneValue)
        Next
    End Sub

    Protected Sub ScrambleMutation(Selected As Chromosome, RangeLength As Integer)
        Dim MaxGeneIndex As Integer = ChromosomeLength - RangeLength
        Dim StartIndex As Integer = _RandomGen.Next(0, MaxGeneIndex)
        Dim EndIndex As Integer = StartIndex + RangeLength

        For i = 0 To RangeLength - 1 'scramble the range
            Dim FirstGeneIndex = _RandomGen.Next(StartIndex, EndIndex + 1)
            Dim SecoundGeneIndex = _RandomGen.Next(StartIndex, EndIndex + 1)

            Dim Temp = Selected.Genes(FirstGeneIndex)
            Selected.Genes(FirstGeneIndex) = Selected.Genes(SecoundGeneIndex)
            Selected.Genes(SecoundGeneIndex) = Temp
        Next
    End Sub
    Protected Sub SwapMutation(Selected As Chromosome, GeneCount As Integer)
        For i = 0 To GeneCount - 1 'scramble the range
            Dim FirstGeneIndex = _RandomGen.Next(ChromosomeLength)
            Dim SecoundGeneIndex = _RandomGen.Next(ChromosomeLength)

            Dim Temp = Selected.Genes(FirstGeneIndex)
            Selected.Genes(FirstGeneIndex) = Selected.Genes(SecoundGeneIndex)
            Selected.Genes(SecoundGeneIndex) = Temp
        Next
    End Sub
    Protected Overridable Sub GenerateFirstPopulation()

        Console.WriteLine("First population produced....")
    End Sub
    Protected Function GenerateUniqeRandomArrayOfIntegers(Count As Integer) As Integer()
        Dim numbers As New HashSet(Of Integer)
        Do Until numbers.Count = Count
            Dim Current As Integer = _RandomGen.Next(Count)
            If Not numbers.Contains(Current) Then
                numbers.Add(Current)
            End If
        Loop
        Return numbers.ToArray()
    End Function
    Protected Sub CrossOver()
        Dim NewGenration As New List(Of Chromosome)

        Dim RandomChromosomesIndex = GenerateUniqeRandomArrayOfIntegers(PopulationCount)

        For i As Integer = 0 To PopulationCount - 1 Step 2
            Dim f = RandomChromosomesIndex(i)
            Dim l = RandomChromosomesIndex(i + 1)
            Dim female = Population(f)
            Dim male = Population(l)
            Population.AddRange(CrossOver(female, male))
        Next


    End Sub
    Protected Function SinglePointCrossOver(Father As Chromosome, Mother As Chromosome) As Chromosome()
        Dim Male As New Chromosome(ChromosomeLength)
        Dim FeMale As New Chromosome(ChromosomeLength)

        Dim SelectedPoint As Integer
        SelectedPoint = _RandomGen.Next(ChromosomeLength)

        For i = 0 To ChromosomeLength - 1
            If i < SelectedPoint Then
                Male.Genes(i) = Father.Genes(i)
                FeMale.Genes(i) = Mother.Genes(i)
            Else
                FeMale.Genes(i) = Father.Genes(i)
                Male.Genes(i) = Mother.Genes(i)
            End If
        Next
        Return New Chromosome() {FeMale, Male}
    End Function

    Protected Function TwoPointCrossOver(Father As Chromosome, Mother As Chromosome) As Chromosome()
        Dim Male As New Chromosome(ChromosomeLength)
        Dim FeMale As New Chromosome(ChromosomeLength)

        Dim SelectedPoint1, SelectedPoint2 As Integer
        SelectedPoint1 = _RandomGen.Next(ChromosomeLength)
        SelectedPoint2 = _RandomGen.Next(ChromosomeLength)

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

    Protected Function UniformCrossOver(Father As Chromosome, Mother As Chromosome) As Chromosome()
        Dim Male As New Chromosome(ChromosomeLength)
        Dim FeMale As New Chromosome(ChromosomeLength)

        For i As Integer = 0 To ChromosomeLength - 1
            Dim Coin = _RandomGen.NextDouble()
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

    Protected Function CrossOver(ch1 As Chromosome, ch2 As Chromosome) As Chromosome()
        Select Case Me.CrossOverMethod
            Case CrossOverMethods.SinglePoint
                Return SinglePointCrossOver(ch1, ch2)
            Case CrossOverMethods.TwoPoint
                Return TwoPointCrossOver(ch1, ch2)
            Case CrossOverMethods.Uniform
                Return UniformCrossOver(ch1, ch2)
            Case Else
                Return SinglePointCrossOver(ch1, ch2)
        End Select
    End Function
    Protected Sub MakeGenarationPool()
        Population = Population.OrderBy(Function(p) p.Fitness).ToList()
        If Population.Count = PopulationCount Then
            Return
        End If
        Population = Population.Take(PopulationCount).ToList()
    End Sub

End Class
