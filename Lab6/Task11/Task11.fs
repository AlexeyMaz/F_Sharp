open System

let rec readList n = 
    if n=0 then []
    else
        let Head = Console.ReadLine() |> Convert.ToInt32
        let Tail = readList (n-1)
        Head::Tail

let rec writeList = function
    | [] -> ()
    | head::tail -> 
        Console.WriteLine($"{head}")
        writeList tail

let Method list func =
    let rec Method1 list func new_list =
        match list with
        | [] -> new_list
        | elem_1::t ->
            let elem_2 = if t <> [] then t.Head else 1
            let elem_3 = if t <> [] && t.Tail <> [] then t.Tail.Head else 1
            let three_to_one = func elem_1 elem_2 elem_3
            let new_new_list = new_list @ [three_to_one]
            let shifted_list = if t <> [] && t.Tail <> [] then t.Tail.Tail else []
            Method1 shifted_list func new_new_list
    Method1 list func []

[<EntryPoint>]
let main argv =
    printfn "Кол-во элементов и список:"
    let list = readList (Console.ReadLine() |> Convert.ToInt32)
    let new_list = Method list (fun x y z -> x + y + z)
    Console.WriteLine("Результат:")
    writeList new_list
    0