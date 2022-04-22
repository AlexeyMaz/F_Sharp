open System

let rec readList n = 
    if n = 0 then []
    else
        let Head = Console.ReadLine() |> Convert.ToInt32
        let Tail = readList (n-1)
        Head::Tail

let rec writeList = function
    | [] -> ()
    | head::tail -> 
        Console.WriteLine($"{head}")
        writeList tail

let GetCount_ElemsBetweenMin list = 
   let min = List.min list
   
   let min1_index = List.findIndex (fun (index, x) -> x = min) (List.indexed list)
   let minlast_index = List.findIndexBack (fun (index, x) -> x = min) (List.indexed list)
   
   minlast_index - min1_index - 1

[<EntryPoint>]
let main argv =
    Console.WriteLine("Введите кол-во элементов и список:")
    let list = readList (Console.ReadLine() |> Convert.ToInt32)

    let result = GetCount_ElemsBetweenMin list
    if result = -1 then Console.WriteLine("В списке нет второго минимального числа")
    else Console.WriteLine($"Элементов между первым и последним минимумами: {result}")
    0