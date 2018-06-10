namespace Todo.Controllers

open Todo.Repos
open Todo.Commons
open Microsoft.AspNetCore.Mvc

type HomeController () =
    inherit Controller()

     let repo =  new StudentRepo(["khurram"])
     
         member this.Index()=
            let result = repo.AddName("mughal")
            this.ViewData.["names"] <- match result with | Fail f -> f.Message | Success item -> item.Data.Names |> List.toSeq |> String.concat ","

            this.View("index")
            
        // member this.AddName(name:string) =
        //      this.ViewData.["names"] <-  repo.AddName(name).Names 
        //      |> List.toSeq 
        //      |> String.concat ","
        //      this.View("index")


        

