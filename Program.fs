open System

let circleVolume radius =
    let pi = 3.14159
    pi * radius * radius
    
let cylinderVolume radius length =
    let cir_vol = circleVolume radius
    length * cir_vol

[<EntryPoint>]
let main(args : string[]) =
    printfn "Введите радиус: "
    let radius_line = Console.ReadLine()
    let radius = float radius_line
    
    printfn "Введите высоту: "
    let length_line = Console.ReadLine()
    let length = float length_line
    
    let answ = cylinderVolume radius length
    printfn "%A" answ
    
    0