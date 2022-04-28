open System

//Перемешать символы в слове, кроме первого и последнего
let Subtask6 s =
    let length = String.length s
    if length <= 3 then
        s
    else
        let rnd = System.Random()
        let order = [1 .. length-2]
        let transposition = [0] @ (List.sortBy(fun _ -> rnd.Next(1, length-2)) order) @ [length - 1]
        String.init length (fun index -> s.[transposition.[index]].ToString())

[<EntryPoint>]
let main argv =

   0