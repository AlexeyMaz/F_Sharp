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

let shift_left (list: 'a list) =
   if list.Length <= 1 then list
   else
       list.Tail @ [list.Head]

[<EntryPoint>]
let main argv =
    Console.WriteLine("Введите кол-во элементов и список:")
    let list = readList (Console.ReadLine() |> Convert.ToInt32)
    let new_list = shift_left list
    Console.WriteLine("Сдвинутый список:")
    writeList new_list
    0