open System

let Bypass n func init =
   let rec Bypass1 n func init cur_divider =
       if cur_divider = 0 then init else
       let newInit = if n%cur_divider = 0 then func init cur_divider else init
       let new_divider = cur_divider-1
       Bypass1 n func newInit new_divider 
   Bypass1 n func init n

[<EntryPoint>]
let main argv =
   Console.WriteLine("Введите число:")
   let number = Convert.ToInt32(Console.ReadLine())
   Console.WriteLine("Произведение делителей числа: {0}", Bypass number (fun x y -> x*y ) 1)
   0 