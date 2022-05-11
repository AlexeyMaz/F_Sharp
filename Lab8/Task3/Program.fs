//ЛР6
//Разработайте программу, которая осуществляет разбор текста с
//использованием библиотеки FParsec. Результатом разбора должны быть
//значения алгебраического типа.
open System
open FParsec

//Алгебраический тип для выражения (число, сумма, разность)
type Expr =
    | Number of float
    | Add of Expr * Expr
    | Sub of Expr * Expr

//Распарсить строку, возможно окруженную пробелами
let pstring_trim s = spaces >>. pstring s .>> spaces
//float без пробелов
let float_ws = spaces >>. pfloat .>> spaces

//Создаем парсер и ref переменную
let parser, parserRef = createParserForwardedToRef<Expr, unit>()

let parsePlusExpr = tuple2 (parser .>> pstring_trim "+") parser |>> Add 
let parseSubExpr = tuple2 (parser .>> pstring_trim "-") parser |>> Sub

//Распарсить между скобками произвольные комбинации (1) и (2). 
let parseOp = between (pstring_trim "(") (pstring_trim ")") (attempt parsePlusExpr <|> parseSubExpr)

//Распарсить float и сохранить в Number
let parseNum = float_ws |>> Number

//Пихаем в parserRef парсер произвольной комбинации float'ов и (3)
parserRef := parseNum <|> parseOp

let rec EvalExpr (e:Expr): float =
    match e with
    | Number(num) -> num
    | Add(a,b) ->
        let left = EvalExpr(a)
        let right = EvalExpr(b)
        let result = left + right
        result
    | Sub(a,b) ->
        let left = EvalExpr(a)
        let right = EvalExpr(b)
        let result = left - right
        result

[<EntryPoint>]
let main argv =
    printfn "Введите выражение (обернуть в скобки): "
    let input = Console.ReadLine() 
    let expr = run parser input

    match expr with
    | Success (result, _, _) -> printfn "Результат: %f" (EvalExpr result)
    | Failure (errorMsg, _, _) -> printfn "Еггог: %s" errorMsg

    0