open System

let rec readList n = 
   if n=0 then []
   else
       let Head = Console.ReadLine() |> Convert.ToInt32
       let Tail = readList (n-1)
       Head::Tail

let rec contains list elem =
    match list with
    | [] -> false
    | head :: t ->
        if head = elem then true
        else contains t elem

let IsLocalMax list idx =
   let rec IsLocalMax1 (list: 'a list) prev index cur_index =
       if index = cur_index then
           match (prev, list.Tail) with
           | (None, []) -> true
           | (None, head::t) -> list.Head > head
           | (Some p, []) -> list.Head > p
           | (Some p, _) -> list.Head > p && list.Head > list.Tail.Head
       else
           let new_list = list.Tail
           let new_prev = list.Head
           let new_cur_index = cur_index + 1
           IsLocalMax1 new_list (Some new_prev) idx new_cur_index
   IsLocalMax1 list None idx 0
   
[<EntryPoint>]
let main argv =
    Console.WriteLine("Введите кол-во элементов и список:")
    let list = readList (Console.ReadLine() |> Convert.ToInt32)
    Console.WriteLine("Введите индекс предполагаемого локального максимума")
    let index = Console.ReadLine() |> Convert.ToInt32

    Console.WriteLine(IsLocalMax list index)
    0