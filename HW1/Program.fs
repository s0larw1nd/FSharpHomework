open System

let circleVolume radius =
    let pi = 3.14159
    pi * radius * radius
    
let cylinderVolume radius length =
    let cir_vol = circleVolume radius
    length * cir_vol
    
let rec digSumTop digit =
    if digit < 10 then digit
    else digit % 10 + digSumTop (digit / 10)
        
let rec digSumTail digit acc=
    if digit < 10 then acc + digit
    else
        digSumTail (digit / 10) (acc + digit % 10)

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
    
    let dst = digSumTop 123456
  
    let digSumTailWrap n = digSumTail n 0
    let dstl = digSumTailWrap 123456
    
    printfn "Вверх: %A\nХвостовая: %A" dst dstl
    
    0