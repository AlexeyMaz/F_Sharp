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

//Расположить все цифры в начале строки, а буквы - в конце
let Subtask12 s =
    let digits = String.filter (fun elem -> Char.IsDigit elem) s
    let letters = String.filter (fun elem -> Char.IsLetter elem) s
    String.concat "" [digits; letters]

let choose = function
| 1 -> Subtask6
| _ -> Subtask12

[<EntryPoint>]
let main argv =
   printfn "Введите строку: "
   let str = Console.ReadLine()
   printfn "Какую задачу решить?"
   printfn "1. Перемешать символы в слове, кроме первого и последнего"
   printfn "2. Расположить все цифры в начале строки, а буквы - в конце"

   Console.ReadLine() |> Int32.Parse |> choose <| str |> printfn "Результат: %s" 
   0