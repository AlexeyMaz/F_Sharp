//ЛР7
//С использованием класса MailboxProcessor реализуйте агента,
//который реагирует на внешние события и выполняет различные действия
//(например, выдает результаты в консоль).
open System

let messageResponder = MailboxProcessor.Start(fun inbox->
    let rec messageLoop() = async {
        let! (msg:string) = inbox.Receive()

        let response = match msg.ToLower() with
        | "привет" -> "Привет"
        | "как дела?" -> "Покатит"
        | "что делаешь?" -> "Принимаю сообщения"
        | "расскажи анекдот" -> "Нет"
        | "агента ответ" -> "Ладно"
        | _ -> "Бывает"

        printfn "%s" response
        return! messageLoop()
    }
    // Запуск обработки сообщений
    messageLoop()
)

let rec askUser() =
    let input = Console.ReadLine().Trim()
    if not (String.IsNullOrEmpty input) then
        messageResponder.Post(input)
        askUser()

[<EntryPoint>]
let main argv =
    printfn "Агент на месте"
    printfn "Для окончания ввода введите пустой символ"
    askUser()
    0