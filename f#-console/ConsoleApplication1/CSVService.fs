module CSVService

open FSharp.Data

type sample = CsvProvider<"sample.csv">

type Result = Success | Fail

type SampleService() =
 member this.GetOnly100Rows = 
    let data = new sample() 
    query {
            for row in data.Rows do
            take 100
        }

  member this.Search search = 
    use data = new sample()
    if data.Rows |> Seq.exists(fun i -> i.PolicyID = search) then Success else Fail
