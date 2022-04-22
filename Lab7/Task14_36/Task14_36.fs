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

let GetMaxOdd list = 
    let sorted = List.sortDescending list
    List.tryFind (fun x -> x % 2 = 1) sorted

[<EntryPoint>]
let main argv =
    Console.WriteLine("Введите кол-во элементов и список:")
    let list = readList (Console.ReadLine() |> Convert.ToInt32)

    let max_odd = GetMaxOdd list
    if Option.isNone max_odd then Console.WriteLine("В списке нет нечетных элементов")
    else Console.WriteLine($"Максимальный нечетный элемент: {Option.get max_odd}")
    0