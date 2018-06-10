module Tests

open System
open Xunit
open Todo.Commons
open Todo.Repos
[<Fact>]
let ``when user doesnt pass any name to save then it should have an error message`` () =
    let repo = new StudentRepo([])
    let result = repo.AddName("")
    match result with | Fail fail -> Assert.Equal(fail.Message , "name is empty") | _ -> Assert.True(false)

