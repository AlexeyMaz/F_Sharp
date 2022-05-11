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

     override this.ToString() = $"Водительское удостоверение {{Фамилия: {this.surname}, Имя: {this.firstname}, Отчество: {this.patronymic}, " + 
    $"Серия: {this.series}, Выдано: {this.issDate.ToShortDateString()}, Истекает: {this.expDate.ToShortDateString()}, Категории: {this.categories}}}"

[<EntryPoint>]
let main argv =
   let lic1 = DriversLicense("Левин", "Валентин", "Александрович", 9966, 471030, DateTime.Parse "17.09.2020", DateTime.Parse "17.09.2030", "B, B1, M")
   Console.WriteLine(lic1)

   0