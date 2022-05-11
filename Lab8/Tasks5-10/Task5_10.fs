open System

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

    override this.ToString() = $"Водительское удостоверение {{Фамилия: {this.surname}, Имя: {this.firstname}, Отчество: {this.patronymic}, " + 
    $"Серия: {this.series}, Выдано: {this.issDate.ToShortDateString()}, Истекает: {this.expDate.ToShortDateString()}, Категории: {this.categories}}}"

[<EntryPoint>]
let main argv =
   let lic1 = DriversLicense("Левин", "Валентин", "Александрович", 9966, 471030, DateTime.Parse "12.12.2020", DateTime.Parse "12.12.2030", "B, B1, M")
   Console.WriteLine(lic1)

   0