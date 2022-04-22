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

[<EntryPoint>]
let main argv =
    Console.WriteLine("Введите кол-во элементов и список:")
    let list = readList (Console.ReadLine() |> Convert.ToInt32)

    Console.WriteLine("Все положительные элементы:")
    List.filter (fun x -> x > 0) list |> writeList

    Console.WriteLine("Все отрицательные элементы:")
    List.filter (fun x -> x < 0) list |> writeList
    0