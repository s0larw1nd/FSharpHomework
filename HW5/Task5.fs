open System
open System.Text.RegularExpressions

let (|ValidName|InvalidName|) (name: string) =
    let pattern = @"^[А-ЯЁ][а-яё]+$"
    if Regex.IsMatch(name, pattern) then ValidName name
    else InvalidName "Имя должно начинаться с заглавной буквы и содержать минимум 2 буквы, и не содержать других символов."

let (|ValidSurname|InvalidSurname|) (surname: string) =
    let pattern = @"^[А-ЯЁ][а-яё]+$"
    if Regex.IsMatch(surname, pattern) then ValidSurname surname
    else InvalidSurname "Фамилия должна начинаться с заглавной буквы и содержать минимум 2 буквы, и не содержать других символов."

let (|ValidPatronymics|InvalidPatronymics|) (patr: string) =
    let pattern = @"^[А-ЯЁ][а-яё]+$"
    if Regex.IsMatch(patr, pattern) then ValidPatronymics patr
    else InvalidPatronymics "Отчество должно начинаться с заглавной буквы и не содержать других символов."

let (|ValidSeries|InvalidSeries|) (srs: string) =
    if Regex.IsMatch(srs, @"^\d{4}$") then ValidSeries srs
    else InvalidSeries "Серия паспорта должна состоять из 4 цифр."

let (|ValidNumber|InvalidNumber|) (nmb: string) =
    if Regex.IsMatch(nmb, @"^\d{6}$") then ValidNumber nmb
    else InvalidNumber "Номер паспорта должен состоять из 6 цифр."

type CitizenPassport(surnm, nm, patr, sx, bdate, bplace, srs, nmb) =
    let mutable surnm = surnm
    let mutable nm = nm
    let mutable patr = patr
    let mutable bdate = bdate
    let mutable bplace = bplace
    let mutable sx = sx
    let mutable srs = srs
    let mutable nmb = nmb
    
    member this.surname
        with get () = surnm
        and set (value) = surnm <- value
    member this.name
        with get () = nm
        and set (value) = nm <- value
    member this.patronymics
        with get () = patr
        and set (value) = patr <- value
    member this.birthday_date
        with get () = bdate
        and set (value) = bdate <- value
    member this.birthplace
        with get () = bplace
        and set (value) = bplace <- value
    member this.sex
        with get () = sx
        and set (value) = sx <- value
    member this.series
        with get () = srs
        and set (value) = srs <- value
    member this.number
        with get () = nmb
        and set (value) = nmb <- value

    override this.ToString() = $"Фамилия: {surnm} Имя: {nm} Отчество: {patr} Дата рождения: {bdate} Место рождения: {bplace} Пол: {sx}"
    member this.Print() = printfn $"{this.ToString()}"

    interface System.IEquatable<CitizenPassport> with
        member this.Equals(other: CitizenPassport) =
            this.series = other.series && this.number = other.number

    override this.Equals(obj) =
        match obj with
        | :? CitizenPassport as other -> (this :> System.IEquatable<CitizenPassport>).Equals(other)
        | _ -> false

    interface System.IComparable<CitizenPassport> with
        member this.CompareTo(other) =
            compare this.series other.series
            
    interface IComparable with
        member this.CompareTo obj =
            match obj with
              | :? CitizenPassport as other -> (this :> IComparable<_>).CompareTo other
              | _ -> failwithf "Значение не имеет тип CitizenPassport"
    
    override this.GetHashCode() =
        hash (this.series, this.number)
    
    static member op_Equality (a: CitizenPassport, b: CitizenPassport) = a.Equals(b)
    static member op_LessThan (a: CitizenPassport, b: CitizenPassport) = (a :> System.IComparable<CitizenPassport>).CompareTo(b) < 0
    static member op_GreaterThan (a: CitizenPassport, b: CitizenPassport) = (a :> System.IComparable<CitizenPassport>).CompareTo(b) > 0
    
[<EntryPoint>]
let main(args : string[]) =
    let tryCreateCitizenPassport (surnm:string) (nm:string) (patr:string) (sx:char) (bdate:DateTime) (bplace:string) (srs:string) (nmb:string) =
        match surnm, nm, patr, srs, nmb with
        | ValidSurname s, ValidName n, ValidPatronymics p, ValidSeries sr, ValidNumber num ->
            Ok (CitizenPassport(s, n, p, sx, bdate, bplace, sr, num))
        | InvalidSurname msg, _, _, _, _ -> Error msg
        | _, InvalidName msg, _, _, _ -> Error msg
        | _, _, InvalidPatronymics msg, _, _ -> Error msg
        | _, _, _, InvalidSeries msg, _ -> Error msg
        | _, _, _, _, InvalidNumber msg -> Error msg
    
    let result = tryCreateCitizenPassport "Иванов" "Иван" "Иванович" 'M' (DateTime(1990, 5, 15)) "Москва" "1234" "123456"
    match result with
    | Ok passport -> printfn "Создан паспорт: %A" (passport.ToString())
    | Error msg -> printfn "Ошибка: %s" msg
    
    let result_wrong = tryCreateCitizenPassport "Иванов1" "Иван" "Иванович" 'M' (DateTime(1990, 5, 15)) "Москва" "1234" "123456"
    match result_wrong with
    | Ok passport -> printfn "Создан паспорт: %A" (passport.ToString())
    | Error msg -> printfn "Ошибка: %s" msg
    
    let result2 = tryCreateCitizenPassport "Иванов" "Иван" "Иванович" 'M' (DateTime(1990, 5, 15)) "Москва" "5678" "123456"
    match result, result2 with
    | Ok passport, Ok passport2 -> printfn "Сравнение: %A" (passport<passport2)
    | _ -> printfn "Ошибка"
    0