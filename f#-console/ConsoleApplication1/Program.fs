module Program
open System
open CSVService
let service = new SampleService()

[<EntryPoint>]
let main argv =
    service.GetOnly100Rows 
    |> Seq.iter(fun p-> printfn "%A" p) 
    |> Console.Read 
    |> ignore
    0
