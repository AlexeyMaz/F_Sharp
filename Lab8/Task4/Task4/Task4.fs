// ЛР4
open System

// Часть 1:
type IPrint = interface
    abstract member Print: unit -> unit
    end

[<AbstractClass>]
type Figure() =
    abstract member GetArea: unit -> double

type Rectangle(width: double, height: double) =
    inherit Figure()
    
    let mutable WidthProperty = width
    let mutable HeightProperty = height
    
    member this.Width
        with get() = WidthProperty
        and set(value) = WidthProperty <- value

    member this.Height
        with get() = HeightProperty
        and set(value) = HeightProperty <- value

    override this.GetArea() = (this.Width * this.Height)
    override this.ToString() = sprintf "Rectangle{Width=%f, Height=%f, Area=%f}" this.Width this.Height (this.GetArea())

    interface IPrint with
        member this.Print() = this.ToString() |> Console.WriteLine

type Square(side: double) =
    inherit Rectangle(side, side)
    override this.ToString() = sprintf "Square{Side=%f, Area=%f}" this.Width (this.GetArea())

    interface IPrint with
        member this.Print() = this.ToString() |> Console.WriteLine

type Circle(radius: double) =
    inherit Figure()
    
    let mutable RadiusProperty = radius
    member this.Radius
        with get() = RadiusProperty
        and set(value) = RadiusProperty <- value

    override this.GetArea() = (Math.PI * this.Radius * this.Radius)
    override this.ToString() = sprintf "Circle{Radius=%f, Area=%f}" this.Radius (this.GetArea())

    interface IPrint with
        member this.Print() = this.ToString() |> Console.WriteLine

// Часть 2:
type GeomFigure = 
    | Rect of double * double
    | Sqr of double
    | Circ of double

let calc_area (fig: GeomFigure) =
    match fig with
    | Rect(w, h) -> w * h
    | Sqr(a) -> a*a
    | Circ(r) -> Math.PI * r * r

[<EntryPoint>]
let main argv =
    let rect = Rectangle(2.0, 4.5)
    printfn "Rect area: %f" (rect.GetArea())

    let circle = Circle(2.0)
    printfn "Circle area: %f" (circle.GetArea())
    
    let fig_sqr = Sqr(1.1)
    printfn "Sqr area: %f" (calc_area fig_sqr)

    0