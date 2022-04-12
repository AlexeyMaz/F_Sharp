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

let GetMaxList list = 
   let rec GetMaxList1 list cur_max = 
      match list with 
      | [] -> cur_max
      | head::t ->
         let new_cur_max = if head > cur_max then head else cur_max
         GetMaxList1 t new_cur_max 
   GetMaxList1 list list.Head

let rec contains list elem =
    match list with
    | [] -> false
    | head :: t ->
        if head = elem then true
        else contains t elem

let get_missing list = 
    let min = GetMinList list
    let max = GetMaxList list
    
    // Собираем все числа от min до max, пропуская те, что есть в исходном списке
    let rec build_missing list new_list current =
        if current = max then new_list
        else
            let new_new_list = if contains list current then new_list else new_list @ [current]
            let new_current = current + 1
            build_missing list new_new_list new_current
            
    build_missing list [] min
        
[<EntryPoint>]
let main argv =
    Console.WriteLine("Введите кол-во элементов и список:")
    let list = readList (Console.ReadLine() |> Convert.ToInt32)
    
    let missing = get_missing list
    Console.WriteLine("Пропущенные числа:")
    writeList missing
    0