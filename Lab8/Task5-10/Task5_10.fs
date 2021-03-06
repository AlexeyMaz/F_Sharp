open System
open System.Text.RegularExpressions
open System.Diagnostics

type DriversLicense(surname: string, firstname: string, patronymic : string, series: int, number: int, issDate: DateTime, expDate: DateTime, categories: string) =
    member this.firstname = firstname
    member this.surname = surname
    member this.patronymic = patronymic
    member this.series = series
    member this.number = number
    member this.issDate = issDate
    member this.expDate = expDate
    member this.categories = categories

    member this.isValid() = DateTime.Now < expDate

    interface IComparable with
       member this.CompareTo(obj: obj): int = 
           match obj with
           | :? DriversLicense as lic2 -> if this.series = lic2.series then this.number.CompareTo lic2.number else this.series.CompareTo lic2.series
           | _ -> invalidArg "obj" "Cannot compare values of different types" 

    override this.GetHashCode() =
       HashCode.Combine(series, number)

    override this.Equals(obj2) =
       match obj2 with
       | :? DriversLicense as lic2 -> (this.series = lic2.series) && (this.number = lic2.number)
       | _ -> false

    override this.ToString() = $"Водительское удостоверение {{Фамилия: {this.surname}, Имя: {this.firstname}, Отчество: {this.patronymic}, Серия: {this.series}, Номер: {this.number}, Выдано: {this.issDate.ToShortDateString()}, Истекает: {this.expDate.ToShortDateString()}, Категории: {this.categories}}}"

//Кидает ошибку если строка не удовлетворяет регулярке
let assertRegex str regex =
    let r = Regex(regex)
    if not (r.IsMatch str) then
        invalidArg str $"Строка не удовлетворяет формату: {regex}"

//Ввод поля в соотв. с регуляркой (пока не будет введено верно)
let rec inputField prompt regex =
    printf $"{prompt}: "
    let input = Console.ReadLine()
    try
        assertRegex input regex
        input
    with
        | :? System.ArgumentException as e ->
            printfn "Ошибка: %s" e.Message
            inputField prompt regex
        | e ->
            printfn "Непредвиденное исключение: %s" e.Message
            reraise()

let inputLicense() =
    printfn "--- Создание водительского удостоверения ---"

    let lastname = inputField "Фамилия" "^[a-zA-Zа-яА-Я]+$"
    let firstname = inputField "Имя" "^[a-zA-Zа-яА-Я]+$"
    let patronymic = inputField "Отчество" "^[a-zA-Zа-яА-Я]+$"
    let series = inputField "Серия" "^\d{4}$" |> Int32.Parse
    let number = inputField "Номер" "^\d{6}$" |> Int32.Parse
    let issDate = inputField "Дата выдачи" "^\s*(3[01]|[12][0-9]|0?[1-9])\.(1[012]|0?[1-9])\.((?:19|20)\d{2})\s*$" |> DateTime.Parse
    let expDate = inputField "Дата окончания" "^\s*(3[01]|[12][0-9]|0?[1-9])\.(1[012]|0?[1-9])\.((?:19|20)\d{2})\s*$" |> DateTime.Parse
    let categories = inputField "Категории" "^\s*((A1?|B[1E]?|C1?E?|D1?E?|M|Tm|Tb)(\,\s)*)+\s*$"

    DriversLicense(lastname, firstname, patronymic, series, number, issDate, expDate, categories)

[<AbstractClass>]
type DocCollection() =
    abstract member searchDoc: DriversLicense -> bool

type ArrayDocCollection(list: DriversLicense list)=
    inherit DocCollection()
    member this.DocArray = Array.ofList list

    override this.searchDoc(lic) = 
        Array.exists (fun x-> x.Equals lic) this.DocArray

type ListDocCollection(list: DriversLicense list)=
    inherit DocCollection()
    member this.DocList = list

    override this.searchDoc(lic) = 
        List.exists (fun x-> x.Equals(lic)) this.DocList

type BinListDocCollection(list: DriversLicense list)=
    inherit DocCollection()

    let rec binSearch (l:'DriversLicense list) (license:'DriversLicense) =
        match List.length l with
        | 0 -> false
        | i ->
            let middle = i/2
            match sign <| compare license l.[middle] with
            | 0 -> true
            | 1 -> binSearch l.[..middle - 1] license
            | _ -> binSearch l.[middle + 1..] license  

    member this.BinList = List.sortBy (fun (x:DriversLicense) -> (x.series, x.number)) list 

    override this.searchDoc(lic) =
        binSearch this.BinList lic

type SetDocCollection(list: DriversLicense list)=
    inherit DocCollection()
    member this.DocSet = Set.ofList list

    override this.searchDoc(lic) = 
        Set.contains lic this.DocSet

let charsForRandom = "абвгдежзиклмнопрстуфхчшщьыъэюя"

let randomStr len (random:Random) = 
    let randomChars = [|for i in 0..len -> charsForRandom.[random.Next(charsForRandom.Length)]|]

    new System.String(randomChars)

let generateRandomLicense (random: Random)=
    let lastname = randomStr 11 random
    let firstname = randomStr 7 random
    let patr = randomStr 12 random
    let series = random.Next (1000, 9999)
    let number = random.Next (100000, 999999)
    let issue = DateTime.Parse "17.09.2020"
    let expire = DateTime.Parse "12.12.2032"
    let categories = randomStr 8 random

    DriversLicense(lastname, firstname, patr, series, number, issue, expire, categories)

let generateRandomLicenseList len random = 
    [for i in 0..len -> generateRandomLicense random]

let measureSearchTime (watch:Stopwatch) searchMethod lic =
    watch.Reset()
    watch.Start()
    let isFound = searchMethod lic
    watch.Stop()

    watch.ElapsedMilliseconds

[<EntryPoint>]
let main argv =
   let lic1 = DriversLicense("Левин", "Валентин", "Александрович", 9966, 471030, DateTime.Parse "17.09.2020", DateTime.Parse "17.09.2030", "B, B1, M")
   let random = Random()
   let licenses =  generateRandomLicenseList 19999 random @ [lic1] @ generateRandomLicenseList 20000 random

   let licArray = ArrayDocCollection(licenses)
   let licList = ListDocCollection(licenses)
   let licBinList = BinListDocCollection(licenses)
   let licSet = SetDocCollection(licenses)

   let watch = new Stopwatch()

   printfn "Array search time: %d ms" (measureSearchTime watch licArray.searchDoc lic1)
   printfn "List search time: %d ms" (measureSearchTime watch licList.searchDoc lic1)
   printfn "BinList search: %d ms" (measureSearchTime watch licBinList.searchDoc lic1)
   printfn "Set search: %d ms" (measureSearchTime watch licSet.searchDoc lic1)

   watch.Reset()
   0