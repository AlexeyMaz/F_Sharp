open System

let Is_Simple x =
    let rec Is_Simple1 x current =
        match current with
        |0->false
        |1->true
        |current->
            if x % current = 0 then false
            else
                let new_current = current - 1
                Is_Simple1 x new_current
    Is_Simple1 x (x - 1)

//Сумма непростых делителей числа
let Sum_notSimDiv n func cur_sum =
   let rec Bypass1 n func cur_sum cur_divider =
       if cur_divider = 0 then cur_sum else
       let newInit = if n % cur_divider = 0 && not (Is_Simple cur_divider) then func cur_sum cur_divider else cur_sum
       let new_divider = cur_divider-1
       Bypass1 n func newInit new_divider 
   Bypass1 n func cur_sum n

[<EntryPoint>]
let main argv =
   Console.WriteLine("Введите число:")
   let x = Convert.ToInt32(Console.ReadLine())//30
   Console.WriteLine("Сумма непростых делителей числа: {0}", Sum_notSimDiv x (fun x y -> x+y) 0)//1 6 10 15 30
   0