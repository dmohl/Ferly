module Ferly

open System
open Otp

Console.Write ":>"  
let mutable input = Console.ReadLine()  

let erlangCookie = "supersecretcookie"
let peerNode = "querly@dmohl-PC"

let connection =
    let cNode = new OtpSelf("querlyclientnode", erlangCookie)
    let sNode = new OtpPeer(peerNode)
    cNode.connect(sNode)

let RemoteCall (sql:string) = 
    let arguments = [|new Erlang.String(sql)|] : Erlang.Object[]
    do connection.sendRPC("querly_client", "sql_query", arguments) 
    connection.receiveRPC().ToString()

while input <> "quit"  
    do match input with
       | _ when input = "quit" -> do connection.close()
       | _ -> 
             Console.Write(RemoteCall input)
             Console.Write "\n\n:>"   
             input <- Console.ReadLine()

