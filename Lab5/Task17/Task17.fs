open System

let rec nod a b =
   if a=b then a
   else 
       if a>b then nod (a-b) b 
       else nod a (b-a) 

//Обход делителей числа с условием
let Bypass_div n cond func init =
   let rec Bypass1 n func init cur_divider =
       if cur_divider = 0 then init else
       let newInit = if n%cur_divider = 0 && cond cur_divider then func init cur_divider else init
       let new_divider = cur_divider-1
       Bypass1 n func newInit new_divider 
   Bypass1 n func init n

//Обход взаимно простых компонентов числа с условием
let Bypass_sim x cond func init =
   let rec Bypass1 x func init number =
       if number = 0 then init else 
       let new_init = if (nod x number) = 1 && cond number then func init number else init
       let new_number = number - 1
       Bypass1 x func new_init new_number
   Bypass1 x func init x

[<EntryPoint>]
let main argv =
   Console.WriteLine("Введите число:")
   let x = Convert.ToInt32(Console.ReadLine())//30
   Console.WriteLine("Произведение простых делителей числа с условием: {0}", Bypass_div x (fun x -> x%3=0) (fun x y -> x*y) 1)//3 6 15 30
   Console.WriteLine("Сумма взаимно простых компонентов числа с условием: {0}", Bypass_sim x (fun x -> x%2=1) (fun x y -> x+y) 0)//1 7 11 13 17 19 23 29
   0