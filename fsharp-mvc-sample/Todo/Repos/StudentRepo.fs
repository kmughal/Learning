namespace Todo.Repos 

open Todo.Commons

 
type StudentRepo(names:List<string>) = 
     member this.Names =  names
     
     member this.AddName(name:string) = 
         if name |> System.String.IsNullOrEmpty then Fail{Message = "name is empty"} else Success{ Data=new StudentRepo(name :: this.Names)}



            