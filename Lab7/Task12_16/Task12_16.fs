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

let ElemsBetweenMax list = 
   let max = List.max list
   
   let max1_index = List.findIndex (fun (index, x) -> x = max) (List.indexed list)
   let max2_index = List.tryFindIndex (fun (index, x) -> index <> max1_index && x = max) (List.indexed list)
   
   if max2_index = None then
       []
   else
       let between_indexed = List.filter (fun x -> fst x > max1_index && fst x < Option.get max2_index) (List.indexed list)
       List.map (fun x -> snd x) between_indexed

[<EntryPoint>]
let main argv =
    Console.WriteLine("Введите кол-во элементов и список:")
    let list = readList (Console.ReadLine() |> Convert.ToInt32)

    let result = ElemsBetweenMax list
    Console.WriteLine("Элементы между первым и вторым максимумами:")
    writeList result
    0