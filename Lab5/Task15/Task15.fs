open System
 
let rec nod a b =
    if a=b then a
    else 
        if a>b then nod (a-b) b 
        else nod a (b-a) 

let Bypass x func init =
    let rec Bypass1 x func init number =
        if number = 0 then init else 
        let new_init = if (nod x number) = 1 then func init number else init
        let new_number = number - 1
        Bypass1 x func new_init new_number
    Bypass1 x func init x
 
[<EntryPoint>]
let main argv =
    Console.WriteLine("Введите число:")
    let x = Convert.ToInt32(Console.ReadLine())
    Console.WriteLine("Произведение: {0}", Bypass x (fun x y -> x*y) 1)
    0