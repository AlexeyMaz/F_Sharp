open System

let rec readList n = 
    if n=0 then []
    else
        let Head = Console.ReadLine() |> Convert.ToInt32
        let Tail = readList (n-1)
        Head::Tail

let GetMinList list = 
   let rec GetMinList1 list cur_min = 
      match list with 
      | [] -> cur_min
      | head::t ->
         let new_cur_min = if head < cur_min then head else cur_min
         GetMinList1 t new_cur_min 
   GetMinList1 list list.Head


let Method list index = 
   let minList = GetMinList list

   let rec Method1 list index flag = 
      match list with
      | [] -> flag
      | h::t ->
          if index <> 0 then
              let new_index = index - 1                
              Method1 t new_index flag
          else
              if h = minList then flag = true
              else flag = false
   Method1 list index true

[<EntryPoint>]
let main argv =
    Console.WriteLine("Введите кол-во элементов и список:")
    let list = readList (Console.ReadLine() |> Convert.ToInt32)
    Console.WriteLine("Введите индекс предпологаемого минимума:")
    let index = (Console.ReadLine() |> Convert.ToInt32)
    Console.WriteLine("Результат:")
    Console.WriteLine(Method list index)
    0