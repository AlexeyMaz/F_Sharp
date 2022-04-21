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

let ShiftLeftN list n =
   let length = List.length list
   let shift_index index = if index < length - n then index + n else if index - (length - n) < length then index - (length - n) else 0
   List.init (List.length list) (fun index -> List.item (shift_index index) list)

[<EntryPoint>]
let main argv =
    Console.WriteLine("Введите кол-во элементов и список:")
    let list = readList (Console.ReadLine() |> Convert.ToInt32)

    let new_list = ShiftLeftN list 3
    Console.WriteLine("Сдвинутый список:")
    writeList new_list
    0