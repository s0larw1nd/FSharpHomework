open System

[<AbstractClass>]
type Shape() =
    abstract member Area: unit -> float

type GeometricShape =
    | Rectangle of width: float * height: float
    | Square of side: float
    | Circle of radius: float

    member this.Area =
        match this with
        | Rectangle(w, h) -> w * h
        | Square(s) -> s * s
        | Circle(r) -> 3.1415 * r * r

let calculateArea (shape: GeometricShape) =
    match shape with
    | Rectangle(w, h) -> w * h
    | Square(s) -> s * s
    | Circle(r) -> 3.1415 * r * r

type IPrint = interface
    abstract member Print: unit -> unit
    end

type RectangleShape(w, h) =
    inherit Shape()
    let mutable width = w
    let mutable height = h

    member this.fig_width
        with get () = width
        and set (value) = width <- value
    member this.fig_height
        with get () = height
        and set (value) = height <- value

    override this.Area () = width * height

    override this.ToString() = $"Width: {width}, Height: {height}, Square: {this.Area()}"

    interface IPrint with
        member this.Print() = printfn $"{this.ToString()}"

type SquareShape(l) =
    inherit RectangleShape(l,l)

type CircleShape(r) =
    inherit Shape()
    let mutable radius = r

    member this.fig_radius
        with get () = radius
        and set (value) = radius <- value

    override this.Area () = 3.1415 * radius * radius

    override this.ToString() = $"Radius: {radius}, Square: {this.Area()}"

    interface IPrint with
        member this.Print() = printfn $"{this.ToString()}"

[<EntryPoint>]
let main(args : string[]) =
    let rect = RectangleShape(10.0, 20.0)
    let square = SquareShape(5.0)
    let circle = CircleShape(3.0)

    Console.Out.WriteLine("Lab 4:")
    (rect :> IPrint).Print()
    (square :> IPrint).Print()
    (circle :> IPrint).Print()

    let rect2 = Rectangle(10.0, 20.0)
    let square2 = Square(5.0)
    let circle2 = Circle(3.0)

    printfn "Rectangle area: %A" (calculateArea rect2)
    printfn "Square area: %A" (calculateArea square2)
    printfn "Circle area: %A" (calculateArea circle2)

    Console.Out.WriteLine("Lab 5:")
    Lab5.mainLab5()

    Console.Out.WriteLine("Lab 6:")
    Lab6.mainLab6()

    Console.Out.WriteLine("Lab 7:")
    Lab7.mainLab7()
    
    0