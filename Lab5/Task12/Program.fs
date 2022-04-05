open System

let answer_to = function
    | "f#" | "prolog" -> "Пoдлизa!"
    | "python" -> "Худший язык"
    | "c#" -> "Лучший язык"
    | "c++" -> "База"
    | _ -> "Ты такие вещи не говори"

[<EntryPoint>]
let main argv =
   System.Console.WriteLine("Какoй твoй любимый язык?")
   //Случай суперпозиции
   (Console.ReadLine>>answer_to>>Console.WriteLine)()

   //----------------------------------------------------------------------//

   System.Console.WriteLine("Какoй твoй любимый язык?")
  // Случай каррирования
   let F y x z =z(x(y()))
   F Console.ReadLine answer_to Console.WriteLine
   0