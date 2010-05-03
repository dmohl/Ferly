module Ferly

open System
open Otp

Console.Write ":>"  
let mutable input = Console.ReadLine()  

let erlangCookie = "supersecretcookie"

let connection =
    let cNode = new OtpSelf("querlyclientnode", erlangCookie)
    let sNode = new OtpPeer("querly@dmohl-PC")
    cNode.connect(sNode)

let RemoteCall (sql:string) = 
    let arguments = [|new Erlang.String(sql)|] : Erlang.Object[]
    do connection.sendRPC("querly_client", "sql_query", arguments) 
    connection.receiveRPC().ToString()
   
while input <> "quit"  
    do  
    if input <> "quit"   
    then
        let result = RemoteCall input  
        Console.Write(result)
        Console.Write "\n:>"   
        input <- Console.ReadLine()

