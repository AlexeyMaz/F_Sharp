//
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

let rec ListSum list sum = 
   match list with 
   | [] -> sum
   | head::t ->
      let new_sum = sum + head
      ListSum t new_sum

let Arithmetic_mean list =
   let listSum = ListSum list 0
   (double) listSum / (double) list.Length

let GetLessAr_mList list pred = 
   let rec GetList list pred new_list = 
      match list with 
      | [] -> new_list
      | head::t ->
         let new_new_list = if pred head then new_list @ [head] else new_list
         GetList t pred new_new_list
   GetList list pred []

[<EntryPoint>]
let main argv =
    Console.WriteLine("Введите кол-во элементов и список:")
    let list = readList (Console.ReadLine() |> Convert.ToInt32)
    
    let ar_m = Arithmetic_mean list
    let new_list = GetLessAr_mList list (fun x -> (double) x < ar_m)
    Console.WriteLine("Элементы, меньшие среднего арифметического элементов массива:")
    writeList new_list
    0