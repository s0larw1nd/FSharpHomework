module Lab7
open System

let mainLab7() =
    let consoleAgent = MailboxProcessor.Start(fun inbox ->
        let rec messageLoop() = async {
            let! msg = inbox.Receive()
            printfn "Сообщение: %s" msg
            if msg = "Time" then printfn "Time: %A" DateTime.Now
            return! messageLoop()
        }
        messageLoop()
    )

    consoleAgent.Post "Time"
    System.Threading.Thread.Sleep(100)
    0