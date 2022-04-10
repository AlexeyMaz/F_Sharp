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

let GetMinList list = 
   let rec GetMinList1 list cur_min = 
      match list with 
      | [] -> cur_min
      | head::t ->
         let new_cur_min = if head < cur_min then head else cur_min
         GetMinList1 t new_cur_min 
   GetMinList1 list list.Head


let GetList_beforeMin list = 
   let minList = GetMinList list

   let rec GetList (list: 'a list) new_list = 
      if list.Head = minList then new_list else
         let new_new_list = new_list @ [list.Head]
         let next_list = list.Tail
         GetList next_list new_new_list
   GetList list []

[<EntryPoint>]
let main argv =
    Console.WriteLine("Введите кол-во элементов и список:")
    let list = readList (Console.ReadLine() |> Convert.ToInt32)
    Console.WriteLine("Элементы, расположенные перед первым минимальным:")
    
    let list_bm = GetList_beforeMin list
    writeList list_bm
    0