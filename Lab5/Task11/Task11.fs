open System

let answer_to = function
    | "f#" | "prolog" -> "Пoдлизa!"
    | "python" -> "Худший язык"
    | "c#" -> "Лучший язык"
    | "c++" -> "База"
    | _ -> "Ты такие вещи не говори"

[<EntryPoint>]
let main argv =
    printfn "Какoй твoй любимый язык?"
    let language = Console.ReadLine().ToLower()
    Console.WriteLine(answer_to language)
    0