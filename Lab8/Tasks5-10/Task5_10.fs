open System

type DriversLicense(first_name: string, last_name: string, series: int, number: int, issDate: DateTime, expDate: DateTime) =
    member this.first_name = first_name
    member this.last_name = last_name
    member this.series = series
    member this.number = number
    member this.issDate = issDate
    member this.expDate = expDate

    member this.isValid() = DateTime.Now < expDate

[<EntryPoint>]
let main argv =

    0