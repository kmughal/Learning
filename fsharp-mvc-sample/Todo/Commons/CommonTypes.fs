namespace Todo.Commons

type Result<'TSuccess , 'TFail> = 
| Success of 'TSuccess | Fail of 'TFail

type Success<'T> = {
    Data : 'T
} 

type Fail = {
    Message:string
}